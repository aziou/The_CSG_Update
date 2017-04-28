﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataCore;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace TheNewInterface
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = TheNewInterface.ViewModel.AllMeterInfo.CreateInstance();
           
        }
        Thread UpdateThread;
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        private void Btn_Config_Click(object sender, RoutedEventArgs e)
        {
            BasePage basepage = new BasePage();
            basepage.ShowDialog();
            ReLoadCheckTime();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            //if (cmb_WorkNumList.Text.Trim() == "")
            //{
            //    MessageBox.Show("请选择工单号！");
            //    return;
            //}
            //OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", cmb_WorkNumList.Text.ToString(), BaseConfigPath);
            OperateData.FunctionXml.UpdateElement("NewUser/CloumMIS/Item", "Name", "TheWorkNum", "Value", "07522300987", BaseConfigPath);

            int MeterCount = ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo.Count;
            List<string> UpDateMeterId=new List<string> ();
            for (int i = 0; i < MeterCount; i++)
            {
                if (ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup == true)
                {
                    UpDateMeterId.Add(ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].PK_LNG_METER_ID);
                }
            }
            this.UpdateProgress.Maximum = UpDateMeterId.Count;
            listBox_UpInfo.Items.Clear();
            UpDateInfomation upinfo = new UpDateInfomation();
            upinfo.Lis_PkId = UpDateMeterId;
            SoftType_G.csFunction s_function = new SoftType_G.csFunction();
            UpdateThread = new Thread(new ParameterizedThreadStart(UpdateToOracle));
            UpdateThread.Start(upinfo);
        }
        #region update
        public void UpdateToOracle(object o)
        {
            UpDateInfomation Lis_Id = o as UpDateInfomation;
            UpdateInfoThread(Lis_Id.Lis_PkId.Count, Lis_Id.Lis_PkId);
        }
        public void UpdateInfoThread(double countItem,List<string> lis_UP_ID)
        {
           
            double i = 0;
            int sleepTime = 800; ;
            double t;

            List<string> MeterUp_info = new List<string>();
            List<string> Seal_info = new List<string>();
            List<string> Demand_info = new List<string>();
            Mis_Interface_Driver.MisDriver cs_Function = null;
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                     cs_Function = new SoftType_G.csFunction();
                    break;
                case "CL3000S":
                     cs_Function = new SoftType_S.csFunction();
                    break;

            }
           // SoftType_G.csFunction cs_G_Function = new SoftType_G.csFunction();
    
            foreach (MeterBaseInfoFactor temp in ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo)
            {
                if (temp.BolIfup == true)
                {
                    t = i + 1;
                    i = t < countItem ? t : countItem;
                    MeterUp_info.Clear();

                    MeterUp_info.Add("第" + temp.LNG_BENCH_POINT_NO.ToString() + "表位" + cs_Function.UpadataBaseInfo(temp.PK_LNG_METER_ID, out Seal_info));

                    #region Add SEAL
                    foreach (string temp_id in Seal_info)
                    {
                        MeterUp_info.Add("添加铅封：" + temp_id + "成功");
                    }
                    #endregion

                    MeterUp_info.Add(cs_Function.UpdataErrorInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataJKRJSWCInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataJKXLWCJLInfo(temp.PK_LNG_METER_ID, out Demand_info));

                    #region Add demand
                    foreach (string temp_id in Demand_info)
                    {
                        MeterUp_info.Add(temp_id);
                    }
                    #endregion

                    MeterUp_info.Add(cs_Function.UpdataSDTQWCJLInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataDNBSSJLInfo(temp.PK_LNG_METER_ID));

                    MeterUp_info.Add(cs_Function.UpdataDNBZZJLInfo(temp.PK_LNG_METER_ID));

                    foreach (string temp_id in MeterUp_info)
                    {
                        listBox_UpInfo.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, double>(UpDateMeter), temp_id, i);
                        Thread.Sleep(sleepTime);
                    }

                }
              
                
                
            }
          
            MessageBox.Show("成功上传 :" + countItem + "个表");
            try
            {
                UpdateThread.Abort();
            }
            catch (Exception e)
            { }
            finally
            {

                this.listBox_UpInfo.Dispatcher.Invoke(new Action(() => {

                    this.listBox_UpInfo.UpdateLayout();
                    
                    this.listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count-1]);
                }));

            }
           

        }

        private void UpDateMeter(string Meter_update_info, double progressCount)
        {

           

           SoftType_G.csFunction cs_G_Function=new SoftType_G.csFunction();

            Stopwatch watch = new Stopwatch();
            watch.Start();


            //zcbhList.Add(temp.AVR_ASSET_NO);
            //listBox_UpInfo.Items.Add(cs_G_Function.UpadataBaseInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataErrorInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataJKRJSWCInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataJKXLWCJLInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataSDTQWCJLInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataDNBSSJLInfo(Meter_ID));

            //listBox_UpInfo.Items.Add(cs_G_Function.UpdataDNBZZJLInfo(Meter_ID));

           
           
                listBox_UpInfo.Items.Add(Meter_update_info);
         

            listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
                  
            this.UpdateProgress.Value = progressCount;
           // listBox_UpInfo.UpdateLayout();
            watch.Stop();
           // listBox_UpInfo.Items.Add("使用时间为：" + watch.ElapsedMilliseconds.ToString() + "毫秒");



        } 
        #endregion
        
        private void btn_download_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_deldteMis_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_MisConfig_Click(object sender, RoutedEventArgs e)
        {
            OracleConfig oracleConfig = new OracleConfig();
            oracleConfig.ShowDialog();
        }

        private void cmb_CheckTime_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_CheckTime.Items.Clear();
            string strSection = "NewUser/CloumMIS/Item";
            string datapath = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            csPublicMember.str_DataPath = datapath;
            csPublicMember.strSoftType = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            csPublicMember.showInfo_less =(bool)chk_ShowLess.IsChecked;
            #region 软件类型判断
            switch (csPublicMember.strSoftType)
            { 
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                   csPublicMember.strCondition  = "datJdrq";
                   csPublicMember.strTableName = "meterinfo";
                    break;
                case "CL3000S":
                    csPublicMember.strCondition = "DTM_TEST_DATE";
                    csPublicMember.strTableName = "METER_INFO";
                    break;

            }

            #endregion
            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
        }
        private void ReLoadCheckTime()
        {
            cmb_CheckTime.Items.Clear();
            string strSection = "NewUser/CloumMIS/Item";
            string datapath = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            csPublicMember.str_DataPath = datapath;
            csPublicMember.strSoftType = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            csPublicMember.showInfo_less = (bool)chk_ShowLess.IsChecked;
            #region 软件类型判断
            switch (csPublicMember.strSoftType)
            {
                case "CL3000G":
                case "CL3000F":
                case "CL3000DV80":
                    csPublicMember.strCondition = "datJdrq";
                    csPublicMember.strTableName = "meterinfo";
                    break;
                case "CL3000S":
                    csPublicMember.strCondition = "DTM_TEST_DATE";
                    csPublicMember.strTableName = "METER_INFO";
                    break;

            }

            #endregion
            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
   
        }
        /// <summary>
        /// 加载检定日期
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="Condition"></param>
        /// <param name="TableName"></param>
        private void LoadCheckTime(string dataPath, string Condition, string TableName,bool IsLess)
        {
            List<string> TimeList = new List<string>();
            OperateData.PublicFunction PbFunction = new OperateData.PublicFunction();
            string Less_SQL = IsLess == true ? " DISTINCT TOP 20" : " DISTINCT";
            if (dataPath.Trim() == "")
            {
                return;
            }
            else
            {
                string Sql = string.Format("select  {3} {0} from {1} order by {2} desc", Condition, TableName, Condition,Less_SQL);
                TimeList = PbFunction.ExcuteAccess(Sql, Condition);
            }
            try
            {
                cmb_CheckTime.Items.Clear();
                foreach (string temp in TimeList)
                {
                    cmb_CheckTime.Items.Add(temp);
                }
            }
            catch (Exception exAddCheckTime)
            {

            }
        }

        private void chk_ShowLess_Click(object sender, RoutedEventArgs e)
        {
            csPublicMember.showInfo_less = (bool)chk_ShowLess.IsChecked;

            LoadCheckTime(csPublicMember.str_DataPath, csPublicMember.strCondition, csPublicMember.strTableName, csPublicMember.showInfo_less);
        }

        private void cmb_CheckTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string CheckTime = cmb_CheckTime.SelectedValue.ToString();
            string Sql = string.Format("Select  * from {0} where {1} =#{2}#",csPublicMember.strTableName,csPublicMember.strCondition ,CheckTime);
            List<MeterBaseInfoFactor> tempBaseInfo = new List<MeterBaseInfoFactor>();
            ObservableCollection<MeterBaseInfoFactor> baseInfo = new ObservableCollection<MeterBaseInfoFactor>();
            OperateData.PublicFunction csFunction=new OperateData.PublicFunction ();
            baseInfo = csFunction.GetBaseInfo(CheckTime, Sql, csPublicMember.strSoftType);

            ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo = baseInfo;

         
        }

        private void chk_SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (chk_SelectAll.IsChecked == true)
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup = true;
                }
            }
            else
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolIfup = false;
                }
            }
        }

    

        private void chk_Terminal_Click(object sender, RoutedEventArgs e)
        {

         
            if (chk_SelectAll.IsChecked == true)
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolTerminalWorkNum = true;
                }
            }
            else
            {
                int count = dg_Info.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewModel.AllMeterInfo.CreateInstance().MeterBaseInfo[i].BolTerminalWorkNum = false;
                }
            }
        }

        private void btn_testFunction_Click(object sender, RoutedEventArgs e)
        {
            //listBox_UpInfo.UpdateLayout();
            listBox_UpInfo.ScrollIntoView(listBox_UpInfo.Items[listBox_UpInfo.Items.Count - 1]);
        }

        
    }

    public class UpDateInfomation
    {
        private List<string> lis_PkId;
        public List<string> Lis_PkId
        {
            get;
            set;
        }

    }
}
