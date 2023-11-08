using System;
using Theme_16.ModelViews.Base;
using Theme_16.Services.Interfaces;
using Theme_16.Stores;

namespace Theme_16.Services
{
    internal class NavigationService<TNavigationStore, TViewModel> : INavigationService
        where TNavigationStore : NavigationStore
        where TViewModel : ViewModel
    {
        private readonly TNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(TNavigationStore navigationService, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationService;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
