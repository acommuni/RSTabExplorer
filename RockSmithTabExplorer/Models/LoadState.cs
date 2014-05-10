using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace RockSmithTabExplorer
{
    public class LoadState : ObservableObject
    {
        bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                RaisePropertyChanged("IsLoading");
                Set<bool>(() => IsLoading, ref _isLoading, value);
            }
        }

        public LoadState()
        {
            Messenger.Default.Register<bool>(this, "IsLoading", (isLoading) => IsLoading = isLoading);
        }
    }
}
