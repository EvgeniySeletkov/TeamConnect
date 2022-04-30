﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nager.Date;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;
using TeamConnect.Models.User;
using TeamConnect.Services.Repository;
using TeamConnect.Services.SettingsManager;

namespace TeamConnect.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public UserService(
            IRepository repository,
            ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region -- IUserService implementation --

        public async Task<OperationResult<IEnumerable<UserModel>>> GetAllUsersAsync()
        {
            var result = new OperationResult<IEnumerable<UserModel>>();

            try
            {
                var user = await _repository.GetAllAsync<UserModel>();

                if (user is not null && user.Count > 0)
                {
                    result.SetSuccess(user);
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
                var users = await _repository.GetAllAsync<UserModel>();

                if (users is not null)
                {
                    var notMissingUsers = new List<UserModel>();

                    foreach (var item in users)
                    {
                        var leave = await _repository.FindAsync<LeaveModel>(
                            r => r.StartDate <= date
                            && r.EndDate >= date
                            && r.UserId == item.Id);

                        if (leave is null)
                        {
                            var isHoliday = DateSystem.IsPublicHoliday(date, item.CountryCode);
                            var isWeekend = DateSystem.IsWeekend(date, item.CountryCode);

                            if (!isHoliday && !isWeekend)
                            {
                                notMissingUsers.Add(item);
                            }
                        }
                    }

                    if (notMissingUsers.Count > 0)
                    {
                        result.SetSuccess(notMissingUsers);
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
                result.SetError($"{nameof(GetNotMissingUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult<UserModel>> GetUserByIdAsync(int userId)
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var user = await _repository.FindAsync<UserModel>(u => u.Id == userId);

                if (user is not null)
                {
                    result.SetSuccess(user);
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

        public async Task<OperationResult<UserModel>> GetCurrentUserAsync()
        {
            var result = new OperationResult<UserModel>();

            try
            {
                var getUserByIdResult = await GetUserByIdAsync(_settingsManager.UserId);

                if (getUserByIdResult.IsSuccess)
                {
                    result.SetSuccess(getUserByIdResult.Result);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetCurrentUserAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
