//using AutoMais.Core.Application.Adapters.Services;
//using AutoMais.Core.Domain.Aggregates.Account;
//using AutoMais.Services.SendGrid.Mail;

//namespace AutoMais.Services.SendGrid.Service
//{
//    public class SendGridMailService : IMailer
//    {
//        private readonly SendGridClient _client;
//        private readonly MailAddress _from;

//        public SendGridMailService(
//            SendGridClient sendGridClient,
//            [FromKeyedServices("FromAddress")] MailAddress fromEmailAddress)
//        {
//            _from = fromEmailAddress;
//            _client = sendGridClient;
//        }

//        public Task SendEmail(EmailToSend email, AccountAgg account = null)
//        {
//            var from = new EmailAddress(_from.Address, _from.DisplayName);
//            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
//                from, 
//                email.To.Select(x => new EmailAddress(x.Address, x.DisplayName)).ToList(), 
//                email.Subject, 
//                email.TextBody, 
//                MailBuilder.GetAccountCreatedHtml(email.TextBody, account));

//            msg.AddCc(from);

//            return _client.SendEmailAsync(msg);
//        }
//    }
//}
