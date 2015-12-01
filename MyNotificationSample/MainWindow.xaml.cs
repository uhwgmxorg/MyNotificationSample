/******************************************************************************/
/*                                                                            */
/*   MyNotificationSample                                                     */
/*   WPF-Window in Notification Bar                                           */
/*                                                                            */
/*   01.12.2015 0.0.0 WA Start                                                */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNotificationSample
{
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon = null;

        /// <summary>
        /// Construcktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_ShowNotificationBalloon_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowNotificationBalloon_Click(object sender, RoutedEventArgs e)
        {
            ShowTrayBalloon();
        }

        /// <summary>
        /// Button_AddToNotificationBar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddToNotificationBar_Click(object sender, RoutedEventArgs e)
        {
            AddToTray();
        }

        /// <summary>
        /// Button_RemoveFromNotificationBar_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_RemoveFromNotificationBar_Click(object sender, RoutedEventArgs e)
        {
            RemoveFromTray();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        /// <summary>
        /// RadMenuItem_Click_Restore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadMenuItem_Click_Restore(object sender, RoutedEventArgs e)
        {
            Show();
            Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        /// <summary>
        /// RadMenuItem_Click_Minimize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadMenuItem_Click_Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
            Hide();
        }

        /// <summary>
        /// TheContextMenu_Click_Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TheContextMenu_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Window_Initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// NotifyIcon_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            ShowTrayMenu();
        }

        /// <summary>
        /// Window_StateChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            var w = sender as MainWindow;
            if (w.WindowState == WindowState.Minimized && _notifyIcon != null && _notifyIcon.Visible == true)
            {
                Hide();
                ShowTrayBalloon();
            }
        }

        /// <summary>
        /// Window_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveFromTray();
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// AddToTray
        /// </summary>
        private void AddToTray()
        {
            if (_notifyIcon == null)
            {
                _notifyIcon = new System.Windows.Forms.NotifyIcon();
                _notifyIcon.Icon = new System.Drawing.Icon(System.IO.Path.Combine(Environment.CurrentDirectory, @"NotificationIcon.ico"));
                _notifyIcon.Click += NotifyIcon_Click;
            }
            _notifyIcon.Visible = true;
            ShowInTaskbar = false;
        }

        /// <summary>
        /// RemoveFromTray
        /// </summary>
        private void RemoveFromTray()
        {
            if (_notifyIcon != null)
                _notifyIcon.Visible = false;
            ShowInTaskbar = true;
        }

        /// <summary>
        /// ShowTrayMenu
        /// </summary>
        private void ShowTrayMenu()
        {
            ContextMenu cm = this.FindResource("TheContextMenu") as ContextMenu;
            cm.IsOpen = true;
        }

        /// <summary>
        /// ShowTrayBalloon
        /// </summary>
        private void ShowTrayBalloon()
        {
            if (_notifyIcon != null && _notifyIcon.Visible == true)
            {
                _notifyIcon.BalloonTipText = "My Notification Sample";
                _notifyIcon.ShowBalloonTip(2000);
            }
            else
                Console.Beep();
        }

        #endregion
    }
}
