using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingLibrary
{
    public class MailSettingItem
    {
        public string FromMail { get; set; }
        public string FromMailPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }

    }
}
