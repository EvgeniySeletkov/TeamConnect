using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Request;
using TeamConnect.Models.User;

namespace TeamConnect.Services.MockDataService
{
    public class MockDataService : IMockDataService
    {
        private readonly TaskCompletionSource<bool> _initCompletionSource = new TaskCompletionSource<bool>();

        private IList<UserModel> _users;
        private IList<RequestModel> _requests;

        public MockDataService()
        {
            Task.Run(InitializeMockDataAsync);
        }

        #region -- Users methods --

        public async Task<OperationResult<IEnumerable<UserModel>>> GetUsersAsync(Func<UserModel, bool> func = null)
        {
            var result = new OperationResult<IEnumerable<UserModel>>();

            try
            {
                await _initCompletionSource.Task;

                if (_users != null)
                {
                    var users = func is null
                        ? _users
                        : _users.Where(func);

                    result.SetSuccess(users);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> AddUserAsync(UserModel user)
        {
            var result = new OperationResult();

            try
            {
                await _initCompletionSource.Task;

                int ordersCount = _users.Count;

                _users.Add(user);

                if (_users.Count == ordersCount + 1)
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
                result.SetError($"{nameof(AddUserAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Requests methods --

        public async Task<OperationResult<IEnumerable<RequestModel>>> GetRequestsAsync(Func<RequestModel, bool> func = null)
        {
            var result = new OperationResult<IEnumerable<RequestModel>>();

            try
            {
                await _initCompletionSource.Task;

                if (_requests != null)
                {
                    var requests = func is null
                        ? _requests
                        : _requests.Where(func);

                    result.SetSuccess(requests);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetUsersAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitializeMockDataAsync()
        {
            await Task.WhenAll(
                InitUsersAsync(),
                InitRequestsAsync());

            _initCompletionSource.TrySetResult(true);
        }

        private Task InitUsersAsync() => Task.Run(() =>
        {
            _users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Yevhen",
                    Surname = "Selietkov",
                    Email = "evgeniy@mail.com",
                    Password = "Evgeniy99",
                    TimeZoneId = "Europe/Kiev",
                    CountryCode = "UA",
                    Position = "Mobile Developer",
                    StartWorkTime = new TimeSpan(9, 0, 0).ToString(),
                    EndWorkTime = new TimeSpan(17, 0, 0).ToString(),
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Hanna",
                    Surname = "Vicheva",
                    Email = "hanna@mail.com",
                    Password = "Hanna98",
                    TimeZoneId = "America/New_York",
                    CountryCode = "US",
                    Position = "HR",
                    StartWorkTime = new TimeSpan(9, 0, 0).ToString(),
                    EndWorkTime = new TimeSpan(17, 0, 0).ToString(),
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Yuriy",
                    Surname = "Ziryanov",
                    Email = "yuriy@mail.com",
                    Password = "Yuriy99",
                    TimeZoneId = "Asia/Tokyo",
                    CountryCode = "AU",
                    Position = "QA",
                    StartWorkTime = new TimeSpan(9, 0, 0).ToString(),
                    EndWorkTime = new TimeSpan(17, 0, 0).ToString(),
                },
            };
        });

        private Task InitRequestsAsync() => Task.Run(() =>
        {
            _requests = new List<RequestModel>
            {
                new RequestModel
                {
                    Id = 1,
                    Type = "Holiday",
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddDays(10).Date,
                    UserId = 1,
                },
                new RequestModel
                {
                    Id = 2,
                    Type = "Holiday",
                    StartDate = DateTime.Now.AddDays(1).Date,
                    EndDate = DateTime.Now.AddDays(3).Date,
                    UserId = 2,
                },
                new RequestModel
                {
                    Id = 3,
                    Type = "Holiday",
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddDays(2).Date,
                    UserId = 3,
                },
                new RequestModel
                {
                    Id = 4,
                    Type = "Holiday",
                    StartDate = DateTime.Now.AddDays(5).Date,
                    EndDate = DateTime.Now.AddDays(5).Date,
                    UserId = 3,
                },
            };
        });

        #endregion
    }
}
