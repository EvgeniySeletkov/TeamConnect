using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;
using TeamConnect.Services.MockDataService;

namespace TeamConnect.Services.LeaveService
{
    public class LeaveService : ILeaveService
    {
        private readonly IMockDataService _mockDataService;

        public LeaveService(
            IMockDataService mockDataService)
        {
            _mockDataService = mockDataService;
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

        #endregion
    }
}
