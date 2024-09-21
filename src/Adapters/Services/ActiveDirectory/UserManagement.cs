
//using Becape.Core.Domain.Aggregates.Account;
//using Becape.Core.Domain.Aggregates.Account.Commands;
//using Microsoft.Graph;
//using Microsoft.Graph.Models;

//namespace Becape.Services.ActiveDirectory
//{
//    public class UserManagement : IUserManagement
//    {
//        private readonly GraphServiceClient _graphClient;
//        private readonly ILogger<UserManagement> _logger;

//        public UserManagement(ILogger<UserManagement> logger, GraphServiceClient graphClient)
//        {
//            _graphClient = graphClient;
//            _logger = logger;
//        }

//        public async Task<Result<AccountAgg>> CreateUser(AccountAgg account, CancellationToken cancellationToken)
//        {
//            try
//            {
//                User user = new()
//                {
//                    AccountEnabled = true,
//                    DisplayName = account.FullName,
//                    MailNickname = account.Firstname,
//                    UserPrincipalName = account.Email,
//                    Mail = account.Email,
//                    GivenName = account.Firstname,
//                    Surname = account.LastName,
//                    Department = account.Department,
//                    JobTitle = account.JobTitle,
//                    PasswordProfile = new PasswordProfile
//                    {
//                        ForceChangePasswordNextSignIn = true,
//                        Password = Guid.NewGuid().ToString(),
//                    }
//                };

//                var resultUser = await _graphClient.Users.PostAsync(user);

//                _logger.LogInformation("User created with email: {UserEmail} and Id {UserId}", user.Mail, user.Id);

//                account.ChangeId(resultUser.Id);

//                return Result.Ok<AccountAgg>(account);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating account in the Active Directory");
//                return Result.Fail(ex.Message);
//            }
//        }

//        public async Task<Result<BlockAccountCommand>> BlockUser(BlockAccountCommand account, CancellationToken cancellationToken)
//        {
//            try
//            {
//                User user = new()
//                {
//                    Id = account.Id,
//                    Mail = account.Email,
//                    AccountEnabled = false
//                };

//                var resultUser = await _graphClient.Users[account.Id].PatchAsync(user);

//                _logger.LogInformation("User {UserId} blocked", account.Email);

//                return Result.Ok<BlockAccountCommand>(account);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error blocking user {UserEmail} in the Active Directory", account.Email);
//                return Result.Fail(ex.Message);
//            }
//        }

//        public async Task<Result<AccountAgg>> UpdateUser(AccountAgg account, CancellationToken cancellationToken)
//        {
//            try
//            {
//                //TODO: Define which fields will be updated
//                User user = new()
//                {
//                    Id = account.Id,
//                    AccountEnabled = true,
//                    DisplayName = account.FullName,
//                    MailNickname = account.Firstname,
//                    UserPrincipalName = account.Email,
//                    GivenName = account.Firstname,
//                    Surname = account.LastName,
//                    Department = account.Department,
//                    JobTitle = account.JobTitle,
//                    PasswordProfile = new PasswordProfile
//                    {
//                        ForceChangePasswordNextSignIn = true,
//                        Password = Guid.NewGuid().ToString(),
//                    }
//                };

//                var resultUser = await _graphClient.Users.PostAsync(user);

//                _logger.LogInformation("User {UserId} updated", user.Id);

//                return Result.Ok<AccountAgg>(account);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating account in the Active Directory");
//                return Result.Fail(ex.Message);
//            }
//        }
//    }
//}
