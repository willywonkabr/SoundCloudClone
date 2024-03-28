using Azure.Messaging.ServiceBus;

//string ConnectionString = "Endpoint";

//ServiceBusClient client;
//ServiceBusProcessor processor;

//client = new ServiceBusClient(ConnectionString);

//processor = client.CreateProcessor("notificacao_queue");

//processor.ProcessMessageAsync += MessageHandler;
//processor.ProcessErrorAsync += ErrorHandler;

//await processor.StartProcessingAsync();

//Console.WriteLine("Processando mensagem, pressione qualqer tecla para fecha.");
//Console.ReadKey();

//await processor.StopProcessingAsync();

/*async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Recebido: {body}");

    await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}
*/