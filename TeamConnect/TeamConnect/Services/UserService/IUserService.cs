using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;

namespace TeamConnect.Services.UserService
{
    public interface IUserService
    {
        Task<OperationResult<UserModel>> GetUserByIdAsync(int userId);

        Task<OperationResult<IEnumerable<UserModel>>> GetAllUsersAsync();

        Task<OperationResult<IEnumerable<UserModel>>> GetMissingUsersAsync(DateTime date);
    }
}
