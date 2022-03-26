using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Request;

namespace TeamConnect.Services.RequestService
{
    public interface IRequestService
    {
        Task<OperationResult<IEnumerable<RequestModel>>> GetAllRequestsAsync();
    }
}
