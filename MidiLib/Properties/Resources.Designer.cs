﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MidiLib.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MidiLib.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;Instruments&gt;
        ///  &lt;PIANO Name_CN=&quot;钢琴&quot;&gt;
        ///    &lt;Acoustic_Grand_Piano Name_CN=&quot;大钢琴&quot; NO=&quot;0&quot;/&gt;
        ///    &lt;Bright_Acoustic_Piano Name_CN=&quot;亮音大钢琴&quot; NO=&quot;1&quot;/&gt;
        ///    &lt;Electric_Grand_Piano Name_CN=&quot;电钢琴&quot; NO=&quot;2&quot;/&gt;
        ///    &lt;Honky_Tonk_Piano Name_CN=&quot;酒吧钢琴&quot; NO=&quot;3&quot;/&gt;
        ///    &lt;Rhodes_Piano Name_CN=&quot;练习音钢琴&quot; NO=&quot;4&quot;/&gt;
        ///    &lt;Chorused_Piano Name_CN=&quot;合唱加钢琴&quot; NO=&quot;5&quot;/&gt;
        ///    &lt;Harpsichord Name_CN=&quot;拨弦古钢琴&quot; NO=&quot;6&quot;/&gt;
        ///    &lt;Clavinet Name_CN=&quot;击弦古钢琴&quot; NO=&quot;7&quot;/&gt;
        ///  &lt;/PIANO&gt;
        ///  &lt;CHROMATIC_PERCUSSION Name_CN=&quot;半音打击乐器&quot;&gt;
        ///     [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string Instrument {
            get {
                return ResourceManager.GetString("Instrument", resourceCulture);
            }
        }
    }
}
