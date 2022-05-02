using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamConnect.Extensions;
using TeamConnect.Models.User;
using TeamConnect.Services.TeamService;
using TeamConnect.Services.UserService;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels.Popups
{
    public class AddMembersPopupPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public AddMembersPopupPageViewModel(
            INavigationService navigationService,
            IUserService userService,
            ITeamService teamService)
            : base(navigationService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        #region -- Public properties --

        private string _searchRequest;
        public string SearchRequest
        {
            get => _searchRequest;
            set => SetProperty(ref _searchRequest, value);
        }

        private bool _isUserListVisible;
        public bool IsUserListVisible
        {
            get => _isUserListVisible;
            set => SetProperty(ref _isUserListVisible, value);
        }

        private List<UserViewModel> _users;
        public List<UserViewModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private int _userListHeight;
        public int UserListHeight
        {
            get => _userListHeight;
            set => SetProperty(ref _userListHeight, value);
        }

        private UserViewModel _selectedUser;
        public UserViewModel SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private ICommand _deleteUserTapCommand;
        public ICommand DeleteUserTapCommand => _deleteUserTapCommand ??= new AsyncCommand(OnDeleteUserTapCommandAsync);

        private ICommand _cancelTapCommand;
        public ICommand CancelTapCommand => _cancelTapCommand ??= new AsyncCommand(OnCancelTapCommandAsync);

        private ICommand _addTapCommand;
        public ICommand AddTapCommand => _addTapCommand ??= new AsyncCommand(OnAddTapCommandAsync);

        #endregion

        #region -- Overrides --

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SearchRequest))
            {
                if (!string.IsNullOrWhiteSpace(SearchRequest))
                {
                    var searchResult = await _userService.SearchUsers(SearchRequest);

                    if (searchResult.IsSuccess)
                    {
                        Users = searchResult.Result.Select(u => u.ToViewModel()).ToList();

                        ICommand userTapCommand = new AsyncCommand<UserViewModel>(OnUserTapCommandAsync);

                        Users.ForEach(u => u.TapCommand = userTapCommand);
                    }
                    else
                    {
                        Users?.Clear();
                    }
                }
                else
                {
                    Users?.Clear();
                }

                ChangeListHeight();
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnUserTapCommandAsync(UserViewModel user)
        {
            SelectedUser = user;

            SearchRequest = string.Empty;

            return Task.CompletedTask;
        }

        private void ChangeListHeight()
        {
            IsUserListVisible = Users?.Count > 0;

            UserListHeight = Users is null 
                ? UserListHeight = 0
                : Users.Count < 3 
                ? Users.Count * 53 
                : UserListHeight = 159;
        }

        private Task OnDeleteUserTapCommandAsync()
        {
            SelectedUser = null;

            return Task.CompletedTask;
        }

        private Task OnCancelTapCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }

        private async Task OnAddTapCommandAsync()
        {
            var addMemberResult = await _teamService.AddMemberAsync(SelectedUser.ToModel());

            var parameters = new NavigationParameters();

            if (!addMemberResult.IsSuccess)
            {
                parameters.Add(Constants.Navigation.MEMBER_NOT_ADDED, addMemberResult.Message);
            }

            await NavigationService.GoBackAsync(parameters);
        }

        #endregion
    }
}
