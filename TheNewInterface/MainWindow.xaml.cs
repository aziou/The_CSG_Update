using System;
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
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        private void Btn_Config_Click(object sender, RoutedEventArgs e)
        {
            BasePage basepage = new BasePage();
            basepage.ShowDialog();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_download_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_deldteMis_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_MisConfig_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmb_CheckTime_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_CheckTime.Items.Clear();
            string strSection = "NewUser/CloumMIS/Item",Condition="",TableName="";
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
    }
}
