﻿#pragma checksum "..\..\..\BasePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7F09C235CD1733033474299D77517494"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TheNewInterface {
    
    
    /// <summary>
    /// BasePage
    /// </summary>
    public partial class BasePage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel DataConfig;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_DataPath;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_SetPath;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_SoftType;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Company;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_equipment;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Jyy;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_Jyy;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AddMember;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Hyy;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_Hyy;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\BasePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Save;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TheNewInterface;component/basepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\BasePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DataConfig = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.txt_DataPath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btn_SetPath = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\BasePage.xaml"
            this.btn_SetPath.Click += new System.Windows.RoutedEventHandler(this.btn_SetPath_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmb_SoftType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.cmb_Company = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.txt_equipment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.cmb_Jyy = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.txt_Jyy = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btn_AddMember = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.cmb_Hyy = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.txt_Hyy = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.btn_Save = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\BasePage.xaml"
            this.btn_Save.Click += new System.Windows.RoutedEventHandler(this.btn_Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

