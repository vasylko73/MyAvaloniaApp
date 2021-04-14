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

namespace AvaloniaApplication2.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Music> Music { get; set; }
        private string _URL { get; set; }
        public string Url{get => _URL; set=> _URL = value; }
        
        public MainWindowViewModel()
        {
            Music = new ObservableCollection<Music>();
        }

        public void AddToDictionary(Dictionary<string, List<string>> DictionaryOfMusic, string key, string value)
        {
            if (DictionaryOfMusic.ContainsKey(key))
            {
                List<string> list = DictionaryOfMusic[key];
                if (list.Contains(value) == false)
                {
                    list.Add(value);
                }
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(value);
                DictionaryOfMusic.Add(key, list);
            }
        }

        public void ParseHTML(string path)
        {
            Dictionary<string, List<string>> DictionaryOfMusic = new Dictionary<string, List<string>>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(path);


            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@class='jsx-2309930405 SongListWrap']//div[@class='jsx-2980427943 jsx-1241753679 songDetail time']").ToArray();

            var playlist = document.DocumentNode.SelectSingleNode("//h1[@class='jsx-3737142392 title']").InnerText;


            for (int i = 0; i < nodes.Length; ++i)
            {
                nodes[i].ChildNodes[1].SelectNodes("//span[@class='jsx-819956409 SongDescItem']//a").ToArray();
                for (int j = 0; j < nodes[i].ChildNodes[1].ChildNodes.Count; ++j)
                   AddToDictionary(DictionaryOfMusic, nodes[i].ChildNodes[0].InnerText, nodes[i].ChildNodes[1].ChildNodes[j].InnerText);
            }

            
            foreach (var mu in DictionaryOfMusic)
            {
                string autors = null;
                string music = null;
                music = mu.Key.Replace("&#x27;", "'");
                
                autors = PrintList(mu.Value);
                autors.Replace("&#x27;", "'");
               
              Music.Add(new Music{PlayList = playlist,Autors = autors,MusicName = music});
            }


        }

        public void ParseClick()
        {
            
            ParseHTML(Url);
        }
        public string PrintList(List<string> myList)
        {
            string autors = null;

            foreach (var aut in myList)
            {
                autors += aut + " , ";
            }
            char[] TrimChar = {  ',', ' ' };

            autors = autors.TrimEnd(TrimChar);
           
            return autors;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
