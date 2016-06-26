using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMinemosyne.Classes
{
    public class KategoriContainer: IBase
    {
        public string Title { get; set; }

        public List<Kategori> Kategoriler { get; set; }

        public KategoriContainer()
        {
            Kategoriler = new List<Kategori>();
        }

        public override string ToString()
        {
            return Title;
        }

        public string GetDataString()
        {
            string result = Title + "\n\n";

            foreach (Kategori kategori in Kategoriler)
            {
                result += kategori.GetDataString();
            }

            return result;
        }
    }
}
