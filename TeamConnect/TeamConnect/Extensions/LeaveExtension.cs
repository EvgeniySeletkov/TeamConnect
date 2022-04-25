using TeamConnect.Models.Leave;
using TeamConnect.Models.User;

namespace TeamConnect.Extensions
{
    public static class LeaveExtension
    {
        public static LeaveViewModel ToViewModel(this LeaveModel leave, UserModel user)
        {
            return new LeaveViewModel
            {
                Id = leave.Id,
                UserPhoto = user.Photo,
                UserName = user.Name,
                UserSurname = user.Surname,
                Type = leave.Type,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                UserId = leave.UserId,
            };
        }
    }
}
