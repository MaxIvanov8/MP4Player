using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mp4Player
{
    class ViewModel : INotifyPropertyChanged
    {
        private Model _selected;
        public ObservableCollection<Model> Bookmarks { get; set; }

        public ViewModel()
        {
            Bookmarks = new ObservableCollection<Model>();
        }

        public Model Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        public void Add(TimeSpan label)
        {
            _selected = new Model
            {
                Number = Bookmarks.Count + 1,
                Bookmark = label
            };
            Bookmarks.Insert(0, Selected);
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