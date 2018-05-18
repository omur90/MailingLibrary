using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailingLibrary;

namespace TestCase
{
    class Program
    {
        static void Main(string[] args)
        {
            MailingLibrary.MailOperation mailOperation = new MailOperation();
            var settingItem = mailOperation.ReadMailSetting();

            string result = mailOperation.UpdateMailSetting("a", "b", "c", 336, true);

            Console.WriteLine(result);
            Console.ReadLine();

        }
    }
}
