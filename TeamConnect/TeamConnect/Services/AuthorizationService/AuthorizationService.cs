using System;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;
using TeamConnect.Services.MockDataService;

namespace TeamConnect.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMockDataService _mockDataService;

        public AuthorizationService(
            IMockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        #region -- IUserService implementation --

        public async Task<OperationResult> CheckIsEmailExistAsync(string email)
        {
            var result = new OperationResult();

            try
            {
                var usersResult = await _mockDataService.GetUsersAsync(u => u.Email == email);

                if (usersResult.IsSuccess)
                {
                    var user = usersResult.Result.FirstOrDefault();

                    if (user != null)
                    {
                        result.SetSuccess();
                    }
                    else
                    {
                        result.SetFailure();
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CheckIsEmailExistAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult<UserModel>> LogInAsync(string email, string password)
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var usersResult = await _mockDataService.GetUsersAsync(u => u.Email == email && u.Password == password);

                if (usersResult.IsSuccess)
                {
                    var user = usersResult.Result.FirstOrDefault();

                    if (user != null)
                    {
                        result.SetSuccess(user);
                    }
                    else
                    {
                        result.SetFailure();
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(LogInAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> CompleteRegistration(UserModel user)
        {
            var result = new OperationResult();

            try
            {
                var addUserResult = await _mockDataService.UpdateUserAsync(user);

                if (addUserResult.IsSuccess)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CompleteRegistration)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> SignUpAsync(UserModel user)
        {
            var result = new OperationResult();

            try
            {
                var addUserResult = await _mockDataService.AddUserAsync(user);

                if (addUserResult.IsSuccess)
                {
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(SignUpAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
