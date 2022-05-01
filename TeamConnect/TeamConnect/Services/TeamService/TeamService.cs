using System;
using System.Threading.Tasks;
using TeamConnect.Helpers;
using TeamConnect.Models.Team;
using TeamConnect.Services.Repository;
using TeamConnect.Services.SettingsManager;
using TeamConnect.Services.UserService;

namespace TeamConnect.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;
        private readonly IUserService _userService;

        public TeamService(
            IRepository repository,
            ISettingsManager settingsManager,
            IUserService userService)
        {
            _repository = repository;
            _settingsManager = settingsManager;
            _userService = userService;
        }

        #region -- ITeamService implementation --

        public bool IsUserInTeam => _settingsManager.TeamId > 0;

        public async Task<OperationResult<TeamModel>> GetTeamAsync()
        {
            var result = new OperationResult<TeamModel>();

            try
            {
                var team = await _repository.FindAsync<TeamModel>(t => t.Id == _settingsManager.TeamId);

                if (team is not null)
                {
                    result.SetSuccess(team);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetTeamAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<OperationResult> CreateTeamAsync(TeamModel team)
        {
            var result = new OperationResult();

            try
            {
                var teamId = await _repository.InsertAsync(team);

                if (teamId > 0)
                {
                    var getCurrentUserResult = await _userService.GetCurrentUserAsync();

                    if (getCurrentUserResult.IsSuccess)
                    {
                        var user = getCurrentUserResult.Result;

                        user.TeamId = teamId;

                        var updateUserResult = await _repository.UpdateAsync(user);

                        _settingsManager.TeamId = teamId;

                        result.SetSuccess();
                    }
                    else
                    {
                        result.SetFailure();
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(CreateTeamAsync)} : exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
