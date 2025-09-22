using System.Collections.Generic;
using zdsimScanner.Models;

namespace zdsimScanner.Models
{
    public static class LocoSignatures
    {
        public static readonly Dictionary<int, LocoType> Signatures = new Dictionary<int, LocoType>
        {
             { 23152,  LocoType.ES5K },
             { 31714,  LocoType.EP1m },
             { 343,    LocoType.CHS2K },
             { 523,    LocoType.CHS4 },
             { 524,    LocoType.CHS4KVR },
             { 621,    LocoType.CHS4T },
             { 822,    LocoType.CHS7 },
             { 812,    LocoType.CHS8 },
             { 811,    LocoType.VL11m },
             { 882,    LocoType.VL82m },
             { 880,    LocoType.VL80t },
             { 885,    LocoType.VL85 },
             { 2070,   LocoType.TEP70 },
             { 21014,  LocoType.TE10U },
             { 1462,   LocoType.M62 },
             { 3154,   LocoType.ED4M },
             { 3159,   LocoType.ED9M },
             { 201318, LocoType.TEM18}
        };

        public static readonly Dictionary<LocoType, string> Names = new Dictionary<LocoType, string>
        {
             { LocoType.ES5K, "2ES5K" },
             { LocoType.EP1m, "EP1m" },
             { LocoType.CHS2K, "CHS2K" },
             { LocoType.CHS4, "CHS4" },
             { LocoType.CHS4KVR, "CHS4 KVR" },
             { LocoType.CHS4T, "CHS4t" },
             { LocoType.CHS7, "CHS7" },
             { LocoType.CHS8, "CHS8" },
             { LocoType.VL11m, "VL11m" },
             { LocoType.VL82m, "VL82m" },
             { LocoType.VL80t, "VL80t" },
             { LocoType.VL85, "VL85" },
             { LocoType.TEP70, "TEP70" },
             { LocoType.TE10U, "2TE10U" },
             { LocoType.M62, "M62" },
             { LocoType.ED4M, "ED4M" },
             { LocoType.ED9M, "ED9M" },
             { LocoType.TEM18, "TEM18dm" }
        };
    }
}
