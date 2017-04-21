using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MidiLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KeyDancer
{
    /// <summary>
    /// 发声管理器
    /// </summary>
    public class PlayManager
    {
        /// <summary>
        /// 起始音符编码值
        /// </summary>
        private int[] startingOctaves = { 0, 2, 4, 5, 7, 9, 11 };


        private NoteInfo.Mode currentMode = NoteInfo.Mode.C;

        /// <summary>
        /// 当前调式，默认为C调
        /// </summary>
        public NoteInfo.Mode CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        ConfigData confData = null;

        /// <summary>
        /// Midi对象
        /// </summary>
        MidiManager midiManager;


        /// <summary>
        /// 用于演奏的键。使用4排键，每一排前七个为音符，后两个为升降调控制键，要组合使用。
        /// </summary>
        private Key[][] playKeys = { 
                                       //主音区，定义为0区，按住空格后转换为-2区
                                       new Key[]{Key.A,Key.S,Key.D,Key.F,Key.J,Key.K,Key.L,Key.Oem1,Key.OemQuotes}, 
                                       //高一个音阶，定义为1区
                                       new Key[]{Key.Q,Key.W,Key.E,Key.R,Key.U,Key.I,Key.O,Key.P,Key.OemOpenBrackets},  
                                       //高二个音阶，定义为2区
                                       new Key[]{Key.D1,Key.D2,Key.D3,Key.D4,Key.D7,Key.D8,Key.D9,Key.D0,Key.OemMinus},  
                                       // 低一音阶，定义为-1区
                                       new Key[]{Key.Z,Key.X,Key.C,Key.V,Key.M,Key.OemComma,Key.OemPeriod,Key.OemQuestion,Key.RightShift}  
                                   };


        /// <summary>
        /// 读取配置，返回配置对象
        /// </summary>
        /// <param name="confFilePath">配置文件位置</param>
        private ConfigData ReadConfig(string confFilePath)
        {
            if (!File.Exists(confFilePath))
            {
                confData = new ConfigData();
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    using (FileStream reader = new FileStream(confFilePath, FileMode.Open))
                    {
                        confData = (ConfigData)bf.Deserialize(reader);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
            return confData;
        }

        /// <summary>
        /// 读取配置，返回对象
        /// </summary>
        /// <returns></returns>
        private ConfigData ReadConfig()
        {
            return ReadConfig(Global.ConfigFile);
        }

        /// <summary>
        /// 获取配置对象
        /// </summary>
        public ConfigData ConfigObject
        {
            get
            {
                return confData;
            }
        }


        MainWindow appWindow = null;
        /// <summary>
        /// 主窗体，用于控制主窗体的显示
        /// </summary>
        public MainWindow AppWindow
        {
            get { return appWindow; }
        }

        /// <summary>
        /// 构建对象
        /// </summary>
        public PlayManager(MainWindow window)
        {
            confData = ReadConfig();
            midiManager = new MidiManager(confData.Device, confData.Channel, confData.Instrument, confData.PlaySpeed, confData.Mode, confData.Controlers);
            currentMode = confData.Mode;
            appWindow = window;
        }

        /// <summary>
        /// 重新加载配置
        /// </summary>
        /// <param name="data"></param>
        public void ReLoadConfig(ConfigData data)
        {
            midiManager.Channel = confData.Channel;
            midiManager.Controlers = confData.Controlers;
            midiManager.DeviceID = confData.Device;
            midiManager.Instrument = confData.Instrument;
            midiManager.Mode = confData.Mode;
            midiManager.PlaySpeed = confData.PlaySpeed;
            midiManager.SetMidiEffect();

            currentMode = confData.Mode;
        }

        /// <summary>
        /// 未找到数据时的索引
        /// </summary>
        private const int UNKNOWN_INDEX = -1;
        /// <summary>
        /// 未找到所在区的区号
        /// </summary>
        private const int UNKNOWN_AREA = 100;
        /// <summary>
        /// 未找到的编码
        /// </summary>
        private const int UNKNOWN_OCTAVE_CODE = -1;

        /// <summary>
        /// 音符的索引
        /// </summary>
        private int octaveIndex = UNKNOWN_INDEX;
        /// <summary>
        /// 区号，便于计算，自定义的
        /// </summary>
        private int area = UNKNOWN_AREA;

        /// <summary>
        /// 重置区号与查找到的音符索引
        /// </summary>
        private void ResetAreaAndOctaveIndex()
        {
            octaveIndex = UNKNOWN_INDEX;
            area = UNKNOWN_AREA;
        }


        /// <summary>
        /// 为octaveIndex和area赋值
        /// </summary>
        /// <param name="key"></param>
        private void ComputeAreaAndOctaveIndexByKey(Key key)
        {
            ResetAreaAndOctaveIndex();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (key == playKeys[i][j])
                    {
                        // 通过i获取区码
                        if (i == 3)
                        {
                            area = -1;
                        }
                        else
                        {
                            area = i;
                        }

                        // 通过j获取起始音符编码索引
                        octaveIndex = j;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 取得某按键的音符编码，-1表示未找到
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetOctaveCodeByKey(Key key)
        {
            ComputeAreaAndOctaveIndexByKey(key);
            /*
               * 一个音阶的每个音符相对于另一个音阶的每一个音符的距离是相等的，
               * 只要知道起始音阶的每个音符值，那么其他音阶的音符就可以算出来：
               * 主音=起始音阶的音符值+调式
               * 每隔12个音是一个音阶
               * 高n阶的音的编码=主音+12n
               * 低n阶的音的编码=主音-12n
               * n就是我们需要的区号，是人为分的，为了好计算音符编码，
               * 为了统一方法，n可取负，这样就形成了-1区，这样
               * 低n阶的音的编码=主音+12n，此处n为负数
          */

            int octaveCode = UNKNOWN_OCTAVE_CODE;
            if (octaveIndex != UNKNOWN_INDEX && area != UNKNOWN_AREA)
            {
                octaveCode = startingOctaves[octaveIndex] + Convert.ToInt32(currentMode) + 12 * area;
            }

            return octaveCode;
        }

        /// <summary>
        /// 按某个键，发声
        /// </summary>
        /// <param name="key"></param>
        private void PlayOctave(Key key)
        {
            int octaveCode = GetOctaveCodeByKey(key);
            if (octaveCode != UNKNOWN_OCTAVE_CODE)
            {
                midiManager.NoteOn(octaveCode);
            }
        }

        /// <summary>
        /// 关闭某个键的发声
        /// </summary>
        /// <param name="key"></param>
        private void CloseOctave(Key key)
        {
            int octaveCode = GetOctaveCodeByKey(key);
            if (octaveCode != UNKNOWN_OCTAVE_CODE)
            {
                midiManager.NoteOff(octaveCode);
            }
        }

        /// <summary>
        /// 获取转换后的区
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private int GetConvertArea(int area)
        {
            int convertArea = UNKNOWN_AREA;
            switch (area)
            {
                case 0:
                    convertArea = -2;
                    break;
                case 1:
                    convertArea = 3;
                    break;
                case 2:
                    convertArea = 4;
                    break;
                case -1:
                    convertArea = -3;
                    break;
            }
            return convertArea;
        }

        /// <summary>
        /// 获取音符编码
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private int GetOctaveCode(int area)
        {
            int octaveCode = UNKNOWN_OCTAVE_CODE;
            if (octaveIndex != UNKNOWN_INDEX && area != UNKNOWN_AREA)
            {
                octaveCode = startingOctaves[octaveIndex] + Convert.ToInt32(currentMode) + 12 * area;
                
                if (IsKeyPressed(GetRaisingKeyByArea(area)))
                {
                    octaveCode += 1;
                }
                else if (IsKeyPressed(GetFallingKeyByArea(area)))
                {
                    octaveCode -= 1;
                }
            }
            return octaveCode;
        }

        /// <summary>
        /// 获取所在区的升调键
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private Key GetRaisingKeyByArea(int area)
        {
            Key raisingKey=Key.None;
            if (area == 0||area==-2)
            {
                raisingKey= playKeys[0][7];
            }
            else if (area == 1 || area == 3)
            {
                raisingKey= playKeys[1][7];
            }
            else if (area == 2 || area == 4)
            {
                raisingKey = playKeys[2][7];
            }
            else if (area == -1 || area == -3)
            {
                raisingKey = playKeys[3][7];
            }

            return raisingKey;
        }

        /// <summary>
        /// 获取某区的降调键
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private Key GetFallingKeyByArea(int area)
        {
            Key fallingKey = Key.None;
            if (area == 0 || area == -2)
            {
                fallingKey = playKeys[0][8];
            }
            else if (area == 1 || area == 3)
            {
                fallingKey = playKeys[1][8];
            }
            else if (area == 2 || area == 4)
            {
                fallingKey = playKeys[2][8];
            }
            else if (area == -1 || area == -3)
            {
                fallingKey = playKeys[3][8];
            }
            return fallingKey;
        }

        /// <summary>
        /// 播放音符
        /// </summary>
        /// <param name="code"></param>
        private void PlayOctave(int code)
        {
            if (code != UNKNOWN_OCTAVE_CODE)
            {
                midiManager.NoteOn(code);
            }
        }

        /// <summary>
        /// 关闭音符发声
        /// </summary>
        /// <param name="code"></param>
        private void CloseOctave(int code)
        {
            if (code != UNKNOWN_OCTAVE_CODE)
            {
                midiManager.NoteOff(code);
            }
        }

        /// <summary>
        /// 按下的键
        /// </summary>
        Dictionary<Key, int> pressedKeys = new Dictionary<Key, int>();

        /// <summary>
        /// 某个键是否处在按下状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsKeyPressed(Key key)
        {
            bool isPressed = false;
            isPressed = pressedKeys.ContainsKey(key);
            return isPressed;
        }

        /// <summary>
        /// 演奏
        /// </summary>
        /// <param name="key"></param>
        public void PlaySound(Key key)
        {
            ComputeAreaAndOctaveIndexByKey(key);
            int octaveCode = UNKNOWN_OCTAVE_CODE;
            
            if (IsKeyPressed(Key.Space))
            {
                int convertArea = GetConvertArea(area);
                octaveCode = GetOctaveCode(convertArea);
            }
            else
            {
                octaveCode = GetOctaveCode(area);
            }

            if (!IsKeyPressed(key))
            {
                PlayOctave(octaveCode);
                pressedKeys.Add(key, octaveCode);
            }
        }

        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="key"></param>
        public void CloseSound(Key key)
        {
            if (confData.IsOngoing)
            {
                int code = pressedKeys[key];
                CloseOctave(code);
            }
            pressedKeys.Remove(key);
        }

        /// <summary>
        /// 关闭Midi设备
        /// </summary>
        public void CloseMidiDevice()
        {
            midiManager.Close();
        }


        /// <summary>
        /// 取得调式字符串表示形式
        /// </summary>
        /// <returns></returns>
        public string GetModeString()
        {
            string modeString = "C";
            switch (currentMode)
            {
                case NoteInfo.Mode.C_R:
                    modeString = "#C";
                    break;
                case NoteInfo.Mode.D_R:
                    modeString = "#D";
                    break;
                case NoteInfo.Mode.F_R:
                    modeString = "#F";
                    break;
                case NoteInfo.Mode.G_R:
                    modeString = "#G";
                    break;
                case NoteInfo.Mode.A_R:
                    modeString = "#A";
                    break;
                default:
                    modeString = currentMode.ToString();
                    break;
            }
            return modeString;
        }

    }
}
