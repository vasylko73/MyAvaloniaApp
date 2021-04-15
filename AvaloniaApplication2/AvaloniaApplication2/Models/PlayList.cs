using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication2.Models
{
    class PlayList :INotifyPropertyChanged
    {
        public PlayList()
        {
            Track = new ObservableCollection<Track>();
        }

        public string PlayListName { get=>_PlayListName;
            set
            {
                _PlayListName = value;
                OnPropertyChanged(nameof(PlayListName));
            }
        }
        public ObservableCollection<Track> Track { get; set; }
        private string _PlayListName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
