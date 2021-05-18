using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mp4Player
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private Bookmark _selected;
        public ObservableCollection<Bookmark> Bookmarks { get; set; }

        public ViewModel()
        {
            Bookmarks = new ObservableCollection<Bookmark>();
        }

        public Bookmark Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public void Add(TimeSpan time)
        {
            _selected = new Bookmark(time, Bookmarks.Count);
            Bookmarks.Insert(0, _selected);
        }

        public void Delete()
        {
            Bookmarks.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}