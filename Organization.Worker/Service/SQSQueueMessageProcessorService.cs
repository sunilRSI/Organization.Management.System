using Amazon.SQS.Model;
using Newtonsoft.Json;
using Organization.Entity.Models; 

namespace Organization.Worker.Service
{
    public class SQSQueueMessageProcessorService
    {
        public async Task<bool> ProcessMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            bool MesaageProcessed = false;
            try
            {
                var queueMessage = JsonConvert.DeserializeObject<Employee>(message.Body);
                MesaageProcessed = true;
                //applay Processing logic

            }
            catch (Exception ex)
            {

            }
            return MesaageProcessed;
        }
    }
}
