using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

/*
 * 管理乐器
 * 管理乐器信息，使用资源中的乐器信息文件。
 */
namespace MidiLib
{
    public class InstrumentManager
    {
        private XmlDocument xmlDoc= new XmlDocument();

        public InstrumentManager()
        {
            xmlDoc.LoadXml(Properties.Resources.Instrument);
        }


        /// <summary>
        /// 获取乐器类别集合
        /// </summary>
        /// <returns></returns>
        public string[] GetInstrumentCategories()
        {
            XmlNodeList nodes = xmlDoc.SelectNodes("Instruments/*/@Name_CN");
            string[] values = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                values[i] = nodes[i].Value;
            }
            return values;
        }


        /// <summary>
        /// 通过xpath获取乐器信息集合
        /// xpath最后取得的必须为一个xmlNode
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private InstrumentInfo[] GetInstrumentInfos(string xpath)
        {
            List<InstrumentInfo> instrumentList = new List<InstrumentInfo>();
            XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                InstrumentInfo info = new InstrumentInfo(node.Attributes["Name_CN"].Value, Convert.ToInt32(node.Attributes["NO"].Value));
                instrumentList.Add(info);
            }
           
            return instrumentList.ToArray();
        }

        /// <summary>
        /// 通过乐器类别获得乐器列表
        /// </summary>
        /// <param name="category">类别</param>
        /// <returns></returns>
        public InstrumentInfo[] GetInstrumentsByCategory(string category)
        {
            InstrumentInfo[] instruments = GetInstrumentInfos(string.Format("Instruments/*[@Name_CN=\"{0}\"]/*", category));
            return instruments;
        }

        /// <summary>
        /// 获取所有乐器
        /// </summary>
        /// <returns></returns>
        public InstrumentInfo[] GetInstruments()
        {
            InstrumentInfo[] instruments = GetInstrumentInfos(string.Format("Instruments/*/*"));
            return instruments;
        }



        /// <summary>
        /// 根据乐器编码获得乐器名称
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public string GetInstrumentNameByCode(int code)
        {
            string name = "";
            XmlNode node = xmlDoc.SelectSingleNode("Instruments/*/*[@NO=\""+code.ToString()+"\"]");
            name = node.Attributes["Name_CN"].Value;
            return name;
        }

        /// <summary>
        /// 根据乐器名称获取编号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetInstrumentCodeByName(string name)
        {
            int code = -1;
            XmlNode node = xmlDoc.SelectSingleNode("Instruments/*/*[@Name_CN=\""+name+"\"]");
            code = Convert.ToInt32(node.Attributes["NO"].Value);
            return code;
        }

    }
}
