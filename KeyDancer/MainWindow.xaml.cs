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
using MidiLib;
using System.Timers;

namespace KeyDancer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            playMgr = new PlayManager(this);
        }

        /// <summary>
        /// 演奏控制器
        /// </summary>
        PlayManager playMgr;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            playMgr.PlaySound(e.Key);
            e.Handled = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            playMgr.CloseSound(e.Key);
            e.Handled = true;
        }

        // 点击空白处关闭菜单
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

            if (setMenu.Visibility == Visibility.Visible)
            {
                Point p = e.GetPosition(setMenu);
                if (setMenu.Width - p.X < 0 || setMenu.Width - p.X > setMenu.Width || setMenu.Height - p.Y < 0 || setMenu.Height - p.Y > setMenu.Height)
                {
                    setMenu.Visibility = Visibility.Hidden;
                }

            }
        }



        #region 关闭
        private void imgClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imgClose_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImageEnter(imgClose, "light_red.png");
        }

        private void imgClose_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImageLeave(imgClose, "red.png");
        }
        #endregion

        #region 最小化
        private void imgMinimum_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImageEnter(imgMinimum, "light_blue.png");
        }

        private void imgMinimum_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImageLeave(imgMinimum, "blue.png");
        }

        private void imgMinimum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region 设置

        private void imgSet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (setMenu.Visibility == Visibility)
            {
                setMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                setMenu.Margin = new Thickness(0, 0, 10, 0);
                setMenu.Visibility = Visibility.Visible;
            }
           
            e.Handled = true;
        }

        private void imgSet_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImageEnter(imgSet, "light_green.png");
        }

        private void imgSet_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImageLeave(imgSet, "green.png");
        }
        #endregion

        /// <summary>
        /// 鼠标移入区域
        /// </summary>
        /// <param name="img"></param>
        /// <param name="picName"></param>
        private void ChangeImageEnter(Image img, string picName)
        {
            BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/" + picName));
            img.Source = image;
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// 鼠标移出区域
        /// </summary>
        /// <param name="img"></param>
        /// <param name="picName"></param>
        private void ChangeImageLeave(Image img, string picName)
        {
            BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/" + picName));
            img.Source = image;
            this.Cursor = Cursors.Arrow;
        }

        #region 菜单

        private void HideMenu()
        {
            setMenu.Visibility = Visibility.Collapsed;
        }

        private void mSet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HideMenu();
            Configuration form = new Configuration(playMgr);
            form.Owner = this;
            form.ShowDialog();
        }

        private void mClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Opacity = playMgr.ConfigObject.Opacity;
            InstrumentManager im = new InstrumentManager();
            tbInstrument.Text = im.GetInstrumentNameByCode(playMgr.ConfigObject.Instrument);
            tbMode.Text = playMgr.GetModeString();
            this.Activate();
        }

        /// <summary>
        /// 获取边框颜色
        /// </summary>
        /// <returns></returns>
        public Color GetBorderColor()
        {
            Color c = new Color();
            c.A = 0xFF;
            c.R = 0x40;
            c.G = 0x85;
            c.B = 0xAD;
            return c;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            border.BorderBrush = new SolidColorBrush(GetBorderColor());
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            SolidColorBrush brush=new SolidColorBrush(Colors.LightGray);
            border.BorderBrush = brush;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            playMgr.CloseMidiDevice();
        }

        private void mAbout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HideMenu();
            About formAbout = new About();
            formAbout.Owner = this;
            formAbout.Show();
        }
       
    }
}
