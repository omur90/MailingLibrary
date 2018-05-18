using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MailingLibrary
{
    public class MailOperation
    {
        public string SendMail(string from, string mailTo, List<string> ccList, string mailSubject, string mailContent,string mailSettingFile)
        {
            var result = string.Empty;
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(mailTo));
                if (ccList !=null)
                {
                    foreach (var item in ccList)
                    {
                        message.CC.Add(new MailAddress(item));
                    }
                }
               

                message.From = new MailAddress(from);
                message.Subject = mailSubject;
                message.Body = mailContent;
                message.IsBodyHtml = true;


                var setMailSetting = ReadMailSetting(mailSettingFile);

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = setMailSetting.FromMail,
                        Password = setMailSetting.FromMailPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = setMailSetting.SmtpHost;
                    smtp.Port = setMailSetting.SmtpPort;
                    smtp.EnableSsl = setMailSetting.EnableSsl;
                    smtp.Send(message);

                }
                result = "Mail gönderimi başarıyla tamamlandı";

            }
            catch (Exception ex)
            {
                result = "Mail gönderimi sırasında bir hata oluştu. Hata :" + ex.Message;
            }

            return result;
        }


        public MailSettingItem ReadMailSetting(string mailSettingFile)
        {
            string json = File.ReadAllText(mailSettingFile);
            //var json = );
            //string json = Path.Combine(Environment.CurrentDirectory, @"\", "mailSetting.json");
            MailSettingItem mailSetting = JsonConvert.DeserializeObject<MailSettingItem>(json);
            return mailSetting;
        }

        public string UpdateMailSetting(string fromMail, string fromMailPassword, string smtpHost, int smptPort,bool enableSsl)
        {
            string result;
            try
            {
                string json = File.ReadAllText(@"d:\mailSetting.json");
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                if (!string.IsNullOrWhiteSpace(fromMail))
                {
                    jsonObj["FromMail"]= fromMail;
                }
                if (!string.IsNullOrWhiteSpace(fromMailPassword))
                {
                    jsonObj["FromMailPassword"] = fromMailPassword;
                }
                if (!string.IsNullOrWhiteSpace(enableSsl.ToString()))
                {
                    jsonObj["EnableSsl"] = enableSsl;

                }
                if (!string.IsNullOrWhiteSpace(smtpHost))
                {
                    jsonObj["SmtpHost"] = smtpHost;
                }
                if (smptPort > 0 )
                {
                    jsonObj["SmtpPort"] = smptPort;
                }


                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"d:\mailSetting.json", output);
                result = "Mail ayarları başarılı bir şekilde güncellenmiştir.";
            }
            catch (Exception ex)
            {
                result = "Mail ayarları güncellemesi sırasında bir hata oluştu. Hata :" + ex.Message;
            }
            return result;
        }
    }
}
