using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdsimScanner
{
    internal class LocoSounds
    {
        //буфера флагов звуков кнопок, если 1, то флаг взведен и звук запрещен
        public static int[] Controls_flag_key_buffer = new int[34];
        public static int[] Neshtatki_flag_key_buffer = new int[100];
        public static int[] ES5K_flag_key_buffer = new int[109];
        public static int[] EP1M_flag_key_buffer = new int[112];
        public static int[] CHS2K_flag_key_buffer = new int[32];
        public static int[] CHS4_flag_key_buffer = new int[55];
        public static int[] CHS4KVR_flag_key_buffer = new int[55];
        public static int[] CHS4T_flag_key_buffer = new int[54];
        public static int[] CHS7_flag_key_buffer = new int[46];
        public static int[] CHS8_flag_key_buffer = new int[63];
        public static int[] VL11M_flag_key_buffer = new int[84];
        public static int[] VL82M_flag_key_buffer = new int[85];
        public static int[] VL80T_flag_key_buffer = new int[49];
        public static int[] VL85_flag_key_buffer = new int[82];
        public static int[] TEP70_flag_key_buffer = new int[36];
        public static int[] TE10U_flag_key_buffer = new int[47];
        public static int[] M62_flag_key_buffer = new int[36];
        public static int[] ED4M_flag_key_buffer = new int[33];
        public static int[] ED9M_flag_key_buffer = new int[30];
        public static int[] tem18_flag_key_buffer = new int[32];

        //коллекция массивов звуков, для zdsim
        public static List<byte[]> list_collect_sounds_zdsim_controls = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_neshtatki = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_es5k = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ep1m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs2k = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4t = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs4kvr = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs7 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_chs8 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl11 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl82 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl80t = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_vl85 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_tep70 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_te10u = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_m62 = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ed4m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_ed9m = new List<byte[]>();
        public static List<byte[]> list_collect_sounds_zdsim_tem18 = new List<byte[]>();
    }
}
