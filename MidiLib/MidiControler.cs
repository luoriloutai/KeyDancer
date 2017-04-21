using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiLib
{
    /// <summary>
    /// 控制器
    /// </summary>
    [Serializable]
    public class MidiControler
    {
        #region 常量

        /// <summary>
        /// 音色库选择MSB
        /// </summary>
        public const int ToneSelect = 0;

        /// <summary>
        /// 颤音深度（粗调）
        /// </summary>
        public const int TrillDepthCoarseTuning = 1;

        /// <summary>
        /// 呼吸（吹管）控制器（粗调）
        /// </summary>
        public const int BreathCoarseTuning = 2;

        /// <summary>
        /// 踏板控制器（粗调）
        /// </summary>
        public const int PadalCoarseTuning = 4;

        /// <summary>
        /// 连滑音速度（粗调）
        /// </summary>
        public const int GlideSpeedCoarseTuning = 5;

        /// <summary>
        /// 高位元组数据输入（Data Entry MSB）
        /// </summary>
        public const int HighByteDataInput = 6;

        /// <summary>
        /// 音量（粗调），控制音量的高低
        /// </summary>
        public const int VolumeCoarseTuning = 7;

        /// <summary>
        /// 平衡控制
        /// </summary>
        public const int Balance = 8;


        /// <summary>
        /// 声像调整（粗调）
        /// </summary>
        public const int SoundImageAdjustCoarseTuning = 10;


        /// <summary>
        /// 情绪控制器（粗调），音量与此控制器的值有关，越小声音越小
        /// </summary>
        public const int MoodCoarseTuning = 11;

        /// <summary>
        /// 一般控制器一
        /// </summary>
        public const int General_1 = 16;

        /// <summary>
        /// 一般控制器二
        /// </summary>
        public const int General_2 = 17;

        /// <summary>
        /// 一般控制器三
        /// </summary>
        public const int General_3 = 18;

        /// <summary>
        /// 一般控制器四
        /// </summary>
        public const int General_4 = 19;


        /// <summary>
        /// 插口选择
        /// </summary>
        public const int SoketSelect = 32;


        /// <summary>
        /// 颤音速度（微调）
        /// </summary>
        public const int TrillSpeedFineTuning = 33;


        /// <summary>
        /// 呼吸（吹管）控制器（微调）
        /// </summary>
        public const int BreathFineTuning = 34;

        /// <summary>
        /// 踏板控制器（微调）
        /// </summary>
        public const int PadalFineTuning = 36;

        /// <summary>
        /// 连滑音速度（微调）
        /// </summary>
        public const int GlideSpeedFineTuning = 37;

        /// <summary>
        /// 低位元组数据输入（Data Entry LSB）
        /// </summary>
        public const int LowByteDataInput = 38;

        /// <summary>
        /// 主音量（微调）
        /// </summary>
        public const int VolumeFineTuning = 39;

        /// <summary>
        /// 平衡控制（微调）
        /// </summary>
        public const int BalanceFineTuning = 40;

        /// <summary>
        /// 声像调整（微调）
        /// </summary>
        public const int SoundImageAdjustFineTuning = 42;

        /// <summary>
        /// 情绪控制器（微调）
        /// </summary>
        public const int MoodFineTuning = 43;

        /// <summary>
        /// 效果FX控制1（微调）
        /// </summary>
        public const int FXEffect_1_FineTuning = 44;

        /// <summary>
        /// 效果FX控制2（微调）
        /// </summary>
        public const int FXEffect_2_FineTuning = 45;

        /// <summary>
        /// 保持音踏板1（延音踏板）
        /// </summary>
        public const int HoldVoicePedal = 64;

        /// <summary>
        /// 滑音（在音头前加入上或下滑音做装饰音）
        /// </summary>
        public const int Glide = 65;


        /// <summary>
        /// 持续音
        /// </summary>
        public const int HoldVoice = 66;

        /// <summary>
        /// 弱音踏板
        /// </summary>
        public const int OffBeatPedal = 67;


        /// <summary>
        /// 连滑音踏板控制器
        /// </summary>
        public const int GlidePedal = 68;

        /// <summary>
        /// 保持音踏板2
        /// </summary>
        public const int HoldVoicePedal_2 = 69;


        /// <summary>
        /// 变调
        /// </summary>
        public const int ModifiedTone = 70;

        /// <summary>
        /// 音色（XG）
        /// </summary>
        public const int Tone = 71;

        /// <summary>
        /// 放音时值
        /// </summary>
        public const int PlayDuration = 72;

        /// <summary>
        /// 起音时值
        /// </summary>
        public const int BeginDuration = 73;

        /// <summary>
        /// 亮音（XG）
        /// </summary>
        public const int HighSound = 74;

        /// <summary>
        /// 声音控制
        /// </summary>
        public const int Sound_1 = 75;
        public const int Sound_2 = 76;
        public const int Sound_3 = 77;
        public const int Sound_4 = 78;
        public const int Sound_5 = 79;

        /// <summary>
        /// 一般控制器
        /// </summary>
        public const int General_5 = 80;
        public const int General_6 = 81;
        public const int General_7 = 82;
        public const int General_8 = 83;

        /// <summary>
        /// 连滑音控制
        /// </summary>
        public const int GlideLiaison = 84;

        /// <summary>
        /// 混响效果深度
        /// </summary>
        public const int ReverberationDepth = 91;


        /// <summary>
        /// 未定义的效果深度
        /// </summary>
        public const int UndefineEffectDepth_1 = 92;

        /// <summary>
        /// 合唱效果深度
        /// </summary>
        public const int ChorasEffectDepth = 93;


        /// <summary>
        /// 未定义的效果深度
        /// </summary>
        public const int UndefineEffectDepth_2 = 94;

        /// <summary>
        /// 移调器深度
        /// </summary>
        public const int TransitionDepth = 95;


        /// <summary>
        /// 数据累增
        /// </summary>
        public const int DataIncreaseByDegrees = 96;

        /// <summary>
        /// 数据递减
        /// </summary>
        public const int DataDecreaseByDegress = 97;

        /// <summary>
        /// 未登记的低元组数值（NRPN LSB）
        /// </summary>
        public const int UnregisteredLowByteValue = 98;

        /// <summary>
        /// 未登记的高元组数值（NRPN MSB）
        /// </summary>
        public const int UnregisteredHighByteValue = 99;

        /// <summary>
        /// 已登记的低元组数值（RPN LSB）
        /// </summary>
        public const int RegisterdLowByteValue = 100;

        /// <summary>
        /// 已登记的高元组数值（RPN MSB）
        /// </summary>
        public const int RegisterdHighByteVale = 101;

        /// <summary>
        /// 关闭所有声音
        /// </summary>
        public const int CloseAllSound = 120;

        /// <summary>
        /// 关闭所有控制器
        /// </summary>
        public const int CloseAllControler = 121;

        #endregion

        private int code;
        /// <summary>
        /// 编码
        /// </summary>
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        private int value;
        /// <summary>
        /// 值
        /// </summary>
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public MidiControler(int code, int value)
        {
            this.code = code;
            this.value = value;
        }
    }
}
