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

namespace KeyDancer
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        Color borderColor;

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
            borderColor = GetBorderColor();
            border.BorderBrush = new SolidColorBrush(borderColor);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.LightGray);
            border.BorderBrush = brush;
        }

        private void imgClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imgClose_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/" + "light_red.png"));
            imgClose.Source = image;
            this.Cursor = Cursors.Hand;
        }

        private void imgClose_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/" + "red.png"));
            imgClose.Source = image;
            this.Cursor = Cursors.Hand;
        }
    }
}
