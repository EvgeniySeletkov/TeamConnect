using System;
using TeamConnect.Models.User;

namespace TeamConnect.Extensions
{
    public static class UserExtension
    {
        #region -- Public helpers --

        public static UserViewModel ToViewModel(this UserModel model)
        {
            return new UserViewModel
            {
                Id = model.Id,
                Photo = model.Photo,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address,
                TimeZoneId = model.TimeZoneId,
                CountryCode = model.CountryCode,
                Position = model.Position,
                StartWorkTime = ConvertTimeByTimeZoneId(TimeSpan.Parse(model.StartWorkTime), model.TimeZoneId, TimeZoneInfo.Local.Id),
                EndWorkTime = ConvertTimeByTimeZoneId(TimeSpan.Parse(model.EndWorkTime), model.TimeZoneId, TimeZoneInfo.Local.Id),
                IsAccountCreated = model.IsAccountCreated,
            };
        }

        public static UserModel ToModel(this UserViewModel viewModel)
        {
            return new UserModel
            {
                Id = viewModel.Id,
                Photo = viewModel.Photo,
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                Address = viewModel.Address,
                TimeZoneId = viewModel.TimeZoneId,
                CountryCode = viewModel.CountryCode,
                Position = viewModel.Position,
                StartWorkTime = ConvertTimeByTimeZoneId(viewModel.StartWorkTime.TimeOfDay, viewModel.TimeZoneId).TimeOfDay.ToString(),
                EndWorkTime = ConvertTimeByTimeZoneId(viewModel.EndWorkTime.TimeOfDay, viewModel.TimeZoneId).TimeOfDay.ToString(),
                IsAccountCreated = viewModel.IsAccountCreated,
            };
        }

        public static UserModel ToModelWithoutTimeConverting(this UserViewModel viewModel)
        {
            return new UserModel
            {
                Id = viewModel.Id,
                Photo = viewModel.Photo,
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                Address = viewModel.Address,
                TimeZoneId = viewModel.TimeZoneId,
                CountryCode = viewModel.CountryCode,
                Position = viewModel.Position,
                StartWorkTime = viewModel.StartWorkTime.TimeOfDay.ToString(),
                EndWorkTime = viewModel.EndWorkTime.TimeOfDay.ToString(),
                IsAccountCreated = viewModel.IsAccountCreated,
            };
        }

        #endregion

        #region -- Private helpers --

        private static DateTime ConvertTimeByTimeZoneId(TimeSpan time, string sourceTimeZoneId, string destinationTimeZoneId)
        {
            var dateTime = DateTime.SpecifyKind(DateTime.Now.Date + time, DateTimeKind.Unspecified);

            var convertedDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                dateTime,
                sourceTimeZoneId,
                destinationTimeZoneId);

            return DateTime.SpecifyKind(convertedDateTime, DateTimeKind.Local);
        }

        private static DateTime ConvertTimeByTimeZoneId(TimeSpan time, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                DateTime.Now.Date + time,
                timeZoneId);
        }

        #endregion
    }
}
