using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;

namespace TeamConnect.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<OperationResult> CheckIsEmailExistAsync(string email);

        Task<OperationResult<UserModel>> LogInAsync(string email, string password);

        Task<OperationResult<UserModel>> GetUserByIdAsync(int userId);

        Task<OperationResult> SignUpAsync(UserModel user);

        Task<OperationResult<IEnumerable<UserModel>>> GetAllUsersAsync();

        Task<OperationResult<IEnumerable<UserModel>>> GetMissingUsersAsync(DateTime date);
    }
}
