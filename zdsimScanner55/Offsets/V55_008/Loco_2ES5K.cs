using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdsimScanner.Offsets.V55_008
{
    /// <summary>
    /// Офсеты памяти для локомотива 2ЭС5К в версии симулятора V55.008
    /// </summary>
    internal static class Loco_2ES5K
    {
        public const int SpeedAddress = 0x0032CBD4;
        public const int PressureAddress = 0x0045AB12;
        public const int BrakeHandleAddress = 0x0078FF20;
    }
}
