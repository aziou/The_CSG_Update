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
using System.Windows.Shapes;

namespace TheNewInterface
{
    /// <summary>
    /// BasePage.xaml 的交互逻辑
    /// </summary>
    public partial class BasePage : Window
    {
        public BasePage()
        {
            InitializeComponent();
            LoadBaseConfig();
        }
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";


        #region 数据加载初始化
        private void LoadBaseConfig()
        {

            #region Combox数据加载
            //软件类型
            cmb_SoftType.Items.Add("CL3000G");
            cmb_SoftType.Items.Add("CL3000S");
            cmb_SoftType.Items.Add("CL3000F");
            cmb_SoftType.Items.Add("CL3000DV80");
            //地区编号

            #endregion
            string strSection = "NewUser/CloumMIS/Item";
            txt_DataPath.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_DataPath", "Value", "", BaseConfigPath);
            txt_equipment.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_equipment", "Value", "", BaseConfigPath);
            txt_Jyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_Jyy", "Value", "", BaseConfigPath);
            txt_Hyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "txt_Hyy", "Value", "", BaseConfigPath);
            cmb_SoftType.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_SoftType", "Value", "", BaseConfigPath);
            cmb_Company.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Company", "Value", "", BaseConfigPath);
            cmb_Hyy.Text = OperateData.FunctionXml.ReadElement(strSection, "Name", "cmb_Hyy", "Value", "", BaseConfigPath);
        }

        #endregion
        /// <summary>
        /// 利用visualtreehelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }
        private void btn_SetPath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog =

                  new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "数据库文件|*.mdb;*.accdb";

            if (dialog.ShowDialog() == true)
            {
                txt_DataPath .Text= dialog.FileName;
     

            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string control_name;
            string control_content;
            List<TextBox> txtList = FindVisualChild<TextBox>(DataConfig);
            List<ComboBox> ComboxList = FindVisualChild<ComboBox>(DataConfig);
            string strSection = "NewUser/CloumMIS/Item";
            foreach (TextBox v in txtList)
            {

                control_content = ((TextBox)v).Text;
                control_name = ((TextBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection,"Name", control_name,"Value", control_content, BaseConfigPath);


            }
            foreach (ComboBox v in ComboxList)
            {

                control_content = ((ComboBox)v).Text;
                control_name = ((ComboBox)v).Name;

                OperateData.FunctionXml.UpdateElement(strSection, "Name", control_name, "Value", control_content, BaseConfigPath);


            }
            this.Close();
        }

  
    }
}
