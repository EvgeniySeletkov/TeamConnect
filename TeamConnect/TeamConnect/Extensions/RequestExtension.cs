using TeamConnect.Models.Request;
using TeamConnect.Models.User;

namespace TeamConnect.Extensions
{
    public static class RequestExtension
    {
        public static RequestViewModel ToViewModel(this RequestModel request, UserModel user)
        {
            return new RequestViewModel
            {
                Id = request.Id,
                UserPhoto = user.Photo,
                UserName = user.Name,
                UserSurname = user.Surname,
                Type = request.Type,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserId = request.UserId,
            };
        }
    }
}
