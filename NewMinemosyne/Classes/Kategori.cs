using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMinemosyne.Classes
{
    public class Kategori : IBase
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TheSifre> TheSifres { get; set; }


        public Kategori()
        {
            TheSifres = new List<TheSifre>();
        }

        public override string ToString()
        {
            return Name;
        }

        public string GetDataString()
        {
            string result = string.Empty;
            string format = "\t{0} : {1}\n";

            result += string.Format(format, "Adı", Name);
            result += string.Format(format, "Açıklama", Description);

            result += "\n";

            foreach (TheSifre sifre in TheSifres)
            {
                result += sifre.GetDataString();
            }

            return result;
        }
    }
}
