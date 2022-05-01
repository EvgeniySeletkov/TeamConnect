using TeamConnect.Models.Notofication;

namespace TeamConnect.Extensions
{
    public static class NotificationExtension
    {
        public static NotificationViewModel ToViewModel(this NotificationModel model)
        {
            return new NotificationViewModel
            {
                Id = model.Id,
                Type = model.Type,
                UserId = model.UserId,
                TeamId = model.TeamId,
            };
        }

        public static NotificationModel ToModel(this NotificationViewModel viewModel)
        {
            return new NotificationModel
            {
                Id = viewModel.Id,
                Type = viewModel.Type,
                UserId = viewModel.UserId,
                TeamId = viewModel.TeamId,
            };
        }
    }
}
