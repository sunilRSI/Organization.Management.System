using Amazon.SQS.Model;
using Newtonsoft.Json;
using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Worker.Service
{
    public class SQSQueueMessageProcessorService
    {
        public async Task<bool> ProcessMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            bool MesaageProcessed = false;
            try
            {
                var queueMessage = JsonConvert.DeserializeObject<EmployeeReadModel>(message.Body);
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
