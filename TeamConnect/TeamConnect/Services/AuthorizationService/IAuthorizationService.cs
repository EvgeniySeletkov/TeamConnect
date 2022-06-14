using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;

namespace TeamConnect.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        public bool IsAuthorized { get; }

        Task<OperationResult> CheckIsEmailExistAsync(string email);

        Task<OperationResult<UserModel>> LogInAsync(string email, string password);

        Task<OperationResult> CompleteRegistration(UserModel user);

        Task<OperationResult> SignUpAsync(UserModel user);

        void LogOut();
    }
}
