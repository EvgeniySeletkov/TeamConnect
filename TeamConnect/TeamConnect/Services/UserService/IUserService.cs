using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;

namespace TeamConnect.Services.UserService
{
    public interface IUserService
    {
        Task<OperationResult<IEnumerable<UserModel>>> GetTeamMembersAsync();

        Task<OperationResult<IEnumerable<UserModel>>> GetNotMissingUsersAsync(DateTime date);

        Task<OperationResult<UserModel>> GetUserByIdAsync(int userId);

        Task<OperationResult<UserModel>> GetCurrentUserAsync();

        Task<OperationResult<IEnumerable<UserModel>>> SearchUsers(string searchRequest);
    }
}
