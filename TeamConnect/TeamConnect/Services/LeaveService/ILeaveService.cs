using System.Collections.Generic;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Leave;

namespace TeamConnect.Services.LeaveService
{
    public interface ILeaveService
    {
        Task<OperationResult<IEnumerable<LeaveModel>>> GetAllLeavesAsync();
    }
}
