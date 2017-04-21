using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace MidiLib
{
    public class MidiManager
    {
        /*
         * 播放MIDI的步骤：
         * 1.检测系统是否支持MIDI播放。调用midiOutGetNumDevs()，获得MIDI播放设备数量，值为0表示没有MIDI输出设备
         * 2.获取MIDI音频输出设备的说明信息。调用midiOutGetDevCaps(),结构体被填充，该结构体就包含了输出设备的信息。
         * 3.打开MIDI输出设备，获得设备句柄。调用midiOutOpen()。
         * 4.生成MIDI音频信息头结构。调用midiOutPrepareHeader()。
         * 5.向MIDI音频输出设备发送MIDI消息。调用midiOutShortMsg()或midiOutLongMsg()。
         * 6.清除数据块信息，释放资源。调用midiOutUnprepareHeader()。
         * 7.关闭MIDI输出设备。调用midiOutClose()。
         * 
         * MIDI设备接收输入消息（命令），然后响应这些消息。这些消息以十六进制表示，MIDI将其按字节取出分析，响应相应的功能。低字节位是状态码，在MIDI中有定义，设备根据此值来确定要响应用户什么功能；次低字节位是相应的值；高字节位有可能无值（不用设置），如改变音色高字节位就无值。举例：0x12 3E 9n，拆开来看，n为通道，取值为0-15（0-0xF）,9为状态码，MIDI设备接收到这个状态码后就播放音符，3E表示音符编码，最大值127（十六进制0x7F）,12表示击打（吹奏）这种乐器的速度，最大值为127，声音高低与此有关。实际操作中把这些信息值按位或即可得到MIDI需要的消息。
         * 
         * 几个状态码说明（以0通道为例）：
         * 0xC0：设置音色（乐器），0x12C0表示将第一个通道的乐器设置为0x12
         * 0x90：播放音符，0x123E90表示用第一个通道播放0x3E这个代码表示的音符，速度是0x12（十进制18）
         * 0xB0：设置控制器，调整音乐的各种属性。0x121AB0，设置编号为0x1A的控制器的值为0x12，控制器最大为121
         * 
         * */


        #region WINAPI

        /// <summary>
        /// 生成音频信息头结构
        /// </summary>
        /// <param name="handle">设备句柄</param>
        /// <param name="headerPtr">MIDIHDR结构的指针，指向MIDI音频信息的头数据块。</param>
        /// <param name="sizeOfMidiHeader"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutPrepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// 播放结束后，清除已准备好的MIDI音频数据块结构MIDIHDR，释放分配的资源
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="headerPtr"></param>
        /// <param name="sizeOfMidiHeader"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutUnprepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// 向MIDI输出设备发送消息
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="headerPtr"></param>
        /// <param name="sizeOfMidiHeader"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutLongMsg(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// 获取MIDI输出设备的说明信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="caps">一个结构，用于说明MIDI输出设备</param>
        /// <param name="sizeOfMidiOutCaps"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutGetDevCaps(int deviceID,
            ref MidiOutCaps caps, int sizeOfMidiOutCaps);

        /// <summary>
        /// 获取MIDI输出设备数量
        /// 可用于检测系统是否支持MIDI
        /// </summary>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutGetNumDevs();


        /// <summary>
        /// 重置midi输出设备
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutReset(int handle);

        /// <summary>
        /// 向输出端口发送信息
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutShortMsg(int handle, int message);
        /// <summary>
        /// 打开midi输出设备
        /// </summary>
        /// <param name="handle">设备句柄</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="proc">回调函数</param>
        /// <param name="instance">传递给回调机制的用户实例</param>
        /// <param name="flags">打开设备的标志</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutOpen(ref int handle, int deviceID,
            MidiOutProc proc, int instance, int flags);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutClose(int handle);


        #endregion WINAPI

        protected delegate void MidiOutProc(int handle, int msg, int instance, int param1, int param2);

        private MidiOutProc midiOutProc;

        protected const int CALLBACK_FUNCTION = 196608;

        /// <summary>
        /// 默认敲击速度
        /// </summary>
        protected const int DEFAULT_SPEED = 127;

        /// <summary>
        /// 默认通道
        /// </summary>
        protected const int DEFAULT_CHANNEL = 0;

        /// <summary>
        /// 默认设备
        /// </summary>
        protected const int DEFAULT_DEVICE = 0;

        protected int hndle = 0;
        public int Handle
        {
            get
            {
                return hndle;
            }
        }
        protected virtual void HandleMessage(int handle, int msg, int instance, int param1, int param2)
        {
        }


        private int channel = DEFAULT_CHANNEL;

        /// <summary>
        /// 获取或设置要使用的通道，默认为0，即使用第一个通道。取值范围0-15。
        /// </summary>
        public int Channel
        {
            get { return channel; }
            set
            {
                if (value < 0 || value > 15)
                {
                    value = DEFAULT_CHANNEL;
                }
                else
                {
                    channel = value;
                }
            }
        }


        private int playSpeed = DEFAULT_SPEED;

        /// <summary>
        /// 获取或设置敲击速度，值范围0-127，该值影响声音的高度，值越大，声音越高
        /// </summary>
        public int PlaySpeed
        {
            get { return playSpeed; }
            set
            {
                if (value < 0 || value > 127)
                {
                    playSpeed = DEFAULT_SPEED;
                }
                else
                {
                    playSpeed = value;
                }
            }
        }

        /// <summary>
        /// 默认乐器
        /// </summary>
        protected const int DEFAULT_INSTRUMENT = 0;

        private int instrument = DEFAULT_INSTRUMENT;

        /// <summary>
        /// 乐器
        /// </summary>
        public int Instrument
        {
            get { return instrument; }
            set
            {
                if (value < 0 || value > 127)
                {
                    instrument = DEFAULT_INSTRUMENT;
                }
                else
                {
                    instrument = value;
                }

            }
        }

        private int deviceID;

        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        private NoteInfo.Mode mode;
        /// <summary>
        /// 调式
        /// </summary>
        public NoteInfo.Mode Mode
        {
            get { return mode; }
            set { mode = value; }
        }


        private List<MidiControler> controlers = new List<MidiControler>();
        /// <summary>
        /// 控制器集合
        /// </summary>
        public List<MidiControler> Controlers
        {
            get { return controlers; }
            set { controlers = value; }
        }


        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="deviceID">设备号</param>
        /// <param name="channel">通道</param>
        /// <param name="instrument">乐器</param>
        /// <param name="playSpeed">敲击速度</param>
        /// <param name="mode">调式</param>
        /// <param name="controlers">控制器集合</param>
        public MidiManager(int deviceID,int channel,int instrument,int playSpeed,NoteInfo.Mode mode,List<MidiControler> controlers)
        {
            if (DeviceCount == 0)
            {
                throw new Exception("未检测到MIDI设备，您的硬件可能不支持！");
            }
            midiOutProc = HandleMessage;
            int result = midiOutOpen(ref hndle, deviceID, midiOutProc, 0, CALLBACK_FUNCTION);
            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                Reset();
                throw new OutputDeviceException(result);
            }

            this.deviceID = deviceID;
            this.channel = channel;
            this.instrument = instrument;
            this.playSpeed = playSpeed;
            this.mode = mode;
            this.controlers = controlers;


            SetMidiEffect();
        }

        /// <summary>
        /// 设置MIDI效果
        /// </summary>
        public void SetMidiEffect()
        {
            // 设置乐器
            ChangeProgram(instrument);

            //控制器选项
            SetControler();
        }

        /// <summary>
        /// 设置控制器
        /// </summary>
        private void SetControler()
        {
            foreach (MidiControler mc in controlers)
            {
                ChangeControl(mc.Code, mc.Value);
            }
        }


        /// <summary>
        /// 使用默认设备构建一个对象
        /// </summary>
        public MidiManager()
        {
            midiOutProc = HandleMessage;

            if (DeviceCount == 0)
            {
                throw new Exception("未检测到MIDI设备，您的硬件可能不支持！");
            }

            //默认使用第一个设备
            int result = midiOutOpen(ref hndle, DEFAULT_DEVICE, midiOutProc, 0, CALLBACK_FUNCTION);
            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                Reset();
                throw new OutputDeviceException(result);
            }

            ChangeProgram(instrument);
        }

        /// <summary>
        /// 重置MIDI设备
        /// </summary>
        public void Reset()
        {
            // 重置
            int result = midiOutReset(Handle);

            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                throw new OutputDeviceException(result);
            }
        }

        /// <summary>
        /// 关闭MIDI设备
        /// </summary>
        public void Close()
        {
            Reset();
            int result = midiOutClose(Handle);

            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                throw new OutputDeviceException(result);
            }
        }

        /// <summary>
        /// 发送消息到MIDI设备
        /// </summary>
        /// <param name="message"></param>
        public void Send(int message)
        {
            int result = midiOutShortMsg(Handle, message);

            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                throw new OutputDeviceException(result);
            }
        }

        /// <summary>
        /// 发送消息到MIDI设备
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="channel">通道</param>
        /// <param name="keyCode">键号</param>
        /// <param name="speed">敲击速度</param>
        public void Send(int status, int channel, int keyCode, int speed)
        {
            int result = midiOutShortMsg(hndle, status | channel | (keyCode << 8) | (speed << 16));
            if (result != MidiDeviceState.MMSYSERR_NOERROR)
            {
                throw new OutputDeviceException(result);
            }
        }



        /// <summary>
        /// 获取设备数量
        /// </summary>
        public static int DeviceCount
        {
            get
            {
                return midiOutGetNumDevs();
            }
        }

        

        /// <summary>
        /// 取得所有Midi设备信息
        /// </summary>
        /// <returns></returns>
        public MidiOutCaps[] GetMidiDevice()
        {
            MidiOutCaps[] caps = new MidiOutCaps[DeviceCount];
            for (int i = 0; i < DeviceCount; i++)
            {
                midiOutGetDevCaps(i, ref caps[i], Marshal.SizeOf(caps[i]));
            }

            return caps;
        }

        /// <summary>
        /// 播放音符
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="keyCode">键号</param>
        /// <param name="speed">敲击速度</param>
        public void NoteOn(int channel, int keyCode, int speed)
        {
            Send(0x90, channel, keyCode, speed);
        }

        /// <summary>
        /// 播放音符
        /// </summary>
        /// <param name="keyCode">键号</param>
        public void NoteOn(int keyCode)
        {
            NoteOn(channel, keyCode, playSpeed);
        }

        /// <summary>
        /// 关闭正在播放的音符
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="keyCode">键号</param>
        public void NoteOff(int channel, int keyCode)
        {
            Send(0x90, channel, keyCode, 0);
        }

        /// <summary>
        /// 关闭正在播放的音符
        /// </summary>
        /// <param name="keyCode">键号（音符）</param>
        public void NoteOff(int keyCode)
        {
            NoteOff(channel, keyCode);
        }

        /// <summary>
        /// 改变音色（乐器），设置乐器
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="instrumentCode">乐器编码</param>
        public void ChangeProgram(int channel, int instrumentCode)
        {
            if (channel > 15 || channel < 0 || instrumentCode > 127 || instrumentCode < 0)
            {
                throw new Exception("传入参数有无效值！channel从0到15，instrumentCode从0到127。");
            }
            Send(0xC0, channel, instrumentCode, 0);
        }

        /// <summary>
        /// 改变音色（乐器），设置乐器
        /// </summary>
        /// <param name="instrumentCode">乐器编码</param>
        public void ChangeProgram(int instrumentCode)
        {
            ChangeProgram(channel, instrumentCode);
        }

        /// <summary>
        /// 改变控制器，设置控制器
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="controler">控制器，取值0-121，可用MidiControler类提供的控制器</param>
        /// <param name="value">值</param>
        public void ChangeControl(int channel, int controler, int value)
        {
            if (channel > 15 || channel < 0 || controler > 121 || controler < 0)
            {
                throw new Exception("传入参数有无效的值！channel范围从0到15，controler范围从0到121。");
            }
            Send(0xB0, channel, controler, value);
        }

        /// <summary>
        /// 改变控制器，设置控制器
        /// </summary>
        /// <param name="controler">控制器</param>
        /// <param name="value">值</param>
        public void ChangeControl(int controler, int value)
        {
            ChangeControl(channel, controler, value);
        }


    }
}
