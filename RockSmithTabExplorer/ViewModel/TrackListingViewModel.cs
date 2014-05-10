using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using RockSmithTabExplorer.Services;
using GalaSoft.MvvmLight.Messaging;

namespace RockSmithTabExplorer.ViewModel
{
    public class TrackListingViewModel : ViewModelBase
    {
        public SongCollection SongCollection { get; private set; }

        private CurrentSongService currentSongService;

        public RSSongInfo SelectedSongInfo
        {
            get { return currentSongService.CurrentSongInfo; }
            set { currentSongService.ChangeSong(value); }
        }

        public void OnLoadingChange(bool isLoading)
        {
            if (SongCollection.AvaliableSongInfos.Count > 0 && SelectedSongInfo == null)
            {
                currentSongService.PickFirstSong();
                RaisePropertyChanged("SelectedSongInfo");
            }
        }

        public TrackListingViewModel(SongCollection songCollection, CurrentSongService currentSongService)
        {
            SongCollection = songCollection;
            this.currentSongService = currentSongService;

            Messenger.Default.Register<bool>(this, "IsLoading", OnLoadingChange);
        }
    }
}
