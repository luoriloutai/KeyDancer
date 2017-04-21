using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace KeyDancer
{
    /// <summary>
    /// 配置信息管理
    /// </summary>
    [Serializable]
    public class ConfigData
    {
        #region 常量

        public const float DEFAULT_OPACITY = 1;

        /// <summary>
        /// 默认敲击速度
        /// </summary>
        public const int DEFAULT_PLAY_SPEED = 127;

        /// <summary>
        /// 默认通道
        /// </summary>
        public const int DEFAULT_CHANNEL = 0;

        /// <summary>
        /// 默认设备
        /// </summary>
        public const int DEFAULT_DEVICE = 0;

        /// <summary>
        /// 默认乐器为大钢琴
        /// </summary>
        public const int DEFAULT_INSTRUMENT = 0;

        /// <summary>
        /// 默认乐器为非不间断发声乐器
        /// </summary>
        public const bool DEFAULT_ONGOING = false;

        /// <summary>
        /// 默认为C调
        /// </summary>
        public const NoteInfo.Mode DEFAULT_MODE = NoteInfo.Mode.C;

        #endregion


        private double opacity = DEFAULT_OPACITY;
        /// <summary>
        /// 默认不透明度
        /// </summary>
        public double Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        private NoteInfo.Mode mode = DEFAULT_MODE;
        /// <summary>
        /// 调式
        /// </summary>
        public NoteInfo.Mode Mode
        {
            get { return mode; }
            set { mode = value; }
        }


        private bool isOngoing = DEFAULT_ONGOING;
        /// <summary>
        /// 是否是不间断发声乐器，比如电音乐器，管弦乐器
        /// </summary>
        public bool IsOngoing
        {
            get { return isOngoing; }
            set { isOngoing = value; }
        }


        private int device = DEFAULT_DEVICE;
        /// <summary>
        /// 设备
        /// </summary>
        public int Device
        {
            get { return device; }
            set { device = value; }
        }

        private int channel = DEFAULT_CHANNEL;
        /// <summary>
        /// 通道
        /// </summary>
        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        
        private int instrument = DEFAULT_INSTRUMENT;
        /// <summary>
        /// 乐器
        /// </summary>
        public int Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        private int playSpeed = DEFAULT_PLAY_SPEED;
        /// <summary>
        /// 敲击速度，影响声音高低
        /// </summary>
        public int PlaySpeed
        {
            get { return playSpeed; }
            set { playSpeed = value; }
        }


        private List<MidiControler> controlers = new List<MidiControler>();
        /// <summary>
        /// MIDI控制器集合
        /// </summary>
        public List<MidiControler> Controlers
        {
            get { return controlers; }
            set { controlers = value; }
        }


        /// <summary>
        /// 初始化控制器
        /// </summary>
        private void InitialControlers()
        {
            controlers.Add(new MidiControler(MidiControler.Balance, 0));
            controlers.Add(new MidiControler(MidiControler.BalanceFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.BeginDuration, 0));
            controlers.Add(new MidiControler(MidiControler.BreathCoarseTuning, 0));
            controlers.Add(new MidiControler(MidiControler.BreathFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.ChorasEffectDepth, 0));
            controlers.Add(new MidiControler(MidiControler.CloseAllControler, 0));
            controlers.Add(new MidiControler(MidiControler.CloseAllSound, 0));
            controlers.Add(new MidiControler(MidiControler.DataDecreaseByDegress, 0));
            controlers.Add(new MidiControler(MidiControler.DataIncreaseByDegrees, 0));
            controlers.Add(new MidiControler(MidiControler.FXEffect_1_FineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.FXEffect_2_FineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.General_1, 0));
            controlers.Add(new MidiControler(MidiControler.General_2, 0));
            controlers.Add(new MidiControler(MidiControler.General_3, 0));
            controlers.Add(new MidiControler(MidiControler.General_4, 0));
            controlers.Add(new MidiControler(MidiControler.General_5, 0));
            controlers.Add(new MidiControler(MidiControler.General_6, 0));
            controlers.Add(new MidiControler(MidiControler.General_7, 0));
            controlers.Add(new MidiControler(MidiControler.General_8, 0));
            controlers.Add(new MidiControler(MidiControler.Glide, 0));
            controlers.Add(new MidiControler(MidiControler.GlideLiaison, 0));
            controlers.Add(new MidiControler(MidiControler.GlidePedal, 0));
            controlers.Add(new MidiControler(MidiControler.GlideSpeedCoarseTuning, 0));
            controlers.Add(new MidiControler(MidiControler.GlideSpeedFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.HighByteDataInput, 0));
            controlers.Add(new MidiControler(MidiControler.HighSound, 0));
            controlers.Add(new MidiControler(MidiControler.HoldVoice, 0));
            controlers.Add(new MidiControler(MidiControler.HoldVoicePedal, 0));
            controlers.Add(new MidiControler(MidiControler.HoldVoicePedal_2, 0));
            controlers.Add(new MidiControler(MidiControler.LowByteDataInput, 0));
            controlers.Add(new MidiControler(MidiControler.ModifiedTone, 0));
            controlers.Add(new MidiControler(MidiControler.MoodCoarseTuning, 127));
            controlers.Add(new MidiControler(MidiControler.MoodFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.OffBeatPedal, 0));
            controlers.Add(new MidiControler(MidiControler.PadalCoarseTuning, 0));
            controlers.Add(new MidiControler(MidiControler.PadalFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.PlayDuration, 0));
            controlers.Add(new MidiControler(MidiControler.RegisterdHighByteVale, 0));
            controlers.Add(new MidiControler(MidiControler.RegisterdLowByteValue, 0));
            controlers.Add(new MidiControler(MidiControler.ReverberationDepth, 0));
            controlers.Add(new MidiControler(MidiControler.SoketSelect, 0));
            controlers.Add(new MidiControler(MidiControler.Sound_1, 0));
            controlers.Add(new MidiControler(MidiControler.Sound_2, 0));
            controlers.Add(new MidiControler(MidiControler.Sound_3, 0));
            controlers.Add(new MidiControler(MidiControler.Sound_4, 0));
            controlers.Add(new MidiControler(MidiControler.Sound_5, 0));
            controlers.Add(new MidiControler(MidiControler.SoundImageAdjustCoarseTuning, 0));
            controlers.Add(new MidiControler(MidiControler.SoundImageAdjustFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.Tone, 0));
            controlers.Add(new MidiControler(MidiControler.ToneSelect, 0));
            controlers.Add(new MidiControler(MidiControler.TransitionDepth, 0));
            controlers.Add(new MidiControler(MidiControler.TrillDepthCoarseTuning, 0));
            controlers.Add(new MidiControler(MidiControler.TrillSpeedFineTuning, 0));
            controlers.Add(new MidiControler(MidiControler.UndefineEffectDepth_1, 0));
            controlers.Add(new MidiControler(MidiControler.UndefineEffectDepth_2, 0));
            controlers.Add(new MidiControler(MidiControler.UnregisteredHighByteValue, 0));
            controlers.Add(new MidiControler(MidiControler.UnregisteredLowByteValue, 0));
            controlers.Add(new MidiControler(MidiControler.VolumeCoarseTuning, 85));
            controlers.Add(new MidiControler(MidiControler.VolumeFineTuning, 0));
        }

        /// <summary>
        /// 根据编码查找MidiControler
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MidiControler FindControlerByCode(int code)
        {
            MidiControler mc = null;
            for (int i = 0; i < controlers.Count; i++)
            {
                if (code == controlers[i].Code)
                {
                    mc = controlers[i];
                    break;
                }
            }

            return mc;
        }

        /// <summary>
        /// 重置控制器设置
        /// </summary>
        /// <returns></returns>
        private void ResetControlers()
        {
            foreach (MidiControler mc in controlers)
            {
                mc.Value = 0;
            }

            MidiControler controler = FindControlerByCode(MidiControler.MoodCoarseTuning);
            controler.Value = 127;
            controler = FindControlerByCode(MidiControler.VolumeCoarseTuning);
            controler.Value = 85;
        }

        /// <summary>
        /// 重置对象
        /// </summary>
        public void Reset()
        {
            this.channel = DEFAULT_CHANNEL;
            this.device = DEFAULT_DEVICE;
            this.instrument = DEFAULT_INSTRUMENT;
            this.isOngoing = DEFAULT_ONGOING;
            this.mode = DEFAULT_MODE;
            this.playSpeed = DEFAULT_PLAY_SPEED;
            this.opacity = DEFAULT_OPACITY;
            ResetControlers();
        }

        /// <summary>
        /// 把配置保存到文件
        /// </summary>
        /// <param name="path"></param>
        private void Save(string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 保存，将对象信息序列化到文件中
        /// </summary>
        public void Save()
        {
            Save(Global.ConfigFile);
        }


        /// <summary>
        /// 构建一个ConfigData对象
        /// </summary>
        public ConfigData()
        {
            InitialControlers();
        }
    }
}
