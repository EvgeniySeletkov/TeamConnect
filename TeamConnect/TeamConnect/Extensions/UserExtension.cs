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
                TeamId = model.TeamId,
                Position = model.Position,
                StartWorkTime = !string.IsNullOrWhiteSpace(model.StartWorkTime)
                ? ConvertTimeByTimeZoneId(TimeSpan.Parse(model.StartWorkTime), model.TimeZoneId, TimeZoneInfo.Local.Id)
                : DateTime.Now.Date,
                EndWorkTime = !string.IsNullOrWhiteSpace(model.EndWorkTime)
                ? ConvertTimeByTimeZoneId(TimeSpan.Parse(model.EndWorkTime), model.TimeZoneId, TimeZoneInfo.Local.Id)
                : DateTime.Now.Date,
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
                TeamId = viewModel.TeamId,
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
                TeamId = viewModel.TeamId,
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
            var result = DateTime.Now.Date + time;

            if (!string.IsNullOrWhiteSpace(sourceTimeZoneId)
                && !string.IsNullOrWhiteSpace(destinationTimeZoneId))
            {
                var dateTime = DateTime.SpecifyKind(result, DateTimeKind.Unspecified);
                var convertedDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, sourceTimeZoneId, destinationTimeZoneId);
                result =  DateTime.SpecifyKind(convertedDateTime, DateTimeKind.Local);
            }

            return result;
        }

        private static DateTime ConvertTimeByTimeZoneId(TimeSpan time, string timeZoneId)
        {
            return !string.IsNullOrWhiteSpace(timeZoneId)
                ? TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.Date + time, timeZoneId)
                : DateTime.Now.Date + time;
        }

        #endregion
    }
}
