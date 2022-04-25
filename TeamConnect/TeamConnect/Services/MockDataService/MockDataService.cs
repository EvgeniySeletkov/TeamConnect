using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;
using TeamConnect.Models.User;

namespace TeamConnect.Services.MockDataService
{
    public class MockDataService : IMockDataService
    {
        private readonly TaskCompletionSource<bool> _initCompletionSource = new TaskCompletionSource<bool>();

        private IList<UserModel> _users;
        private IList<LeaveModel> _leaves;

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

                int usersCount = _users.Count;

                _users.Add(user);

                if (_users.Count == usersCount + 1)
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

        public async Task<OperationResult> UpdateUserAsync(UserModel user)
        {
            var result = new OperationResult();

            try
            {
                await _initCompletionSource.Task;

                var userId = _users.ToList().FindIndex(u => u.Id == user.Id);

                if (user is not null)
                {
                    _users[userId] = user;

                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(UpdateUserAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Leaves methods --

        public async Task<OperationResult<IEnumerable<LeaveModel>>> GetLeavesAsync(Func<LeaveModel, bool> func = null)
        {
            var result = new OperationResult<IEnumerable<LeaveModel>>();

            try
            {
                await _initCompletionSource.Task;

                if (_leaves != null)
                {
                    var leaves = func is null
                        ? _leaves
                        : _leaves.Where(func);

                    result.SetSuccess(leaves);
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

        public async Task<OperationResult> AddLeaveAsync(LeaveModel leave)
        {
            var result = new OperationResult();

            try
            {
                await _initCompletionSource.Task;

                int leavesCount = _leaves.Count;

                _leaves.Add(leave);

                if (_leaves.Count == leavesCount + 1)
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
                result.SetError($"{nameof(AddLeaveAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitializeMockDataAsync()
        {
            await Task.WhenAll(
                InitUsersAsync(),
                InitLeavesAsync());

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
                    IsAccountCreated = true,
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
                    IsAccountCreated = true,
                },
            };
        });

        private Task InitLeavesAsync() => Task.Run(() =>
        {
            _leaves = new List<LeaveModel>
            {
                new LeaveModel
                {
                    Id = 1,
                    Type = "Holiday",
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddDays(10).Date,
                    UserId = 1,
                },
                new LeaveModel
                {
                    Id = 2,
                    Type = "Holiday",
                    StartDate = DateTime.Now.AddDays(1).Date,
                    EndDate = DateTime.Now.AddDays(3).Date,
                    UserId = 2,
                },
                new LeaveModel
                {
                    Id = 3,
                    Type = "Holiday",
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddDays(2).Date,
                    UserId = 3,
                },
                new LeaveModel
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
