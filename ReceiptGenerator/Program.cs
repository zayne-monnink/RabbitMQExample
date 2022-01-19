// See https://aka.ms/new-console-template for more information

/// Summary:
/// This console app connects to a local RabbitMQ queue and listens for messages.
/// After receiving a message it generates a receipt and emails it to the recipient.
/// The reason why emails need to be sent in a process that is separate from the main website
/// is because it is a slow, network-bound process that could negatively affect the user
/// experience. Having items in a queue will also allow multiple attempts if any emails fail.

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReceiptGenerator;
using System.Text.Json;

Console.WriteLine("RabbitMQ Receipt Generator");

var factory = new ConnectionFactory() { HostName = "rabbitmq" };
factory.RequestedConnectionTimeout = new TimeSpan(0, 1, 0);
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "SalesQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    var json = JsonDocument.Parse(message);

    // Decide price,etc based on data submitted in the message
    var ticketType = json.RootElement.GetProperty("TicketType").GetString() ?? "";
    decimal price = 0;
    if (ticketType == "G") price = 10;
    else if (ticketType == "R") price = 20;
    else if (ticketType == "V") price = 50;
    else throw new Exception("Invalid ticket type");

    // Pretend that the credit card payment was processed here for demo purposes

    // Generate and send the receipt
    var receipt = new Receipt();
    receipt.ReceiptNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
    receipt.RecipientEmailAddress = json.RootElement.GetProperty("EmailAddress").GetString() ?? "";
    receipt.RecipientName = json.RootElement.GetProperty("FullName").GetString() ?? "";
    receipt.OrderTotal = price;

    try
    {
        receipt.Send();
        Console.WriteLine($"Email sent for order {receipt.ReceiptNumber}");
    }
    catch (Exception)
    {
        Console.WriteLine($"Sending email failed for order {receipt.ReceiptNumber}");
    }
};

channel.BasicConsume(queue: "SalesQueue",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("Press ENTER key to exit");
Console.ReadLine();
