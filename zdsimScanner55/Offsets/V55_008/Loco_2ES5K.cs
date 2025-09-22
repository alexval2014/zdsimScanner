namespace zdsimScanner.Offsets.V55_008
{
    /// <summary>
    /// Офсеты памяти для локомотива 2ЭС5К в версии симулятора V55.008.
    /// Все константы подписаны, чтобы упростить поддержку.
    /// </summary>
    internal static class Loco_2ES5K
    {
        // -----------------------------
        // Скорости
        // -----------------------------
        public const int SpeedDop = 0x0032CA78;   // дополнительная скорость
        public const int SpeedTek = 0x04F6FCE0;   // текущая скорость (float)

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
        public const int NaprugaKS = 0x08CF3F30;  // Напряжение контактной сети (double)
        public const int NaprugaTD = 0x04F6FA38;  // Напряжение ТД (float)
        public const int Tok1 = 0x04F6FCC8;       // Ток 1 (float)

        // -----------------------------
        // Динамические давления (sig_pos_pnevm)
        // -----------------------------
        public const int PNM = 0x80;   // НМ (float)
        public const int PTM = 0x58;   // ТМ (float)
        public const int PUR = 0x30;   // УР (float)
        public const int PTC = 0xA8;   // ТЦ (float)

        // -----------------------------
        // Электрооборудование (sig_pos_elektro)
        // -----------------------------
        public const int Lamp_TMLeak = 0x39C4;   // Лампа: утечка ТМ
        public const int Lamp_C1 = 0x614D;       // Лампа: С1
        public const int Lamp_C2 = 0x6229;       // Лампа: С2
        public const int Lamp_Vent1 = 0x6BC8;    // Лампа: вентилятор 1
        public const int Lamp_GV_Right = 0x64E8; // Лампа: ГВ правый
    }
}