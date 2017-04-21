﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiLib
{
    /// <summary>
    /// 音符信息
    /// </summary>
    public class NoteInfo
    {
        /*
         * 音符按12个（在钢琴中，7个白键，5个黑键，共12个音符）循环排列，
         * 每升高或降低一个音阶，就将当前的音符值加上或减去12。由于钢琴
         * 按键排列的问题，在用一些调时需要升或降调，但是电脑按键模拟则
         * 不需要升降，每个按键的音符值都是计算出来的，按照正常C调弹奏即可。
         * 
         * 最低的八度音符+调号值就是当前调号音符值，在此基础上将每个音符值相应加减N*12后即可得出
         * 高N个或低N个八度的音符值，高低音即可表示。
         * 
         * 以下是MIDI音符表，字母表示音名（C，D，E，F，G，A，B，唱名指do,re,mi等），
         * 后面的数字代表音阶，带下划线的表示是这个音的升调
         * --------------------------------------------------------------------------------------------------
         * 
         * C0 = 0,  --0音阶的基本音C
        C_0 = 1,    --0音阶的基本音C的升调，即#C
        D0 = 2,
        D_0 = 3,
        E0 = 4,
        F0 = 5,
        F_0 = 6,
        G0 = 7,
        G_0 = 8,
        A0 = 9,
        A_0 = 10,
        B0 = 11,
        C1 = 12,
        C_1 = 13,
        D1 = 14,
        D_1 = 15,
        E1 = 16,
        F1 = 17,
        F_1 = 18,
        G1 = 19,
        G_1 = 20,
        A1 = 21,
        A_1 = 22,
        B1 = 23,
        C2 = 24,
        C_2 = 25,
        D2 = 26,
        D_2 = 27,
        E2 = 28,
        F2 = 29,
        F_2 = 30,
        G2 = 31,
        G_2 = 32,
        A2 = 33,
        A_2 = 34,
        B2 = 35,
        C3 = 36,
        C_3 = 37,
        D3 = 38,
        D_3 = 39,
        E3 = 40,
        F3 = 41,
        F_3 = 42,
        G3 = 43,
        G_3 = 44,
        A3 = 45,
        A_3 = 46,
        B3 = 47,
        C4 = 48,
        C_4 = 49,
        D4 = 50,
        D_4 = 51,
        E4 = 52,
        F4 = 53,
        F_4 = 54,
        G4 = 55,
        G_4 = 56,
        A4 = 57,
        A_4 = 58,
        B4 = 59,
        C5 = 60,
        C_5 = 61,
        D5 = 62,
        D_5 = 63,
        E5 = 64,
        F5 = 65,
        F_5 = 66,
        G5 = 67,
        G_5 = 68,
        A5 = 69,
        A_5 = 70,
        B5 = 71,
        C6 = 72,
        C_6 = 73,
        D6 = 74,
        D_6 = 75,
        E6 = 76,
        F6 = 77,
        F_6 = 78,
        G6 = 79,
        G_6 = 80,
        A6 = 81,
        A_6 = 82,
        B6 = 83,
        C7 = 84,
        C_7 = 85,
        D7 = 86,
        D_7 = 87,
        E7 = 88,
        F7 = 89,
        F_7 = 90,
        G7 = 91,
        G_7 = 92,
        A7 = 93,
        A_7 = 94,
        B7 = 95,
        C8 = 96,
        C_8 = 97,
        D8 = 98,
        D_8 = 99,
        E8 = 100,
        F8 = 101,
        F_8 = 102,
        G8 = 103,
        G_8 = 104,
        A8 = 105,
        A_8 = 106,
        B8 = 107,
        C9 = 108,
        C_9 = 109,
        D9 = 110,
        D_9 = 111,
        E9 = 112,
        F9 = 113,
        F_9 = 114,
        G9 = 115,
        G_9 = 116,
        A9 = 117,
        A_9 = 118,
        B9 = 119,
        C10 = 120,
        C_10 = 121,
        D10 = 122,
        D_10 = 123,
        E10 = 124,
        F10 = 125,
        F_10 = 126,
        G10 = 127
         */



        /// <summary>
        /// 最低八度音符，其他的音符都在此基础上加减12的倍数，升高或降低音阶
        /// </summary>
        public enum Octave
        {
            C = 0,
            D = 2,
            E = 4,
            F = 5,
            G = 7,
            A = 9,
            B = 11
        }

        /*
         * 升调比基本调值大1
         */
        /// <summary>
        /// 调式
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// C调
            /// </summary>
            C = 60,
            /// <summary>
            /// D调
            /// </summary>
            D = 62,
            /// <summary>
            /// E调
            /// </summary>
            E = 64,
            /// <summary>
            /// F调
            /// </summary>
            F = 65,
            /// <summary>
            /// G调
            /// </summary>
            G = 67,
            /// <summary>
            /// A调
            /// </summary>
            A = 69,
            /// <summary>
            /// B调
            /// </summary>
            B = 71,
            /// <summary>
            /// #C调
            /// </summary>
            C_R=61,
            /// <summary>
            /// #D调
            /// </summary>
            D_R=63,
            /// <summary>
            /// #F调
            /// </summary>
            F_R=66,
            /// <summary>
            /// #G调
            /// </summary>
            G_R=68,
            /// <summary>
            /// #A调
            /// </summary>
            A_R=70
        }
    }
}
