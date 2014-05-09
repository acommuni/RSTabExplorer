using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace RockSmithTabExplorer.ViewModel
{
    public class StatusViewModel : ViewModelBase
    {
        private SongCollection songCollection;

        private string _status;
        public string Status
        {
            get { return _status; }
            private set { Set<string>(() => Status, ref _status, value); }
        }

        private void OnSongCollectionChange(object sender, PropertyChangedEventArgs args)
        {
            Status = String.Join(", ", songCollection.fileNames);
        }

        public StatusViewModel(SongCollection songCollection)
        {
            this.songCollection = songCollection;
            songCollection.PropertyChanged += OnSongCollectionChange;

            if (ViewModelBase.IsInDesignModeStatic)
                Status = @"C:\SteamLibrary\SteamApps\common\Rocksmith2014";
        }
    }
}
