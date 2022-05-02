using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Team;
using TeamConnect.Models.User;

namespace TeamConnect.Services.TeamService
{
    public interface ITeamService
    {
        public bool IsUserInTeam { get; }

        Task<OperationResult<TeamModel>> GetTeamAsync();

        Task<OperationResult> CreateTeamAsync(TeamModel team);

        Task<OperationResult> AddMemberAsync(UserModel user);
    }
}
