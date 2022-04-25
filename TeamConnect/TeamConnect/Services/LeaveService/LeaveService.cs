using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.MockDataService;
using TeamConnect.Services.SettingsManager;

namespace TeamConnect.Services.LeaveService
{
    public class LeaveService : ILeaveService
    {
        private readonly IMockDataService _mockDataService;
        private readonly ISettingsManager _settingsManager;

        public LeaveService(
            IMockDataService mockDataService,
            ISettingsManager settingsManager)
        {
            _mockDataService = mockDataService;
            _settingsManager = settingsManager;
        }

        #region -- ILeaveService implementation --

        public async Task<OperationResult<IEnumerable<LeaveModel>>> GetAllLeavesAsync()
        {
            var result = new OperationResult<IEnumerable<LeaveModel>>();

            try
            {
                var leavesResult = await _mockDataService.GetLeavesAsync();

                if (leavesResult.IsSuccess)
                {
                    result.SetSuccess(leavesResult.Result);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllLeavesAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> AddLeaveAsync(LeaveModel newLeave)
        {
            var result = new OperationResult();

            try
            {
                var leavesResult = await _mockDataService.GetLeavesAsync(
                    l => l.UserId == _settingsManager.UserId
                    && l.StartDate <= newLeave.StartDate
                    && l.EndDate >= newLeave.StartDate
                    || l.UserId == _settingsManager.UserId
                    && l.StartDate <= newLeave.EndDate
                    && l.EndDate >= newLeave.EndDate);

                if (leavesResult.IsSuccess)
                {
                    var leave = leavesResult.Result.FirstOrDefault();

                    if (leave is null)
                    {
                        newLeave.UserId = _settingsManager.UserId;

                        var addLeaveResult = await _mockDataService.AddLeaveAsync(newLeave);

                        if (addLeaveResult.IsSuccess)
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
                        result.SetFailure(Strings.OverlappingLeavesError);
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllLeavesAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
