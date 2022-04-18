﻿using System;
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
                Email = model.Email,
                Password = model.Password,
                Position = model.Position,
                StartWorkTime = TimeSpan.Parse(model.StartWorkTime),
                EndWorkTime = TimeSpan.Parse(model.EndWorkTime),
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
                Position = viewModel.Position,
                StartWorkTime = viewModel.StartWorkTime.ToString(),
                EndWorkTime = viewModel.EndWorkTime.ToString(),
                IsAccountCreated = viewModel.IsAccountCreated,
            };
        }
    }
}
