using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;
using System.Text;
using WebUI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public bool Terms { get; set; } // Need this to get Terms checkbox to post to another model

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost(EventBookingModel model)
        {
            System.Diagnostics.Debug.WriteLine("TEST");

            // Save the data in a queue
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            string json = JsonSerializer.Serialize(model);
            var body = Encoding.UTF8.GetBytes(json);
            

            channel.BasicPublish(exchange: "",
                                 routingKey: "SalesQueue",
                                 basicProperties: null,
                                 body: body);


            // Redirect the user to the thank you page
            Response.Redirect("Thanks");
        }

    }
}