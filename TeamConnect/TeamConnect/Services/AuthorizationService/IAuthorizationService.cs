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

        Task<OperationResult> SignUpAsync(UserModel user);
    }
}
