using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
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
            var reader = new StreamReader(body);
            //var p = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
            var p = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            
            string result = PassDataToNAV(p);
            return (result);
        }

        string PassDataToNAV(string data)
        {
            var navPort = new NAV.TFS2NAV_PortClient();
            if (!String.IsNullOrEmpty(Kine.Properties.Settings.Default.WSUserName))
            {
                navPort.ClientCredentials.UserName.Password = Properties.Settings.Default.WSUserPwd;
                navPort.ClientCredentials.UserName.UserName = Properties.Settings.Default.WSUserName;
            } else
            {
                navPort.ClientCredentials.Windows.ClientCredential = (NetworkCredential)System.Net.CredentialCache.DefaultCredentials;
            }
            var result = navPort.TFS2NAV(data);
            return (result);
        }
    }
}
