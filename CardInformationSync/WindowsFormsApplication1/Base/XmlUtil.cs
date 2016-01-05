using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using log4net.Util;

namespace WindowsFormsApplication1.Base
{
    public static class XmlUtil
    {
        public static bool SaveToXml(object config,string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(config.GetType());
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        serializer.Serialize(sw, config);
                        return true;
                    }
                }      
            }
            catch (Exception)
            {
                throw;
            }
               
        }

        public static T LoadFromXml<T>(string path)
        {
         
                using (StreamReader sr =new StreamReader(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return  (T)serializer.Deserialize(sr);
                }
        
            
        }
    }
}
