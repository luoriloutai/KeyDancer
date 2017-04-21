using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiLib
{
    /// <summary>
    /// 乐器信息
    /// </summary>
    public class InstrumentInfo
    {
        public InstrumentInfo(string name, int code)
        {
            this.name = name;
            this.code = code;
        }

        private string name;

        /// <summary>
        /// 乐器名称
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        private int code;

        /// <summary>
        /// 名器代码
        /// </summary>
        public int Code
        {
            get { return code; }
        }
    }
}
