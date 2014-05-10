using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using RockSmithTabExplorer.Controls;
using RockSmithTabExplorer.Services;
using GalaSoft.MvvmLight.Messaging;

namespace RockSmithTabExplorer.ViewModel
{
    public class MainToolbarViewModel : ViewModelBase
    {
        public ICommand OpenFileCommand { get; private set; }
        public ICommand LoadDiskTracksCommand { get; private set; }
        public ICommand LoadDLCTracksCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private readonly PrintService printService;
        private readonly ExportImageService saveService;
        private readonly CurrentSongService currentSongService;

        public LoadState LoadState { get; private set; }

        public bool IsATrackLoaded { get { return currentSongService.Score != null; } }

        private string selectedGuitarPath;
        public string SelectedGuitarPath
        {
            get { return selectedGuitarPath; }
            set
            {
                selectedGuitarPath = value;
                currentSongService.SetGuitarPath(value);
                RaisePropertyChanged("SelectedGuitarPath");
            }
        }

        public MainToolbarViewModel(SongLoader songLoader, CurrentSongService currentSongService, PrintService printService, ExportImageService saveService)
        {
            LoadState = new LoadState();
            this.printService = printService;
            this.saveService = saveService;
            this.currentSongService = currentSongService;

            SelectedGuitarPath = currentSongService.GuitarPath.Name;

            OpenFileCommand = new RelayCommand(songLoader.OpenFile);
            LoadDiskTracksCommand = new RelayCommand(songLoader.LoadDiskTracks);
            LoadDLCTracksCommand = new RelayCommand(songLoader.LoadDLCTracks);
            PrintCommand = new RelayCommand<TabControl>(PrintTab);
            SaveCommand = new RelayCommand<TabControl>(SaveTabImage);

            Messenger.Default.Register<SongChange>(this, (SongChange c) => { if (c == SongChange.Score) RaisePropertyChanged("IsATrackLoaded"); });
        }

        private void SaveTabImage(TabControl tabControl)
        {
            saveService.SavePNGFromDialog(tabControl, NameFromScore(currentSongService.Score));
        }

        private void PrintTab(TabControl tabControl)
        {
            printService.Print(tabControl, NameFromScore(currentSongService.Score));
        }

        private string NameFromScore(AlphaTab.Model.Score score)
        {
            return score.Artist + " - " + score.Title;
        }
    }
}
