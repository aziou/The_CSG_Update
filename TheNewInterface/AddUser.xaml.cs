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
    /// AddUser.xaml 的交互逻辑
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            LoadUser();
        }
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string UserName = "", UserNumber = "",strSection = "NewUser/User/Item";;
            UserName = txt_username.Text.ToString();
            UserNumber = txt_userNumber.Text.ToString();

            OperateData.FunctionXml.UpdateElement(strSection, "UserName", UserName, "UserNumber", UserNumber, BaseConfigPath);

            LoadUser();
        }

        

        private void AddListToCombox(ComboBox cmb, List<string> addList)
        {
            cmb.Items.Clear();
            foreach (string temp in addList)
            {
                cmb.Items.Add(temp);
            }
        }

        private void LoadUser()
        {
            List<string> UserList = new List<string>();
            UserList = OperateData.FunctionXml.GetAllNodeData("NewUser/User", "Item", "UserName", BaseConfigPath);

            AddListToCombox(cmb_LoadUserName, UserList);
        }
        private void cmb_LoadUserName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_username.Text = cmb_LoadUserName.SelectedValue.ToString();
            txt_userNumber.Text = OperateData.FunctionXml.ReadElement("NewUser/User/Item", "UserName", cmb_LoadUserName.SelectedValue.ToString(), "UserNumber", "", BaseConfigPath);
        }
    }
}
