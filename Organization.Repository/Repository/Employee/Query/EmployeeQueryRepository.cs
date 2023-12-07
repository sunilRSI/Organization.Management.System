using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime.Internal;
using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.Employee.Query
{
    public class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public EmployeeQueryRepository(IDynamoDBContext context)
        {
            _dynamoDBContext = context;
        }

        public async Task<IEnumerable<EmployeeReadModel>> FindEmployeeAsync(EmployeeCreateModel employeeFindModel, CancellationToken cancellationToken)
        {
            var scanConditions = new List<ScanCondition>();
            if (!string.IsNullOrEmpty(employeeFindModel.Name.ToString()))
                scanConditions.Add(new ScanCondition("Name", ScanOperator.Equal, employeeFindModel.Name));
            if (!string.IsNullOrEmpty(employeeFindModel.Designation))
                scanConditions.Add(new ScanCondition("Designation", ScanOperator.Equal, employeeFindModel.Designation));
            return await _dynamoDBContext.ScanAsync<EmployeeReadModel>(scanConditions, null).GetRemainingAsync(cancellationToken);
        }

        public async Task<IEnumerable<EmployeeReadModel>> GetAllEmployeeAsync(CancellationToken cancellationToken)
        {
            //var ss = await All();
            return await _dynamoDBContext.ScanAsync<EmployeeReadModel>(default).GetRemainingAsync(cancellationToken);
        }

        public async Task<EmployeeReadModel> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<EmployeeReadModel>(Id, cancellationToken);

            // This will work if table contains range key e.g id is hashkey and name is the range key
            //var result = await _dynamoDBContext.QueryAsync<EmployeeReadModel>(id).GetRemainingAsync();
            //return result[0];

        }
        private async Task<IEnumerable<EmployeeReadModel>> All(string paginationToken = "")
        {
            // Get the Table ref from the Model
            var table = _dynamoDBContext.GetTargetTable<EmployeeReadModel>();

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
            IEnumerable<EmployeeReadModel> employees = _dynamoDBContext.FromDocuments<EmployeeReadModel>(data);
            return employees;

            /* The Non-Pagination approach */
            //var scanConditions = new List<ScanCondition>() { new ScanCondition("Id", ScanOperator.IsNotNull) };
            //var searchResults = _context.ScanAsync<Employee>(scanConditions, null);
            //return await searchResults.GetNextSetAsync();

        }
    }
}
