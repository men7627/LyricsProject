﻿#pragma checksum "..\..\LyricsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4DAC213E01F1E3F71FC3304053C29E9C3618DC6D"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Presto.SDK;
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


namespace Presto.SWCamp.Lyrics {
    
    
    /// <summary>
    /// LyricsWindow
    /// </summary>
    public partial class LyricsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 56 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textTitle;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush songImg;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button oneLine;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button allLine;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LyricPanel;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\LyricsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textLyrics;
        
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
            System.Uri resourceLocater = new System.Uri("/Presto.SWCamp.Lyrics;component/lyricswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LyricsWindow.xaml"
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
            
            #line 33 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.clickPlus);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 44 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.clickMinus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 59 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.clickInfo);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 70 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exitButton);
            
            #line default
            #line hidden
            return;
            case 6:
            this.songImg = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 7:
            this.oneLine = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\LyricsWindow.xaml"
            this.oneLine.Click += new System.Windows.RoutedEventHandler(this.oneLine_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 119 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.backClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 130 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.repeat);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 142 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 157 "..\..\LyricsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.frontClick);
            
            #line default
            #line hidden
            return;
            case 12:
            this.allLine = ((System.Windows.Controls.Button)(target));
            
            #line 168 "..\..\LyricsWindow.xaml"
            this.allLine.Click += new System.Windows.RoutedEventHandler(this.allLine_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.LyricPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 14:
            this.textLyrics = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

