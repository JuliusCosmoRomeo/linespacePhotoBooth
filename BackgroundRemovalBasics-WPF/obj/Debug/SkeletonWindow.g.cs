﻿#pragma checksum "..\..\SkeletonWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9F2CBBAC1D0369804058E2BFDB26D146"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace Microsoft.Samples.Kinect.BackgroundRemovalBasics {
    
    
    /// <summary>
    /// SkeletonWindow
    /// </summary>
    public partial class SkeletonWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid layoutGrid;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxSeatedMode;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.StatusBar statusBar;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock statusBarText;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\SkeletonWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonChangeWindow;
        
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
            System.Uri resourceLocater = new System.Uri("/BackgroundRemovalBasics-WPF;component/skeletonwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SkeletonWindow.xaml"
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
            
            #line 4 "..\..\SkeletonWindow.xaml"
            ((Microsoft.Samples.Kinect.BackgroundRemovalBasics.SkeletonWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.WindowLoaded);
            
            #line default
            #line hidden
            
            #line 4 "..\..\SkeletonWindow.xaml"
            ((Microsoft.Samples.Kinect.BackgroundRemovalBasics.SkeletonWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.WindowClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.layoutGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Image = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.checkBoxSeatedMode = ((System.Windows.Controls.CheckBox)(target));
            
            #line 64 "..\..\SkeletonWindow.xaml"
            this.checkBoxSeatedMode.Checked += new System.Windows.RoutedEventHandler(this.CheckBoxSeatedModeChanged);
            
            #line default
            #line hidden
            
            #line 64 "..\..\SkeletonWindow.xaml"
            this.checkBoxSeatedMode.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxSeatedModeChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.statusBar = ((System.Windows.Controls.Primitives.StatusBar)(target));
            return;
            case 6:
            this.statusBarText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.buttonChangeWindow = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\SkeletonWindow.xaml"
            this.buttonChangeWindow.Click += new System.Windows.RoutedEventHandler(this.ButtonOpenBackgroundRemoval);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 71 "..\..\SkeletonWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

