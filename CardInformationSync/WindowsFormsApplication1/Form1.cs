using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApplication1.Base;
using WindowsFormsApplication1.Business;
using log4net;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly ILog _log = LogManager.GetLogger("MyLogger");
        private Config _config = new Config();

        public Form1()
        {
            InitializeComponent();
        }

        private void CardInfomation_timer_Tick(object sender, EventArgs e)
        {
            CardInfomation_timer.Enabled = false;
            Thread thread = new Thread()
            if (AllParmetersIsNotNull())
            {
                #region get card infomation and send it

                var card = new CardOperation(_config.DbConnectString);
                var dt = card.GetCardInfo();
                foreach (DataRow row in dt.Rows)
                {
                    card.SendParameters(_config.SyncPassword, row["oldcardid"].ToString(), row["newcardid"].ToString());
                }

                #endregion
            }
            CardInfomation_timer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region onload config

            //file path
            var configPath = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            if (File.Exists(configPath))
            {
                _config = XmlUtil.LoadFromXml<Config>(configPath);
                txtDbip.Text = _config.DbIp;
                txtDbid.Text = _config.DbName;
                txtDbname.Text = _config.DbUsername;
                txtDbpwd.Text = _config.DbPassWord;
                txtTime.Text = _config.SyncTime;
                txtSyncpwd.Text = _config.SyncPassword;

                //reset Interval 
                if (!string.IsNullOrEmpty(_config.SyncTime))
                {
                    CardInfomation_timer.Interval = int.Parse(_config.SyncTime)*1000;
                }
                else
                {
                    //default Interval is '1 min'
                    CardInfomation_timer.Interval = 1000;
                }
            }

            #endregion
        }

        /// <summary>
        ///     configuration and creat 'Config.xml' file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (AllParmetersIsNotNull())
                {
                    var config = new Config
                    {
                        DbIp = txtDbip.Text,
                        DbName = txtDbid.Text,
                        DbUsername = txtDbname.Text,
                        DbPassWord = txtDbpwd.Text,
                        SyncTime = txtTime.Text,
                        SyncPassword = txtSyncpwd.Text
                    };

                    var r = XmlUtil.SaveToXml(config, AppDomain.CurrentDomain.BaseDirectory + "config.xml");
                    if (r)
                    {
                        MessageBox.Show("保存成功");
                    }
                   
                }
                else
                {
                    MessageBox.Show("参数不能为空");
                }
            }
            catch (Exception exc)
            {
                _log.Error(exc.Message);
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        ///     judeg winform panel prameters is't have a null value
        /// </summary>
        /// <returns>true-notnull</returns>
        private bool AllParmetersIsNotNull()
        {
            //flage
            var isNotnull = !string.IsNullOrEmpty(txtDbid.Text);
            if (string.IsNullOrEmpty(txtDbip.Text))
                isNotnull = false;
            if (string.IsNullOrEmpty(txtDbname.Text))
                isNotnull = false;
            if (string.IsNullOrEmpty(txtDbpwd.Text))
                isNotnull = false;
            if (string.IsNullOrEmpty(txtSyncpwd.Text))
                isNotnull = false;
            if (string.IsNullOrEmpty(txtTime.Text))
                isNotnull = false;
            return isNotnull;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CardInfomation_timer.Enabled = true;
        }
    }
}