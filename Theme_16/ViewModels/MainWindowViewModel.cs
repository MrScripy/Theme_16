using System;
using Theme_16.ModelViews.Base;
using Theme_16.Services.Interfaces;
using Theme_16.Stores;

namespace Theme_16.ViewModels
{
    internal class MainWindowViewModel : ViewModel, IDisposable
    {

        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
      
        public MainWindowViewModel(IDataCreator dataCreator, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _navigationStore.CurrentViewModel = new LoginViewModel();

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void Dispose() {}
    }
}
