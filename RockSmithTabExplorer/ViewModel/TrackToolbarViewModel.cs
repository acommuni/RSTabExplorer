using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RockSmithTabExplorer.Services;

namespace RockSmithTabExplorer.ViewModel
{
    public class TrackToolbarViewModel : ViewModelBase
    {
        private readonly CurrentSongService currentSongService;

        public bool IsATrackLoaded { get { return currentSongService.Score != null || ViewModelBase.IsInDesignModeStatic; } }

        public RSSongInfo CurrentSongInfo { get { return currentSongService.CurrentSongInfo; } }

        private void OnSongChange(SongChange source)
        {
            if (source == SongChange.Score)
                RaisePropertyChanged("IsATrackLoaded");
        }

        public TrackToolbarViewModel(CurrentSongService currentSongService)
        {
            this.currentSongService = currentSongService;

            Messenger.Default.Register<SongChange>(this,OnSongChange);
        }
    }
}
