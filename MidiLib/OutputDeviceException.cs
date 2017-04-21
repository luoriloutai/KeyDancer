using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MidiLib
{
    public class OutputDeviceException:Exception
    {
        /// <summary>
        /// 获取错误信息，WinAPI
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="message"></param>
        /// <param name="sizeOfMessage"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int midiOutGetErrorText(int errCode,
            StringBuilder message, int sizeOfMessage);

        private StringBuilder message = new StringBuilder(128);

        public OutputDeviceException(int errorCode)
        {
            midiOutGetErrorText(errorCode, message, message.Capacity);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public override string Message
        {
            get
            {
                return message.ToString();
            }
        }
    }
}
