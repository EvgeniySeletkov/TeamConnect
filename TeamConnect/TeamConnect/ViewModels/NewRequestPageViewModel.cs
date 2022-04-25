﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using TeamConnect.Extensions;
using TeamConnect.Models.Leave;
using TeamConnect.Resources.Strings;
using TeamConnect.Services.LeaveService;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TeamConnect.ViewModels
{
    public class NewRequestPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly ILeaveService _leaveService;

        public NewRequestPageViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            ILeaveService leaveService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _leaveService = leaveService;
        }

        #region -- Public properties --

        private List<string> _requestTypes;
        public List<string> RequestTypes
        {
            get => _requestTypes;
            set => SetProperty(ref _requestTypes, value);
        }

        private string _selectedRequestType;
        public string SelectedRequestType
        {
            get => _selectedRequestType;
            set => SetProperty(ref _selectedRequestType, value);
        }

        private bool _isStartDateChanged;
        public bool IsStartDateChanged
        {
            get => _isStartDateChanged;
            set => SetProperty(ref _isStartDateChanged, value);
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private bool _isEndDateChanged;
        public bool IsEndDateChanged
        {
            get => _isEndDateChanged;
            set => SetProperty(ref _isEndDateChanged, value);
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private ICommand _selectRequestTypeCommand;
        public ICommand SelectRequestTypeCommand => _selectRequestTypeCommand ??= new AsyncCommand(OnSelectRequestTypeCommandAsync);

        private ICommand _selectStartDateCommand;
        public ICommand SelectStartDateCommand => _selectStartDateCommand ??= new AsyncCommand(OnSelectStartDateCommandAsync);

        private ICommand _selectEndDateCommand;
        public ICommand SelectEndDateCommand => _selectEndDateCommand ??= new AsyncCommand(OnSelectEndDateCommandAsync);

        private ICommand _createRequestTapCommand;
        public ICommand CreateRequestTapCommand => _createRequestTapCommand ??= new AsyncCommand(OnCreateRequestTapCommandAsync);

        #endregion

        #region -- Overrides --

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            InitializePositions();
        }

        #endregion

        #region -- Private helpers --

        private void InitializePositions()
        {
            RequestTypes = new List<string>
            {
                Strings.Vacation,
                Strings.SickLeave,
                Strings.UnpaidLeave,
            };
        }

        private async Task OnSelectRequestTypeCommandAsync()
        {
            var selectedType = await _userDialogs.ActionSheetAsync(Strings.SelectType, Strings.Cancel, null, null, RequestTypes.ToArray());

            if (selectedType != Strings.Cancel)
            {
                SelectedRequestType = selectedType;
            }
        }

        private async Task OnSelectStartDateCommandAsync()
        {
            var config = new DatePromptConfig();
            config.iOSPickerStyle = iOSPickerStyle.Wheels;

            var datePromptResult = await _userDialogs.DatePromptAsync(config);
            
            if (datePromptResult.Ok)
            {
                if (!IsEndDateChanged)
                {
                    IsStartDateChanged = true;
                    StartDate = datePromptResult.SelectedDate.Date;
                    IsEndDateChanged = true;
                    EndDate = StartDate;
                }
                else
                {
                    if (datePromptResult.SelectedDate <= EndDate)
                    {
                        IsStartDateChanged = true;
                        StartDate = datePromptResult.SelectedDate.Date;
                    }
                    else
                    {
                        await _userDialogs.AlertAsync(Strings.StartRequestDateError);
                    }
                }
            }
        }

        private async Task OnSelectEndDateCommandAsync()
        {
            var config = new DatePromptConfig();
            config.iOSPickerStyle = iOSPickerStyle.Wheels;

            var datePromptResult = await _userDialogs.DatePromptAsync(config);

            if (datePromptResult.Ok)
            {
                if (!IsStartDateChanged)
                {
                    IsEndDateChanged = true;
                    EndDate = datePromptResult.SelectedDate.Date;
                    IsStartDateChanged = true;
                    StartDate = EndDate;
                }
                else
                {
                    if (datePromptResult.SelectedDate >= StartDate)
                    {
                        IsEndDateChanged = true;
                        EndDate = datePromptResult.SelectedDate.Date;
                    }
                    else
                    {
                        await _userDialogs.AlertAsync(Strings.EndRequestDateError);
                    }
                }
            }
        }

        private async Task OnCreateRequestTapCommandAsync()
        {
            var leave = new LeaveViewModel
            {
                Type = SelectedRequestType,
                StartDate = StartDate,
                EndDate = EndDate,
            };

            var addLeaveResult = await _leaveService.AddLeaveAsync(leave.ToModel());

            if (addLeaveResult.IsSuccess)
            {
                await NavigationService.GoBackAsync(null, false, true);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(addLeaveResult.Message))
                {
                    await _userDialogs.AlertAsync(addLeaveResult.Message);
                }
                else
                {
                    await _userDialogs.AlertAsync(Strings.OoopsSomethingWentWrong);
                }
            }
        }

        #endregion
    }
}
