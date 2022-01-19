using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models
{
    public class EventBookingModel
    {
        [BindProperty] public string TicketType { get; set; }
        [BindProperty] public string FullName { get; set; }
        [BindProperty] public string PhoneNumber { get; set; }
        [BindProperty] public string EmailAddress { get; set; }
        [BindProperty] public string CreditCardNumber { get; set; }
        [BindProperty] public string ExpiryYear { get; set; }
        [BindProperty] public string ExpiryMonth { get; set; }
        [BindProperty] public string CVCNumber { get; set; }
        [BindProperty] public bool Terms { get; set; }

    }
}
