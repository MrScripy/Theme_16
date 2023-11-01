using Theme_16.ModelViews.Base;
using Theme_16.Services.Interfaces;
using System;
using Theme_16.Stores;

namespace Theme_16.ViewModels
{
    internal class MainWindowViewModel : ViewModel, IDisposable
    {

        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
      


        //private ICommand _downloadDataCommand;
        //public ICommand DownloadDataCommand => _downloadDataCommand ??=
        //    new LambdaCommand(OnDownloadDataCommandExecuted, CanDownloadDataCommandExecute);

        //private bool CanDownloadDataCommandExecute(object p) => true;
        //private void OnDownloadDataCommandExecuted(object p)
        //{
        //    Task.Run(async () => await DownloadCustomersData());
        //}





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

      


        public void Dispose()
        {
            
        }
    }
}
