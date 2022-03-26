using TeamConnect.Models.User;

namespace TeamConnect.Extensions
{
    public static class UserExtension
    {
        public static UserViewModel ToViewModel(this UserModel model)
        {
            return new UserViewModel
            {
                Id = model.Id,
                Photo = model.Photo,
                Name = model.Name,
                Surname = model.Surname,
                StartWorkTime = model.StartWorkTime,
                EndWorkTime = model.EndWorkTime,
            };
        }
    }
}
