using System;
using System.Text;
using System.Threading;
//using zdsimScanner;

namespace zdsimScanner
{
    class LocoRead_V55_008
    {
        private static byte ReadByteFromString(Int32 address, int maxLength = 4)
        {
            // читаем несколько байт (с запасом, т.к. строка "12" = 2 символа + '\0')
            byte[] buffer = Loco.read_bytes((Int32)Loco.BA + address, maxLength);

            // преобразуем в строку
            string str = Encoding.ASCII.GetString(buffer);

            // убираем мусор
            str = str.Replace("\0", "").Trim();

            // пробуем распарсить
            if (byte.TryParse(str, out byte value))
            {
                return value;
            }
            else
            {
                return 0; // если строка не число — вернём 0     
            }
        }
        //===================================================================================
        // Читаем из памти игры параметры локомотивов
        //===================================================================================
        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз 2ЭС5К"
        //-----------------------------------------------------------------------------------
        public static byte[] read_2se5k_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт отсуствует
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            d_temp = Math.Abs(d_temp);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз. отсуствует
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2 отсуствует
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3 отсуствует
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4 отсуствует
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1 отсуствует
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //-------------------------------------------------------------------------
            //                               Лампы
            //-------------------------------------------------------------------------
            //Лампа ТЦ
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);

            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ О отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ П отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ Т отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //-------------------------------------------------------------------------
            //Лампа РК С отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //-------------------------------------------------------------------------
            //Лампа РК СП отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //-------------------------------------------------------------------------
            //Лампа РК П отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //-------------------------------------------------------------------------
            //                          лампы 1 круг 49 ламп
            //-------------------------------------------------------------------------
            //Лампа 1 круг Утечка ТМ 0x39c4
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x39c4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг С1 0x614d
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг С2 0x6229
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6229, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Вент1 0x6bc8
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6bc8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Гв прав 0x64e8
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x64e8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);

            Thread.Sleep(10);

            //-------------------------------------------------------------------------
            //                          лампы 2 круг 49 ламп
            //-------------------------------------------------------------------------
            //Лампа 2 круг Утечка ТМ 0x39c4
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x39c4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг С1 0x614d
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг С2 0x6229
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6229, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Вент1 0x6bc8
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6bc8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Гв прав 0x64e8
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x64e8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);

            Thread.Sleep(10);

            //-------------------------------------------------------------------------
            //                          лампы 3 круг 49 ламп
            //-------------------------------------------------------------------------
            //Лампа 3 круг Утечка ТМ 0x39c4
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x39c4, 1);

            //(3круг Утечка ТМ 0x39c4 == 1) или (1круг Утечка ТМ 0x39c4 == 1) или (2круг Утечка ТМ 0x39c4 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг С1 0x614d
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614d, 1);

            //(3круг С1 0x614d == 1) или (1круг С1 0x614d == 1) или (2круг С1 0x614d == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг С2 0x6229
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6229, 1);

            //(3круг С2 0x6229 == 1) или (1круг С2 0x6229 == 1) или (2круг С2 0x6229 == 1) или ((3круг С1 == 1) или (3круг С2 == 1))
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1 || Loco.out_buffer[45] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //-------------------------------------------------------------------------
            //Лампа ВВ откр1 отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //-------------------------------------------------------------------------
            //Лампа Вспом. комп. отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //-------------------------------------------------------------------------
            //Лампа Комп1 отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //-------------------------------------------------------------------------
            //Лампа Вент1. 0x6229
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6229, 1);

            //(Вент1. 0x6229 == 1) или (1круг Вент1 0x6bc8 == 1) или (2круг Вент1 0x6bc8 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //-------------------------------------------------------------------------
            //Лампа rp_850_gv 0x64e8
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x64e8, 1);

            //(rp_850_gv 0x64e8 == 1) или (
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[25] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //-------------------------------------------------------------------------
            //Лампа zemla_bv отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //-------------------------------------------------------------------------
            //Лампа РК 0_1 отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //-------------------------------------------------------------------------
            //Лампа РК пром.1 отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //-------------------------------------------------------------------------
            //Лампа Песок отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //-------------------------------------------------------------------------
            //Лампа Насос отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //-------------------------------------------------------------------------
            //Лампа Ход прав отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЭП1М"
        //-----------------------------------------------------------------------------------
        public static byte[] read_ep1m_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг
            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6908, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 47, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6960, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 48, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66f8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 49, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x67a8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 50, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6858, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 51, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5fc0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 52, 1);

            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x60f4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 53, 1);

            //тд1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614c, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 55, 1);

            //тд2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61a4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 56, 1);

            //тд3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61fc, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 57, 1);

            //тд4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6254, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 58, 1);

            //тд5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x62ac, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 59, 1);

            //тд6
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6304, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 60, 1);

            Thread.Sleep(10);

            //лампы 2 круг
            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6908, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 47, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6960, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 48, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66f8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 49, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x67a8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 50, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6858, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 51, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5fc0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 52, 1);

            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x60f4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 53, 1);

            //тд1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614c, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 54, 1);

            //тд2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61a4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 55, 1);

            //тд3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61fc, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 56, 1);

            //тд4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6254, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 57, 1);

            //тд5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x62ac, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 58, 1);

            //тд6
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6304, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 59, 1);

            Thread.Sleep(10);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x614c, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[54] == 1 || Loco.temp_buffer3[54] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 105, 1);

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61a4, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[55] == 1 || Loco.temp_buffer3[55] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 106, 1);

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x61fc, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[56] == 1 || Loco.temp_buffer3[56] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 107, 1);

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6254, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[57] == 1 || Loco.temp_buffer3[57] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 108, 1);

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x62ac, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[58] == 1 || Loco.temp_buffer3[58] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 109, 1);

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6304, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[59] == 1 || Loco.temp_buffer3[59] == 1 || Loco.temp_buffer2[105] == 1 || Loco.temp_buffer2[106] == 1 || Loco.temp_buffer2[107] == 1 || Loco.temp_buffer2[108] == 1 || Loco.temp_buffer2[109] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6908, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[47] == 1 || Loco.temp_buffer3[47] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6960, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[48] == 1 || Loco.temp_buffer3[48] == 1 || Loco.out_buffer[48] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66f8, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[49] == 1 || Loco.temp_buffer3[49] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x67a8, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[50] == 1 || Loco.temp_buffer3[50] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6858, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[51] == 1 || Loco.temp_buffer3[51] == 1 || Loco.temp_buffer2[100] == 1 || Loco.temp_buffer2[101] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //гв
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x60f4, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[53] == 1 || Loco.temp_buffer3[53] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром.1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5fc0, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[52] == 1 || Loco.temp_buffer3[52] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС2К"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs2k_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //Ограничение скорости.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(f_temp);
            if (i_temp >= 1 && i_temp <= 20)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(f_temp);
            if (i_temp >= 21 && i_temp <= 34)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(f_temp);
            if (i_temp >= 35 && i_temp <= 46)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг

            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp == 0)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //гв
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4); //читаем напр. КС
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp <= 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);
            //ход прав
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp == 20 || f_temp == 34 || f_temp == 46)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС4"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs4_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FE80, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг

            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1f79, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2c5d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3aa1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3b7d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3c59, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);
            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4079, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4db5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5469, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x68dd, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7041, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            Thread.Sleep(10);

            //лампы 2 круг
            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1f79, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2c5d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3aa1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3b7d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3c59, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);
            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4079, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4db5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564d, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5469, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x68dd, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7041, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            Thread.Sleep(10);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1f79, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2c5d, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3aa1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3b7d, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3c59, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1 || Loco.temp_buffer2[100] == 1 || Loco.temp_buffer2[101] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //гв
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4079, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[6] == 1 || Loco.temp_buffer3[6] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4db5, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564d, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5469, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x68dd, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7041, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС4КВР"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs4kvr_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FE80, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические

            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг

            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1fd1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2d65, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3BA9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C85, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3D61, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);
            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4181, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x59E9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57D9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6C79, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x73DD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            Thread.Sleep(10);

            //лампы 2 круг
            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1fd1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2d65, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3BA9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C85, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3D61, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);
            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4181, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x59E9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57D9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6C79, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x73DD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            Thread.Sleep(10);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1fd1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2d65, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3BA9, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);
            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C85, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);
            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3D61, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1 || Loco.temp_buffer2[100] == 1 || Loco.temp_buffer2[101] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //гв
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4181, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[6] == 1 || Loco.temp_buffer3[6] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x59E9, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57D9, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6C79, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x73DD, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС4Т"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs4t_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //-------------------------------------------------------------------------
            //                               статические
            //-------------------------------------------------------------------------
            //-------------------------------------------------------------------------
            //скорость доп.
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //-------------------------------------------------------------------------
            //скорость тек.
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //-------------------------------------------------------------------------
            //Светофор АЛС
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //-------------------------------------------------------------------------
            //Лампа бдительность
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //-------------------------------------------------------------------------
            //скор тек 2
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //-------------------------------------------------------------------------
            //Ток эпт
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //-------------------------------------------------------------------------
            //Время часы
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //-------------------------------------------------------------------------
            //Время минуты
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //-------------------------------------------------------------------------
            //Время секунды
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //-------------------------------------------------------------------------
            //напруга (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //-------------------------------------------------------------------------
            //контроллер поз.
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //-------------------------------------------------------------------------
            //напруга тд
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //-------------------------------------------------------------------------
            //ток1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //-------------------------------------------------------------------------
            //ток2
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FE80, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //-------------------------------------------------------------------------
            //ток3
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //-------------------------------------------------------------------------
            //ток4
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //-------------------------------------------------------------------------
            //РК поз1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);



            //координата если ЭК=0,то не горит
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x003E79A8, 8);
            //double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            //MainWindow.i_koordinata = Convert.ToUInt32(d_temp);

            //текущий пикет
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CBFC, 4);
            //MainWindow.i_piket_tek = BitConverter.ToUInt16(Loco.temp_buffer, 0);

            //след.огранич.скор
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CA7C, 2);
            //MainWindow.i_sled_ogranichenie = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //расст.до след.огранич.
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x04F711BC, 4);
            //f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            //MainWindow.i_rasst_sled_ogranichenie = Convert.ToInt16(f_temp);

            //маневр./поезд.
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CA84, 1);
            //MainWindow.i_rezhim_p_m = Loco.temp_buffer[0];

            //АЛС
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08BEB744, 1);
            //MainWindow.i_ALS = Loco.temp_buffer[0];

            //бдительность
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CBC0, 1);
            // MainWindow.i_bdit = Loco.temp_buffer[0];

            //расст.до цели
            // Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08BEB730, 4);
            // MainWindow.i_rasst_do_cely = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //имя светофора
            // baze_address_temp = read_pointer((int)Loco.BA + 0x0032B12C, new int[2] { 0x08, 0x3c });
            // Loco.temp_buffer = Loco.read_bytes(baze_address_temp, 4);
            //MainWindow.s_svetofor_name = System.Text.Encoding.GetEncoding("windows-1251").GetString(Loco.temp_buffer);
            // if (MainWindow.s_svetofor_name.IndexOf('\0') != -1)
            //   MainWindow.s_svetofor_name = MainWindow.s_svetofor_name.Remove(MainWindow.s_svetofor_name.IndexOf('\0'));
            //MainWindow.s_svetofor_name = Regex.Replace(MainWindow.s_svetofor_name, "\0", "");

            //ЭК
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x04F711D1, 1);
            //MainWindow.i_EK = Loco.temp_buffer[0];

            //частота
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CA98, 1);
            //MainWindow.i_chastota = Loco.temp_buffer[0];

            //номер пути
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x04F711D0, 1);
            //MainWindow.i_path_number = Loco.temp_buffer[0];

            //путь прав./неправ.
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x04F711D1, 1);
            //MainWindow.i_path_prav = Loco.temp_buffer[0];

            //ускорение
            //Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CAAC, 8);
            //MainWindow.d_uskorenie = BitConverter.ToDouble(Loco.temp_buffer, 0);

            //строка команд
            //baze_address_temp = read_pointer((int)Loco.BA + 0x0032B14C, new int[2] { 0x08, 0x13 });
            //Loco.temp_buffer = Loco.read_bytes(baze_address_temp, 16);
            //MainWindow.s_stroka_command = System.Text.Encoding.GetEncoding("windows-1251").GetString(Loco.temp_buffer);
            //if (MainWindow.s_stroka_command.IndexOf('\0') != -1)
            //   MainWindow.s_stroka_command = MainWindow.s_stroka_command.Remove(MainWindow.s_stroka_command.IndexOf('\0'));
            //MainWindow.s_stroka_command = Regex.Replace(MainWindow.s_stroka_command, "\0", "");

            //читаем реверс
            //baze_address_temp = read_pointer((int)Loco.BA + 0x07905ACC, new int[1] { 0x1d8 });
            //Loco.temp_buffer = Loco.read_bytes(baze_address_temp, 1);
            //int i_temp = Loco.temp_buffer[0];
            //if (i_temp == 1) MainWindow.i_revers = 0;
            //if (i_temp == 0) MainWindow.i_revers = 1;
            //if (i_temp == 2) MainWindow.i_revers = 2;


            //-------------------------------------------------------------------------
            //                               динамические
            //-------------------------------------------------------------------------
            //                               Манометры
            //-------------------------------------------------------------------------
            //Манометр НМ (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //-------------------------------------------------------------------------
            //Манометр ТМ (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xA8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //-------------------------------------------------------------------------
            //Манометр УР (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //-------------------------------------------------------------------------
            //Манометр ТЦ (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);


            //ТМ
            //Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xA8, 8);
            //MainWindow.d_TM = BitConverter.ToDouble(Loco.temp_buffer, 0);

            //УР
            //Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            //MainWindow.d_UR = BitConverter.ToDouble(Loco.temp_buffer, 0);

            //ТЦ
            //Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            //MainWindow.d_TC = BitConverter.ToDouble(Loco.temp_buffer, 0);

            //-------------------------------------------------------------------------
            //                               Лампы
            //-------------------------------------------------------------------------
            //Лампа ТЦ отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ_О 0x08BEB4D1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];

            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //-------------------------------------------------------------------------
            //Лампа ЭПТ_П (0x08BEB4D1 0x0032CBD0)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];

            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];

            //(ЭПТ_контроль 0x08BEB4D1 и (кран395 0x0032CBD0 == 2 или кран395 0x0032CBD0 == 3))
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ_Т (0x08BEB4D1 и 0x0032CBD0)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];

            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];

            //(ЭПТ_контроль 0x08BEB4D1 == 1 и (кран395 0x0032CBD0 == 4) или (кран395 0x0032CBD0 == 5) или (кран395 0x0032CBD0 == 6))
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //-------------------------------------------------------------------------
            //Лампа РК_С отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //-------------------------------------------------------------------------
            //Лампа РК_СП отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //-------------------------------------------------------------------------
            //Лампа РК_П отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //-------------------------------------------------------------------------
            //                          лампы 1 круг
            //-------------------------------------------------------------------------
            //Лампа 1 круг ВВ_откр1 0x4C29
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4C29, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг вспом. комп. 0x2F49
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2F49, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг комп1 0x66F9
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66F9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг комп2 0x6961
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6961, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Вент1 0x5DB1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5DB1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Вент3 0x5E61
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5E61, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг рп850_1 0x6AC1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6AC1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг земля1 0x77FD
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x77FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг РК0_1 0x8699
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг РК пром.1 0x7B6D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7B6D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Песок 0xAD9D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xAD9D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            //-------------------------------------------------------------------------
            //Лампа 1 круг Насос 0x12ED
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x12ED, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 12, 1);

            Thread.Sleep(10);

            //-------------------------------------------------------------------------
            //                           лампы 2 круг
            //-------------------------------------------------------------------------
            //Лампа 2 круг ВВ_откр1 0x4C29
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4C29, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг вспом. комп. 0x2F49
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2F49, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг комп1 0x66F9
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66F9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг комп2 0x6961
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6961, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Вент1 0x5DB1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5DB1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Вент3 0x5E61
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5E61, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг рп850_1 0x6AC1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6AC1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг земля1 0x77FD
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x77FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг РК 0_1 0x8699
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг РК пром.1 0x7B6D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7B6D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Песок 0xAD9D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xAD9D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            //-------------------------------------------------------------------------
            //Лампа 2 круг Насос 0x12ED
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x12ED, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 12, 1);

            Thread.Sleep(10);

            //-------------------------------------------------------------------------
            //                           лампы 3 круг
            //-------------------------------------------------------------------------
            //Лампа 3 круг утечка ТМ отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг ТД отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг ВВ_откр1 0x4C29
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4C29, 1);

            //(3круг ВВ_откр1 0x4C29 == 1) или (1круг ВВ_откр1 0x4C29 == 1) или (2круг ВВ_откр1 0x4C29 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг вспом.комп. 0x2F49
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2F49, 1);

            //(3круг вспом.комп. 0x2F49 == 1) или (1круг вспом.комп. 0x2F49 == 1) или (2круг вспом.комп. 0x2F49 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг комп1 0x66F9
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x66F9, 1);

            //(3круг комп1 0x66F9 == 1) или (1круг комп1 0x66F9 == 1) или (1круг комп2 0x6961 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer2[4] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг комп2 0x6961
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6961, 1);

            //(3круг комп2 0x6961 == 1) или (2круг комп1 0x66F9 == 1) или (2круг комп2 0x6961 == 1) или ((3круг комп1 0x66F9 == 1) или (1круг комп1 0x66F9 == 1) или (1круг комп2 0x6961 == 1))
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer3[3] == 1 || Loco.temp_buffer3[4] == 1 || Loco.temp_buffer2[100] == 1)
            {
                Loco.temp_buffer[0] = 1;
            }
            else
            {
                Loco.temp_buffer[0] = 0;
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг Вент1 0x5DB1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5DB1, 1);

            //(3круг Вент1 0x5DB1 == 1) или (1круг Вент1 0x5DB1 == 1) или (1круг Вент3 0x5E61 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer2[6] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг Вент3 0x5E61
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5E61, 1);

            //(3круг Вент3 0x5E61 == 1) или (2круг Вент1 0x5DB1 == 1) или (2круг Вент3 0x5E61 == 1) или ((3круг Вент1 0x5DB1 == 1) или (1круг Вент1 0x5DB1 == 1) или (1круг Вент3 0x5E61 == 1))
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer3[5] == 1 || Loco.temp_buffer3[6] == 1 || Loco.temp_buffer2[100] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг рп850_1 0x6AC1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6AC1, 1);

            //(3круг рп850_1 0x6AC1 == 1) или (1круг рп850_1 0x6AC1 == 1) или (2круг рп850_1 0x6AC1 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг земля1 0x77FD
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x77FD, 1);

            //(3круг земля1 0x77FD == 1) или (1круг земля1 0x77FD == 1) или (2круг земля1 0x77FD == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг РК0_1 0x8699
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);

            //(3круг РК0_1 0x8699 == 1) или (1круг РК0_1 0x8699 == 1) или (2круг РК0_1 0x8699 == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг РК пром.1 0x7B6D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7B6D, 1);

            //(3круг РКпром.1 0x7B6D == 1) или (1круг РКпром.1 0x7B6D == 1) или (2круг РКпром.1 0x7B6D == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг Песок 0xAD9D
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xAD9D, 1);

            //(3круг Песок 0xAD9D == 1) или (1круг Песок 0xAD9D == 1) или (2круг Песок 0xAD9D == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг утечка насос 0x12ED
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x12ED, 1);

            //(3круг утечка насос 0x12ED == 1) или (1круг Насос 0x12ED == 1) или (2круг Насос 0x12ED == 1)
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[12] == 1 || Loco.temp_buffer3[12] == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг ход прав отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС7"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs7_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB9360, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB9364, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB9368, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB936C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //-------------------------------------------------------------------------
            //                               Лампы
            //-------------------------------------------------------------------------
            //Лампа ТЦ
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ О
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //-------------------------------------------------------------------------
            //Лампа ЭПТ П
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //-------------------------------------------------------------------------
            //Лампа эпт Т
            //------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];

            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //-------------------------------------------------------------------------
            //Лампа РК С
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp >= 1 && f_temp <= 20)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //-------------------------------------------------------------------------
            //Лампа РК СП
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp >= 21 && f_temp <= 38)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //-------------------------------------------------------------------------
            //Лампа РК П
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp >= 39 && f_temp <= 56)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //-------------------------------------------------------------------------
            //                          лампы 3 круг
            //-------------------------------------------------------------------------
            //Лампа 3 круг утечка ТМ
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp == 0)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг ВВ откр1 
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг вспом. комп.
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //-------------------------------------------------------------------------
            //Лампа 3 круг Вент3 
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //850
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4); //читаем РК поз.
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp == 20 || f_temp == 38 || f_temp == 56)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ЧС8"
        //-----------------------------------------------------------------------------------
        public static byte[] read_chs8_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FF30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FDD0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FE28, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические

            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг
            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1B85, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //вв откр2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1BDD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xA29, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);

            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD475, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC841, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD57D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD525, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD4CD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC949, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 12, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC8F1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 13, 1);

            //вент6
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC899, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 14, 1);

            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8221, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 15, 1);

            //рп 850_2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9141, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 16, 1);

            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8B3D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 17, 1);

            //земля2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9A5D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 18, 1);

            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4414, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 19, 1);

            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4390, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 20, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFBFD, 1);
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFC29, 1);
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 0 && b_temp2 == 0)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 24, 1);

            Thread.Sleep(10);

            //лампы 2 круг
            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1B85, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);

            //вв откр2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1BDD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xA29, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);

            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD475, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC841, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD57D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD525, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD4CD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC949, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 12, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC8F1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 13, 1);

            //вент6
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC899, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 14, 1);

            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8221, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 15, 1);

            //рп 850_2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9141, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 16, 1);

            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8B3D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 17, 1);

            //земля2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9A5D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 18, 1);

            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4414, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 19, 1);

            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4390, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 20, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFBFD, 1);
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFC29, 1);
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 0 && b_temp2 == 0)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 24, 1);

            Thread.Sleep(10);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1B85, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //вв откр2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1BDD, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1 || Loco.out_buffer[46] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xA29, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[6] == 1 || Loco.temp_buffer3[6] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD475, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //комп2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC841, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1 || Loco.out_buffer[48] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD57D, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD525, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xD4CD, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 102, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC949, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[12] == 1 || Loco.temp_buffer3[12] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 103, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC8F1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[13] == 1 || Loco.temp_buffer3[13] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 104, 1);

            //вент6
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xC899, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[14] == 1 || Loco.temp_buffer3[14] == 1 || Loco.temp_buffer2[100] == 1 || Loco.temp_buffer2[101] == 1 || Loco.temp_buffer2[102] == 1 || Loco.temp_buffer2[103] == 1 || Loco.temp_buffer2[104] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //рп 850_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8221, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[15] == 1 || Loco.temp_buffer3[15] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //рп 850_2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9141, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[16] == 1 || Loco.temp_buffer3[16] == 1 || Loco.out_buffer[50] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //земля1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8B3D, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[17] == 1 || Loco.temp_buffer3[17] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //земля2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x9A5D, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[18] == 1 || Loco.temp_buffer3[18] == 1 || Loco.out_buffer[51] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4414, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[19] == 1 || Loco.temp_buffer3[19] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром. 1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4390, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[20] == 1 || Loco.temp_buffer3[20] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //насос
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFBFD, 1);
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0xFC29, 1);
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 0 && b_temp2 == 0)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[24] == 1 || Loco.temp_buffer3[24] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ВЛ11М"
        //-----------------------------------------------------------------------------------
        public static byte[] read_vl11m_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FF30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FF30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические

            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент3
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //850
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ВЛ82М"
        //-----------------------------------------------------------------------------------
        public static byte[] read_vl82m_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB883C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB8838, 4);
            float f_temp2 = BitConverter.ToSingle(Loco.temp_buffer, 0);
            if (f_temp >= 2) Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB883C, 4);
            if (f_temp2 >= 1) Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB8838, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FF30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические

            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг

            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6EE1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);
            //тд
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x87CD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);
            //вент прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x850D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);
            //вент лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x85BD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);
            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8171, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);
            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x81F5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);
            //бв квц прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82A5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);
            //бв квц лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8355, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8985, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);

            Thread.Sleep(10);

            //лампы 2 круг

            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6EE1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);
            //тд
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x87CD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);
            //вент прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x850D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);
            //вент лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x85BD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);
            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8171, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);
            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x81F5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);
            //бв квц прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82A5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);
            //бв квц лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8355, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8985, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);

            Thread.Sleep(10);

            //лампы 3 круг

            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6EE1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8699, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x87CD, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x850D, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);
            //вент лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x85BD, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1 || Loco.temp_buffer2[100] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8171, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[6] == 1 || Loco.temp_buffer3[6] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);
            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x81F5, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1 || Loco.temp_buffer2[101] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //бв квц прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82A5, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 102, 1);
            //бв квц лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8355, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1 || Loco.temp_buffer2[102] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x8985, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ВЛ80Т"
        //-----------------------------------------------------------------------------------
        public static byte[] read_vl80t_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //-------------------------------------------------------------------------
            //                               статические
            //-------------------------------------------------------------------------
            float f_temp;
            UInt16 i_temp;
            float i_skor_tek1;

            //-------------------------------------------------------------------------
            //скорость доп.
            //-------------------------------------------------------------------------
            //Loco.temp_buffer = new byte[] { 255, 255 };
            Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0032CA78, 2);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 0, 2);//копируем переменную в массив Loco.out_buffer 0й элемент 2 байта

            //-------------------------------------------------------------------------
            //скорость тек.
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);


            Form1.i_skor_tek2 = Convert.ToInt16(f_temp);
            //-------------------------------------------------------------------------
            //Светофор АЛС
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);//копируем переменную в массив Loco.out_buffer 4й элемент 1 байта

            //-------------------------------------------------------------------------
            //Лампа бдительность
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 5, 1);

            //-------------------------------------------------------------------------
            //скорость тек 2
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);


            //-------------------------------------------------------------------------
            //Ток эпт  отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //-------------------------------------------------------------------------
            //Время часы
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //-------------------------------------------------------------------------
            //Время минуты
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //-------------------------------------------------------------------------
            //Время секунды
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //-------------------------------------------------------------------------
            //Стрелочный прибор напряжение КС (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //-------------------------------------------------------------------------
            //Индикатор позиции контроллера скорости. (контроллер поз.) отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //-------------------------------------------------------------------------
            //Стрелочный прибор напряжение ТД (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //-------------------------------------------------------------------------
            //Стрелочный прибор ток1 (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FF30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //-------------------------------------------------------------------------
            //Стрелочный прибор ток2 (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //-------------------------------------------------------------------------
            //Стрелочный прибор ток3 (float)  отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //-------------------------------------------------------------------------
            //Стрелочный прибор ток4 (float)  отсуствует
            //-------------------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //-------------------------------------------------------------------------
            //РК поз1
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F9C, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //-------------------------------------------------------------------------
            //                               динамические
            //-------------------------------------------------------------------------
            //                               Манометры
            //-------------------------------------------------------------------------
            //Манометр НМ (float) //ТЦ
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //-------------------------------------------------------------------------
            //Манометр ТМ (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //-------------------------------------------------------------------------
            //Манометр УР (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //-------------------------------------------------------------------------
            //Манометр ТЦ (float)
            //-------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);



            //резерв
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //--------------------------------------------------------------------------
            //Лампы
            //--------------------------------------------------------------------------
            //Лампа ЭПТ-О
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //--------------------------------------------------------------------------
            //Лампа ЭПТ-П
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //--------------------------------------------------------------------------
            //Лампа ЭПТ-Т
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //--------------------------------------------------------------------------
            //Лампы 1й опрос
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //Лампа ВУ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);

            //--------------------------------------------------------------------------
            //Лампа ТР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x819D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);

            //--------------------------------------------------------------------------
            //Лампа утечка ТМ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5361, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);

            //--------------------------------------------------------------------------
            //Лампа ТД
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x543D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x559D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);

            //--------------------------------------------------------------------------
            //Лампа ЗБ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x14A4, 1);//читаем зб1
            byte zb1 = Loco.temp_buffer[0];

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2A20, 1);//читаем зб2
            byte zb2 = Loco.temp_buffer[0];

            if (zb1 == 1 && zb2 == 1)
                Loco.temp_buffer = new byte[1];
            else
                Loco.temp_buffer = new byte[1] { 1 };

            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);

            //--------------------------------------------------------------------------
            //Лампа МК
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4809, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5049, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);

            //--------------------------------------------------------------------------
            //Лампа РЗ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x83AD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x56FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 12, 1);

            //--------------------------------------------------------------------------
            //Лампа РБ Боксование 
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5CA8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 13, 1);

            //--------------------------------------------------------------------------
            //Лампа ФР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57AD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 14, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5729, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 15, 1);

            //--------------------------------------------------------------------------
            Thread.Sleep(10);
            //--------------------------------------------------------------------------
            //Лампы 2й опрос
            //--------------------------------------------------------------------------
            //--------------------------------------------------------------------------
            //Лампа ВУ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);

            //--------------------------------------------------------------------------
            //Лампа ТР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x819D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);

            //--------------------------------------------------------------------------
            //Лампа утечка ТМ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5361, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);

            //--------------------------------------------------------------------------
            //Лампа ТД
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x543D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x559D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);

            //--------------------------------------------------------------------------
            //Лампа ЗБ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x14A4, 1);//читаем зб1
            zb1 = Loco.temp_buffer[0];

            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2A20, 1);//читаем зб2
            zb2 = Loco.temp_buffer[0];

            if (zb1 == 1 && zb2 == 1)
                Loco.temp_buffer = new byte[1];
            else
                Loco.temp_buffer = new byte[1] { 1 };

            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);
            //--------------------------------------------------------------------------
            //Лампа МК
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4809, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5049, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);

            //--------------------------------------------------------------------------
            //Лампа РЗ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x83AD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x56FD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 12, 1);

            //--------------------------------------------------------------------------
            //Лампа РБ Боксование 
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5CA8, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 13, 1);

            //--------------------------------------------------------------------------
            //Лампа ФР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57AD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 14, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5729, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 15, 1);

            //--------------------------------------------------------------------------
            Thread.Sleep(10);//задержка между опросами ламп
            //--------------------------------------------------------------------------
            //Лампы 3й опрос
            //--------------------------------------------------------------------------
            //Лампа ВУ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x82FD, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //--------------------------------------------------------------------------
            //Лампа ТР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x819D, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //--------------------------------------------------------------------------
            //Лампа утечка ТМ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5361, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //--------------------------------------------------------------------------
            //Лампа ТД
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x543D, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x559D, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //--------------------------------------------------------------------------
            //Лампа ЗБ
            //--------------------------------------------------------------------------
            //зб1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x14A4, 1); //читаем зб1
            b_temp1 = Loco.temp_buffer[0];

            //зб2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x2A20, 1); //читаем зб2
            b_temp2 = Loco.temp_buffer[0];

            //зб
            if (b_temp1 == 1 && b_temp2 == 1)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };

            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 100, 1);

            //зб
            if (Loco.temp_buffer[0] == 1 && Loco.temp_buffer2[6] == 1 && Loco.temp_buffer3[6] == 1 && Loco.temp_buffer3[100] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //--------------------------------------------------------------------------
            //Лампа МК
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4809, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //--------------------------------------------------------------------------
            //Лампа Вент лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x564D, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1 || Loco.temp_buffer2[100] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5049, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //--------------------------------------------------------------------------
            //Лампа РЗ
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x83AD, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //--------------------------------------------------------------------------
            //Лампа ГВ прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x4FC5, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП прав
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x56FD, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[12] == 1 || Loco.temp_buffer3[12] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //--------------------------------------------------------------------------
            //Лампа РБ Боксование 
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5CA8, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[13] == 1 || Loco.temp_buffer3[13] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //--------------------------------------------------------------------------
            //Лампа ФР
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x57AD, 1);

            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[14] == 1 || Loco.temp_buffer3[14] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //--------------------------------------------------------------------------
            //Лампа 0,ХП лев
            //--------------------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x5729, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[15] == 1 || Loco.temp_buffer3[15] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;

            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);

            //--------------------------------------------------------------------------
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электровоз ВЛ85"
        //-----------------------------------------------------------------------------------
        public static byte[] read_vl85_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08CF3F30, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //эпт О
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //эпт П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //эпт Т
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 1 круг
            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C01, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 1, 1);

            //С1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1D11, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 2, 1);

            //С2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1CB9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 3, 1);

            //вспом. комп.
            //зб
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7855, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 4, 1);

            //мк
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7409, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 5, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6E89, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 6, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6F91, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 7, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7099, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 8, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x71A1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 9, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7301, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 10, 1);

            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 11, 1);

            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 12, 1);

            //фр
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x63E1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 13, 1);

            Thread.Sleep(10);

            //лампы 2 круг
            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C01, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 1, 1);

            //С1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1D11, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 2, 1);

            //С2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1CB9, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 3, 1);

            //вспом. комп.
            //зб
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7855, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 4, 1);

            //мк
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7409, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 5, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6E89, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 6, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6F91, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 7, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7099, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 8, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x71A1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 9, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7301, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 10, 1);

            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 11, 1);

            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 12, 1);

            //фр
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x63E1, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer3, 13, 1);

            Thread.Sleep(10);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x3C01, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[1] == 1 || Loco.temp_buffer3[1] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //С1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1D11, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[2] == 1 || Loco.temp_buffer3[2] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 100, 1);

            //С2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1CB9, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[3] == 1 || Loco.temp_buffer3[3] == 1 || Loco.temp_buffer2[100] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //вспом. комп.
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7855, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[4] == 1 || Loco.temp_buffer3[4] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7409, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[5] == 1 || Loco.temp_buffer3[5] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //вент1
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6E89, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[6] == 1 || Loco.temp_buffer3[6] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 101, 1);

            //вент2
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x6F91, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[7] == 1 || Loco.temp_buffer3[7] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 102, 1);

            //вент3
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7099, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[8] == 1 || Loco.temp_buffer3[8] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 103, 1);

            //вент4
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x71A1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[9] == 1 || Loco.temp_buffer3[9] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 104, 1);

            //вент5
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x7301, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[10] == 1 || Loco.temp_buffer3[10] == 1 || Loco.temp_buffer2[101] == 1 || Loco.temp_buffer2[102] == 1 || Loco.temp_buffer2[103] == 1 || Loco.temp_buffer2[104] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //гв прав
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[11] == 1 || Loco.temp_buffer3[11] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.temp_buffer2, 105, 1);

            //гв лев
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x1E45, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[12] == 1 || Loco.temp_buffer3[12] == 1 || Loco.temp_buffer2[105] == 1)
                Loco.temp_buffer[0] = 1;
            else
                Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //фр
            Loco.temp_buffer = Loco.read_bytes(sig_pos_elektro + 0x63E1, 1);
            if (Loco.temp_buffer[0] == 1 || Loco.temp_buffer2[13] == 1 || Loco.temp_buffer3[13] == 1) Loco.temp_buffer[0] = 1;
            else Loco.temp_buffer[0] = 0;
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Тепловоз ТЭП70"
        //-----------------------------------------------------------------------------------
        public static byte[] read_tep70_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB9384, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp * 100);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);

            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //вент3
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //РП850
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром.1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Тепловоз 2ТЭ10У"
        //-----------------------------------------------------------------------------------
        public static byte[] read_2te10u_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4E4, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_tok_ept_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);


            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            //Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FED8, 4);
            //f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            //f_temp = Math.Abs(f_temp);
            //i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            ///Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            //Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);


            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Тепловоз М62"
        //-----------------------------------------------------------------------------------
        public static byte[] read_m62_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические

            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы

            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);


            //лампы 3 круг

            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент3
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //850
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электропоезд ЭД4М"
        //-----------------------------------------------------------------------------------
        public static byte[] read_ed4m_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            byte[] b;
            Int32 i32;

            //статические
            //Ограничение скорости (допустимая скорость)
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0034987C, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0) & 0xFF;

            // скорость тек. с множетелем для стрелочных приборов на ШД
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F8C28C, 4);  // читаем float по адресу в памяти симулятора
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);           // преобразуем 4 байта в float
            f_temp = Math.Abs(f_temp);                                           // модуль (на случай отрицательных значений)
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);  // масштабируем в целое значение
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);                    // упаковываем в 2 байта
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);              // кладём в выходной буфер

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08C07ECC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            
            //Расстояние до цели в метрах
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08C07EB8, 4);
            Form1.i_rasstoyanie_do_tseli = BitConverter.ToInt16(Loco.temp_buffer, 0);
            //Console.WriteLine($"[DEBUG] Расстояние до цели в метрах = {Form1.i_rasstoyanie_do_tseli}");

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //Скорость текущая без множетеля
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F8C28C, 4);  // читаем float по адресу в памяти симулятора
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);                 // преобразуем 4 байта в float
            f_temp = Math.Abs(f_temp);                                           // модуль (на случай отрицательных значений)
            i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);         // масштабируем в целое значение
            Form1.i_skor_tek = i_temp;                                           // Запишим значение в переменную текущей скорости для дальнейших вычислений
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);                    // упаковываем в 2 байта
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);              // кладём в выходной буфер

            // !!! Добавляем отладку здесь !!!
            Console.WriteLine($"[DEBUG] Текущая скорость (float из памяти) = {f_temp}, после конвертации = {i_temp}");

            // Часы
            Loco.out_buffer[10] = ReadByteFromString(0x08C08041);

            // Минуты
            Loco.out_buffer[11] = ReadByteFromString(0x08C08047);

            // Секунды
            Loco.out_buffer[12] = ReadByteFromString(0x08C0804D);

            // ===============================================================
            // Напряжение контактной сети (KS)
            // адрес: BA + 0x08DD5B0C, тип: Double
            // ===============================================================
            // читаем 8 байт из процесса по новому адресу
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DD5B0C, 8);

            // преобразуем байты в double
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);

            // берём абсолютное значение (на случай отрицательных шумов)
            d_temp = Math.Abs(d_temp);

            // масштабируем в "целое напряжение" по коэффициенту
            i_temp = Convert.ToUInt16(d_temp * Loco.i_napruga_ks_convert);

            // переводим обратно в байты
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);

            // копируем результат в выходной буфер (2 байта начиная с позиции 13)
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            // ===============================================================
            // Контроллер ED4M
            // адрес: BA + 0x08DD5B05, тип: 1 байт (0–255)
            // ===============================================================
            byte[] b_temp_byte1 = new byte[1]; // [0] по умолчанию = 0

            // читаем 1 байт из процесса (положение рукоятки контроллера)
            byte[] tmp = Loco.read_bytes((Int32)Loco.BA + 0x08DD5B05, 1);   // читаем ОДИН раз
            byte ctrl = (tmp != null && tmp.Length > 0) ? tmp[0] : (byte)0;

            // 1) копируем этот байт в выходной буфер на позицию 15
            Loco.out_buffer[15] = ctrl;

            // 2) в кэш b_temp_byte1 для последующего использования (ЭПТ и пр.)
            b_temp_byte1[0] = ctrl;
            
            //динамические
            //Манометр НМ. float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);
            //---------------------------------------------------------------
            //Лампы
            //---------------------------------------------------------------
            //тц SIM
            //---------------------------------------------------------------
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //---------------------------------------------------------------
            //эпт О SIM
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            // ========================= ЭПТ «П» SIM =========================
            // читаем флаги один раз в локальные байты
            byte b_ept = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1)[0];  // ЭПТ контроль
            byte b_kran = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1)[0];  // кран 395

            // условие использует кэш контроллера b_temp_byte1[0], без повторных чтений
            bool eptP = (b_ept == 1) && (b_kran == 2 || b_kran == 3 || b_temp_byte1[0] == 255 || b_temp_byte1[0] == 254 || b_temp_byte1[0] == 253);

            // кладём результат в out_buffer позиция 39
            Loco.out_buffer[39] = (byte)(eptP ? 1 : 0);

            //---------------------------------------------------------------
            //эпт Т SIM
            byte b_temp2 = 0;
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6 || b_temp_byte1[0] == 252 || b_temp_byte1[0] == 251))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);
            //---------------------------------------------------------------
            //Лампа ПР ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFB, 1);//читаем ПР ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //---------------------------------------------------------------
            //Лампа ВЦ ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFC, 1);//читаем ВЦ ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //---------------------------------------------------------------
            //EPK_State SIM
            b = Loco.read_bytes((Int32)Loco.BA + 0x07905ACC, 4); //читаем состояние ключа EPK
            i32 = BitConverter.ToInt32(b, 0) + 0x38;
            if (i32 != 0)
                Loco.temp_buffer = new byte[] { 0 };
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);
            //---------------------------------------------------------------
            //Лампа ЛК ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFF8, 1); //двери ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //Лампа ЛК SIM
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB92AD, 1); //читаем контроллер
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 0)//если 0, сбрасываем флаги
            {
                Loco.i_lampa_LK = 0;
                Loco.i_lampa_LK_flag = 0;
                Form1.i_lampa_LK_sec_flag = 0;
            }

            //если х1 или т1 после 0 и не вышел таймер 500ms, зажигаем ЛК
            if ((b_temp1 == 1 || b_temp1 == 255) && Loco.i_lampa_LK_flag == 0
                 && Loco.out_buffer[50] == 0 && Loco.out_buffer[51] == 0)
            {
                Loco.i_lampa_LK = 1;
                Loco.i_lampa_LK_flag = 1;
            }
            Loco.temp_buffer = BitConverter.GetBytes(Loco.i_lampa_LK);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);
            //---------------------------------------------------------------
            //двери SIM
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BE7C28, 1);
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BE7C29, 1);
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && b_temp2 == 1)
            {
                Loco.i_dvery_current = 1;
            }

            if (b_temp1 == 0 || b_temp2 == 0)
            {
                Loco.i_dvery_current = 0;
            }

            //двери ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFA, 1); //двери ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //---------------------------------------------------------------
            //Лампа О ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFF, 1);//читаем О и Т ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //---------------------------------------------------------------
            //Лампа РН ZTE.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFF6, 1); //читаем РН ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //Лампа РН SIM.
            if (Loco.out_buffer[13] != 0 || Loco.out_buffer[14] != 0)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //---------------------------------------------------------------
            //БВ SIM
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB92AE, 1); //читаем БВ
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 0 };
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //БВ ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFF7, 1);//читаем БВ zte
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //---------------------------------------------------------------
            //Лампа K ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFF9, 1);//читаем K zte
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //---------------------------------------------------------------
            //Лампа РБ Бокс. SIM
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x003759DC, 1); //читаем Боксование
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //---------------------------------------------------------------
            //Лампа СОТ   ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFD, 1);//читаем СОТ   ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);
            //---------------------------------------------------------------
            //Лампа СОТx  ZTE
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFE, 1);//читаем СОТx  ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            //---------------------------------------------------------------
            //Лампа Тяга  ZTE
            //Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFE, 1);//читаем Тяга  ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 1);
            //---------------------------------------------------------------
            //Лампа ЭДТ  ZTE
            //Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0145FFFE, 1);//читаем ЭДТ  ZTE
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0)
                Loco.temp_buffer = new byte[] { 1 };
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 28, 1);
            //---------------------------------------------------------------
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 1);
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 26, 1);
            return Loco.out_buffer;
        }


        // ===============================================================
        // Чтение данных ED4M (версия 55)
        // Переписано с учётом новых универсальных функций
        // ===============================================================
        public void read_ed4m_V55_008_test()
        {
            // --- Контроллер ---
            // Адрес: BA + 0x08DD5B05 (1 байт)
            // Выход: out_buffer[15]
            // Кэш: b_temp_byte1 (для проверки ЭПТ)
            byte[] b_temp_byte1 = new byte[1];
            Loco.ReadByteToBuffer(0x08DD5B05, 15, b_temp_byte1);

            // --- Напряжение контактной сети ---
            // Адрес: BA + 0x08DD5B0C (Double)
            // Масштабируется → UInt16
            // Выход: out_buffer[13–14]
            Loco.ReadScaledDoubleToBuffer(0x08DD5B0C, 13, Loco.i_napruga_ks_convert);

            // --- Скорость ---
            // Адрес: BA + 0x04F8C28C (float)
            // Масштабируется → UInt16
            // Выход: out_buffer[6–7]
            Loco.ReadScaledFloatToBuffer(0x04F8C28C, 6, Loco.i_skor_tek_convert);

            // --- ЭПТ ---
            // Контроль ЭПТ (1 байт, BA + 0x08BEB4D1)
            byte b_temp1 = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1)[0];

            // Кран 395 (1 байт, BA + 0x0032CBD0)
            byte b_temp2 = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1)[0];

            // Условие активации ЭПТ
            // b_temp_byte1 = контроллер (0x08DD5B05)
            if (b_temp1 == 1 &&
                (b_temp2 == 2 || b_temp2 == 3 ||
                 b_temp_byte1[0] == 255 || b_temp_byte1[0] == 254 || b_temp_byte1[0] == 253))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 0 };
            }

            // Записываем результат проверки ЭПТ в out_buffer[39]
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
        }

        public static byte[] read_ed4m_V55_008_test2(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
                byte[] b; int i32;

                // --------- Ограничение скорости (допустимая) ---------
                // (как у тебя было — сохраняем в Form1, в буфер не кладём)
                Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x0034987C, 2);
                Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0) & 0xFF;

                // --------- Скорость (float с множителем) -> out[6..7] ---------
                Loco.ReadScaledFloatToBuffer(0x04F8C28C, 6, Loco.i_skor_tek_convert);
                // параллельно сохраним текущее целочисленное в Form1.i_skor_tek, как и делал
                    var buf = Loco.read_bytes((int)Loco.BA + 0x04F8C28C, 4);
                    float f = Math.Abs(BitConverter.ToSingle(buf, 0));
                    UInt16 i_temp = (UInt16)Math.Min(Math.Max(f * Loco.i_skor_tek_convert, 0), ushort.MaxValue);
                    Form1.i_skor_tek = i_temp;

                // --------- АЛС -> out[4] ---------
                Loco.ReadByteToBuffer(0x08C07ECC, 4);

                // --------- Расстояние до цели (4 байта) ---------
                Loco.temp_buffer = Loco.read_bytes((int)Loco.BA + 0x08C07EB8, 4);
                Form1.i_rasstoyanie_do_tseli = BitConverter.ToInt16(Loco.temp_buffer, 0); // оставляю как у тебя

                // --------- Бдительность ---------
                Loco.i_bdit_current = Loco.ReadByteValue(0x0032CBC0);

                // --------- Часы/Минуты/Секунды -> out[10],[11],[12] ---------
                Loco.ReadByteToBuffer(0x08C08041, 10); // часы
                Loco.ReadByteToBuffer(0x08C08047, 11); // минуты
                Loco.ReadByteToBuffer(0x08C0804D, 12); // секунды

                // --------- Напряжение КС (Double) -> scale -> out[13..14] ---------
                Loco.ReadScaledDoubleToBuffer(0x08DD5B0C, 13, Loco.i_napruga_ks_convert);

                // --------- Контроллер ED4M -> out[15] + кэш b_temp_byte1 ---------
                byte[] b_temp_byte1 = new byte[1];
                Loco.ReadByteToBuffer(0x08DD5B05, 15, b_temp_byte1); // 0..255

                // --------- Манометры (double) -> scale -> out ---------
                // НМ  -> out[29..30]
                Loco.ReadScaledDoubleToBuffer(sig_pos_pnevm + 0x58, 29, Loco.i_pnevmo_convert);
                // ТМ  -> out[31..32]
                Loco.ReadScaledDoubleToBuffer(sig_pos_pnevm + 0x30, 31, Loco.i_pnevmo_convert);
                // УР  -> out[33..34]
                Loco.ReadScaledDoubleToBuffer(sig_pos_pnevm + 0xA8, 33, Loco.i_pnevmo_convert);
                // ТЦ  -> out[35..36]
                Loco.ReadScaledDoubleToBuffer(sig_pos_pnevm + 0x80, 35, Loco.i_pnevmo_convert);

                // --------- Лампы ---------

                // ТЦ SIM (порог 0.3) -> out[37]
                    var buf1 = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
                    double d = BitConverter.ToDouble(buf1, 0);
                    Loco.out_buffer[37] = (byte)(d <= 0.3 ? 0 : 1);

                // ЭПТ О SIM -> out[38]
               // Loco.WriteBoolFromByteAddr(0x08BEB4D1, 38);

                // ЭПТ «П» SIM -> out[39]
                    byte b_ept = Loco.ReadByteValue(0x08BEB4D1);  // контроль ЭПТ
                    byte b_kran = Loco.ReadByteValue(0x0032CBD0);  // кран 395
                    bool eptP = (b_ept == 1) &&
                                (b_kran == 2 || b_kran == 3 ||
                                 b_temp_byte1[0] == 255 || b_temp_byte1[0] == 254 || b_temp_byte1[0] == 253);
                    Loco.out_buffer[39] = (byte)(eptP ? 1 : 0);

                // ЭПТ «Т» SIM -> out[40]
                    b_ept = Loco.ReadByteValue(0x08BEB4D1);
                    b_kran = Loco.ReadByteValue(0x0032CBD0);
                    bool eptT = (b_ept == 1) &&
                                (b_kran == 4 || b_kran == 5 || b_kran == 6 ||
                                 b_temp_byte1[0] == 252 || b_temp_byte1[0] == 251);
                    Loco.out_buffer[40] = (byte)(eptT ? 1 : 0);

                // ПР ZTE -> out[41]
                //Loco.WriteBoolFromByteAddr(0x0145FFFB, 41);

                // ВЦ ZTE -> out[42]
                //Loco.WriteBoolFromByteAddr(0x0145FFFC, 42);

                // EPK_State SIM -> out[43]
                    b = Loco.read_bytes((int)Loco.BA + 0x07905ACC, 4);
                    i32 = BitConverter.ToInt32(b, 0) + 0x38;
                    Loco.out_buffer[43] = (byte)(i32 != 0 ? 0 : 1);

                // ЛК ZTE -> out[44]
                //Loco.WriteBoolFromByteAddr(0x0145FFF8, 44);

                // ЛК SIM (используем НОВЫЙ адрес контроллера из кэша)
                    byte ctrl = b_temp_byte1[0]; // 0x08DD5B05
                    if (ctrl == 0)
                    {
                        Loco.i_lampa_LK = 0;
                        Loco.i_lampa_LK_flag = 0;
                        Form1.i_lampa_LK_sec_flag = 0;
                    }
                    if ((ctrl == 1 || ctrl == 255) &&
                        Loco.i_lampa_LK_flag == 0 &&
                        Loco.out_buffer[50] == 0 && Loco.out_buffer[51] == 0)
                    {
                        Loco.i_lampa_LK = 1;
                        Loco.i_lampa_LK_flag = 1;
                    }
                    Loco.out_buffer[45] = (byte)Loco.i_lampa_LK;

                // Двери SIM (обновляем флаг состояния)
                    byte left = Loco.ReadByteValue(0x08BE7C28);
                    byte right = Loco.ReadByteValue(0x08BE7C29);
                    Loco.i_dvery_current = (left == 1 && right == 1) ? 1 : 0;

                // Двери ZTE -> out[47]
                //Loco.WriteBoolFromByteAddr(0x0145FFFA, 47);

                // Лампа О ZTE -> out[48]
                //Loco.WriteBoolFromByteAddr(0x0145FFFF, 48);

                // Лампа РН ZTE -> out[49]
                //Loco.WriteBoolFromByteAddr(0x0145FFF6, 49);

                // Лампа РН SIM -> out[50] (если КС есть -> 0, иначе 1)
                    bool ksOn = (Loco.out_buffer[13] != 0 || Loco.out_buffer[14] != 0);
                    Loco.out_buffer[50] = (byte)(ksOn ? 0 : 1);

                // БВ SIM -> out[51] (0x08DB92AE как у тебя)
                    byte bv = Loco.ReadByteValue(0x08DB92AE);
                    Loco.out_buffer[51] = (byte)(bv != 0 ? 0 : 1);

                // БВ ZTE -> out[52]
                //Loco.WriteBoolFromByteAddr(0x0145FFF7, 52);

                // Лампа K ZTE -> out[53]
                //Loco.WriteBoolFromByteAddr(0x0145FFF9, 53);

                // Лампа РБ Бокс. SIM -> out[54]
                //Loco.WriteBoolFromByteAddr(0x003759DC, 54);

                // СОТ ZTE -> out[55]
                //Loco.WriteBoolFromByteAddr(0x0145FFFD, 55);

                // СОТx ZTE -> out[56]
                //Loco.WriteBoolFromByteAddr(0x0145FFFE, 56);

                // Тяга ZTE / ЭДТ ZTE — адресов нет → выставляем 0, чтобы не тащить «старый» b_temp1
                Loco.out_buffer[27] = 0; // TODO: указать верные адреса
                Loco.out_buffer[28] = 0; // TODO: указать верные адреса

                // «заглушки», как у тебя
                Loco.out_buffer[8] = 0; Loco.out_buffer[9] = 0;
                Loco.out_buffer[17] = 0; Loco.out_buffer[18] = 0;
                Loco.out_buffer[19] = 0; Loco.out_buffer[20] = 0;
                Loco.out_buffer[21] = 0; Loco.out_buffer[22] = 0;
                Loco.out_buffer[23] = 0; Loco.out_buffer[24] = 0;
                Loco.out_buffer[25] = 0;
                Loco.out_buffer[26] = 0;

                return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Электропоезд ЭД9М"
        //-----------------------------------------------------------------------------------
        public static byte[] read_ed9m_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);

            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);

            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);

            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];

            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);

            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);

            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);

            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);

            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);

            //напруга float
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB92C0, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            d_temp = Math.Abs(d_temp);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_napruga_ks_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);

            //контроллер поз.
            byte[] b_temp_byte1 = new byte[1] { 0 };
            b_temp_byte1 = Loco.read_bytes((Int32)Loco.BA + 0x08DB92BD, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);

            //напруга тд
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);

            //ток1
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);

            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);

            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);

            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);

            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);

            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);

            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);

            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);

            //эпт О
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            byte b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 1)
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);

            //эпт П
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            byte b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 2 || b_temp2 == 3 || b_temp_byte1[0] == 255 || b_temp_byte1[0] == 254 || b_temp_byte1[0] == 253))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);

            //эпт Т
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB4D1, 1); //читаем эпт контроль
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBD0, 1); //читаем кран 395
            b_temp2 = Loco.temp_buffer[0];
            if (b_temp1 == 1 && (b_temp2 == 4 || b_temp2 == 5 || b_temp2 == 6 || b_temp_byte1[0] == 252 || b_temp_byte1[0] == 251))
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            else
                Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);

            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);

            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);


            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //ЛК
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB92BD, 1); //читаем контроллер
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 == 0)//если 0, сбрасываем флаги
            {
                Loco.i_lampa_LK = 0;
                Loco.i_lampa_LK_flag = 0;
                Form1.i_lampa_LK_sec_flag = 0;
            }
            //если х1 или т1 после 0 и не вышел таймер 500ms, зажигаем ЛК
            if ((b_temp1 == 1 || b_temp1 == 255) && Loco.i_lampa_LK_flag == 0
                 && Loco.out_buffer[50] == 0 && Loco.out_buffer[51] == 0)
            {
                Loco.i_lampa_LK = 1;
                Loco.i_lampa_LK_flag = 1;
            }
            Loco.temp_buffer = BitConverter.GetBytes(Loco.i_lampa_LK);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //двери
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BE7C28, 1);
            b_temp1 = Loco.temp_buffer[0];
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BE7C29, 1);
            b_temp2 = Loco.temp_buffer[0];

            if (b_temp1 == 1 && b_temp2 == 1)
            {
                Loco.i_dvery_current = 1;
            }

            if (b_temp1 == 0 || b_temp2 == 0)
            {
                Loco.i_dvery_current = 0;
            }

            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);

            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);

            //вент3
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);

            //РП850
            if (Loco.out_buffer[13] != 0 || Loco.out_buffer[14] != 0)
            {
                Loco.temp_buffer = new byte[] { 0 };
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
            {
                Loco.temp_buffer = new byte[] { 1 };
            }
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);

            //БВ
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08DB92BE, 1); //читаем БВ
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0) Loco.temp_buffer = new byte[] { 0 };
            else Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);

            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);

            //РК пром.1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);

            //песок
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x003759DC, 1); //читаем Боксование
            b_temp1 = Loco.temp_buffer[0];
            if (b_temp1 != 0) Loco.temp_buffer = new byte[] { 1 };
            else Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);

            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }

        //-----------------------------------------------------------------------------------
        // Функция читает из памяти игры параметры локомотива "Маневровый тепловоз ТЭМ18"
        //-----------------------------------------------------------------------------------
        public static byte[] read_tem18_V55_008(int sig_pos_pnevm = 0, int sig_pos_elektro = 0)
        {
            //статические
            //скорость доп.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CA78, 2);
            Form1.i_skor_dop = BitConverter.ToInt16(Loco.temp_buffer, 0);
            //скорость тек.
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            float f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            UInt16 i_temp = Convert.ToUInt16(f_temp * Loco.i_skor_tek_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 2, 2);
            //АЛС
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB744, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 4, 1);
            //бдительность
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x0032CBC0, 1);
            Loco.i_bdit_current = Loco.temp_buffer[0];
            //скор тек 2
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCE0, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp);
            Form1.i_skor_tek = i_temp;
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 6, 2);
            //ток эпт
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 8, 2);
            //час
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8AC, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 10, 1);
            //минута
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B0, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 11, 1);
            //секунда
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x08BEB8B4, 1);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 12, 1);
            //напруга float
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 13, 2);
            //контроллер поз.
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 15, 2);
            //напруга тд
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FA38, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_napruga_td_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 17, 2);
            //ток1
            Loco.temp_buffer = Loco.read_bytes((Int32)Loco.BA + 0x04F6FCC8, 4);
            f_temp = BitConverter.ToSingle(Loco.temp_buffer, 0);
            f_temp = Math.Abs(f_temp);
            i_temp = Convert.ToUInt16(f_temp / Loco.i_tok_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 19, 2);
            //ток2
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 21, 2);
            //ток3
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 23, 2);
            //ток4
            Loco.temp_buffer = new byte[] { 0, 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 25, 2);
            //РК поз1
            Loco.temp_buffer = new byte[] { 255, 255 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 27, 2);

            //динамические
            //НМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x80, 8);
            double d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 29, 2);
            //ТМ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x58, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 31, 2);
            //УР float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0x30, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 33, 2);
            //ТЦ float
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8);
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            i_temp = Convert.ToUInt16(d_temp * Loco.i_pnevmo_convert);
            Loco.temp_buffer = BitConverter.GetBytes(i_temp);
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 35, 2);

            //Лампы
            //тц
            Loco.temp_buffer = Loco.read_bytes(sig_pos_pnevm + 0xa8, 8); //читаем давление тц
            d_temp = BitConverter.ToDouble(Loco.temp_buffer, 0);
            if (d_temp <= 0.3)
            {
                Loco.temp_buffer = new byte[] { 0 };
            }
            else
                Loco.temp_buffer = new byte[] { 1 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 37, 1);
            //эпт О
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 38, 1);
            //эпт П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 39, 1);
            //эпт Т
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 40, 1);

            //РК С
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 41, 1);
            //РК СП
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 42, 1);
            //РК П
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 43, 1);

            //лампы 3 круг
            //утечка ТМ
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 44, 1);

            //тд
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 45, 1);

            //вв откр1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 46, 1);
            //вспом. комп.
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 47, 1);
            //комп2
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 48, 1);
            //вент3
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 49, 1);
            //850
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 50, 1);
            //земля1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 51, 1);
            //РК 0_1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 52, 1);
            //РК пром. 1
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 53, 1);
            //песок
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 54, 1);
            //насос
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 55, 1);

            //ход прав
            Loco.temp_buffer = new byte[] { 0 };
            Array.Copy(Loco.temp_buffer, 0, Loco.out_buffer, 56, 1);
            return Loco.out_buffer;
        }
        //-----------------------------------------------------------------------------------
    }
}
