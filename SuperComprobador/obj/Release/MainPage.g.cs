﻿

#pragma checksum "D:\Mis documentos\Visual Studio 2015\Projects\SuperComprobador_PRO\SuperComprobador\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A2F44E097FA54DA509ED0AF704E8AC3C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuperComprobadorPRO
{
    partial class MainPage : global::SuperComprobadorPRO.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 11 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).LayoutUpdated += this.pageRoot_LayoutUpdated;
                 #line default
                 #line hidden
                #line 11 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).SizeChanged += this.pageRoot_SizeChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 92 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.GridBotones_ItemClick;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 84 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

