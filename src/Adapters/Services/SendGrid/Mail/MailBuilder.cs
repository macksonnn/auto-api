//using AutoMais.Core.Domain.Aggregates.Account;

//namespace AutoMais.Services.SendGrid.Mail
//{
//    internal static class MailBuilder
//    {
//        /// <summary>
//        /// Get the Html Body from the Account cReation
//        /// </summary>
//        /// <param name="operationText"></param>
//        /// <param name="account"></param>
//        /// <returns></returns>
//        public static string GetAccountCreatedHtml(string operationText, AccountAgg? account = null)
//        {
//            return
//                 $"<html>" +
//                    $"<head></head>" +
//                    $"<body>" +
//                        $"<p>Dear Admin, </p>" +
//                        $"<p>{operationText}</p>" +
//                        $"{ AppendRolePermissionsEmailBody(account?.Roles)} " +
//                        $"<p>Regards,</p>" +
//                        $"<p>Supervisor</p>" +
//                    $"</body>" +
//                $"</html>";
//        }

//        private static string AppendRolePermissionsEmailBody(IEnumerable<UserApplicationRole>? applications)
//        {
//            if (applications != null)
//            {
//                string appendEmailBody = string.Empty;
//                foreach (var app in applications)
//                    appendEmailBody += $"<p>Please assign the role: \"{app.RoleName}\" in the application {app.Application.Name}.</p>";

//                return appendEmailBody;
//            }
//            return string.Empty;
//        }
//    }
//}
