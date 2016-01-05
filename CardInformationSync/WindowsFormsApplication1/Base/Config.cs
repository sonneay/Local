using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WindowsFormsApplication1.Base
{
    [Serializable]
    public class Config
    {
        /// <summary>
        /// DB connectstring
        /// </summary>
        [XmlIgnoreAttribute]
        public string DbConnectString
        {
            get
            {
                return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", DbIp, DbName, DbUsername, DbPassWord);
            }
        }
        /// <summary>
        /// DataBaseIp
        /// </summary>
        public string DbIp { get; set; }
        /// <summary>
        /// DataBaseName
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// DataBaseId
        /// </summary>
        public string DbUsername { get; set; }
        /// <summary>
        /// DataBasePwd
        /// </summary>
        public string DbPassWord { get; set; }

        /// <summary>
        /// WebserviceIp
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// WebServicePort
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// how long time to sync (s)
        /// </summary>
        public string SyncTime { get; set; }

        /// <summary>
        ///Do synchronization with password
        /// </summary>
        public string SyncPassword { get; set; }
    }
}
