using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyDancer
{
    /// <summary>
    /// 全局信息，可在此配置
    /// </summary>
    internal class Global
    {
        /// <summary>
        /// 配置文件位置
        /// </summary>
        internal static string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "data.config";
    }
}
