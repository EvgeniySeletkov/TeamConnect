using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamConnect.Extensions;
using TeamConnect.Models.Request;
using TeamConnect.Services.RequestService;
using TeamConnect.Services.AuthorizationService;

namespace TeamConnect.ViewModels
{
    public class RequestsPageViewModel : BaseViewModel
    {
        private readonly IRequestService _requestService;
        private readonly IAuthorizationService _authorizationService;

        public RequestsPageViewModel(
            INavigationService navigationService,
            IRequestService requestService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _requestService = requestService;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private List<RequestGroupViewModel> _requests;
        public List<RequestGroupViewModel> Requests
        {
            get => _requests;
            set => SetProperty(ref _requests, value);
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            await LoadRequestsAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task LoadRequestsAsync()
        {
            var getRequestsResult = await _requestService.GetAllRequestsAsync();

            if (getRequestsResult.IsSuccess)
            {
                var requests = getRequestsResult.Result;

                var requestsGroups = new List<RequestGroupViewModel>();

                for (int i = 0; i < 7; i++)
                {
                    requestsGroups.Add(new RequestGroupViewModel(DateTime.Now.AddDays(i)));
                }

                foreach (var requestGroup in requestsGroups)
                {
                    foreach (var request in requests)
                    {
                        if (requestGroup.Date >= request.StartDate && requestGroup.Date <= request.EndDate)
                        {
                            var getUserResult = await _authorizationService.GetUserByIdAsync(request.UserId);

                            if (getUserResult.IsSuccess)
                            {
                                requestGroup.Add(request.ToViewModel(getUserResult.Result));
                            }
                        }
                    }
                }

                Requests = requestsGroups;
            }
        }

        #endregion
    }
}
