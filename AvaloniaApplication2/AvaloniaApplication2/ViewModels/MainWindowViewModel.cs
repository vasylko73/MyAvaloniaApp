using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication2.Models;
using HtmlAgilityPack;
using Microsoft.CodeAnalysis.Operations;

namespace AvaloniaApplication2.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        
        private string _URL { get; set; }
        public string Url{get => _URL; set=> _URL = value; }

        public PlayList Playlist
        {
            get => _PlayList;
            set
            {
                _PlayList = value;
                OnPropertyChanged(nameof(Playlist));
            }
        }

        private PlayList _PlayList;
        public MainWindowViewModel()
        {
            Playlist = new PlayList();
        }

        public void Parse(string URL)
        {
            string playlist=null;
            Dictionary<string, List<string>> DictionaryOfMusic = new Dictionary<string, List<string>>();
            HtmlParser parser = new HtmlParser();
            DictionaryOfMusic = parser.ParseHTML(Url, out playlist);
            foreach (var mu in DictionaryOfMusic)
            {
                string autors = null;
                string music = null;
                music = mu.Key.Replace("&#x27;", "'");

                autors = PrintList(mu.Value);
                autors.Replace("&#x27;", "'");

                Playlist.Track.Add(new Track() {Autors = autors, MusicName = music});
            }

            Playlist.PlayListName = playlist;
        }
        public string PrintList(List<string> myList)
        {
            string autors = null;

            foreach (var aut in myList)
            {
                autors += aut + ", ";
            }
            char[] TrimChar = { ',', ' ' };

            autors = autors.TrimEnd(TrimChar);

            return autors;
        }

        public void ParseClick()
        {
            Parse(Url);
           
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
