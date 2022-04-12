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

        public UserService(
            IMockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        #region -- IUserService implementation --

        public async Task<OperationResult<UserModel>> GetUserByIdAsync(int userId)
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var usersResult = await _mockDataService.GetUsersAsync(u => u.Id == userId);

                if (usersResult.IsSuccess)
                {
                    result.SetSuccess(usersResult.Result.FirstOrDefault());
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
                var usersResult = await _mockDataService.GetUsersAsync();

                if (usersResult.IsSuccess)
                {
                    result.SetSuccess(usersResult.Result);
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

        public async Task<OperationResult<IEnumerable<UserModel>>> GetMissingUsersAsync(DateTime date)
        {
            var result = new OperationResult<IEnumerable<UserModel>>();

            try
            {
                var users = await _mockDataService.GetUsersAsync();

                if (users.IsSuccess)
                {
                    var missingUsers = new List<UserModel>();

                    foreach (var item in users.Result)
                    {
                        var userLeaveResult = await _mockDataService.GetRequestsAsync(
                            r => r.StartDate <= date
                            && r.EndDate >= date
                            && r.UserId == item.Id);

                        if (userLeaveResult.IsSuccess)
                        {
                            var userLeave = userLeaveResult.Result.FirstOrDefault();

                            if (userLeave is null)
                            {
                                missingUsers.Add(item);
                            }
                        }
                    }

                    result.SetSuccess(missingUsers);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetMissingUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
