using System;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.User;
using TeamConnect.Services.MockDataService;
using TeamConnect.Services.Repository;
using TeamConnect.Services.SettingsManager;

namespace TeamConnect.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        private readonly IMockDataService _mockDataService;
        private readonly ISettingsManager _settingsManager;

        public AuthorizationService(
            IRepository repository,
            IMockDataService mockDataService,
            ISettingsManager settingsManager)
        {
            _repository = repository;
            _mockDataService = mockDataService;
            _settingsManager = settingsManager;
        }

        #region -- IUserService implementation --

        public bool IsAuthorized => _settingsManager.UserId > 0;

        public async Task<OperationResult> CheckIsEmailExistAsync(string email)
        {
            var result = new OperationResult();

            try
            {
                var user = await _repository.FindAsync<UserModel>(u => u.Email == email);

                if (user != null)
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
                result.SetError($"{nameof(CheckIsEmailExistAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult<UserModel>> LogInAsync(string email, string password)
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var user = await _repository.FindAsync<UserModel>(u => u.Email == email.ToLower() && u.Password == password);

                if (user is not null)
                {
                    if (user.IsAccountCreated)
                    {
                        _settingsManager.UserId = user.Id;
                    }

                    result.SetSuccess(user);
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
                if (user.Id != Constants.DEFAULT_ID)
                {
                    await _repository.UpdateAsync(user);

                    _settingsManager.UserId = user.Id;

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
                if (user is not null)
                {
                    await _repository.InsertAsync(user);

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

        public void LogOut()
        {
            _settingsManager.ClearSettings();
        }

        #endregion
    }
}
