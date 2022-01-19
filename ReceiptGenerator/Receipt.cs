using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator
{
    internal class Receipt
    {
        private const string SMTP_HOST = "localhost"; // Change this to your own SMTP server
        private const string FROM_ADDRESS = "rabbitmq@test.com"; // Put your own email address in here

        public string ReceiptNumber { get; set; }
        public string RecipientEmailAddress { get; set; }
        public string RecipientName { get; set; }
        public decimal OrderTotal { get; set; }

        public void Send()
        {
            using var client = new SmtpClient(SMTP_HOST);
            using var message = new MailMessage();
            message.To.Add(this.RecipientEmailAddress);
            message.From = new MailAddress(FROM_ADDRESS);
            message.Subject = $"Receipt {this.ReceiptNumber}";
            message.Body =
                $"Dear {this.RecipientName}\n\n" + $"Thank your for booking a ticket for our event. This email confirms that you have paid ${OrderTotal:n2}. Your receipt number is:\n\n" +
                $"{this.ReceiptNumber}\n\n" +
                $"Please bring this receipt with you to the event as proof of payment.\n\n" +
                $"Regards,\n" +
                $"The online web store team";
            //client.Send(message); //NOTE: Uncomment this if you want to actually send the email. Make sure you have set the host and from address first.
        }
    }

}
