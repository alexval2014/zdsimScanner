using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace zdsimScanner
{
    public partial class Form2 : Form
    {
        //============================================================================
        // Для работы button_zdsim_sbros_sound_Click()
        //============================================================================
        private sealed class LocoSoundBinding
        {
            public string UiName;                         // "Controls", "2ES5K", ...
            public string[] WavPathBuffer;                // Form1.*_wav_path_key_buffer
            public StringCollection WavSettings; // Properties.Settings.Default.sb_*_wav_path_data_settings
        }

        private Dictionary<string, LocoSoundBinding> _soundByLocoName;

        private void EnsureSoundBindings()
        {
            if (_soundByLocoName != null) return;

            _soundByLocoName = new Dictionary<string, LocoSoundBinding>(StringComparer.Ordinal)
            {
                ["Controls"] = new LocoSoundBinding
                {
                    UiName = "Controls",
                    WavPathBuffer = Form1.controls_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_controls_wav_path_data_settings
                },
                ["Neshtatki"] = new LocoSoundBinding
                {
                    UiName = "Neshtatki",
                    WavPathBuffer = Form1.neshtatki_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_neshtatki_wav_path_data_settings
                },
                ["2ES5K"] = new LocoSoundBinding
                {
                    UiName = "2ES5K",
                    WavPathBuffer = Form1.es5k_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_es5k_wav_path_data_settings
                },
                ["EP1M"] = new LocoSoundBinding
                {
                    UiName = "EP1M",
                    WavPathBuffer = Form1.ep1m_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_ep1m_wav_path_data_settings
                },
                ["CHS2K"] = new LocoSoundBinding
                {
                    UiName = "CHS2K",
                    WavPathBuffer = Form1.chs2k_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs2k_wav_path_data_settings
                },
                ["CHS4"] = new LocoSoundBinding
                {
                    UiName = "CHS4",
                    WavPathBuffer = Form1.chs4_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs4_wav_path_data_settings
                },
                ["CHS4 KVR"] = new LocoSoundBinding
                {
                    UiName = "CHS4 KVR",
                    WavPathBuffer = Form1.chs4kvr_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings
                },
                ["CHS4T"] = new LocoSoundBinding
                {
                    UiName = "CHS4T",
                    WavPathBuffer = Form1.chs4t_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs4t_wav_path_data_settings
                },
                ["CHS7"] = new LocoSoundBinding
                {
                    UiName = "CHS7",
                    WavPathBuffer = Form1.chs7_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs7_wav_path_data_settings
                },
                ["CHS8"] = new LocoSoundBinding
                {
                    UiName = "CHS8",
                    WavPathBuffer = Form1.chs8_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_chs8_wav_path_data_settings
                },
                ["VL11M"] = new LocoSoundBinding
                {
                    UiName = "VL11M",
                    WavPathBuffer = Form1.vl11_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_vl11_wav_path_data_settings
                },
                ["VL82M"] = new LocoSoundBinding
                {
                    UiName = "VL82M",
                    WavPathBuffer = Form1.vl82_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_vl82_wav_path_data_settings
                },
                ["VL80T"] = new LocoSoundBinding
                {
                    UiName = "VL80T",
                    WavPathBuffer = Form1.vl80t_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_vl80t_wav_path_data_settings
                },
                ["VL85"] = new LocoSoundBinding
                {
                    UiName = "VL85",
                    WavPathBuffer = Form1.vl85_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_vl85_wav_path_data_settings
                },
                ["TEP70"] = new LocoSoundBinding
                {
                    UiName = "TEP70",
                    WavPathBuffer = Form1.tep70_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_tep70_wav_path_data_settings
                },
                ["2TE10U"] = new LocoSoundBinding
                {
                    UiName = "2TE10U",
                    WavPathBuffer = Form1.te10u_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_te10u_wav_path_data_settings
                },
                ["M62"] = new LocoSoundBinding
                {
                    UiName = "M62",
                    WavPathBuffer = Form1.m62_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_m62_wav_path_data_settings
                },
                ["ED4M"] = new LocoSoundBinding
                {
                    UiName = "ED4M",
                    WavPathBuffer = Form1.ed4m_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_ed4m_wav_path_data_settings
                },
                ["ED9M"] = new LocoSoundBinding
                {
                    UiName = "ED9M",
                    WavPathBuffer = Form1.ed9m_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_ed9m_wav_path_data_settings
                },
                ["tem18"] = new LocoSoundBinding
                {
                    UiName = "tem18",
                    WavPathBuffer = Form1.tem18_wav_path_key_buffer,
                    WavSettings = Properties.Settings.Default.sb_tem18_wav_path_data_settings
                },
            };
        }

        //=====================================================================
        // Сохранение отрисовки звуков
        //=====================================================================
        private void sbBufferWavPathSave()
        {
            EnsureSoundBindings();

            foreach (var b in _soundByLocoName.Values)
            {
                b.WavSettings.Clear();
                for (int i = 0; i < b.WavPathBuffer.Length; i++)
                    b.WavSettings.Add(b.WavPathBuffer[i]);
            }
        }


    }
}