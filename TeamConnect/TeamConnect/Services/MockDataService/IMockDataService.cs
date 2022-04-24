using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Request;
using TeamConnect.Models.User;

namespace TeamConnect.Services.MockDataService
{
    public interface IMockDataService
    {
        Task<OperationResult<IEnumerable<UserModel>>> GetUsersAsync(Func<UserModel, bool> func = null);

        Task<OperationResult> AddUserAsync(UserModel user);

        Task<OperationResult> UpdateUserAsync(UserModel user);

        Task<OperationResult<IEnumerable<RequestModel>>> GetRequestsAsync(Func<RequestModel, bool> func = null);
    }
}
