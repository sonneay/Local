using System;
using System.Data;
using WindowsFormsApplication1.Base;
using WindowsFormsApplication1.UpdateCard;
using log4net;

namespace WindowsFormsApplication1.Business
{
    internal class CardOperation
    {
        private readonly ILog _log;

        public CardOperation(string connectString)
        {
            ConnectString = connectString;
        }

        public string ConnectString { get; set; }

        /// <summary>
        ///     get oldcardid and newcardid for database
        /// </summary>
        /// <param name="connString">db connect string</param>
        /// <returns></returns>
        public DataTable GetCardInfo()
        {
            try
            {
 const string sql = "select * form T_CardReissue where sync = 0";
            return SqlOperation.RunTable(sql, ConnectString);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        /// <summary>
        ///     send parmeters to webservice and return result
        /// </summary>
        /// <param name="password"></param>
        /// <param name="oldcard"></param>
        /// <param name="newcard"></param>
        public void SendParameters(string password, string oldcard, string newcard)
        {
            try
            {
 var card = new ServiceSoapClient();
            var result = card.UpdateCard(password, oldcard, newcard);
            switch (result.State)
            {
                case 1:
                    _log.Info(string.Format("UpDateCardId Success:{0}", result.Data));
                    break;
                case 0:
                    _log.Info(string.Format("UpDateCardId Fail:{0}", result.Data));
                    break;
            }
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public void SetSyncStatus()
        {
            try
            {
                var sql = "T_CardReissue set Sync = 1";
                var count = SqlOperation.ExcuteSql(sql, ConnectString);
                if (count > 0)
                {
                    _log.Info(string.Format("SetSyncStatus Success:effect {0}", count));
                }
                else
                {
                    _log.Error("SetSyncStatus Fail:no data");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}