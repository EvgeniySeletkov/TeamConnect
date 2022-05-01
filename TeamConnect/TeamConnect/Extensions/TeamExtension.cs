using TeamConnect.Models.Team;

namespace TeamConnect.Extensions
{
    public static class TeamExtension
    {
        public static TeamViewModel ToViewModel(this TeamModel model)
        {
            return new TeamViewModel
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public static TeamModel ToModel(this TeamViewModel viewModel)
        {
            return new TeamModel
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
            };
        }
    }
}
