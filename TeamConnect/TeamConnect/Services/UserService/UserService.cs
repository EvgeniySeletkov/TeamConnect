using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;
using TeamConnect.Services.MockDataService;

namespace TeamConnect.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMockDataService _mockDataService;

        public UserService(IMockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        #region -- IUserService implementation --

        public async Task<OperationResult<UserModel>> GetUserByIdAsync(int userId)
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var requests = await _mockDataService.GetUsersAsync(u => u.Id == userId);

                if (requests.IsSuccess)
                {
                    result.SetSuccess(requests.Result.FirstOrDefault());
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUserByIdAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult<IEnumerable<UserModel>>> GetAllUsersAsync()
        {
            var result = new OperationResult<IEnumerable<UserModel>>();

            try
            {
                var requests = await _mockDataService.GetUsersAsync();

                if (requests.IsSuccess)
                {
                    result.SetSuccess(requests.Result);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
