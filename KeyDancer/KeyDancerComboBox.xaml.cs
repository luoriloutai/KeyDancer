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

namespace KeyDancer
{
    /// <summary>
    /// KeyDancerComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class KeyDancerComboBox : UserControl
    {
        public KeyDancerComboBox()
        {
            InitializeComponent();
            FillControl();
        }
        /// <summary>
        /// 虚方法，供继承者重写
        /// </summary>
        protected virtual void FillControl()
        {
            for (int i = 0; i < 128; i++)
            {
                ContentBox.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// 检查输入值是否有效
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="c">引发事件的控件</param>
        /// <param name="e">键盘事件参数</param>
        protected virtual void CheckValue(string input, ComboBox c, KeyEventArgs e)
        {
            int result = -1;
            if (e.Key != Key.Back)
            {
                if (!int.TryParse(input, out result))
                {
                    MessageBox.Show("请输入从0到127之间的数字！", "提示",MessageBoxButton.OK,MessageBoxImage.Error);
                    c.Focus();
                    return;
                }

                if (result < 0 || result > 127)
                {
                    MessageBox.Show("输入值超出允许范围，取值范围为0到127！", "提示",MessageBoxButton.OK,MessageBoxImage.Error);
                    c.Focus();
                    return;
                }
            }
        }

        private void ContentBox_KeyUp(object sender, KeyEventArgs e)
        {
            CheckValue(ContentBox.Text, ContentBox, e);
        }

        private void ContentBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ContentBox.Text == "")
            {
                ContentBox.Text = "0";
            }
        }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get
            {
                return ContentBox.Text;
            }
            set
            {
                ContentBox.Text = value;
            }
        }

    }
}
