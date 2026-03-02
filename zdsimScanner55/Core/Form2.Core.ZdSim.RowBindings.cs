using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace zdsimScanner
{
    public partial class Form2 : Form
    {
        //============================================================================
        // Для работы с button_zdsim_sbros_key_Click()
        //============================================================================
        private sealed class LocoRowBindingDef
        {
            public int[] KeyBuffer;                      // Form1.*_key_buffer
            public int[,] AxisBuffer;                    // Form1.*_axis_buffer (N x 2)
            public string[] SbAxisData;                  // sb_*_axis_data

            public StringCollection KeySettings;   // *_buffer_key_settings
            public StringCollection AxisSettings1; // *_buffer_axis_settings
            public StringCollection AxisSettings2; // *_buffer_axis_settings2
        }

        private Dictionary<string, LocoRowBindingDef> _locoRowBindingByName;

        private void EnsureLocoRowBindingDefs()
        {
            if (_locoRowBindingByName != null) return;

            _locoRowBindingByName = new Dictionary<string, LocoRowBindingDef>(StringComparer.Ordinal)
            {
                ["Controls"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.Controls_key_buffer,
                    AxisBuffer = Form1.Controls_axis_buffer,
                    SbAxisData = sb_controls_axis_data,
                    KeySettings = Properties.Settings.Default.controls_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.controls_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.controls_buffer_axis_settings2,
                },
                ["Neshtatki"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.Neshtatki_key_buffer,
                    AxisBuffer = Form1.Neshtatki_axis_buffer,
                    SbAxisData = sb_neshtatki_axis_data,
                    KeySettings = Properties.Settings.Default.neshtatki_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.neshtatki_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.neshtatki_buffer_axis_settings2,
                },
                ["2ES5K"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.ES5K_key_buffer,
                    AxisBuffer = Form1.ES5K_axis_buffer,
                    SbAxisData = sb_es5k_axis_data,
                    KeySettings = Properties.Settings.Default.es5k_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.es5k_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.es5k_buffer_axis_settings2,
                },
                ["EP1M"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.EP1M_key_buffer,
                    AxisBuffer = Form1.EP1M_axis_buffer,
                    SbAxisData = sb_ep1m_axis_data,
                    KeySettings = Properties.Settings.Default.ep1m_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.ep1m_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.ep1m_buffer_axis_settings2,
                },
                ["CHS2K"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS2K_key_buffer,
                    AxisBuffer = Form1.CHS2K_axis_buffer,
                    SbAxisData = sb_chs2k_axis_data,
                    KeySettings = Properties.Settings.Default.chs2k_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs2k_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs2k_buffer_axis_settings2,
                },
                ["CHS4"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS4_key_buffer,
                    AxisBuffer = Form1.CHS4_axis_buffer,
                    SbAxisData = sb_chs4_axis_data,
                    KeySettings = Properties.Settings.Default.chs4_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs4_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs4_buffer_axis_settings2,
                },
                ["CHS4 KVR"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS4KVR_key_buffer,
                    AxisBuffer = Form1.CHS4KVR_axis_buffer,
                    SbAxisData = sb_chs4kvr_axis_data,
                    KeySettings = Properties.Settings.Default.chs4kvr_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs4kvr_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs4kvr_buffer_axis_settings2,
                },
                ["CHS4T"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS4T_key_buffer,
                    AxisBuffer = Form1.CHS4T_axis_buffer,
                    SbAxisData = sb_chs4t_axis_data,
                    KeySettings = Properties.Settings.Default.chs4t_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs4t_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs4t_buffer_axis_settings2,
                },
                ["CHS7"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS7_key_buffer,
                    AxisBuffer = Form1.CHS7_axis_buffer,
                    SbAxisData = sb_chs7_axis_data,
                    KeySettings = Properties.Settings.Default.chs7_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs7_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs7_buffer_axis_settings2,
                },
                ["CHS8"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.CHS8_key_buffer,
                    AxisBuffer = Form1.CHS8_axis_buffer,
                    SbAxisData = sb_chs8_axis_data,
                    KeySettings = Properties.Settings.Default.chs8_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.chs8_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.chs8_buffer_axis_settings2,
                },
                ["VL11M"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.VL11M_key_buffer,
                    AxisBuffer = Form1.VL11M_axis_buffer,
                    SbAxisData = sb_vl11_axis_data,
                    KeySettings = Properties.Settings.Default.vl11_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.vl11_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.vl11_buffer_axis_settings2,
                },
                ["VL82M"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.VL82M_key_buffer,
                    AxisBuffer = Form1.VL82M_axis_buffer,
                    SbAxisData = sb_vl82_axis_data,
                    KeySettings = Properties.Settings.Default.vl82_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.vl82_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.vl82_buffer_axis_settings2,
                },
                ["VL80T"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.VL80T_key_buffer,
                    AxisBuffer = Form1.VL80T_axis_buffer,
                    SbAxisData = sb_vl80t_axis_data,
                    KeySettings = Properties.Settings.Default.vl80t_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.vl80t_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.vl80t_buffer_axis_settings2,
                },
                ["VL85"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.VL85_key_buffer,
                    AxisBuffer = Form1.VL85_axis_buffer,
                    SbAxisData = sb_vl85_axis_data,
                    KeySettings = Properties.Settings.Default.vl85_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.vl85_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.vl85_buffer_axis_settings2,
                },
                ["TEP70"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.TEP70_key_buffer,
                    AxisBuffer = Form1.TEP70_axis_buffer,
                    SbAxisData = sb_tep70_axis_data,
                    KeySettings = Properties.Settings.Default.tep70_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.tep70_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.tep70_buffer_axis_settings2,
                },
                ["2TE10U"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.TE10U_key_buffer,
                    AxisBuffer = Form1.TE10U_axis_buffer,
                    SbAxisData = sb_te10u_axis_data,
                    KeySettings = Properties.Settings.Default.te10u_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.te10u_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.te10u_buffer_axis_settings2,
                },
                ["M62"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.M62_key_buffer,
                    AxisBuffer = Form1.M62_axis_buffer,
                    SbAxisData = sb_m62_axis_data,
                    KeySettings = Properties.Settings.Default.m62_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.m62_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.m62_buffer_axis_settings2,
                },
                ["ED4M"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.ED4M_key_buffer,
                    AxisBuffer = Form1.ED4M_axis_buffer,
                    SbAxisData = sb_ed4m_axis_data,
                    KeySettings = Properties.Settings.Default.ed4m_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.ed4m_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.ed4m_buffer_axis_settings2,
                },
                ["ED9M"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.ED9M_key_buffer,
                    AxisBuffer = Form1.ED9M_axis_buffer,
                    SbAxisData = sb_ed9m_axis_data,
                    KeySettings = Properties.Settings.Default.ed9m_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.ed9m_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.ed9m_buffer_axis_settings2,
                },
                ["tem18"] = new LocoRowBindingDef
                {
                    KeyBuffer = Form1.tem18_key_buffer,
                    AxisBuffer = Form1.tem18_axis_buffer,
                    SbAxisData = sb_tem18_axis_data,
                    KeySettings = Properties.Settings.Default.tem18_buffer_key_settings,
                    AxisSettings1 = Properties.Settings.Default.tem18_buffer_axis_settings,
                    AxisSettings2 = Properties.Settings.Default.tem18_buffer_axis_settings2,
                },
            };
        }

        //============================================================================
        // Единая функция «удалить привязку в строке»
        //============================================================================
        private void ClearBindingRow(LocoRowBindingDef def, int rowIndex)
        {
            if (def == null) return;
            if (rowIndex < 0) return;

            // Буферы
            if (def.KeyBuffer != null && (uint)rowIndex < (uint)def.KeyBuffer.Length)
                def.KeyBuffer[rowIndex] = 0;

            if (def.AxisBuffer != null &&
                (uint)rowIndex < (uint)def.AxisBuffer.GetLength(0) &&
                def.AxisBuffer.GetLength(1) >= 2)
            {
                def.AxisBuffer[rowIndex, 0] = 0;
                def.AxisBuffer[rowIndex, 1] = 0;
            }

            if (def.SbAxisData != null && (uint)rowIndex < (uint)def.SbAxisData.Length)
                def.SbAxisData[rowIndex] = null;

            // Settings: НЕ чистим всю коллекцию, только гарантируем длину и пишем по индексу
            if (def.KeySettings != null)
            {
                int need = def.KeyBuffer?.Length ?? 0;
                EnsureSizeAtLeastFillZeros(def.KeySettings, need);
                if ((uint)rowIndex < (uint)def.KeySettings.Count) def.KeySettings[rowIndex] = "0";
            }

            if (def.AxisSettings1 != null)
            {
                int need = def.AxisBuffer?.GetLength(0) ?? 0;
                EnsureSizeAtLeastFillZeros(def.AxisSettings1, need);
                if ((uint)rowIndex < (uint)def.AxisSettings1.Count) def.AxisSettings1[rowIndex] = "0";
            }

            if (def.AxisSettings2 != null)
            {
                int need = def.AxisBuffer?.GetLength(0) ?? 0;
                EnsureSizeAtLeastFillZeros(def.AxisSettings2, need);
                if ((uint)rowIndex < (uint)def.AxisSettings2.Count) def.AxisSettings2[rowIndex] = "0";
            }
        }

    }
}