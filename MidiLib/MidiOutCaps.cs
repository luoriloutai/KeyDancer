using System;
using System.Runtime.InteropServices;

namespace MidiLib
{
    /// <summary>
    /// MIDI输出设备信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MidiOutCaps
    {
        /// <summary>
        /// 厂商编号
        /// </summary>
        public short ManufacturerID;

        /// <summary>
        /// 产品编号 
        /// </summary>
        public short ProductID;

        /// <summary> 
        /// MIDI输出设备的驱动版本号
        /// </summary>
        public int DriverVersion;

        /// <summary>
        /// 产品名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string ProductName;

        /// <summary>
        /// MIDI输出设备类型描述标志
        /// </summary>
        public short Technology;

        /// <summary>
        /// 一个合成器支持的声音数量
        /// </summary>
        public short Voices;

        /// <summary>
        /// 一个内部合成器同时播放的最大音符数
        /// </summary>
        public short Notes;

        /// <summary>
        /// 合成器的通道数
        /// </summary>
        public short ChannelMask;

        /// <summary>
        /// 设备支持的可选功能
        /// </summary>
        public int Support;
    }

}
