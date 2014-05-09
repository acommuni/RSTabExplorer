using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RockSmithTabExplorer
{
    public class SongCollection : INotifyPropertyChanged //SongCollection
    {
        private List<ArcFileWrapper> files = new List<ArcFileWrapper>();
        public List<string> fileNames = new List<string>();

        public List<RSSongInfo> AvaliableSongInfos { get; private set; }

        public SongCollection()
        {
            Clear();
        }

        public void Add(string file)
        {
            var arcFile = new ArcFileWrapper(file);
            files.Add(arcFile);
            fileNames.Add(file);

            AvaliableSongInfos = AvaliableSongInfos.Union(arcFile.GetAllSongInfos()).ToList();
            OnPropertyChanged("AvaliableSongInfos");
        }

        public void Clear()
        {
            files.Clear();
            fileNames.Clear();
            AvaliableSongInfos = new List<RSSongInfo>();
        }

        public RSSongInfo GetFirstSong()
        {
            return AvaliableSongInfos.FirstOrDefault();
        }

        public TrackDetail GetTrackDetail(string songKey, string arrangmentName)
        {
            return files.Select(f=>f.GetTrackDetail(songKey,arrangmentName)).FirstOrDefault(td=>td!=null);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChangedExplicit(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)// = null) [CallerMemberName]
        {
            OnPropertyChangedExplicit(propertyName);
        }

        #endregion
    }
}
