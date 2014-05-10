using GalaSoft.MvvmLight;
using AlphaTab.Model;
using RockSmithTabExplorer.Services;
using GalaSoft.MvvmLight.Messaging;

namespace RockSmithTabExplorer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Track CurrentTrack { get { return currentSongService.GetAlphaTabTrack(); } }

        private CurrentSongService currentSongService;

        private void OnTrackChange(SongChange c)
        {
            RaisePropertyChanged("CurrentTrack");
        }

        public MainViewModel(CurrentSongService currentSongService)
        {
            this.currentSongService = currentSongService;

            Messenger.Default.Register<SongChange>(this, OnTrackChange );
        }
    }
}
