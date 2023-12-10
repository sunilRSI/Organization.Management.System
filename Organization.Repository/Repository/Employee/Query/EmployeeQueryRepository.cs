using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel; 

namespace Organization.Repository.Repository.Employee.Query
{
    public class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public EmployeeQueryRepository(IDynamoDBContext context)
        {
            _dynamoDBContext = context;
        }

        public async Task<IEnumerable<Entity.Models.Employee>> FindEmployeeAsync(Entity.Models.Employee employee, CancellationToken cancellationToken)
        {
            var scanConditions = new List<ScanCondition>();
            if (!string.IsNullOrEmpty(employee.Name.ToString()))
                scanConditions.Add(new ScanCondition("Name", ScanOperator.Equal, employee.Name));
            if (!string.IsNullOrEmpty(employee.Designation))
                scanConditions.Add(new ScanCondition("Designation", ScanOperator.Equal, employee.Designation));
            return await _dynamoDBContext.ScanAsync<Organization.Entity.Models.Employee>(scanConditions, null).GetRemainingAsync(cancellationToken);
        }

        public async Task<IEnumerable<Entity.Models.Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken)
        {
            //var ss = await All();
            return await _dynamoDBContext.ScanAsync<Entity.Models.Employee>(default).GetRemainingAsync(cancellationToken);
        }

        public async Task<Entity.Models.Employee> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<Entity.Models.Employee>(Id, cancellationToken);

            // This will work if table contains range key e.g id is hashkey and name is the range key
            //var result = await _dynamoDBContext.QueryAsync<EmployeeReadModel>(id).GetRemainingAsync();
            //return result[0];

        }
        private async Task<IEnumerable<Entity.Models.Employee>> All(string paginationToken = "")
        {
            // Get the Table ref from the Model
            var table = _dynamoDBContext.GetTargetTable<Entity.Models.Employee>();

            // If there's a PaginationToken
            // Use it in the Scan options
            // to fetch the next set
            var scanOps = new ScanOperationConfig();

            if (!string.IsNullOrEmpty(paginationToken))
            {
                scanOps.PaginationToken = paginationToken;
            }

            // returns the set of Document objects
            // for the supplied ScanOptions
            var results = table.Scan(scanOps);
            List<Document> data = await results.GetNextSetAsync();

            // transform the generic Document objects
            // into our Entity Model
            IEnumerable<Entity.Models.Employee> employees = _dynamoDBContext.FromDocuments<Entity.Models.Employee>(data);
            return employees;

            /* The Non-Pagination approach */
            //var scanConditions = new List<ScanCondition>() { new ScanCondition("Id", ScanOperator.IsNotNull) };
            //var searchResults = _context.ScanAsync<Employee>(scanConditions, null);
            //return await searchResults.GetNextSetAsync();

        }
    }
}
