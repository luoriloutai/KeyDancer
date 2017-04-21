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
    /// KeyDancerControlSet.xaml 的交互逻辑
    /// </summary>
    public partial class KeyDancerControlSet : UserControl
    {
        public KeyDancerControlSet()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 获取或设置标签的宽度
        /// </summary>
        public double LabelWidth
        {
            get { return Label.Width; }
            set
            {
                Label.Width = value;
            }
        }

        /// <summary>
        /// 设置或获取标签的高度
        /// </summary>
        public double LabelHeight
        {
            get { return Label.Height; }
            set { Label.Height = value; }
        }

        /// <summary>
        /// 获取或设置数据容器的宽度
        /// </summary>
        public double DataContainerWidth
        {
            get
            {
                return Container.Width;
            }

            set
            {
                Container.Width = value;
            }
        }

        /// <summary>
        /// 获取或设置数据容器的高度
        /// </summary>
        public double DataContainerHeight
        {
            get { return Container.Height; }
            set { Container.Height = value; }
        }

        /// <summary>
        /// 获取或设置标签文字
        /// </summary>
        public string LabelText
        {
            get { return Label.Content.ToString(); }
            set { Label.Content = value; }
        }

        /// <summary>
        /// 设置数据项文本
        /// </summary>
        public string DataText
        {
            get { return Container.Text; }
            set { Container.Text = value; }
        }

    }
}
