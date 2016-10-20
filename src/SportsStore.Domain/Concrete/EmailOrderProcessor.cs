using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings EmailSettings { get; set; }

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            EmailSettings = emailSettings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient
            {
                EnableSsl = EmailSettings.EnableSsl,
                Host = EmailSettings.Host,
                Port = EmailSettings.Port,
                UseDefaultCredentials = false,
            })
            {
                smtpClient.Credentials = new NetworkCredential(EmailSettings.Username, EmailSettings.Pw);
                if (EmailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = EmailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                var body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", line.Quantity, line.Product.Name, subtotal);
                }
                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");

                var mailMessage = new MailMessage(EmailSettings.MailFromAddress, EmailSettings.MailToAddress, "New order submitted", body.ToString());

                if (EmailSettings.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.ASCII;

                smtpClient.Send(mailMessage);
            }
        }
    }
}
