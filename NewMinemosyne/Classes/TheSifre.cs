using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMinemosyne.Classes
{
    public class TheSifre : IBase
    {
        public Guid ID { get; set; }            // 73FD293C-7639-47E2-A86C-A2374CD664AC
        public string Title { get; set; }       // Facebook
        public string Username { get; set; }    // mmm@mmm.com
        public string Password { get; set; }    // 123456
        public string Description { get; set; } // www.facebook.com..

        public override string ToString()
        {
            return Title;
        }

        public string GetDataString()
        {
            string result = string.Empty;
            string format = "\t\t{0} : {1}\n";

            result += string.Format(format, "Başlık", Title);
            result += string.Format(format, "Kullanıcı Adı", Username);
            result += string.Format(format, "Şifre", Password);
            result += string.Format(format, "Açıklama", Description);

            result += "\n";

            return result;
        }
    }
}
