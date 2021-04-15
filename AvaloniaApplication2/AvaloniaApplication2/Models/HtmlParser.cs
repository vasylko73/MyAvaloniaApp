using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AvaloniaApplication2.Models
{
    class HtmlParser
    {
       
        public Dictionary<string, List<string>> ParseHTML(string path, out string PlayList )
        {
            Dictionary<string, List<string>> DictionaryOfMusic = new Dictionary<string, List<string>>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(path);


            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@class='jsx-2309930405 SongListWrap']//div[@class='jsx-2980427943 jsx-1241753679 songDetail time']").ToArray();

            var playlist = document.DocumentNode.SelectSingleNode("//h1[@class='jsx-3737142392 title']").InnerText;
            PlayList = playlist;

            for (int i = 0; i < nodes.Length; ++i)
            {
                nodes[i].ChildNodes[1].SelectNodes("//span[@class='jsx-819956409 SongDescItem']//a").ToArray();
                for (int j = 0; j < nodes[i].ChildNodes[1].ChildNodes.Count; ++j)
                    AddToDictionary(DictionaryOfMusic, nodes[i].ChildNodes[0].InnerText, nodes[i].ChildNodes[1].ChildNodes[j].InnerText);
            }

            return DictionaryOfMusic;

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

        


    }
}
