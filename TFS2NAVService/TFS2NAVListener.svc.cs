using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Kine
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TFS2NAVListener : ITFS2NAVListener
    {
        public string ToNAV(Stream body)
        {
            string p = "";
            using (var reader = new StreamReader(body))
            {
                p = reader.ReadToEnd();
                reader.Close();
            }
            string result = "";
            result = PassDataToNAV(p);
            return (result);
        }

        string PassDataToNAV(string data)
        {
            var tfs2nav = new NAV.TFS2NAV();
            if (!String.IsNullOrEmpty(Kine.Properties.Settings.Default.WSUserName))
            {
                var cred = new CredentialCache();
                cred.Add(new Uri(tfs2nav.Url), Properties.Settings.Default.WSauthType, 
                    new NetworkCredential(
                        Properties.Settings.Default.WSUserName, 
                        Properties.Settings.Default.WSUserPwd,
                        (String.IsNullOrEmpty(Properties.Settings.Default.WSUserDomain)?"": Properties.Settings.Default.WSUserDomain)
                        ));
                tfs2nav.Credentials = cred;
            }
            else
            {
                tfs2nav.UseDefaultCredentials = true;
            }
            Debug.WriteLine("Sending:" + data);
            var result = tfs2nav.CallTFS2NAV(data);
            return (result);
        }
    }
    public class RawContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            if (contentType.Contains("text/json") || contentType.Contains("application/json"))
            {
                return WebContentFormat.Raw;
            }
            else
            {
                return WebContentFormat.Default;
            }
        }
    }
}
