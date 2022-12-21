using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Dublicates = System.Collections.Generic.List<FileExplorer.FileData>;

namespace FileExplorer
{
    internal class DublicateDetect
    {
        public enum SortBy
        {
            NAME,
            TIME,
            LENGTH
        }
        public static List<Dublicates> getDublicates(Dublicates directoryDatas, SortBy sortBy)
        {
            Dublicates temp = new Dublicates();
            Dublicates sorted = sort(directoryDatas, sortBy);
            List<Dublicates> dublicatesList = new List<Dublicates>();



            for (int i = 1; i < sorted.Count; i++)
            {
                temp.Add(sorted[i - 1]);
                
                if ( ! getField(sorted[i - 1], sortBy).Equals(getField(sorted[i], sortBy)))
                {
                    if(temp.Count > 1) dublicatesList.Add(new Dublicates(temp));                                            
                    temp.Clear();
                }
                else if(i == sorted.Count - 1)
                {
                    temp.Add(sorted.Last());
                    if (temp.Count > 1) dublicatesList.Add(new Dublicates(temp));
                    temp.Clear();
                }
            }         
            
            return dublicatesList;
        }

        private static dynamic getField(FileData data, SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.NAME:
                    return data.Name.Substring(0, data.Name.IndexOf('.'));
                case SortBy.TIME:
                    return data.CreationTime.ToString();
                case SortBy.LENGTH:
                    return data.Size;
                default:
                    return new object();
            }
        }
        private static Dublicates sort(Dublicates directoryDatas, SortBy sortBy)
        {

            switch(sortBy)
            {
                case SortBy.NAME:
                    return directoryDatas.OrderBy(d => d.Name.Substring(0, d.Name.IndexOf('.'))).ToList();
                case SortBy.TIME:
                    return directoryDatas.OrderBy(d => d.CreationTime).ToList();
                case SortBy.LENGTH:
                    return directoryDatas.OrderBy(d => d.Size).ToList();
                default:
                    return new Dublicates();
            }

        }


    }
}
