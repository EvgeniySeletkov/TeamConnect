using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Request;
using TeamConnect.Services.MockDataService;

namespace TeamConnect.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly IMockDataService _mockDataService;

        public RequestService(
            IMockDataService mockDataService)
        {
            _mockDataService = mockDataService;
        }

        #region -- IRequestService implementation --
        public async Task<OperationResult<IEnumerable<RequestModel>>> GetAllRequestsAsync()
        {
            var result = new OperationResult<IEnumerable<RequestModel>>();

            try
            {
                var requests = await _mockDataService.GetRequestsAsync();

                if (requests.IsSuccess)
                {
                    result.SetSuccess(requests.Result);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllRequestsAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
