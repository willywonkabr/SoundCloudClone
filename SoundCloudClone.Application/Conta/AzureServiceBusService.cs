using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoundCloudClone.Application.Conta
{
    public class AzureServiceBusService
    {

        public AzureServiceBusService() { }

        public async Task SendMessage(Notificacao notificacao)
        {
            //ServiceBusClient client;
            //ServiceBusSender sender;

            //client = new ServiceBusClient(ConnectionString);

            //sender = client.CreateSender("notificacao_queue");

            //var body = JsonSerializer.Serialize(notificacao);

            //var message = new ServiceBusMessage(body);

            //await sender.SendMessageAsync(message);
        }
    }
}
