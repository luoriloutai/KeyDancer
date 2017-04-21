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
using MidiLib;

namespace KeyDancer
{
    /// <summary>
    /// Configuration.xaml 的交互逻辑
    /// </summary>
    public partial class Configuration : Window
    {
        /// <summary>
        /// 乐器管理对象
        /// </summary>
        InstrumentManager instrumentMgr;

        /// <summary>
        /// 所有乐器信息
        /// </summary>
        InstrumentInfo[] allInstruments;

        /// <summary>
        /// 传入的参数
        /// </summary>
        PlayManager playmgr;

        public Configuration(PlayManager pm)
        {
            InitializeComponent();
            playmgr = pm;
            instrumentMgr = new InstrumentManager();
        }

        #region 填充下拉列表

        private void AddComboBoxItem(int start, int end, ComboBox cbx, string msg)
        {
            for (int i = start; i <= end; i++)
            {
                cbx.Items.Add(msg + i.ToString());
            }
        }

        private void InitialChannel()
        {
            AddComboBoxItem(1, 16, cbxChannel, "通道 ");
        }

        private void InitialDevice()
        {
            int dc = MidiManager.DeviceCount;
            AddComboBoxItem(0, dc - 1, cbxDevice, "设备 ");
        }

        private void InitialInstrument()
        {
            allInstruments = instrumentMgr.GetInstruments();
            for (int i = 0; i < 128; i++)
            {
                cbxInstrument.Items.Add(allInstruments[i].Name);
            }
        }

        #endregion


        /// <summary>
        /// 读取控制器配置
        /// </summary>
        /// <param name="code">控制器编码</param>
        /// <param name="control">控件</param>
        private void ReadControler(int code,KeyDancerControlSet control)
        {
            control.DataText = playmgr.ConfigObject.FindControlerByCode(code).Value.ToString();
        }

        /// <summary>
        /// 加载控制器设置
        /// </summary>
        private void LoadControler()
        {
            ReadControler(MidiControler.Balance, Balance);
            ReadControler(MidiControler.BalanceFineTuning, BalanceFineTuning);
            ReadControler(MidiControler.BeginDuration, BeginDuration);
            ReadControler(MidiControler.BreathCoarseTuning, BreathCoarsingTuning);
            ReadControler(MidiControler.BreathFineTuning, BreathFineTuning);
            ReadControler(MidiControler.ChorasEffectDepth, ChorasEffectDepth);
            ReadControler(MidiControler.CloseAllControler, CloseAllControler);
            ReadControler(MidiControler.CloseAllSound, CloseAllSound);
            ReadControler(MidiControler.DataDecreaseByDegress, DataDecreaseByDegress);
            ReadControler(MidiControler.DataIncreaseByDegrees, DataIncreaseByDegrees);
            ReadControler(MidiControler.FXEffect_1_FineTuning, FXEffect_1_FineTuning);
            ReadControler(MidiControler.FXEffect_2_FineTuning, FXEffect_2_FineTuning);
            ReadControler(MidiControler.General_1, General_1);
            ReadControler(MidiControler.General_2, General_2);
            ReadControler(MidiControler.General_3, General_3);
            ReadControler(MidiControler.General_4, General_4);
            ReadControler(MidiControler.General_5, General_5);
            ReadControler(MidiControler.General_6, General_6);
            ReadControler(MidiControler.General_7, General_7);
            ReadControler(MidiControler.General_8, General_8);
            ReadControler(MidiControler.Glide, Glide);
            ReadControler(MidiControler.GlideLiaison, GlideLiaison);
            ReadControler(MidiControler.GlidePedal, GlidePedal);
            ReadControler(MidiControler.GlideSpeedCoarseTuning, GlideSpeedCoarseTuning);
            ReadControler(MidiControler.GlideSpeedFineTuning, GlideSpeedFineTuning);
            ReadControler(MidiControler.HighByteDataInput, HighByteDataInput);
            ReadControler(MidiControler.HighSound, HighSound);
            ReadControler(MidiControler.HoldVoice, HoldVoice);
            ReadControler(MidiControler.HoldVoicePedal, HoldVoicePedal);
            ReadControler(MidiControler.HoldVoicePedal_2, HoldVoicePedal_2);
            ReadControler(MidiControler.LowByteDataInput, LowByteDataInput);
            ReadControler(MidiControler.ModifiedTone, ModifiedTone);
            ReadControler(MidiControler.MoodCoarseTuning, MoodCoarseTuning);
            ReadControler(MidiControler.MoodFineTuning, MoodFineTuning);
            ReadControler(MidiControler.OffBeatPedal, OffBeatPedal);
            ReadControler(MidiControler.PadalCoarseTuning, PadalCoarseTuning);
            ReadControler(MidiControler.PadalFineTuning, PadalFineTuning);
            ReadControler(MidiControler.PlayDuration, PlayDuration);
            ReadControler(MidiControler.RegisterdHighByteVale, RegisterdHighByteVale);
            ReadControler(MidiControler.RegisterdLowByteValue, RegisterdLowByteValue);
            ReadControler(MidiControler.ReverberationDepth, ReverberationDepth);
            ReadControler(MidiControler.SoketSelect, SoketSelect);
            ReadControler(MidiControler.Sound_1, Sound_1);
            ReadControler(MidiControler.Sound_2, Sound_2);
            ReadControler(MidiControler.Sound_3, Sound_3);
            ReadControler(MidiControler.Sound_4, Sound_4);
            ReadControler(MidiControler.Sound_5, Sound_5);
            ReadControler(MidiControler.SoundImageAdjustCoarseTuning, SoundImageAdjustCoarseTuning);
            ReadControler(MidiControler.SoundImageAdjustFineTuning, SoundImageAdjustFineTuning);
            ReadControler(MidiControler.Tone, Tone);
            ReadControler(MidiControler.ToneSelect, ToneSelect);
            ReadControler(MidiControler.TransitionDepth, TransitionDepth);
            ReadControler(MidiControler.TrillDepthCoarseTuning, TrillDepth);
            ReadControler(MidiControler.TrillSpeedFineTuning, TrillSpeedFineTuning);
            ReadControler(MidiControler.UndefineEffectDepth_1, UndefineEffectDepth_1);
            ReadControler(MidiControler.UndefineEffectDepth_2, UndefineEffectDepth_2);
            ReadControler(MidiControler.UnregisteredHighByteValue, UnregisteredHighByteValue);
            ReadControler(MidiControler.UnregisteredLowByteValue, UnregisteredLowByteValue);
            ReadControler(MidiControler.VolumeCoarseTuning, VolumeCoarseTuning);
            ReadControler(MidiControler.VolumeFineTuning, VolumeFineTuning);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialBaseInfo();

            ReadConfiguration();
        }

        /// <summary>
        /// 读取配置信息
        /// </summary>
        private void ReadConfiguration()
        {
            cbxDevice.Text = "设备 " + playmgr.ConfigObject.Device.ToString();
            cbxChannel.Text = "通道 " + (playmgr.ConfigObject.Channel + 1).ToString();
            cbxInstrument.Text = instrumentMgr.GetInstrumentNameByCode(playmgr.ConfigObject.Instrument);
            kdcbPlaySpeed.Text = playmgr.ConfigObject.PlaySpeed.ToString();
            cbStopPlay.IsChecked = playmgr.ConfigObject.IsOngoing;
            cbxMode.Text = playmgr.GetModeString();
            sliderOpacity.Value = playmgr.ConfigObject.Opacity;

            LoadControler();
        }

        /// <summary>
        /// 初始化基本信息
        /// </summary>
        private void InitialBaseInfo()
        {
            InitialChannel();
            InitialDevice();
            InitialInstrument();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            playmgr.AppWindow.Opacity = playmgr.ConfigObject.Opacity;
            this.Close();
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            MessageBoxResult result = MessageBox.Show("确定要重置配置信息吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                playmgr.ConfigObject.Reset();
                ReadConfiguration();
                playmgr.ConfigObject.Save();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 通过字符串获得调式对象
        /// </summary>
        /// <param name="modeString"></param>
        /// <returns></returns>
        private NoteInfo.Mode GetModeByString(string modeString)
        {
            NoteInfo.Mode m = NoteInfo.Mode.C;
            switch (modeString)
            {
                case "C":
                    m = NoteInfo.Mode.C;
                    break;
                case "D":
                    m = NoteInfo.Mode.D;
                    break;
                case "E":
                    m = NoteInfo.Mode.E;
                    break;
                case "F":
                    m = NoteInfo.Mode.F;
                    break;
                case "G":
                    m = NoteInfo.Mode.G;
                    break;
                case "A":
                    m = NoteInfo.Mode.A;
                    break;
                case "B":
                    m = NoteInfo.Mode.B;
                    break;
                case "#C":
                    m = NoteInfo.Mode.C_R;
                    break;
                case "#D":
                    m = NoteInfo.Mode.D_R;
                    break;
                case "#F":
                    m = NoteInfo.Mode.F_R;
                    break;
                case "#G":
                    m = NoteInfo.Mode.G_R;
                    break;
                case "#A":
                    m = NoteInfo.Mode.A_R;
                    break;
            }
            return m;
        }


        /// <summary>
        /// 设置控制器
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="ctrl">控件</param>
        private void SetControler(int code,KeyDancerControlSet ctrl)
        {
            playmgr.ConfigObject.FindControlerByCode(code).Value = Convert.ToInt32(ctrl.DataText);
        }

        /// <summary>
        /// 设置所有控制器
        /// </summary>
        private void SetControlers()
        {
            SetControler(MidiControler.Balance, Balance);
            SetControler(MidiControler.BalanceFineTuning, BalanceFineTuning);
            SetControler(MidiControler.BeginDuration, BeginDuration);
            SetControler(MidiControler.BreathCoarseTuning, BreathCoarsingTuning);
            SetControler(MidiControler.BreathFineTuning, BreathFineTuning);
            SetControler(MidiControler.ChorasEffectDepth, ChorasEffectDepth);
            SetControler(MidiControler.CloseAllControler, CloseAllControler);
            SetControler(MidiControler.CloseAllSound, CloseAllSound);
            SetControler(MidiControler.DataDecreaseByDegress, DataDecreaseByDegress);
            SetControler(MidiControler.DataIncreaseByDegrees, DataIncreaseByDegrees);
            SetControler(MidiControler.FXEffect_1_FineTuning, FXEffect_1_FineTuning);
            SetControler(MidiControler.FXEffect_2_FineTuning, FXEffect_2_FineTuning);
            SetControler(MidiControler.General_1, General_1);
            SetControler(MidiControler.General_2, General_2);
            SetControler(MidiControler.General_3, General_3);
            SetControler(MidiControler.General_4, General_4);
            SetControler(MidiControler.General_5, General_5);
            SetControler(MidiControler.General_6, General_6);
            SetControler(MidiControler.General_7, General_7);
            SetControler(MidiControler.General_8, General_8);
            SetControler(MidiControler.Glide, Glide);
            SetControler(MidiControler.GlideLiaison, GlideLiaison);
            SetControler(MidiControler.GlidePedal, GlidePedal);
            SetControler(MidiControler.GlideSpeedCoarseTuning, GlideSpeedCoarseTuning);
            SetControler(MidiControler.GlideSpeedFineTuning, GlideSpeedFineTuning);
            SetControler(MidiControler.HighByteDataInput, HighByteDataInput);
            SetControler(MidiControler.HighSound, HighSound);
            SetControler(MidiControler.HoldVoice, HoldVoice);
            SetControler(MidiControler.HoldVoicePedal, HoldVoicePedal);
            SetControler(MidiControler.HoldVoicePedal_2, HoldVoicePedal_2);
            SetControler(MidiControler.LowByteDataInput, LowByteDataInput);
            SetControler(MidiControler.ModifiedTone, ModifiedTone);
            SetControler(MidiControler.MoodCoarseTuning, MoodCoarseTuning);
            SetControler(MidiControler.MoodFineTuning, MoodFineTuning);
            SetControler(MidiControler.OffBeatPedal, OffBeatPedal);
            SetControler(MidiControler.PadalCoarseTuning, PadalCoarseTuning);
            SetControler(MidiControler.PadalFineTuning, PadalFineTuning);
            SetControler(MidiControler.PlayDuration, PlayDuration);
            SetControler(MidiControler.RegisterdHighByteVale, RegisterdHighByteVale);
            SetControler(MidiControler.RegisterdLowByteValue, RegisterdLowByteValue);
            SetControler(MidiControler.ReverberationDepth, ReverberationDepth);
            SetControler(MidiControler.SoketSelect, SoketSelect);
            SetControler(MidiControler.Sound_1, Sound_1);
            SetControler(MidiControler.Sound_2, Sound_2);
            SetControler(MidiControler.Sound_3, Sound_3);
            SetControler(MidiControler.Sound_4, Sound_4);
            SetControler(MidiControler.Sound_5, Sound_5);
            SetControler(MidiControler.SoundImageAdjustCoarseTuning, SoundImageAdjustCoarseTuning);
            SetControler(MidiControler.SoundImageAdjustFineTuning, SoundImageAdjustFineTuning);
            SetControler(MidiControler.Tone, Tone);
            SetControler(MidiControler.ToneSelect, ToneSelect);
            SetControler(MidiControler.TransitionDepth, TransitionDepth);
            SetControler(MidiControler.TrillDepthCoarseTuning, TrillDepth);
            SetControler(MidiControler.TrillSpeedFineTuning, TrillSpeedFineTuning);
            SetControler(MidiControler.UndefineEffectDepth_1, UndefineEffectDepth_1);
            SetControler(MidiControler.UndefineEffectDepth_2, UndefineEffectDepth_2);
            SetControler(MidiControler.UnregisteredHighByteValue, UnregisteredHighByteValue);
            SetControler(MidiControler.UnregisteredLowByteValue, UnregisteredLowByteValue);
            SetControler(MidiControler.VolumeCoarseTuning, VolumeCoarseTuning);
            SetControler(MidiControler.VolumeFineTuning, VolumeFineTuning);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //修改基本设置
            playmgr.ConfigObject.Channel = cbxChannel.SelectedIndex==-1?0:cbxChannel.SelectedIndex;
            playmgr.ConfigObject.Device = cbxDevice.SelectedIndex == -1 ? 0 : cbxDevice.SelectedIndex;
            playmgr.ConfigObject.Instrument = cbxInstrument.SelectedIndex==-1?0:cbxInstrument.SelectedIndex;
            playmgr.ConfigObject.IsOngoing = cbStopPlay.IsChecked.Value;
            playmgr.ConfigObject.Mode = GetModeByString(cbxMode.Text);
            //不透明度
            playmgr.ConfigObject.Opacity = sliderOpacity.Value;
            //修改控制器设置
            SetControlers();
            // 保存
            playmgr.ConfigObject.Save();
            // 重新加载配置信息
            playmgr.ReLoadConfig(playmgr.ConfigObject);

            //主窗体设置
            // 配置不透明度
            playmgr.AppWindow.Opacity = playmgr.ConfigObject.Opacity;
            // 当前使用乐器显示
            playmgr.AppWindow.tbInstrument.Text = instrumentMgr.GetInstrumentNameByCode(playmgr.ConfigObject.Instrument);
            // 当前调式显示
            playmgr.AppWindow.tbMode.Text = playmgr.GetModeString();
           
            this.Close();
        }


        // 改变不透明度时预览效果
        private void sliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (playmgr != null)
            {
                playmgr.AppWindow.Opacity = sliderOpacity.Value;
                this.Opacity = sliderOpacity.Value;
            }
        }

        Color borderColor;

        private void Window_Activated(object sender, EventArgs e)
        {
            borderColor = playmgr.AppWindow.GetBorderColor();
            this.border.BorderBrush = new SolidColorBrush(borderColor);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.border.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }



    }
}
