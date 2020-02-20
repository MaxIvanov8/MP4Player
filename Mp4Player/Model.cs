using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mp4Player
{
    class Model : INotifyPropertyChanged
    {
        private TimeSpan _bookmark;
        private int _number;

        public TimeSpan Bookmark
        {
            get { return _bookmark; }
            set
            {
                _bookmark = value;
                OnPropertyChanged("Bookmark");
            }
        }

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}