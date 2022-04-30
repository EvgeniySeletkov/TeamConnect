using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.Repository;
using TeamConnect.Services.SettingsManager;

namespace TeamConnect.Services.LeaveService
{
    public class LeaveService : ILeaveService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public LeaveService(
            IRepository repository,
            ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        #region -- ILeaveService implementation --

        public async Task<OperationResult<IEnumerable<LeaveModel>>> GetLeavesByDatesAsync(DateTime startDate, DateTime endDate)
        {
            var result = new OperationResult<IEnumerable<LeaveModel>>();

            try
            {
                var leaves = await _repository.GetAllAsync<LeaveModel>(
                    l => l.StartDate >= startDate 
                    && l.StartDate <= endDate
                    || l.EndDate >= startDate
                    && l.EndDate <= endDate);

                if (leaves is not null && leaves.Count > 0)
                {
                    result.SetSuccess(leaves);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetLeavesByDatesAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> AddLeaveAsync(LeaveModel newLeave)
        {
            var result = new OperationResult();

            try
            {
                var leave = await _repository.FindAsync<LeaveModel>(
                    l => l.UserId == _settingsManager.UserId
                    && l.StartDate <= newLeave.StartDate
                    && l.EndDate >= newLeave.StartDate
                    || l.UserId == _settingsManager.UserId
                    && l.StartDate <= newLeave.EndDate
                    && l.EndDate >= newLeave.EndDate);

                if (leave is null)
                {
                    newLeave.UserId = _settingsManager.UserId;

                    await _repository.InsertAsync(newLeave);

                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure(Strings.OverlappingLeavesError);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetLeavesByDatesAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
