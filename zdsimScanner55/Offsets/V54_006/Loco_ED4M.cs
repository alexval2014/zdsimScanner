using System;
using System.Text.RegularExpressions;
using zdsimScanner;

namespace zdsimScanner.Offsets.V54_006
{
    /// <summary>
    /// Офсеты памяти для локомотива ED4M в версии симулятора V54.006.
    /// Все константы подписаны, чтобы упростить поддержку.
    /// </summary>
    internal static class Loco_ED4M
    {
        // -----------------------------
        // Скорости
        // -----------------------------
        public const int SpeedDop = 0x0032CA78;   // Ограничение скорости (int)
        public const int SpeedTek = 0x04F6FCE0;   // Текущая скорость (float)

        // -----------------------------
        // Сигналы безопасности
        // -----------------------------
        public const int ALS = 0x08BEB744;        // АЛС
        public const int Bditelnost = 0x0032CBC0; // Бдительность

        // -----------------------------
        // Время
        // -----------------------------
        public const int TimeHour = 0x08BEB8AC;   // Часы
        public const int TimeMinute = 0x08BEB8B0; // Минуты
        public const int TimeSecond = 0x08BEB8B4; // Секунды

        // -----------------------------
        // Электрические параметры
        // -----------------------------
        public const int NaprugaKS = 0x08DB92B4;  // Напряжение контактной сети (double);
        public const int NaprugaTD = 0x04F6FA38;  // Напряжение ТД (float)
        public const int Tok1 = 0x04F6FCC8;       // Ток 1 (float)

        // -----------------------------
        // Динамические давления (sig_pos_pnevm)
        // -----------------------------
        public const int PNM = 0x80;   // НМ (float)
        public const int PTM = 0x58;   // ТМ (float)
        public const int PUR = 0x30;   // УР (float)
        public const int PTC = 0xA8;   // ТЦ (float)

        public const int ADDR_395 = 0x0032CBD0;                 // Кран 395
        public const int ADDR_254 = 0x003499E4;                 // Кран 254
        public const int ReversState = 0x04F6FA2E;              // Реверсор
        public const int KontrollerState = 0x08DB92B4;          // Положение контроллера
        public const int EPT_Control = 0x08BEB4D1;              // эпт контроль
        public const int ADDR_N_Puti = 0x04F711D0;              // Номер пути
        public const int ADDR_RasstoyanieDoCeli = 0x04F711E2;   // Растояние до цели в метрах
        public const int ADDR_NEXT_OGRANICH = 0x00349880;       // Следующее ограничение скорости из клуба
        public const int ADDR_CelevayaScorostKLUB = 0x0032CA7C; // Целевая скорость клуб
        public const int ADDR_ChastotaKLUB = 0x0032CA98;        // Частота клуб
        public const int ADDR_UscorenieKLUB = 0x0032CAAC;       // Ускорение клуб
        public const int ADDR_RegimRabotuKLUB = 0x0032CA84;     // Режим работы П/М/РДТ клуб
        public const int ADDR_PriznakPutiKLUB = 0x0032CA8C;     // Признак пути ПП/НП клуб
        public const int ADDR_VvodStateKLUB = 0x0032CA88;       // Состояние ввода клуб
        public const int ADDR_Ktorm = 0x0032CB8C;               // Коэффициент торможения
        public const int ADDR_RB = 0x0072CB08;                  // РБ - кнопка Z
        public const int ADDR_RBS = 0x0072CB04;                 // РБС - кнопка М
        public const int ADDR_TIFON = 0x003499DC;               // Тифон
        public const int ADDR_SVISTOK = 0x003499D8;             // Свисток
        //0x07905ACC; //читаем состояние ключа EPK
        public const int ADDR_ORDINATA = 0x003E79A8;            // Координаты поезда
        public const int ADDR_piket_tek = 0x0032CBFC;           // Текущий пикет



        /*
        //координата если ЭК=0,то не горит
        temp_buffer = read_bytes((int) BA + 0x003E79A8, 8);
        double d_temp = BitConverter.ToDouble(temp_buffer, 0);
        MainWindow.i_koordinata = Convert.ToUInt32(d_temp);

        //текущий пикет
        temp_buffer = read_bytes((int) BA + 0x0032CBFC, 4);
        MainWindow.i_piket_tek = BitConverter.ToUInt16(temp_buffer, 0);

        //час
        temp_buffer = read_bytes((int) BA + 0x08BEB8AC, 1);
        MainWindow.i_chas = temp_buffer[0];

        //минута
        temp_buffer = read_bytes((int) BA + 0x08BEB8B0, 1);
        MainWindow.i_minuta = temp_buffer[0];

        //секунда
        temp_buffer = read_bytes((int) BA + 0x08BEB8B4, 1);
        MainWindow.i_secunda = temp_buffer[0];

        //скорость доп.
        temp_buffer = read_bytes((int) BA + 0x0032CA78, 2);
        MainWindow.i_skor_dop = BitConverter.ToInt16(temp_buffer, 0);

        //скорость тек.
        temp_buffer = read_bytes((int) BA + 0x04F6FCE0, 4);
        float f_temp = BitConverter.ToSingle(temp_buffer, 0);
        f_temp = Math.Abs(f_temp);
        MainWindow.i_skor_tek = Convert.ToInt16(f_temp);

        //след.огранич.скор
        temp_buffer = read_bytes((int) BA + 0x0032CA7C, 2);
        MainWindow.i_sled_ogranichenie = BitConverter.ToInt16(temp_buffer, 0);

        //расст.до след.огранич.
        temp_buffer = read_bytes((int) BA + 0x04F711BC, 4);
        f_temp = BitConverter.ToSingle(temp_buffer, 0);
        MainWindow.i_rasst_sled_ogranichenie = Convert.ToInt16(f_temp);

        //маневр./поезд.
        temp_buffer = read_bytes((int) BA + 0x0032CA84, 1);
        MainWindow.i_rezhim_p_m = temp_buffer[0];

        //АЛС
        temp_buffer = read_bytes((int) BA + 0x08BEB744, 1);
        MainWindow.i_ALS = temp_buffer[0];

        //бдительность
        temp_buffer = read_bytes((int) BA + 0x0032CBC0, 1);
        MainWindow.i_bdit = temp_buffer[0];

        //расст.до цели
        temp_buffer = read_bytes((int) BA + 0x08BEB730, 4);
        MainWindow.i_rasst_do_cely = BitConverter.ToInt16(temp_buffer, 0);

        //имя светофора
        baze_address_temp = read_pointer((int) BA + 0x0032B12C, new int[2] { 0x08, 0x3c });
        temp_buffer = read_bytes(baze_address_temp, 4);
        MainWindow.s_svetofor_name = System.Text.Encoding.GetEncoding("windows-1251").GetString(temp_buffer);
        if (MainWindow.s_svetofor_name.IndexOf('\0') != -1)
        MainWindow.s_svetofor_name = MainWindow.s_svetofor_name.Remove(MainWindow.s_svetofor_name.IndexOf('\0'));
        MainWindow.s_svetofor_name = Regex.Replace(MainWindow.s_svetofor_name, "\0", "");

        //ЭК
        temp_buffer = read_bytes((int) BA + 0x04F711D1, 1);
        MainWindow.i_EK = temp_buffer[0];

        //частота
        temp_buffer = read_bytes((int) BA + 0x0032CA98, 1);
        MainWindow.i_chastota = temp_buffer[0];

        //номер пути
        temp_buffer = read_bytes((int) BA + 0x04F711D0, 1);
        MainWindow.i_path_number = temp_buffer[0];

        //путь прав./неправ.
        temp_buffer = read_bytes((int) BA + 0x04F711D1, 1);
        MainWindow.i_path_prav = temp_buffer[0];

        //ускорение
        temp_buffer = read_bytes((int) BA + 0x0032CAAC, 8);
        MainWindow.d_uskorenie = BitConverter.ToDouble(temp_buffer, 0);

        //строка команд
        baze_address_temp = read_pointer((int) BA + 0x0032B14C, new int[2] { 0x08, 0x13 });
        temp_buffer = read_bytes(baze_address_temp, 16);
        MainWindow.s_stroka_command = System.Text.Encoding.GetEncoding("windows-1251").GetString(temp_buffer);
        if (MainWindow.s_stroka_command.IndexOf('\0') != -1)
        MainWindow.s_stroka_command = MainWindow.s_stroka_command.Remove(MainWindow.s_stroka_command.IndexOf('\0'));
        MainWindow.s_stroka_command = Regex.Replace(MainWindow.s_stroka_command, "\0", "");

        //ТМ
        temp_buffer = read_bytes(sig_pos_pnevm + 0x30, 8);
        MainWindow.d_TM = BitConverter.ToDouble(temp_buffer, 0);

        //УР
        temp_buffer = read_bytes(sig_pos_pnevm + 0xa8, 8);
        MainWindow.d_UR = BitConverter.ToDouble(temp_buffer, 0);

        //ТЦ
        temp_buffer = read_bytes(sig_pos_pnevm + 0x80, 8);
        MainWindow.d_TC = BitConverter.ToDouble(temp_buffer, 0);

        //читаем реверс
        temp_buffer = read_bytes((int)BA + 0x04F6FA2E, 1);
        int i_temp = temp_buffer[0];
        if (i_temp == 0) MainWindow.i_revers = 0;   // 0
        if (i_temp == 1) MainWindow.i_revers = 1;   // вперед
        if (i_temp == 255) MainWindow.i_revers = 2; // назад
*/











        // -----------------------------
        // Электрооборудование (sig_pos_elektro)
        // -----------------------------
        public const int Lamp_TMLeak = 0x39C4;    // Лампа: утечка ТМ
        public const int Lamp_C1 = 0x614D;        // Лампа: С1
        public const int Lamp_C2 = 0x6229;        // Лампа: С2
        public const int Lamp_Vent1 = 0x6BC8;     // Лампа: вентилятор 1
        public const int Lamp_GV_Right = 0x64E8;  // Лампа: ГВ правый

        public const int ADDR_R_DOOR = 0x08BE7C28; // Лампа: Правая дверь
        public const int ADDR_L_DOOR = 0x08BE7C29; // Лампа: Левая дверь
        public const int ADDR_TP = 0x08DB92AF;     // Лампа: Пантограф (состояние)
        public const int ADDR_BV = 0x08DB92AE;     // Лампа: БВ(вкл/выкл)
        public const int ADDR_Box = 0x003759DC;    // Лампа: Боксование
    }
}