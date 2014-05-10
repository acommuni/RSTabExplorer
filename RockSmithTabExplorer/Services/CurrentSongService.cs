using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaTab.Model;
using RocksmithToolkitLib.Xml;
using GalaSoft.MvvmLight.Messaging;

namespace RockSmithTabExplorer.Services
{
    public enum SongChange { Score, Track, SongInfo, TrackInfo, Level, TrackDetail, Path }

    public class CurrentSongService : GalaSoft.MvvmLight.ObservableObject
    {
        private SongCollection songCollection;


        private int _currentTrackIndex;
        public Track GetAlphaTabTrack()
        {
            if (_score == null || _currentTrackIndex < 0 || _currentTrackIndex >= _score.Tracks.Count)
                return null;

            return _score.Tracks[_currentTrackIndex] as Track;
        }

        public RSSongInfo CurrentSongInfo { get; private set; }
        public void ChangeSong(RSSongInfo newSong)
        {
            CurrentSongInfo = newSong;

            setTrackFromPath();
        }

        public CurrentSongService(SongCollection songCollection)
        {
            this.songCollection = songCollection;
            GuitarPath = GuitarPath.FromSettings();
        }

        public void PickFirstSong()
        {
            ChangeSong(songCollection.GetFirstSong());
        }

        public GuitarPath GuitarPath { get; private set; }
        public void SetGuitarPath(string name)
        {
            GuitarPath = GuitarPath.Persist(name);
            setTrackFromPath();
        }

        /////////////////////////////////////////////////////////////////////////
        ///////////////// From MainViewModel. To Be Refactored. /////////////////
        /////////////////////////////////////////////////////////////////////////

        private Score _score;
        public Score Score
        {
            get { return _score; }
            protected set
            {
                _score = value;
                if (value != null && _score.Tracks.Count > 0)
                    _currentTrackIndex = 0;
                else
                    _currentTrackIndex = -1;
                RaisePropertyChanged("Score");
                //Messenger.Default.Send<SongChange>(SongChange.Track);
                Messenger.Default.Send<SongChange>(SongChange.Score);
            }
        }

        private void setTrackFromPath()
        {
            if (CurrentSongInfo != null && CurrentSongInfo.TrackInfos != null)
                SelectedRockSmithTrack = GuitarPath.pickTrack(CurrentSongInfo.TrackInfos);
        }

        RSTrackInfo _selectedRockSmithTrack;
        public RSTrackInfo SelectedRockSmithTrack
        {
            get { return _selectedRockSmithTrack; }
            set
            {
                _selectedRockSmithTrack = value;
                RaisePropertyChanged("SelectedRockSmithTrack");
                Messenger.Default.Send<SongChange>(SongChange.TrackInfo);
                if (value != null)
                    TrackDetail = songCollection.GetTrackDetail(CurrentSongInfo.Key, value.Name);
                else
                    TrackDetail = null;
            }
        }

        //Is set by user by selecting in dropdown. Automtically set when TrackDetail is set.
        SongLevel2014 _selectedLevel;
        public SongLevel2014 SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                _selectedLevel = value;
                RaisePropertyChanged("SelectedLevel");
                Messenger.Default.Send<SongChange>(SongChange.Level);
                GenerateScore();
            }
        }

        private void GenerateScore()
        {
            if (TrackDetail != null && SelectedLevel != null)
            {
                if (LevelOnlySelected)
                    Score = RockSmithImporter.GetScoreForExactDifficultyLevel(TrackDetail.RockSmithSong, SelectedLevel.Difficulty);
                else
                    Score = RockSmithImporter.GetScoreForMaxDifficultyLevel(TrackDetail.RockSmithSong, SelectedLevel.Difficulty);
            }
            else
                Score = null;
        }

        bool _levelOnlySelected;
        public bool LevelOnlySelected
        {
            get { return _levelOnlySelected; }
            set
            {
                _levelOnlySelected = value;
                RaisePropertyChanged("LevelOnlySelected");
                GenerateScore();
            }
        }



        //Is set when SelectedRockSmithTrack changes. Acts as source for level-dropdown
        TrackDetail _trackDetail;
        public TrackDetail TrackDetail
        {
            get { return _trackDetail; }
            protected set
            {
                _trackDetail = value;
                RaisePropertyChanged("TrackDetail");
                if (_trackDetail != null && _trackDetail.RockSmithSong != null && _trackDetail.RockSmithSong.Levels != null)
                    SelectedLevel = _trackDetail.RockSmithSong.Levels.LastOrDefault();
                else
                    SelectedLevel = null;
                Messenger.Default.Send<SongChange>(SongChange.TrackDetail);
            }
        }

    }
}
