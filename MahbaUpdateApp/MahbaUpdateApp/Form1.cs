using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahbaUpdateApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Model.NjitEntities db = new Model.NjitEntities();

        public static string GetMacAddress()
        {
            string hostName = System.Net.Dns.GetHostName(); // Retrive the Name of HOST  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
        
        private void DeleteAllFile()
        {
            string macaddress = GetMacAddress();
            string PathApp = db.VersionClients.Where(a => a.ClientIP == macaddress).OrderByDescending(a => a.ID).FirstOrDefault().MashinPath;

            System.IO.DirectoryInfo di = new DirectoryInfo(PathApp);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void UpdateApplication()
        {
            try
            {
                #region گرفتن فایل های جدید از سرور که فایل های آن درون سرور ریختیم
                string NewSourcePathApp = db.ProgramSettings.FirstOrDefault().AppUpdatePathExe;
                #endregion
                #region آدرس برنامه ورژن قدیمی که روی سیستم کاربر نصب است بر اساس ای پی سیستم میگیرد
                string macaddress = GetMacAddress();
                string PathApp = db.VersionClients.Where(a => a.ClientIP == macaddress).OrderByDescending(a => a.ID).FirstOrDefault().MashinPath;
                #endregion

                foreach (string dirPath in Directory.GetDirectories(NewSourcePathApp, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(NewSourcePathApp, PathApp));
                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(NewSourcePathApp, "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(NewSourcePathApp, PathApp), true);
                #region MyRegion

                //var versionApp = db.VesionApps.Where(a => a.OldVersion != a.NewVersion).FirstOrDefault();
                //versionApp.OldVersion = versionApp.NewVersion;
                //versionApp.CreateDate = DateTime.Now;
                //db.SaveChanges();
                //var Lastclient = db.VersionClients.Where(a => a.ClientIP == macaddress).OrderByDescending(a => a.ID).FirstOrDefault();
                //var List = db.VersionClients.Where(a => a.ClientIP == macaddress).ToList();
                //db.VersionClients.RemoveRange(List);
                //db.SaveChanges();

                //db.VersionClients.Add(new Model.VersionClient
                //{
                //    ClientIP = Lastclient.ClientIP,
                //    IsInstallUpdate = true,
                //    MashinPath = Lastclient.MashinPath,
                //    userId = Lastclient.userId
                //});
                //db.SaveChanges();
                #endregion
                #region اجرا شدن برنامه روی سیستم کاربر بعد از تمام شدن کپی فایل های جدید
                string MahbaApp = db.VersionClients.Where(a => a.ClientIP == macaddress).OrderByDescending(a => a.ID).FirstOrDefault().MashinPath + "Mahba.exe";
                System.Diagnostics.Process.Start(MahbaApp);
                #endregion

                Application.Exit();
            }
            catch (Exception ex)
            {
                var Result = MessageBox.Show(ex.Message, "خطا!!! لطفا متن خطا به پشتیبانی ارسال نمایید");
                Application.Exit();
            }

        }
        bool isStart = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuCircleProgressbar1.Value == 50)
            {
                if (isStart)
                {
                    isStart = false;
                    UpdateApplication();
                }
                bunifuCircleProgressbar1.Value += 1;
            }
            else if (bunifuCircleProgressbar1.Value < bunifuCircleProgressbar1.MaxValue)
            {
                bunifuCircleProgressbar1.Value += 1;
            }
            else
            {
                bunifuCircleProgressbar1.Value = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateApplication();
        }
    }
}
