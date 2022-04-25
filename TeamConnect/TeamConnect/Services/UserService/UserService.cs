using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nager.Date;
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

        public async Task<OperationResult<IEnumerable<UserModel>>> GetNotMissingUsersAsync(DateTime date)
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
                        var userLeaveResult = await _mockDataService.GetLeavesAsync(
                            r => r.StartDate <= date
                            && r.EndDate >= date
                            && r.UserId == item.Id);

                        if (userLeaveResult.IsSuccess)
                        {
                            var userLeave = userLeaveResult.Result.FirstOrDefault();

                            var isHoliday = DateSystem.IsPublicHoliday(date, item.CountryCode);
                            var isWeekend = DateSystem.IsWeekend(date, item.CountryCode);

                            if (userLeave is null && !isHoliday && !isWeekend)
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
                result.SetError($"{nameof(GetNotMissingUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

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

        #endregion
    }
}
