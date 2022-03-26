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
        private readonly TaskCompletionSource<bool> _initComletionSource = new TaskCompletionSource<bool>();

        private IEnumerable<UserModel> _users;
        private IEnumerable<RequestModel> _requests;

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
                await _initComletionSource.Task;

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

        #endregion

        #region -- Requests methods --

        public async Task<OperationResult<IEnumerable<RequestModel>>> GetRequestsAsync(Func<RequestModel, bool> func = null)
        {
            var result = new OperationResult<IEnumerable<RequestModel>>();

            try
            {
                await _initComletionSource.Task;

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

            _initComletionSource.TrySetResult(true);
        }

        private Task InitUsersAsync() => Task.Run(() =>
        {
            var time = 

            _users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Yevhen",
                    Surname = "Selietkov",
                    StartWorkTime = new DateTime() + new TimeSpan(9, 0, 0),
                    EndWorkTime = new DateTime() + new TimeSpan(17, 0, 0),
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Hanna",
                    Surname = "Vicheva",
                    StartWorkTime = new DateTime() + new TimeSpan(6, 0, 0),
                    EndWorkTime = new DateTime() + new TimeSpan(14, 0, 0),
                },
                new UserModel
                {
                    Id = 3,
                    Name = "Yuriy",
                    Surname = "Ziryanov",
                    StartWorkTime = new DateTime() + new TimeSpan(13, 0, 0),
                    EndWorkTime = new DateTime() + new TimeSpan(21, 0, 0),
                },
                //new UserModel
                //{
                //    Id = 1,
                //    Name = "Yevhen",
                //    Surname = "Selietkov",
                //    StartWorkTime = new DateTime() + new TimeSpan(9, 0, 0),
                //    EndWorkTime = new DateTime() + new TimeSpan(17, 0, 0),
                //},
                //new UserModel
                //{
                //    Id = 2,
                //    Name = "Hanna",
                //    Surname = "Vicheva",
                //    StartWorkTime = new DateTime() + new TimeSpan(6, 0, 0),
                //    EndWorkTime = new DateTime() + new TimeSpan(14, 0, 0),
                //},
                //new UserModel
                //{
                //    Id = 3,
                //    Name = "Yuriy",
                //    Surname = "Ziryanov",
                //    StartWorkTime = new DateTime() + new TimeSpan(14, 0, 0),
                //    EndWorkTime = new DateTime() + new TimeSpan(21, 0, 0),
                //},
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
