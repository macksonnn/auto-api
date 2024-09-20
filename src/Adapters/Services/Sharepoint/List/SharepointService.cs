//using Microsoft.Graph;
//using System.Net.Mail;

//namespace AutoMais.Services.Sharepoint.List
//{
//    public class SharepointService : ISharepointService
//    {
//        private readonly string adminListName;
//        private readonly string rolesAndPermissionsListName;
//        private readonly GraphServiceClient _graphServiceClient;
//        private readonly Microsoft.Graph.Models.Site? mainSite;
//        private readonly IEnumerable<Microsoft.Graph.Models.List> siteLists;

//        public SharepointService(Setup.SharepointSettings settings, GraphServiceClient graphServiceClient, [FromKeyedServices("MainSite")] Microsoft.Graph.Models.Site site)
//        {
//            adminListName = settings.AppAdminList;
//            rolesAndPermissionsListName = settings.RolesAndPermissionList;

//            mainSite = site;
//            _graphServiceClient = graphServiceClient;
//        }

//        public async Task<IEnumerable<MailAddress>> GetAdminEmails(IEnumerable<string> applications = null)
//        {
//            IEnumerable<Microsoft.Graph.Models.ListItem>? adminListItems = await GetSharepointList(adminListName, "fields($select=Application,AdminEmail)");

//            List<MailAddress> emails = new List<MailAddress>();

//            foreach (var item in adminListItems)
//            {
//                item.Fields.AdditionalData.TryGetValue("Application", out object app);
//                if (applications == null || applications.Any(a => a.ToLower() == app.ToString().ToLower()))
//                {
//                    item.Fields.AdditionalData.TryGetValue("AdminEmail", out object email);
//                    emails.Add(new MailAddress(email.ToString()));
//                }
//            }

//            return emails;
//        }

//        public async Task<string> GetRole(string application, string department, string division, string position)
//        {
//            IEnumerable<Microsoft.Graph.Models.ListItem>? rolePermissionList = await GetSharepointList(rolesAndPermissionsListName, "fields($select=Application,Department, Division,Position,Role)");

//            List<MailAddress> emails = new List<MailAddress>();

//            foreach (var item in rolePermissionList)
//            {
//                if (item.Fields != null)
//                {
//                    item.Fields.AdditionalData.TryGetValue("Application", out object app);
//                    item.Fields.AdditionalData.TryGetValue("Department", out object dep);
//                    item.Fields.AdditionalData.TryGetValue("Division", out object div);
//                    item.Fields.AdditionalData.TryGetValue("Position", out object pos);

//                    if (application.ToLower() == app.ToString().ToLower() && department.ToLower() == dep.ToString().ToLower() && division.ToLower() == div.ToString().ToLower() && position.ToLower() == pos.ToString().ToLower())
//                    {
//                        item.Fields.AdditionalData.TryGetValue("Role", out object rol);
//                        return rol.ToString();
//                    }
//                }
//            }

//            return string.Empty;
//        }

//        private async Task<IEnumerable<Microsoft.Graph.Models.ListItem>?> GetSharepointList(string listName, string expandExpression, string filterExpression = "")
//        {
//            var list = mainSite.Lists?.Where(l => l.DisplayName == listName).FirstOrDefault();

//            var listItems = (await _graphServiceClient.Sites[mainSite.Id].Lists[list.Id].Items.GetAsync((requestConfiguration) =>
//            {
//                requestConfiguration.QueryParameters.Expand = new string[] { expandExpression };
//                requestConfiguration.QueryParameters.Filter = filterExpression;
//            }))?.Value;

//            return listItems;
//        }
//    }
//}
