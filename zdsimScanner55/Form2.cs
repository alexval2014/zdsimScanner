using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;
using SysAction = System.Action;
using System.Collections.Specialized;
using System.Collections.Generic;
// если вдруг тебе понадобится DirectInput.Action, можно так:
// using DiAction = Microsoft.DirectX.DirectInput.Action;

namespace zdsimScanner
{    
    public partial class Form2 : Form
    {
        public int i_temp_row_number_f2;
        public int i_temp_column_number_f2;
        public int i_temp_loco_select;
        public int i_temp_datagird_select_f2;
        public string s_temp_row_number_f3;       
        public string[] sb_temp_osi_select;
        public static string s_current_loco_select = "";
        public string[] sb_axis_name = new string[]{
        "",
        "ARx ",
        "ARy ",
        "ARz ",
        "AX  ",
        "AY  ",
        "AZ  ",
        "FRx ",
        "FRy ",
        "FRz ",
        "FX  ",
        "FY  ",
        "FZ  ",
        "Rx  ",
        "Ry  ",
        "Rz  ",
        "VRx ",
        "VRy ",
        "VRz ",
        "VX  ",
        "VY  ",
        "VZ  ",
        "X   ",
        "Y   ",
        "Z   ",
        "POV ",
        "Sld ",
        "ASld",
        "FSld",
        "VSld",
        };

        public string[] sb_controls_axis_data = new string[34];
        public string[] sb_neshtatki_axis_data = new string[100];
        public string[] sb_es5k_axis_data = new string[109];
        public string[] sb_ep1m_axis_data = new string[112];
        public string[] sb_chs2k_axis_data = new string[32];
        public string[] sb_chs4_axis_data = new string[55];
        public string[] sb_chs4kvr_axis_data = new string[55];
        public string[] sb_chs4t_axis_data = new string[54];
        public string[] sb_chs7_axis_data = new string[46];
        public string[] sb_chs8_axis_data = new string[63];
        public string[] sb_vl11_axis_data = new string[83];
        public string[] sb_vl82_axis_data = new string[83];
        public string[] sb_vl80t_axis_data = new string[49];
        public string[] sb_vl85_axis_data = new string[82];
        public string[] sb_tep70_axis_data = new string[36];
        public string[] sb_te10u_axis_data = new string[47];
        public string[] sb_m62_axis_data = new string[36];
        public string[] sb_ed4m_axis_data = new string[33];
        public string[] sb_ed9m_axis_data = new string[30];
        public string[] sb_tem18_axis_data = new string[32];

        Device device;

        //============================================================================
        private sealed class AxisUiDef
        {
            public Func<int> Get;          // получить текущее значение (как int)
            public Func<ushort> GetU16;    // значение для progressBar (ushort)
        }

        private Dictionary<string, AxisUiDef> _uiAxis;

        //=====================================================================
        // Сохранение отрисовки осей
        //=====================================================================
        private void EnsureUiAxis()
        {
            if (_uiAxis != null) return;

            _uiAxis = new Dictionary<string, AxisUiDef>(StringComparer.Ordinal)
            {
                // Обычные оси
                ["ARx"] = new AxisUiDef { Get = () => device.CurrentJoystickState.ARx, GetU16 = () => (ushort)device.CurrentJoystickState.ARx },
                ["ARy"] = new AxisUiDef { Get = () => device.CurrentJoystickState.ARy, GetU16 = () => (ushort)device.CurrentJoystickState.ARy },
                ["ARz"] = new AxisUiDef { Get = () => device.CurrentJoystickState.ARz, GetU16 = () => (ushort)device.CurrentJoystickState.ARz },

                ["AX"] = new AxisUiDef { Get = () => device.CurrentJoystickState.AX, GetU16 = () => (ushort)device.CurrentJoystickState.AX },
                ["AY"] = new AxisUiDef { Get = () => device.CurrentJoystickState.AY, GetU16 = () => (ushort)device.CurrentJoystickState.AY },
                ["AZ"] = new AxisUiDef { Get = () => device.CurrentJoystickState.AZ, GetU16 = () => (ushort)device.CurrentJoystickState.AZ },

                ["FRx"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FRx, GetU16 = () => (ushort)device.CurrentJoystickState.FRx },
                ["FRy"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FRy, GetU16 = () => (ushort)device.CurrentJoystickState.FRy },
                ["FRz"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FRz, GetU16 = () => (ushort)device.CurrentJoystickState.FRz },

                ["FX"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FX, GetU16 = () => (ushort)device.CurrentJoystickState.FX },
                ["FY"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FY, GetU16 = () => (ushort)device.CurrentJoystickState.FY },
                ["FZ"] = new AxisUiDef { Get = () => device.CurrentJoystickState.FZ, GetU16 = () => (ushort)device.CurrentJoystickState.FZ },

                ["Rx"] = new AxisUiDef { Get = () => device.CurrentJoystickState.Rx, GetU16 = () => (ushort)device.CurrentJoystickState.Rx },
                ["Ry"] = new AxisUiDef { Get = () => device.CurrentJoystickState.Ry, GetU16 = () => (ushort)device.CurrentJoystickState.Ry },
                ["Rz"] = new AxisUiDef { Get = () => device.CurrentJoystickState.Rz, GetU16 = () => (ushort)device.CurrentJoystickState.Rz },

                ["VRx"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VRx, GetU16 = () => (ushort)device.CurrentJoystickState.VRx },
                ["VRy"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VRy, GetU16 = () => (ushort)device.CurrentJoystickState.VRy },
                ["VRz"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VRz, GetU16 = () => (ushort)device.CurrentJoystickState.VRz },

                ["VX"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VX, GetU16 = () => (ushort)device.CurrentJoystickState.VX },
                ["VY"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VY, GetU16 = () => (ushort)device.CurrentJoystickState.VY },
                ["VZ"] = new AxisUiDef { Get = () => device.CurrentJoystickState.VZ, GetU16 = () => (ushort)device.CurrentJoystickState.VZ },

                ["X"] = new AxisUiDef { Get = () => device.CurrentJoystickState.X, GetU16 = () => (ushort)device.CurrentJoystickState.X },
                ["Y"] = new AxisUiDef { Get = () => device.CurrentJoystickState.Y, GetU16 = () => (ushort)device.CurrentJoystickState.Y },
                ["Z"] = new AxisUiDef { Get = () => device.CurrentJoystickState.Z, GetU16 = () => (ushort)device.CurrentJoystickState.Z },

                // POV — с сохранением твоей “нормализации”
                ["POV"] = new AxisUiDef
                {
                    Get = () =>
                    {
                        int v = device.CurrentJoystickState.GetPointOfView()[0];
                        if (v == -1) v = 65535;
                        if (v == 0) v = 1;
                        return v;
                    },
                    GetU16 = () =>
                    {
                        int v = device.CurrentJoystickState.GetPointOfView()[0];
                        if (v == -1) v = 65535;
                        if (v == 0) v = 1;
                        return (ushort)v;
                    }
                },

                // Слайдеры
                ["Slider"] = new AxisUiDef
                {
                    Get = () => device.CurrentJoystickState.GetSlider()[0],
                    GetU16 = () => (ushort)device.CurrentJoystickState.GetSlider()[0]
                },
                ["ASlider"] = new AxisUiDef
                {
                    Get = () => device.CurrentJoystickState.GetASlider()[0],
                    GetU16 = () => (ushort)device.CurrentJoystickState.GetASlider()[0]
                },
                ["FSlider"] = new AxisUiDef
                {
                    Get = () => device.CurrentJoystickState.GetFSlider()[0],
                    GetU16 = () => (ushort)device.CurrentJoystickState.GetFSlider()[0]
                },
                ["VSlider"] = new AxisUiDef
                {
                    Get = () => device.CurrentJoystickState.GetVSlider()[0],
                    GetU16 = () => (ushort)device.CurrentJoystickState.GetVSlider()[0]
                },
            };
        }

        //============================================================================
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            progressBar_osi.Minimum = 0;
            progressBar_osi.Maximum = 65535;
            // если нужно — чтобы не было “шага” крупными блоками
            // progressBar_osi.Step = 1;

            comboBox_zdsimLoco.Text = comboBox_zdsimLoco.Items[0].ToString();           
            comboBox_osi_select.SelectedIndex = 0;

            this.textBox1.Text = (Properties.Settings.Default.skor_tek);
            this.textBox2.Text = (Properties.Settings.Default.tok_ept);
            this.textBox3.Text = (Properties.Settings.Default.napr_ks);
            this.textBox4.Text = (Properties.Settings.Default.napr_td);
            this.textBox5.Text = (Properties.Settings.Default.tok);
            this.textBox6.Text = (Properties.Settings.Default.pnevmatika);
            this.numericUpDown7.Value = Properties.Settings.Default.step_steper_motor;
            this.numericUpDown8.Value = Properties.Settings.Default.bdit;
            this.numericUpDown_shum.Value = Properties.Settings.Default.joystick_shum;
            if (Form1.i_priem_peredacha == 1)
            {
                radio_priem.Select();
            }
            if (Form1.i_priem_peredacha == 2)
            {
                radio_peredacha.Select();
            }
            if (Form1.i_priem_peredacha == 3)
            {
                radio_priem_peredacha.Select();
            }
            if (Form1.i_dvery_control_off_settings == 1)
            {
                checkBox_dvery_control.Checked = false;
            }
            if (Form1.i_dvery_control_off_settings == 0)
            {
                checkBox_dvery_control.Checked = true;
            }
            if (Form1.i_sound_peredacha == 1)
            {
                checkBox_sound_peredacha.Checked = true;
            }
            if (Form1.i_sound_peredacha == 0)
            {
                checkBox_sound_peredacha.Checked = false;
            }
            Joystick_init();
            if (device == null)
            {
                radio_priem.Select();
                Form1.i_priem_peredacha = 1;
                tabControl1.Enabled = false;
                tabControl1.Visible = false;
                Form1.i_priem_peredacha = 1;
                radio_peredacha.Enabled = false;
                radio_priem_peredacha.Enabled = false;
                textBox_joy_name.Clear();
                textBox_joy_name.ForeColor = Color.Red;
                textBox_joy_name.AppendText("Устройство ввода не обнаружено");
                timer1.Enabled = false;
            }
            else
            {
                if (radio_priem.Checked == true)
                {
                    tabControl1.Enabled = false;
                    tabControl1.Visible = false;
                    Form1.i_priem_peredacha = 1;
                }
                radio_peredacha.Enabled = true;
                radio_priem_peredacha.Enabled = true;
                textBox_joy_name.Clear();
                textBox_joy_name.ForeColor = Color.Gold;
                textBox_joy_name.AppendText("Joy: " + Form1.i_joy_name);
                timer1.Enabled = true;
            }
            sbBufferUpdate();
            KeyDataUpdate();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (device != null)
            {
                UpdateJoystickState();
            }
        }

        //=====================================================================
        // Для работы sbBufferUpdate()
        //=====================================================================
        private sealed class SbMap
        {
            public string[] Runtime;           // sb_controls_axis_data
            public StringCollection Settings;  // Properties.Settings.Default.sb_controls_axis_data_settings
        }

        private static void LoadSbIfNotStart(string[] runtime, StringCollection settings)
        {
            if (runtime == null || settings == null) return;
            if (settings.Count == 0) return;

            // твой маркер "start"
            if (settings[0] == "start") return;

            // безопасно: берём минимум, чтобы не улететь по индексу
            int n = Math.Min(runtime.Length, settings.Count);
            for (int i = 0; i < n; i++)
                runtime[i] = settings[i];

            // если settings короче массива — остальное можно оставить как есть
            // если наоборот длиннее — лишнее игнорируем
        }

        //=====================================================================
        // загрузка отрисовки осей
        //=====================================================================
        private void sbBufferUpdate()
        {
            // Таблица соответствий "runtime массив" <-> "настройки"
            var map = new Dictionary<string, SbMap>(StringComparer.Ordinal)
            {
                ["Controls"] = new SbMap { Runtime = sb_controls_axis_data, Settings = Properties.Settings.Default.sb_controls_axis_data_settings },
                ["Neshtatki"] = new SbMap { Runtime = sb_neshtatki_axis_data, Settings = Properties.Settings.Default.sb_neshtatki_axis_data_settings },
                ["2ES5K"] = new SbMap { Runtime = sb_es5k_axis_data, Settings = Properties.Settings.Default.sb_es5k_axis_data_settings },
                ["EP1M"] = new SbMap { Runtime = sb_ep1m_axis_data, Settings = Properties.Settings.Default.sb_ep1m_axis_data_settings },
                ["CHS2K"] = new SbMap { Runtime = sb_chs2k_axis_data, Settings = Properties.Settings.Default.sb_chs2k_axis_data_settings },
                ["CHS4"] = new SbMap { Runtime = sb_chs4_axis_data, Settings = Properties.Settings.Default.sb_chs4_axis_data_settings },
                ["CHS4KVR"] = new SbMap { Runtime = sb_chs4kvr_axis_data, Settings = Properties.Settings.Default.sb_chs4kvr_axis_data_settings },
                ["CHS4T"] = new SbMap { Runtime = sb_chs4t_axis_data, Settings = Properties.Settings.Default.sb_chs4t_axis_data_settings },
                ["CHS7"] = new SbMap { Runtime = sb_chs7_axis_data, Settings = Properties.Settings.Default.sb_chs7_axis_data_settings },
                ["CHS8"] = new SbMap { Runtime = sb_chs8_axis_data, Settings = Properties.Settings.Default.sb_chs8_axis_data_settings },
                ["VL11M"] = new SbMap { Runtime = sb_vl11_axis_data, Settings = Properties.Settings.Default.sb_vl11_axis_data_settings },
                ["VL82M"] = new SbMap { Runtime = sb_vl82_axis_data, Settings = Properties.Settings.Default.sb_vl82_axis_data_settings },
                ["VL80T"] = new SbMap { Runtime = sb_vl80t_axis_data, Settings = Properties.Settings.Default.sb_vl80t_axis_data_settings },
                ["VL85"] = new SbMap { Runtime = sb_vl85_axis_data, Settings = Properties.Settings.Default.sb_vl85_axis_data_settings },
                ["TEP70"] = new SbMap { Runtime = sb_tep70_axis_data, Settings = Properties.Settings.Default.sb_tep70_axis_data_settings },
                ["2TE10U"] = new SbMap { Runtime = sb_te10u_axis_data, Settings = Properties.Settings.Default.sb_te10u_axis_data_settings },
                ["M62"] = new SbMap { Runtime = sb_m62_axis_data, Settings = Properties.Settings.Default.sb_m62_axis_data_settings },
                ["ED4M"] = new SbMap { Runtime = sb_ed4m_axis_data, Settings = Properties.Settings.Default.sb_ed4m_axis_data_settings },
                ["ED9M"] = new SbMap { Runtime = sb_ed9m_axis_data, Settings = Properties.Settings.Default.sb_ed9m_axis_data_settings },
                ["tem18"] = new SbMap { Runtime = sb_tem18_axis_data, Settings = Properties.Settings.Default.sb_tem18_axis_data_settings },
            };

            foreach (var kv in map)
                LoadSbIfNotStart(kv.Value.Runtime, kv.Value.Settings);

            Properties.Settings.Default.Save();
        }

        //=====================================================================
        // Для работы sbBufferSave()
        //=====================================================================
        private static void SaveSb(string[] runtime, StringCollection settings)
        {
            if (settings == null) return;

            settings.Clear();
            if (runtime == null) return;

            for (int i = 0; i < runtime.Length; i++)
                settings.Add(runtime[i] ?? "");
        }

        //=====================================================================
        // Сохранение отрисовки осей
        //=====================================================================
        private void sbBufferSave()
        {
            var map = new System.Collections.Generic.Dictionary<string, SbMap>(StringComparer.Ordinal)
            {
                ["Controls"] = new SbMap { Runtime = sb_controls_axis_data, Settings = Properties.Settings.Default.sb_controls_axis_data_settings },
                ["Neshtatki"] = new SbMap { Runtime = sb_neshtatki_axis_data, Settings = Properties.Settings.Default.sb_neshtatki_axis_data_settings },
                ["2ES5K"] = new SbMap { Runtime = sb_es5k_axis_data, Settings = Properties.Settings.Default.sb_es5k_axis_data_settings },
                ["EP1M"] = new SbMap { Runtime = sb_ep1m_axis_data, Settings = Properties.Settings.Default.sb_ep1m_axis_data_settings },
                ["CHS2K"] = new SbMap { Runtime = sb_chs2k_axis_data, Settings = Properties.Settings.Default.sb_chs2k_axis_data_settings },
                ["CHS4"] = new SbMap { Runtime = sb_chs4_axis_data, Settings = Properties.Settings.Default.sb_chs4_axis_data_settings },
                ["CHS4KVR"] = new SbMap { Runtime = sb_chs4kvr_axis_data, Settings = Properties.Settings.Default.sb_chs4kvr_axis_data_settings },
                ["CHS4T"] = new SbMap { Runtime = sb_chs4t_axis_data, Settings = Properties.Settings.Default.sb_chs4t_axis_data_settings },
                ["CHS7"] = new SbMap { Runtime = sb_chs7_axis_data, Settings = Properties.Settings.Default.sb_chs7_axis_data_settings },
                ["CHS8"] = new SbMap { Runtime = sb_chs8_axis_data, Settings = Properties.Settings.Default.sb_chs8_axis_data_settings },
                ["VL11M"] = new SbMap { Runtime = sb_vl11_axis_data, Settings = Properties.Settings.Default.sb_vl11_axis_data_settings },
                ["VL82M"] = new SbMap { Runtime = sb_vl82_axis_data, Settings = Properties.Settings.Default.sb_vl82_axis_data_settings },
                ["VL80T"] = new SbMap { Runtime = sb_vl80t_axis_data, Settings = Properties.Settings.Default.sb_vl80t_axis_data_settings },
                ["VL85"] = new SbMap { Runtime = sb_vl85_axis_data, Settings = Properties.Settings.Default.sb_vl85_axis_data_settings },
                ["TEP70"] = new SbMap { Runtime = sb_tep70_axis_data, Settings = Properties.Settings.Default.sb_tep70_axis_data_settings },
                ["2TE10U"] = new SbMap { Runtime = sb_te10u_axis_data, Settings = Properties.Settings.Default.sb_te10u_axis_data_settings },
                ["M62"] = new SbMap { Runtime = sb_m62_axis_data, Settings = Properties.Settings.Default.sb_m62_axis_data_settings },
                ["ED4M"] = new SbMap { Runtime = sb_ed4m_axis_data, Settings = Properties.Settings.Default.sb_ed4m_axis_data_settings },
                ["ED9M"] = new SbMap { Runtime = sb_ed9m_axis_data, Settings = Properties.Settings.Default.sb_ed9m_axis_data_settings },
                ["tem18"] = new SbMap { Runtime = sb_tem18_axis_data, Settings = Properties.Settings.Default.sb_tem18_axis_data_settings },
            };

            foreach (var kv in map)
                SaveSb(kv.Value.Runtime, kv.Value.Settings);

            Properties.Settings.Default.Save();
        }

        //=====================================================================
        // Инициализация джойстика
        //=====================================================================
        public string Joystick_init()
        {
            // если вызывается повторно
            try { device?.Unacquire(); } catch { }
            try { device?.Dispose(); } catch { }
            device = null;

            Form1.i_joy_name = "";

            foreach (DeviceInstance instance in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                try
                {
                    device = new Device(instance.ProductGuid);
                    device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);

                    foreach (DeviceObjectInstance doi in device.Objects)
                    {
                        if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                        {
                            device.Properties.SetRange(ParameterHow.ById, doi.ObjectId, new InputRange(0, 65535));
                        }
                    }

                    device.Acquire();
                    Form1.i_joy_name = Convert.ToString(device.DeviceInformation.InstanceName);
                    break; // берём первое устройство
                }
                catch
                {
                    try { device?.Dispose(); } catch { }
                    device = null;
                    Form1.i_joy_name = "";
                }
            }

            return Form1.i_joy_name;
        }

        //=====================================================================
        // Обновление состояния джойстика
        //=====================================================================
        public void UpdateJoystickState()
        {
            if (device == null) return;

            int[] b_temp;
            JoystickState j = device.CurrentJoystickState;

            // --- буфер осей ---
            Form1.joystick_axis_buffer[0] = j.ARx;
            Form1.joystick_axis_buffer[1] = j.ARy;
            Form1.joystick_axis_buffer[2] = j.ARz;
            Form1.joystick_axis_buffer[3] = j.AX;
            Form1.joystick_axis_buffer[4] = j.AY;
            Form1.joystick_axis_buffer[5] = j.AZ;
            Form1.joystick_axis_buffer[6] = j.FRx;
            Form1.joystick_axis_buffer[7] = j.FRy;
            Form1.joystick_axis_buffer[8] = j.FRz;
            Form1.joystick_axis_buffer[9] = j.FX;
            Form1.joystick_axis_buffer[10] = j.FY;
            Form1.joystick_axis_buffer[11] = j.FZ;
            Form1.joystick_axis_buffer[12] = j.Rx;
            Form1.joystick_axis_buffer[13] = j.Ry;
            Form1.joystick_axis_buffer[14] = j.Rz;
            Form1.joystick_axis_buffer[15] = j.VRx;
            Form1.joystick_axis_buffer[16] = j.VRy;
            Form1.joystick_axis_buffer[17] = j.VRz;
            Form1.joystick_axis_buffer[18] = j.VX;
            Form1.joystick_axis_buffer[19] = j.VY;
            Form1.joystick_axis_buffer[20] = j.VZ;
            Form1.joystick_axis_buffer[21] = j.X;
            Form1.joystick_axis_buffer[22] = j.Y;
            Form1.joystick_axis_buffer[23] = j.Z;

            b_temp = j.GetPointOfView();
            Form1.joystick_axis_buffer[24] = b_temp[0];

            b_temp = j.GetSlider();
            Form1.joystick_axis_buffer[25] = b_temp[0];

            b_temp = j.GetASlider();
            Form1.joystick_axis_buffer[26] = b_temp[0];

            b_temp = j.GetFSlider();
            Form1.joystick_axis_buffer[27] = b_temp[0];

            b_temp = j.GetVSlider();
            Form1.joystick_axis_buffer[28] = b_temp[0];

            Form1.joystick_buttons_buffer = j.GetButtons();

            // --- UI часть ---
            EnsureUiAxis();

            string sel = Convert.ToString(comboBox_osi_select.SelectedItem);
            if (!string.IsNullOrEmpty(sel) && _uiAxis.TryGetValue(sel, out var d))
            {
                ushort pv = d.GetU16();

                // защита от выхода за пределы ProgressBar
                int min = progressBar_osi.Minimum;
                int max = progressBar_osi.Maximum;

                if (pv < min) pv = (ushort)min;
                if (pv > max) pv = (ushort)max;

                progressBar_osi.Value = pv;
                label_progressbar_osi.Text = d.Get().ToString();
            }
        }

        //=====================================================================
        // Список органов управления локомотивами
        //=====================================================================
        public string[] LocoKeyData = new string[]
        {
            //общие
            "Свисток",
            "Тифон",
            "Кран 395 1",
            "Кран 395 2",
            "Кран 395 3",
            "Кран 395 4",
            "Кран 395 5э",
            "Кран 395 5",
            "Кран 395 6",
            "Кран 254 1",
            "Кран 254 2",
            "Кран 254 3",
            "Кран 254 4",
            "Кран 254 5",
            "Кран 254 6",
            "Вид влево",
            "Вид вправо",
            "Вид вверх",
            "Вид вниз",
            "Вид приближ.",
            "Вид удаление",
            "Вид внешний",
            "Вид вперед",
            "Вид назад",
            "Протяжка ленты",
            "Бдительность Z",
            "Бдительность M",
            "Песок",
            "Дворники 0",
            "Дворники 1",
            "Дворники 2",
            "Дворники 3",
            "Дворники 4",
            "Дворники 5",

            //2es5k
            "Контр 0",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",
            "Контр ход16",
            "Контр ход17",
            "Контр ход18",
            "Контр ход19",
            "Контр ход20",
            "Контр ход21",
            "Контр ход22",
            "Контр ход23",
            "Контр ход24",
            "Контр ход25",
            "Контр ход26",
            "Контр ход27",
            "Контр ход28",
            "Контр ход29",
            "Контр ход30",
            "Контр ход31",
            "Контр ход32",
            "Контр ход33",
            "Контр ход34",
            "Контр ход35",
            "Контр ход36",

            "Контр тормоз4",
            "Контр тормоз5",
            "Контр тормоз6",
            "Контр тормоз7",
            "Контр тормоз8",
            "Контр тормоз9",
            "Контр тормоз10",
            "Контр тормоз11",
            "Контр тормоз12",
            "Контр тормоз13",
            "Контр тормоз14",
            "Контр тормоз15",
            "Контр тормоз16",
            "Контр тормоз17",
            "Контр тормоз18",
            "Контр тормоз19",
            "Контр тормоз20",
            "Контр тормоз21",
            "Контр тормоз22",
            "Контр тормоз23",
            "Контр тормоз24",
            "Контр тормоз25",
            "Контр тормоз26",
            "Контр тормоз27",
            "Контр тормоз28",
            "Контр тормоз29",
            "Контр тормоз30",
            "Контр тормоз31",
            "Контр тормоз32",
            "Контр тормоз33",
            "Контр тормоз34",
            "Контр тормоз35",
            "Контр тормоз36",

            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Рег. скор 0",
            "Рег. скор +",
            "Рег. скор -",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "Возврат БВ",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Управление_0",
            "Управление_1",
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "МСУД_0",
            "МСУД_1",
            "Вспом. машины_0",
            "Вспом. машины_1",
            "Освещ. каб_0",
            "Освещ. каб_1",
            "ЭПК_0",
            "ЭПК_1",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",
            "Сигнализ. 1_0",
            "Сигнализ. 1_1",
            "Сигнализ. 2_0",
            "Сигнализ. 2_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",
            "Авторегулировка_0",
            "Авторегулировка_1",

            //ep1m
            "Контр 0",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",
            "Контр ход16",
            "Контр ход17",
            "Контр ход18",
            "Контр ход19",
            "Контр ход20",
            "Контр ход21",
            "Контр ход22",
            "Контр ход23",
            "Контр ход24",
            "Контр ход25",
            "Контр ход26",
            "Контр ход27",
            "Контр ход28",
            "Контр ход29",
            "Контр ход30",
            "Контр ход31",
            "Контр ход32",
            "Контр ход33",
            "Контр ход34",
            "Контр ход35",
            "Контр ход36",

            "Контр тормоз4",
            "Контр тормоз5",
            "Контр тормоз6",
            "Контр тормоз7",
            "Контр тормоз8",
            "Контр тормоз9",
            "Контр тормоз10",
            "Контр тормоз11",
            "Контр тормоз12",
            "Контр тормоз13",
            "Контр тормоз14",
            "Контр тормоз15",
            "Контр тормоз16",
            "Контр тормоз17",
            "Контр тормоз18",
            "Контр тормоз19",
            "Контр тормоз20",
            "Контр тормоз21",
            "Контр тормоз22",
            "Контр тормоз23",
            "Контр тормоз24",
            "Контр тормоз25",
            "Контр тормоз26",
            "Контр тормоз27",
            "Контр тормоз28",
            "Контр тормоз29",
            "Контр тормоз30",
            "Контр тормоз31",
            "Контр тормоз32",
            "Контр тормоз33",
            "Контр тормоз34",
            "Контр тормоз35",
            "Контр тормоз36",

            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Рег. скор 0",
            "Рег. скор +",
            "Рег. скор -",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "Возврат БВ",
            "Блокировка ВВК_0",
            "Блокировка ВВК_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Управление_0",
            "Управление_1",
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "Вентилятор 3_0",
            "Вентилятор 3_1",
            "МСУД_0",
            "МСУД_1",
            "Вспом. машины_0",
            "Вспом. машины_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",
            "Авторегулировка_0",
            "Авторегулировка_1",

            //chs2k
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "<reserved>",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Вентилятор_0",
            "Вентилятор_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",

            //chs4
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "БВ_2",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 1_2",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Компрессор 2_2",
            "Вентилятор_0",
            "Вентилятор_1",
            "Вентилятор_2",
            "Вентилятор_3",
            "Вентилятор_4",
            "Вентилятор_5",
            "Вентилятор_6",
            "Вентилятор_7",
            "Всп.компр.песок_0",
            "Всп.компр.песок_1",
            "Всп.компр.песок_2",
            "Всп.компр.песок_3",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "Освещ. каб 1_3",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Авар.набор_0",
            "Авар.набор_1",
            "Авар.набор_2",
            "Авар.набор_3",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",

             //chs4kvr
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "БВ_2",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 1_2",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Компрессор 2_2",
            "Вентилятор_0",
            "Вентилятор_1",
            "Вентилятор_2",
            "Вентилятор_3",
            "Вентилятор_4",
            "Вентилятор_5",
            "Вентилятор_6",
            "Вентилятор_7",
            "Всп.компр.песок_0",
            "Всп.компр.песок_1",
            "Всп.компр.песок_2",
            "Всп.компр.песок_3",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "Освещ. каб 1_3",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Авар.набор_0",
            "Авар.набор_1",
            "Авар.набор_2",
            "Авар.набор_3",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",

            //chs4t
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "БВ_2",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 1_2",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Компрессор 2_2",
            "Вентилятор_0",
            "Вентилятор_1",
            "Вентилятор_2",
            "Всп.компр.песок_0",
            "Всп.компр.песок_1",
            "Всп.компр.песок_2",
            "Всп.компр.песок_3",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "Освещ. каб 1_3",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Авар.набор_0",
            "Авар.набор_1",
            "Авар.набор_2",
            "Авар.набор_3",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",
            "Жалюзи 1_0",
            "Жалюзи 1_1",
            "<<reserved>>",
            "<<reserved>>",

            //chs7
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "БВ_2",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. пер_2",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Токопр. зад_2",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 1_2",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Компрессор 2_2",
            "Вентилятор_0",
            "Вентилятор_1",
            "Вентилятор_2",
            "Сброс СП",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",
            "Жалюзи 1_0",
            "Жалюзи 1_1",

            //chs8
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр +",
            "Контр -",
            "Контр +1",
            "Контр -1",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "БВ_0",
            "БВ_1",
            "восст БВ",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "Компрессор 1_0",
            "Компрессор 1_1",
            "Компрессор 1_2",
            "Компрессор 2_0",
            "Компрессор 2_1",
            "Компрессор 2_2",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 1_2",
            "Вентилятор 1_3",
            "Вентилятор 1_4",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "Вентилятор 2_2",
            "Вентилятор 2_3",
            "Вентилятор 2_4",
            "Всп.компр.песок_0",
            "Всп.компр.песок_1",
            "Всп.компр.песок_2",
            "Всп.компр.песок_3",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "Освещ. каб 1_3",
            "Освещ. каб 1_4",
            "Освещ. каб 1_5",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Авар.набор_0",
            "Авар.набор_1",
            "Авар.набор_2",
            "Авар.набор_3",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",
            "Реост.торм.проверка",
            "Реост.торм_0",
            "Реост.торм_1",
            "Реост.торм_2",

            //vl11 
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",
            "Контр ход16",
            "Контр ход17",
            "Контр ход18",
            "Контр ход19",
            "Контр ход20",
            "Контр ход21",
            "Контр ход22",
            "Контр ход23",
            "Контр ход24",
            "Контр ход25",
            "Контр ход26",
            "Контр ход27",
            "Контр ход28",
            "Контр ход29",
            "Контр ход30",
            "Контр ход31",
            "Контр ход32",
            "Контр ход33",
            "Контр ход34",
            "Контр ход35",
            "Контр ход36",
            "Контр ход37",
            "Контр ход38",
            "Контр ход39",
            "Контр ход40",
            "Контр ход41",
            "Контр ход42",
            "Контр ход43",
            "Контр ход44",
            "Контр ход45",
            "Контр ход46",
            "Контр ход47",
            "Контр ход48",


            "Шунт_0",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр. общ_0",
            "Токопр. общ_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "БВ_0",
            "БВ_1",
            "Возврат БВ",         
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 1_2",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",          
            "Прожектор 0",
            "Прожектор 1",
            "Прожектор 2",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",

            //vl82
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр откл.БВ",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",
            "Контр ход16",
            "Контр ход17",
            "Контр ход18",
            "Контр ход19",
            "Контр ход20",
            "Контр ход21",
            "Контр ход22",
            "Контр ход23",
            "Контр ход24",
            "Контр ход25",
            "Контр ход26",
            "Контр ход27",
            "Контр ход28",
            "Контр ход29",
            "Контр ход30",
            "Контр ход31",
            "Контр ход32",
            "Контр ход33",
            "Контр ход34",
            "Контр ход35",
            "Контр ход36",
            "Контр ход37",
            "Контр ход38",

            "Шунт_0",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Шунт_4",
            "Шунт_реостат",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр. общ_0",
            "Токопр. общ_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "ГВ_0",
            "ГВ_1",
            "БВ_0",
            "БВ_1",
            "Возврат ГВ",         
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "КВЦ_0",
            "КВЦ_1",
            "возвр.КВЦ",
            "Управление_0",
            "Управление_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",

            //vl80
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Контр откл.БВ",
            "Контр 0",
            "Контр 1",
            "Контр 2",
            "Контр 3",
            "Контр 4",
            "Контр 5",
            "Контр 6",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр. общ_0",
            "Токопр. общ_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "ГВ_0",
            "ГВ_1",
            "Возврат ГВ",         
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "Вентилятор 3_0",
            "Вентилятор 3_1",
            "Вентилятор 4_0",
            "Вентилятор 4_1",
            "ФР_0",
            "ФР_1",
            "Управление_0",
            "Управление_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",

            //vl85
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Шунт_1",
            "Шунт_2",
            "Шунт_3",
            "Контр откл.БВ",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",
            "Контр ход16",
            "Контр ход17",
            "Контр ход18",
            "Контр ход19",
            "Контр ход20",
            "Контр ход21",
            "Контр ход22",
            "Контр ход23",
            "Контр ход24",
            "Контр ход25",
            "Контр ход26",
            "Контр ход27",
            "Контр ход28",
            "Контр ход29",
            "Контр ход30",
            "Контр ход31",
            "Контр ход32",

            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр. общ_0",
            "Токопр. общ_1",
            "Токопр. пер_0",
            "Токопр. пер_1",
            "Токопр. зад_0",
            "Токопр. зад_1",
            "ГВ_0",
            "ГВ_1",
            "Возврат ГВ",         
            "Компрессор_0",
            "Компрессор_1",
            "Вентилятор 1_0",
            "Вентилятор 1_1",
            "Вентилятор 2_0",
            "Вентилятор 2_1",
            "Вентилятор 3_0",
            "Вентилятор 3_1",
            "Вентилятор 4_0",
            "Вентилятор 4_1",
            "ФР_0",
            "ФР_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",
            "Сигнализ. общ_0",
            "Сигнализ. общ_1",
            "Сигнализ. 1_0",
            "Сигнализ. 1_1",
            "Сигнализ. 2_0",
            "Сигнализ. 2_1",

            //tep70
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",

            
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Насос_0",
            "Насос_1",
            "Пуск",         
            "Управление_0",
            "Управление_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Прожектор 0",
            "Прожектор 1",
            "Прожектор 2",

             //2te10u
             "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",

            
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Насос_1_0",
            "Насос_1_1",                 
            "Насос_2_0",
            "Насос_2_1",
            "Пуск_0",    
            "Пуск_1",
            "Управление_0",
            "Управление_1",
            "Движение_0",
            "Движение_1",
            "Переходы_0",
            "Переходы_1",
            "Холост_1_0",
            "Холост_1_1",                 
            "Холост_2_0",
            "Холост_2_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Прожектор 0",
            "Прожектор 1",
            "Прожектор 2",

            //m62
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",
            "Контр ход9",
            "Контр ход10",
            "Контр ход11",
            "Контр ход12",
            "Контр ход13",
            "Контр ход14",
            "Контр ход15",

            "Кран ТМ_0",
            "Кран ТМ_1",
            "Насос_0",
            "Насос_1",
            "Пуск",         
            "Управление_0",
            "Управление_1",
            "Переходы_0",
            "Переходы_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. каб 1_2",
            "ЭПК_0",
            "ЭПК_1",
            "Прожектор 0",
            "Прожектор 1",
            "Прожектор 2",

            //ed9m
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр торм1",
            "Контр торм2",
            "Контр торм3",
            "Контр торм4",
            "Контр торм5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр_0",
            "Токопр_1",
            "БВ_0",
            "БВ_1",         
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Двери лев_0",
            "Двери лев_1",
            "Двери пр_0",
            "Двери пр_1",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",

            //ed4m
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр торм1",
            "Контр торм2",
            "Контр торм3",
            "Контр торм4",
            "Контр торм5",
            "Кран ТМ_0",
            "Кран ТМ_1",
            "Токопр_0",
            "Токопр_1",
            "БВ_0",
            "БВ_1",           
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "ЭПК_0",
            "ЭПК_1",
            "ЭПТ_0",
            "ЭПТ_1",
            "Двери лев_0",
            "Двери лев_1",
            "Двери пр_0",
            "Двери пр_1",
            "Прожектор_0",
            "Прожектор_1",
            "Прожектор_2",

            //tem18
            "Реверс 0",
            "Реверс вп",
            "Реверс нз",
            "Контр 0",
            "Контр ход1",
            "Контр ход2",
            "Контр ход3",
            "Контр ход4",
            "Контр ход5",
            "Контр ход6",
            "Контр ход7",
            "Контр ход8",

            "Кран ТМ_0",
            "Кран ТМ_1",
            "Насос_масл0",
            "Насос_масл1",
            "Насос_топл0",
            "Насос_топл1",
            "Пуск",         
            "Управление_0",
            "Управление_1",
            "Переходы_0",
            "Переходы_1",
            "Освещ. каб 1_0",
            "Освещ. каб 1_1",
            "Освещ. приб 1_0",
            "Освещ. приб 1_1",
            "ЭПК_0",
            "ЭПК_1",
            "Прожектор 1_0",
            "Прожектор 1_1",
            "Прожектор 1_2",

            //нештатки
            "Нештатка 1",
            "Нештатка 2",
            "Нештатка 3",
            "Нештатка 4",
            "Нештатка 5",
            "Нештатка 6",
            "Нештатка 7",
            "Нештатка 8",
            "Нештатка 9",
            "Нештатка 10",
            "Нештатка 11",
            "Нештатка 12",
            "Нештатка 13",
            "Нештатка 14",
            "Нештатка 15",
            "Нештатка 16",
            "Нештатка 17",
            "Нештатка 18",
            "Нештатка 19",
            "Нештатка 20",
            "Нештатка 21",
            "Нештатка 22",
            "Нештатка 23",
            "Нештатка 24",
            "Нештатка 25",
            "Нештатка 26",
            "Нештатка 27",
            "Нештатка 28",
            "Нештатка 29",
            "Нештатка 30",
            "Нештатка 31",
            "Нештатка 32",
            "Нештатка 33",
            "Нештатка 34",
            "Нештатка 35",
            "Нештатка 36",
            "Нештатка 37",
            "Нештатка 38",
            "Нештатка 39",
            "Нештатка 40",
            "Нештатка 41",
            "Нештатка 42",
            "Нештатка 43",
            "Нештатка 44",
            "Нештатка 45",
            "Нештатка 46",
            "Нештатка 47",
            "Нештатка 48",
            "Нештатка 49",
            "Нештатка 50",
            "Нештатка 51",
            "Нештатка 52",
            "Нештатка 53",
            "Нештатка 54",
            "Нештатка 55",
            "Нештатка 56",
            "Нештатка 57",
            "Нештатка 58",
            "Нештатка 59",
            "Нештатка 60",
            "Нештатка 61",
            "Нештатка 62",
            "Нештатка 63",
            "Нештатка 64",
            "Нештатка 65",
            "Нештатка 66",
            "Нештатка 67",
            "Нештатка 68",
            "Нештатка 69",
            "Нештатка 70",
            "Нештатка 71",
            "Нештатка 72",
            "Нештатка 73",
            "Нештатка 74",
            "Нештатка 75",
            "Нештатка 76",
            "Нештатка 77",
            "Нештатка 78",
            "Нештатка 79",
            "Нештатка 80",
            "Нештатка 81",
            "Нештатка 82",
            "Нештатка 83",
            "Нештатка 84",
            "Нештатка 85",
            "Нештатка 86",
            "Нештатка 87",
            "Нештатка 88",
            "Нештатка 89",
            "Нештатка 90",
            "Нештатка 91",
            "Нештатка 92",
            "Нештатка 93",
            "Нештатка 94",
            "Нештатка 95",
            "Нештатка 96",
            "Нештатка 97",
            "Нештатка 98",
            "Нештатка 99",
            "Нештатка 100"
        };

        //=====================================================================
        // Для работы KeyDataUpdate()
        //=====================================================================
        private sealed class LocoUiDef
        {
            public int LocoId;                // i_temp_loco_select
            public int LocoKeyStart;          // j start in LocoKeyData
            public int RowCount;              // сколько строк рисовать

            public string[] SbAxisData;       // sb_*_axis_data (строковые подписи осей/меток)
            public int[] KeyBuffer;           // Form1.*_key_buffer
            public string[] WavPathBuffer;    // Form1.*_wav_path_key_buffer
        }

        // безопасное имя файла (чтобы не падать на null/пустых/без '\')
        private static string SafeFileName(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) return "";
            try { return System.IO.Path.GetFileName(fullPath); }
            catch { return ""; }
        }

        private static void EnsureGridColumns(DataGridView g)
        {
            // делаем один раз: 3 колонки (имя / кнопка-ось / звук)
            if (g.Columns.Count == 3) return;

            g.Rows.Clear();
            g.Columns.Clear();
            g.Columns.Add(null, null); // имя
            g.Columns.Add(null, null); // кнопка или ось
            g.Columns.Add(null, null); // звук
        }

        private Dictionary<string, LocoUiDef> _locoUi;

        //=====================================================================
        // Таблица соответствий локомотивов для работы KeyDataUpdate()
        //=====================================================================
        private void EnsureLocoUi()
        {
            if (_locoUi != null) return;

            _locoUi = new System.Collections.Generic.Dictionary<string, LocoUiDef>(StringComparer.Ordinal)
            {
                ["Controls"] = new LocoUiDef
                {
                    LocoId = 0,
                    LocoKeyStart = 0,
                    RowCount = 34,
                    SbAxisData = sb_controls_axis_data,
                    KeyBuffer = Form1.Controls_key_buffer,
                    WavPathBuffer = Form1.controls_wav_path_key_buffer
                },

                ["2ES5K"] = new LocoUiDef
                {
                    LocoId = 1,
                    LocoKeyStart = 34,
                    RowCount = 109,
                    SbAxisData = sb_es5k_axis_data,
                    KeyBuffer = Form1.ES5K_key_buffer,
                    WavPathBuffer = Form1.es5k_wav_path_key_buffer
                },

                ["EP1M"] = new LocoUiDef
                {
                    LocoId = 2,
                    LocoKeyStart = 143,
                    RowCount = 112,
                    SbAxisData = sb_ep1m_axis_data,
                    KeyBuffer = Form1.EP1M_key_buffer,
                    WavPathBuffer = Form1.ep1m_wav_path_key_buffer
                },

                ["CHS2K"] = new LocoUiDef
                {
                    LocoId = 3,
                    LocoKeyStart = 255,
                    RowCount = 32,
                    SbAxisData = sb_chs2k_axis_data,
                    KeyBuffer = Form1.CHS2K_key_buffer,
                    WavPathBuffer = Form1.chs2k_wav_path_key_buffer
                },

                ["CHS4"] = new LocoUiDef
                {
                    LocoId = 4,
                    LocoKeyStart = 287,
                    RowCount = 55,
                    SbAxisData = sb_chs4_axis_data,
                    KeyBuffer = Form1.CHS4_key_buffer,
                    WavPathBuffer = Form1.chs4_wav_path_key_buffer
                },

                ["CHS4 KVR"] = new LocoUiDef
                {
                    LocoId = 5,
                    LocoKeyStart = 342,
                    RowCount = 55,
                    SbAxisData = sb_chs4kvr_axis_data,
                    KeyBuffer = Form1.CHS4KVR_key_buffer,
                    WavPathBuffer = Form1.chs4kvr_wav_path_key_buffer
                },

                ["CHS4T"] = new LocoUiDef
                {
                    LocoId = 6,
                    LocoKeyStart = 397,
                    RowCount = 52,
                    SbAxisData = sb_chs4t_axis_data,
                    KeyBuffer = Form1.CHS4T_key_buffer,
                    WavPathBuffer = Form1.chs4t_wav_path_key_buffer
                },

                ["CHS7"] = new LocoUiDef
                {
                    LocoId = 7,
                    LocoKeyStart = 451,
                    RowCount = 46,
                    SbAxisData = sb_chs7_axis_data,
                    KeyBuffer = Form1.CHS7_key_buffer,
                    WavPathBuffer = Form1.chs7_wav_path_key_buffer
                },

                ["CHS8"] = new LocoUiDef
                {
                    LocoId = 8,
                    LocoKeyStart = 497,
                    RowCount = 63,
                    SbAxisData = sb_chs8_axis_data,
                    KeyBuffer = Form1.CHS8_key_buffer,
                    WavPathBuffer = Form1.chs8_wav_path_key_buffer
                },

                ["VL11M"] = new LocoUiDef
                {
                    LocoId = 9,
                    LocoKeyStart = 560,
                    RowCount = 83,
                    SbAxisData = sb_vl11_axis_data,
                    KeyBuffer = Form1.VL11M_key_buffer,
                    WavPathBuffer = Form1.vl11_wav_path_key_buffer
                },

                ["VL82M"] = new LocoUiDef
                {
                    LocoId = 10,
                    LocoKeyStart = 643,
                    RowCount = 83,
                    SbAxisData = sb_vl82_axis_data,
                    KeyBuffer = Form1.VL82M_key_buffer,
                    WavPathBuffer = Form1.vl82_wav_path_key_buffer
                },

                ["VL80T"] = new LocoUiDef
                {
                    LocoId = 11,
                    LocoKeyStart = 726,
                    RowCount = 49,
                    SbAxisData = sb_vl80t_axis_data,
                    KeyBuffer = Form1.VL80T_key_buffer,
                    WavPathBuffer = Form1.vl80t_wav_path_key_buffer
                },

                ["VL85"] = new LocoUiDef
                {
                    LocoId = 12,
                    LocoKeyStart = 775,
                    RowCount = 77,
                    SbAxisData = sb_vl85_axis_data,
                    KeyBuffer = Form1.VL85_key_buffer,
                    WavPathBuffer = Form1.vl85_wav_path_key_buffer
                },

                ["TEP70"] = new LocoUiDef
                {
                    LocoId = 13,
                    LocoKeyStart = 852,
                    RowCount = 36,
                    SbAxisData = sb_tep70_axis_data,
                    KeyBuffer = Form1.TEP70_key_buffer,
                    WavPathBuffer = Form1.tep70_wav_path_key_buffer
                },

                ["2TE10U"] = new LocoUiDef
                {
                    LocoId = 14,
                    LocoKeyStart = 888,
                    RowCount = 47,
                    SbAxisData = sb_te10u_axis_data,
                    KeyBuffer = Form1.TE10U_key_buffer,
                    WavPathBuffer = Form1.te10u_wav_path_key_buffer
                },

                ["M62"] = new LocoUiDef
                {
                    LocoId = 15,
                    LocoKeyStart = 935,
                    RowCount = 36,
                    SbAxisData = sb_m62_axis_data,
                    KeyBuffer = Form1.M62_key_buffer,
                    WavPathBuffer = Form1.m62_wav_path_key_buffer
                },

                ["ED4M"] = new LocoUiDef
                {
                    LocoId = 16,
                    LocoKeyStart = 1001,
                    RowCount = 33,
                    SbAxisData = sb_ed4m_axis_data,
                    KeyBuffer = Form1.ED4M_key_buffer,
                    WavPathBuffer = Form1.ed4m_wav_path_key_buffer
                },

                ["ED9M"] = new LocoUiDef
                {
                    LocoId = 17,
                    LocoKeyStart = 971,
                    RowCount = 30,
                    SbAxisData = sb_ed9m_axis_data,
                    KeyBuffer = Form1.ED9M_key_buffer,
                    WavPathBuffer = Form1.ed9m_wav_path_key_buffer
                },

                ["tem18"] = new LocoUiDef
                {
                    LocoId = 18,
                    LocoKeyStart = 1034,
                    RowCount = 32,
                    SbAxisData = sb_tem18_axis_data,
                    KeyBuffer = Form1.tem18_key_buffer,
                    WavPathBuffer = Form1.tem18_wav_path_key_buffer
                },

                ["Neshtatki"] = new LocoUiDef
                {
                    LocoId = 19,
                    LocoKeyStart = 1066,
                    RowCount = 100,
                    SbAxisData = sb_neshtatki_axis_data,
                    KeyBuffer = Form1.Neshtatki_key_buffer,
                    WavPathBuffer = Form1.neshtatki_wav_path_key_buffer
                },
            };
        }

        //=====================================================================
        // Обновление состояния кнопок
        //=====================================================================
        public void KeyDataUpdate()
        {
            s_current_loco_select = comboBox_zdsimLoco.Text;

            EnsureLocoUi();

            if (string.IsNullOrEmpty(s_current_loco_select))
                return;

            if (_locoUi == null || !_locoUi.TryGetValue(s_current_loco_select, out var def))
                return;

            i_temp_loco_select = def.LocoId;

            EnsureGridColumns(dataGridView_Zdsimulator);

            dataGridView_Zdsimulator.Rows.Clear();
            if (def.RowCount > 0)
                dataGridView_Zdsimulator.Rows.Add(def.RowCount);

            // локальные ссылки + защита
            var sb = def.SbAxisData;
            var keys = def.KeyBuffer;
            var wav = def.WavPathBuffer;

            int j = def.LocoKeyStart;

            for (int i = 0; i < def.RowCount; i++)
            {
                var row = dataGridView_Zdsimulator.Rows[i];

                // 0) Имя действия (LocoKeyData[j])
                if (LocoKeyData != null && (uint)j < (uint)LocoKeyData.Length)
                    row.Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                else
                    row.Cells[0].Value = "";

                // 1) Сначала ось/метка (sb_*_axis_data[i]) — как в твоём коде
                if (sb != null && (uint)i < (uint)sb.Length && sb[i] != null)
                    row.Cells[1].Value = sb[i];

                // 2) Если назначена кнопка — она ПЕРЕЗАТИРАЕТ ось (твой приоритет)
                if (keys != null && (uint)i < (uint)keys.Length && keys[i] != 0)
                    row.Cells[1].Value = "Button " + keys[i];

                // 3) Звук (только имя файла)
                if (wav != null && (uint)i < (uint)wav.Length && wav[i] != null)
                    row.Cells[2].Value = SafeFileName(wav[i]);

                j++;
            }
        }

        //=====================================================================
        // Кнопка "Настройки приборов по умолчанию" служит для установки значений
        // для шаговых двигателей bj28
        //=====================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            Form1.Init_pribor();
            textBox1.Text = Convert.ToString(100);   //Множетель Скорости
            textBox2.Text = Convert.ToString(100);   //Множетель Тока ЭПТ
            textBox3.Text = Convert.ToString(100);   //Множетель Напряжения КС
            textBox4.Text = Convert.ToString(200);   //Множетель Напряжения ТД
            textBox5.Text = Convert.ToString(100);   //Множетель Токов ТД
            textBox6.Text = Convert.ToString(100);   //Множетель Пневматики
            numericUpDown7.Value = 3200;             //Количество шагов ШД
            numericUpDown8.Value = 500;              //Время мигания лампы бдительности и ограничения скорости
        }

        //=====================================================================
        // Кнопка "ОК" Кнопка созранения вненсенных изменений и закрытия
        // окна настройка.
        //=====================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            Loco.i_skor_tek_convert = Convert.ToSingle(textBox1.Text);
            Loco.i_tok_ept_convert = Convert.ToSingle(textBox2.Text);
            Loco.i_napruga_ks_convert = Convert.ToSingle(textBox3.Text);
            Loco.i_napruga_td_convert = Convert.ToSingle(textBox4.Text);
            Loco.i_tok_convert = Convert.ToSingle(textBox5.Text);
            Loco.i_pnevmo_convert = Convert.ToSingle(textBox6.Text);
            Form1.i_step_steper_motor = Convert.ToUInt16(numericUpDown7.Value);
            Form1.i_bdit = Convert.ToUInt16(numericUpDown8.Value);
            Form1.i_shum_joystick = Convert.ToUInt16(numericUpDown_shum.Value);

            if (Convert.ToBoolean(radio_priem.Checked) == true)
            {
                Form1.i_priem_peredacha = 1;
            }

            if (Convert.ToBoolean(radio_peredacha.Checked) == true)
            {
                Form1.i_priem_peredacha = 2;
            }

            if (Convert.ToBoolean(radio_priem_peredacha.Checked) == true)
            {
                Form1.i_priem_peredacha = 3;
            }

            sbBufferSave();
            sbBufferWavPathSave();
            Close();
        }

        //=====================================================================
        // Кнопка "Отмена" служит для отмены изменений и закрытия окна настроек 
        //=====================================================================
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "скорость текущая *"
        //=====================================================================
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "ток ЭПТ *"
        //=====================================================================
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "напр. КС *"
        //=====================================================================
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "напр. ТД / *"
        //=====================================================================
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "токи / *"
        //=====================================================================
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Текст бокс для ввода множителя приборов "пневматика *"
        //=====================================================================
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "скорость текущая *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0") || (textBox1.Text == "")) textBox1.Text = "1";
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "ток ЭПТ *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if ((textBox2.Text == "0") || (textBox2.Text == "")) textBox2.Text = "1";
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "напр. КС *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if ((textBox3.Text == "0") || (textBox3.Text == "")) textBox3.Text = "1";
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "напр. ТД / *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if ((textBox4.Text == "0") || (textBox4.Text == "")) textBox4.Text = "1";
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "токи / *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if ((textBox5.Text == "0") || (textBox5.Text == "")) textBox5.Text = "1";
        }

        //=====================================================================
        // Если текст бокс для ввода множителя приборов "пневматика *"
        // пустой или = 0 то по умолчанию пропишем = 1.
        //=====================================================================
        private void textBox6_Leave(object sender, EventArgs e)
        {
            if ((textBox6.Text == "0") || (textBox6.Text == "")) textBox6.Text = "1";
        }

        //=====================================================================
        // Текст бокс для отображения имени джойстика, если он подключен.
        //=====================================================================
        private void textBox_joy_name_Enter(object sender, EventArgs e)
        {
            label7.Focus();
        }

        //=====================================================================
        // Чек бокс выбор режима работы сканера (кружок) "Только Приём".
        //=====================================================================
        private void radio_priem_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            tabControl1.Visible = false;
            button_metka_osi.Enabled = false;
            button_sbros_metok.Enabled = false;
            Form1.i_priem_peredacha = 1;
        }

        //=====================================================================
        // Чек бокс выбор режима работы сканера (кружок) "Только Передача".
        //=====================================================================
        private void radio_peredacha_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
            tabControl1.Visible = true;
            button_metka_osi.Enabled = true;
            button_sbros_metok.Enabled = true;
            Form1.i_priem_peredacha = 2;
        }

        //=====================================================================
        // Чек бокс выбор режима работы сканера (кружок) "Приём и Передача".
        //=====================================================================
        private void radio_priem_peredacha_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
            tabControl1.Visible = true;
            button_metka_osi.Enabled = true;
            button_sbros_metok.Enabled = true;
            Form1.i_priem_peredacha = 3;
        }

        private void comboBox_zdsimLoco_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_Zdsimulator.Focus();
            KeyDataUpdate();
        }

        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        private void dataGridView_Zdsimulator_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            i_temp_datagird_select_f2 = 1;
            i_temp_row_number_f2 = Convert.ToInt16(dataGridView_Zdsimulator.CurrentRow.Index);
            i_temp_column_number_f2 = Convert.ToInt16(dataGridView_Zdsimulator.CurrentCell.ColumnIndex);

            //если нажат 2 столбец, открываем окно Form3
            Form_joystick_control f3 = new Form_joystick_control();

            if (i_temp_column_number_f2 == 1)
            {
                f3.Owner = this;
                f3.ShowDialog();
            }
            //если нажат 3 столбец, открываем FileDialog

            openFileDialog1.InitialDirectory = Application.StartupPath;//начальный путь в папке приложения
            openFileDialog1.Filter = "Wav files (*.wav)|*.wav";        //только wav
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;                   //восстанавливать прежний путь

            if (i_temp_column_number_f2 == 2)
            {
                string strFileName = "";
                string strFilePath = "";
                string strAllPath = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
                    strFileName = fInfo.Name;
                    strFilePath = fInfo.DirectoryName;
                    strAllPath = strFilePath + "\\" + strFileName;
                }
                
                s_current_loco_select = comboBox_zdsimLoco.Text;
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "Локомотив")
                {
                    Form1.controls_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "2ES5K")
                {
                    Form1.es5k_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "EP1M")
                {
                    Form1.ep1m_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS2K")
                {
                    Form1.chs2k_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4")
                {
                    Form1.chs4_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4 KVR")
                {
                    Form1.chs4kvr_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4T")
                {
                    Form1.chs4t_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS7")
                {
                    Form1.chs7_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS8")
                {
                    Form1.chs8_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL11M")
                {
                    Form1.vl11_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL82M")
                {
                    Form1.vl82_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL80T")
                {
                    Form1.vl80t_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL85")
                {
                    Form1.vl85_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "TEP70")
                {
                    Form1.tep70_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "2TE10U")
                {
                    Form1.te10u_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "M62")
                {
                    Form1.m62_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "ED4M")
                {
                    Form1.ed4m_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "ED9M")
                {
                    Form1.ed9m_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "tem18")
                {
                    Form1.tem18_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
                if (Convert.ToString(comboBox_zdsimLoco.Text) == "Neshtatki")
                {
                    Form1.neshtatki_wav_path_key_buffer[i_temp_row_number_f2] = strAllPath;
                }
            }
            KeyDataUpdate();
            dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = dataGridView_Zdsimulator.Rows[i_temp_row_number_f2].Index;
            dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i_temp_row_number_f2].Cells[i_temp_column_number_f2];
        }

        //============================================================================
        // Регулировка фильтра шума джойстика
        //============================================================================
        private void numericUpDown_shum_ValueChanged(object sender, EventArgs e)
        {
            Form1.i_shum_joystick = Convert.ToInt16(numericUpDown_shum.Value);
        }

        //============================================================================
        // Для button_metka_osi_Click() Установка меток на быбранной оси джойстика
        //============================================================================
        private sealed class AxisPointDef
        {
            public Func<int> GetValue;          // текущее значение оси/слайдера/POV
            public Func<int[]> GetPoints;       // текущий массив меток
            public Action<int[]> SetPoints;     // присвоить новый массив меток
        }

        private Dictionary<string, AxisPointDef> _axisPoints;

        private static void AppendPoint(ref int[] arr, int value)
        {
            if (arr == null)
            {
                arr = new int[1];
                arr[0] = value;
                return;
            }

            int n = arr.Length;
            int[] next = new int[n + 1];
            Array.Copy(arr, next, n);
            next[n] = value;
            arr = next;
        }

        private void EnsureAxisPoints()
        {
            if (_axisPoints != null) return;

            _axisPoints = new System.Collections.Generic.Dictionary<string, AxisPointDef>(StringComparer.Ordinal)
            {
                ["ARx"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.ARx,
                    GetPoints = () => Form1.joystick_ARx_point_buffer,
                    SetPoints = a => Form1.joystick_ARx_point_buffer = a
                },
                ["ARy"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.ARy,
                    GetPoints = () => Form1.joystick_ARy_point_buffer,
                    SetPoints = a => Form1.joystick_ARy_point_buffer = a
                },
                ["ARz"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.ARz,
                    GetPoints = () => Form1.joystick_ARz_point_buffer,
                    SetPoints = a => Form1.joystick_ARz_point_buffer = a
                },

                ["AX"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.AX,
                    GetPoints = () => Form1.joystick_AX_point_buffer,
                    SetPoints = a => Form1.joystick_AX_point_buffer = a
                },
                ["AY"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.AY,
                    GetPoints = () => Form1.joystick_AY_point_buffer,
                    SetPoints = a => Form1.joystick_AY_point_buffer = a
                },
                ["AZ"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.AZ,
                    GetPoints = () => Form1.joystick_AZ_point_buffer,
                    SetPoints = a => Form1.joystick_AZ_point_buffer = a
                },

                ["FRx"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FRx,
                    GetPoints = () => Form1.joystick_FRx_point_buffer,
                    SetPoints = a => Form1.joystick_FRx_point_buffer = a
                },
                ["FRy"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FRy,
                    GetPoints = () => Form1.joystick_FRy_point_buffer,
                    SetPoints = a => Form1.joystick_FRy_point_buffer = a
                },
                ["FRz"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FRz,
                    GetPoints = () => Form1.joystick_FRz_point_buffer,
                    SetPoints = a => Form1.joystick_FRz_point_buffer = a
                },

                ["FX"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FX,
                    GetPoints = () => Form1.joystick_FX_point_buffer,
                    SetPoints = a => Form1.joystick_FX_point_buffer = a
                },
                ["FY"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FY,
                    GetPoints = () => Form1.joystick_FY_point_buffer,
                    SetPoints = a => Form1.joystick_FY_point_buffer = a
                },
                ["FZ"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.FZ,
                    GetPoints = () => Form1.joystick_FZ_point_buffer,
                    SetPoints = a => Form1.joystick_FZ_point_buffer = a
                },

                ["Rx"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.Rx,
                    GetPoints = () => Form1.joystick_Rx_point_buffer,
                    SetPoints = a => Form1.joystick_Rx_point_buffer = a
                },
                ["Ry"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.Ry,
                    GetPoints = () => Form1.joystick_Ry_point_buffer,
                    SetPoints = a => Form1.joystick_Ry_point_buffer = a
                },
                ["Rz"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.Rz,
                    GetPoints = () => Form1.joystick_Rz_point_buffer,
                    SetPoints = a => Form1.joystick_Rz_point_buffer = a
                },

                ["VRx"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VRx,
                    GetPoints = () => Form1.joystick_VRx_point_buffer,
                    SetPoints = a => Form1.joystick_VRx_point_buffer = a
                },
                ["VRy"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VRy,
                    GetPoints = () => Form1.joystick_VRy_point_buffer,
                    SetPoints = a => Form1.joystick_VRy_point_buffer = a
                },
                ["VRz"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VRz,
                    GetPoints = () => Form1.joystick_VRz_point_buffer,
                    SetPoints = a => Form1.joystick_VRz_point_buffer = a
                },

                ["VX"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VX,
                    GetPoints = () => Form1.joystick_VX_point_buffer,
                    SetPoints = a => Form1.joystick_VX_point_buffer = a
                },
                ["VY"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VY,
                    GetPoints = () => Form1.joystick_VY_point_buffer,
                    SetPoints = a => Form1.joystick_VY_point_buffer = a
                },
                ["VZ"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.VZ,
                    GetPoints = () => Form1.joystick_VZ_point_buffer,
                    SetPoints = a => Form1.joystick_VZ_point_buffer = a
                },

                ["X"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.X,
                    GetPoints = () => Form1.joystick_X_point_buffer,
                    SetPoints = a => Form1.joystick_X_point_buffer = a
                },
                ["Y"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.Y,
                    GetPoints = () => Form1.joystick_Y_point_buffer,
                    SetPoints = a => Form1.joystick_Y_point_buffer = a
                },
                ["Z"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.Z,
                    GetPoints = () => Form1.joystick_Z_point_buffer,
                    SetPoints = a => Form1.joystick_Z_point_buffer = a
                },

                ["POV"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.GetPointOfView()[0],
                    GetPoints = () => Form1.joystick_POV_point_buffer,
                    SetPoints = a => Form1.joystick_POV_point_buffer = a
                },

                ["Slider"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.GetSlider()[0],
                    GetPoints = () => Form1.joystick_Slider_point_buffer,
                    SetPoints = a => Form1.joystick_Slider_point_buffer = a
                },
                ["ASlider"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.GetASlider()[0],
                    GetPoints = () => Form1.joystick_ASlider_point_buffer,
                    SetPoints = a => Form1.joystick_ASlider_point_buffer = a
                },
                ["FSlider"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.GetFSlider()[0],
                    GetPoints = () => Form1.joystick_FSlider_point_buffer,
                    SetPoints = a => Form1.joystick_FSlider_point_buffer = a
                },
                ["VSlider"] = new AxisPointDef
                {
                    GetValue = () => device.CurrentJoystickState.GetVSlider()[0],
                    GetPoints = () => Form1.joystick_VSlider_point_buffer,
                    SetPoints = a => Form1.joystick_VSlider_point_buffer = a
                },
            };
        }

        //============================================================================
        // Установка меток на быбранной оси джойстика
        //============================================================================
        private void button_metka_osi_Click(object sender, EventArgs e)
        {
            if (device == null) return;

            EnsureAxisPoints();

            string sel = Convert.ToString(comboBox_osi_select.SelectedItem);
            if (string.IsNullOrEmpty(sel)) return;

            if (_axisPoints == null || !_axisPoints.TryGetValue(sel, out var def))
                return;

            int val = def.GetValue();

            int[] points = def.GetPoints();
            AppendPoint(ref points, val);
            def.SetPoints(points);
        }

        private void button_metka_osi_MouseDown(object sender, MouseEventArgs e)
        {
            progressBar_osi.ForeColor = Color.LimeGreen;
            label_progressbar_osi.ForeColor = Color.LimeGreen;
        }

        private void button_metka_osi_MouseUp(object sender, MouseEventArgs e)
        {
            progressBar_osi.ForeColor = Color.OrangeRed;
            label_progressbar_osi.ForeColor = Color.OrangeRed;
        }

        private void button_metka_osi_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            progressBar_osi.ForeColor = Color.LimeGreen;
            label_progressbar_osi.ForeColor = Color.LimeGreen;
        }

        private void button_metka_osi_KeyUp(object sender, KeyEventArgs e)
        {
            progressBar_osi.ForeColor = Color.OrangeRed;
            label_progressbar_osi.ForeColor = Color.OrangeRed;
        }

        //============================================================================
        // Для работы sbros_metok_clear_buffer()
        //============================================================================
        private sealed class LocoAxisBinding
        {
            public int[,] Axis;      // Form1.*_axis_buffer
            public string[] SbAxis;  // sb_*_axis_data
        }

        private List<LocoAxisBinding> _locoAxisBindings;

        private void EnsureLocoAxisBindings()
        {
            if (_locoAxisBindings != null) return;

            _locoAxisBindings = new System.Collections.Generic.List<LocoAxisBinding>
    {
        new LocoAxisBinding { Axis = Form1.Controls_axis_buffer,  SbAxis = sb_controls_axis_data  },
        new LocoAxisBinding { Axis = Form1.Neshtatki_axis_buffer, SbAxis = sb_neshtatki_axis_data },
        new LocoAxisBinding { Axis = Form1.ES5K_axis_buffer,      SbAxis = sb_es5k_axis_data      },
        new LocoAxisBinding { Axis = Form1.EP1M_axis_buffer,      SbAxis = sb_ep1m_axis_data      },
        new LocoAxisBinding { Axis = Form1.CHS2K_axis_buffer,     SbAxis = sb_chs2k_axis_data     },
        new LocoAxisBinding { Axis = Form1.CHS4_axis_buffer,      SbAxis = sb_chs4_axis_data      },
        new LocoAxisBinding { Axis = Form1.CHS4KVR_axis_buffer,   SbAxis = sb_chs4kvr_axis_data   },
        new LocoAxisBinding { Axis = Form1.CHS4T_axis_buffer,     SbAxis = sb_chs4t_axis_data     },
        new LocoAxisBinding { Axis = Form1.CHS7_axis_buffer,      SbAxis = sb_chs7_axis_data      },
        new LocoAxisBinding { Axis = Form1.CHS8_axis_buffer,      SbAxis = sb_chs8_axis_data      },
        new LocoAxisBinding { Axis = Form1.VL11M_axis_buffer,     SbAxis = sb_vl11_axis_data      },
        new LocoAxisBinding { Axis = Form1.VL82M_axis_buffer,     SbAxis = sb_vl82_axis_data      },
        new LocoAxisBinding { Axis = Form1.VL80T_axis_buffer,     SbAxis = sb_vl80t_axis_data     },
        new LocoAxisBinding { Axis = Form1.VL85_axis_buffer,      SbAxis = sb_vl85_axis_data      },
        new LocoAxisBinding { Axis = Form1.TEP70_axis_buffer,     SbAxis = sb_tep70_axis_data     },
        new LocoAxisBinding { Axis = Form1.TE10U_axis_buffer,     SbAxis = sb_te10u_axis_data     },
        new LocoAxisBinding { Axis = Form1.M62_axis_buffer,       SbAxis = sb_m62_axis_data       },
        new LocoAxisBinding { Axis = Form1.ED4M_axis_buffer,      SbAxis = sb_ed4m_axis_data      },
        new LocoAxisBinding { Axis = Form1.ED9M_axis_buffer,      SbAxis = sb_ed9m_axis_data      },
        new LocoAxisBinding { Axis = Form1.tem18_axis_buffer,     SbAxis = sb_tem18_axis_data     }, // если есть
    };
        }

        private static void ClearAxisMarksInBinding(LocoAxisBinding b, int axisId)
        {
            if (b == null || b.Axis == null) return;

            int rows = b.Axis.GetLength(0);
            int cols = b.Axis.GetLength(1);
            if (cols < 2) return; // ожидаем [row,0]=axisId, [row,1]=point

            for (int i = 0; i < rows; i++)
            {
                if (b.Axis[i, 0] == axisId)
                {
                    b.Axis[i, 0] = 0;
                    b.Axis[i, 1] = 0;

                    if (b.SbAxis != null && (uint)i < (uint)b.SbAxis.Length)
                        b.SbAxis[i] = null;
                }
            }
        }

        //============================================================================
        // Сброс меток на быбранной оси джойстика
        // axisId = то же, что ты пишешь в *_axis_buffer[row,0] (1..29)
        //============================================================================
        private void sbros_metok_clear_buffer(int axisId)
        {
            EnsureLocoAxisBindings();
            if (_locoAxisBindings == null) return;

            foreach (var b in _locoAxisBindings)
                ClearAxisMarksInBinding(b, axisId);
        }

        //============================================================================
        // Для работы с button_sbros_metok_Click()
        //============================================================================
        private sealed class AxisResetDef
        {
            public int AxisId;                                   // 1..29 (то, что хранится в *_axis_buffer[row,0])
            public string UiName;                                // то, что в comboBox_osi_select.SelectedItem.ToString()
            public SysAction ClearPointBuffer;                   // Form1.joystick_*_point_buffer = null;
            public SysAction ResetSettingToStart;                // Properties.Settings.Default.* = temp_string_buffer_start
        }

        private Dictionary<string, AxisResetDef> _axisResetByUiName;

        private void EnsureAxisResetDefs()
        {
            if (_axisResetByUiName != null) return;

            _axisResetByUiName = new System.Collections.Generic.Dictionary<string, AxisResetDef>(StringComparer.Ordinal)
            {
                ["ARx"] = new AxisResetDef
                {
                    AxisId = 1,
                    UiName = "ARx",
                    ClearPointBuffer = () => Form1.joystick_ARx_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.ARx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["ARy"] = new AxisResetDef
                {
                    AxisId = 2,
                    UiName = "ARy",
                    ClearPointBuffer = () => Form1.joystick_ARy_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.ARy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["ARz"] = new AxisResetDef
                {
                    AxisId = 3,
                    UiName = "ARz",
                    ClearPointBuffer = () => Form1.joystick_ARz_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.ARz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["AX"] = new AxisResetDef
                {
                    AxisId = 4,
                    UiName = "AX",
                    ClearPointBuffer = () => Form1.joystick_AX_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.AX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["AY"] = new AxisResetDef
                {
                    AxisId = 5,
                    UiName = "AY",
                    ClearPointBuffer = () => Form1.joystick_AY_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.AY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["AZ"] = new AxisResetDef
                {
                    AxisId = 6,
                    UiName = "AZ",
                    ClearPointBuffer = () => Form1.joystick_AZ_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.AZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FRx"] = new AxisResetDef
                {
                    AxisId = 7,
                    UiName = "FRx",
                    ClearPointBuffer = () => Form1.joystick_FRx_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FRy"] = new AxisResetDef
                {
                    AxisId = 8,
                    UiName = "FRy",
                    ClearPointBuffer = () => Form1.joystick_FRy_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FRz"] = new AxisResetDef
                {
                    AxisId = 9,
                    UiName = "FRz",
                    ClearPointBuffer = () => Form1.joystick_FRz_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FX"] = new AxisResetDef
                {
                    AxisId = 10,
                    UiName = "FX",
                    ClearPointBuffer = () => Form1.joystick_FX_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FY"] = new AxisResetDef
                {
                    AxisId = 11,
                    UiName = "FY",
                    ClearPointBuffer = () => Form1.joystick_FY_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FZ"] = new AxisResetDef
                {
                    AxisId = 12,
                    UiName = "FZ",
                    ClearPointBuffer = () => Form1.joystick_FZ_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Rx"] = new AxisResetDef
                {
                    AxisId = 13,
                    UiName = "Rx",
                    ClearPointBuffer = () => Form1.joystick_Rx_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Rx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Ry"] = new AxisResetDef
                {
                    AxisId = 14,
                    UiName = "Ry",
                    ClearPointBuffer = () => Form1.joystick_Ry_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Ry_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Rz"] = new AxisResetDef
                {
                    AxisId = 15,
                    UiName = "Rz",
                    ClearPointBuffer = () => Form1.joystick_Rz_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Rz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VRx"] = new AxisResetDef
                {
                    AxisId = 16,
                    UiName = "VRx",
                    ClearPointBuffer = () => Form1.joystick_VRx_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VRy"] = new AxisResetDef
                {
                    AxisId = 17,
                    UiName = "VRy",
                    ClearPointBuffer = () => Form1.joystick_VRy_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VRz"] = new AxisResetDef
                {
                    AxisId = 18,
                    UiName = "VRz",
                    ClearPointBuffer = () => Form1.joystick_VRz_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VX"] = new AxisResetDef
                {
                    AxisId = 19,
                    UiName = "VX",
                    ClearPointBuffer = () => Form1.joystick_VX_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VY"] = new AxisResetDef
                {
                    AxisId = 20,
                    UiName = "VY",
                    ClearPointBuffer = () => Form1.joystick_VY_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VZ"] = new AxisResetDef
                {
                    AxisId = 21,
                    UiName = "VZ",
                    ClearPointBuffer = () => Form1.joystick_VZ_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["X"] = new AxisResetDef
                {
                    AxisId = 22,
                    UiName = "X",
                    ClearPointBuffer = () => Form1.joystick_X_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.X_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Y"] = new AxisResetDef
                {
                    AxisId = 23,
                    UiName = "Y",
                    ClearPointBuffer = () => Form1.joystick_Y_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Y_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Z"] = new AxisResetDef
                {
                    AxisId = 24,
                    UiName = "Z",
                    ClearPointBuffer = () => Form1.joystick_Z_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Z_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["POV"] = new AxisResetDef
                {
                    AxisId = 25,
                    UiName = "POV",
                    ClearPointBuffer = () => Form1.joystick_POV_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.POV_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["Slider"] = new AxisResetDef
                {
                    AxisId = 26,
                    UiName = "Slider",
                    ClearPointBuffer = () => Form1.joystick_Slider_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.Slider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                // ВАЖНО: у тебя в UpdateJoystickState имена "ASlider/FSlider/VSlider",
                // а здесь в if-ах "SliderAS/SliderFS/SliderVS". Приведи к одному!
                ["ASlider"] = new AxisResetDef
                {
                    AxisId = 27,
                    UiName = "ASlider",
                    ClearPointBuffer = () => Form1.joystick_ASlider_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.ASlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["FSlider"] = new AxisResetDef
                {
                    AxisId = 28,
                    UiName = "FSlider",
                    ClearPointBuffer = () => Form1.joystick_FSlider_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.FSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },

                ["VSlider"] = new AxisResetDef
                {
                    AxisId = 29,
                    UiName = "VSlider",
                    ClearPointBuffer = () => Form1.joystick_VSlider_point_buffer = null,
                    ResetSettingToStart = () => Properties.Settings.Default.VSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start
                },
            };
        }

        //============================================================================
        // Кнопка "Сброс точек оси" служит для сброса ранее установленных меток
        // на выбранной оси джойстика
        //============================================================================
        private void button_sbros_metok_Click(object sender, EventArgs e)
        {
            const string title = "Удаление точек оси";
            const string msg = "Точки текущей оси будут удалены, уверены?";

            var result = MessageBox.Show(msg, title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.OK)
                return;

            EnsureAxisResetDefs();

            string sel = Convert.ToString(comboBox_osi_select.SelectedItem);
            if (string.IsNullOrEmpty(sel))
                return;

            // Если в ComboBox у тебя реально "SliderAS/SliderFS/SliderVS" — можно сделать алиасы:
            if (sel == "SliderAS") sel = "ASlider";
            else if (sel == "SliderFS") sel = "FSlider";
            else if (sel == "SliderVS") sel = "VSlider";

            if (!_axisResetByUiName.TryGetValue(sel, out var def))
                return;

            // 1) стерли point_buffer
            def.ClearPointBuffer?.Invoke();

            // 2) сбросили settings на "start"
            def.ResetSettingToStart?.Invoke();

            // 3) вычистили привязки в *_axis_buffer и sb_*_axis_data
            sbros_metok_clear_buffer(def.AxisId);

            // 4) сохранения/перерисовка (оставляем единым блоком)
            sbBufferSave();
            Form1.SaveBuffersSettings();
            KeyDataUpdate();

            Properties.Settings.Default.Save();
        }

        //============================================================================
        // Для работы с button_zdsim_sbros_tek_Click()
        //============================================================================
        private SysAction _resetCurrentLoco;
        private Dictionary<string, SysAction> _locoResetByName;

        private void EnsureLocoResetDefs()
        {
            if (_locoResetByName != null) return;

            _locoResetByName = new System.Collections.Generic.Dictionary<string, SysAction>(System.StringComparer.Ordinal)
            {
                ["Controls"] = () =>
                {
                    // кнопки/оси
                    if (Form1.Controls_key_buffer != null) System.Array.Clear(Form1.Controls_key_buffer, 0, Form1.Controls_key_buffer.Length);
                    if (Form1.Controls_axis_buffer != null) System.Array.Clear(Form1.Controls_axis_buffer, 0, Form1.Controls_axis_buffer.Length);
                    if (sb_controls_axis_data != null) System.Array.Clear(sb_controls_axis_data, 0, sb_controls_axis_data.Length);

                    Properties.Settings.Default.controls_buffer_key_settings?.Clear();
                    Properties.Settings.Default.controls_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.controls_buffer_axis_settings2?.Clear();

                    // звук
                    if (Form1.controls_wav_path_key_buffer != null) System.Array.Clear(Form1.controls_wav_path_key_buffer, 0, Form1.controls_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_controls_wav_path_data_settings?.Clear();
                },

                ["Neshtatki"] = () =>
                {
                    if (Form1.Neshtatki_key_buffer != null) System.Array.Clear(Form1.Neshtatki_key_buffer, 0, Form1.Neshtatki_key_buffer.Length);
                    if (Form1.Neshtatki_axis_buffer != null) System.Array.Clear(Form1.Neshtatki_axis_buffer, 0, Form1.Neshtatki_axis_buffer.Length);
                    if (sb_neshtatki_axis_data != null) System.Array.Clear(sb_neshtatki_axis_data, 0, sb_neshtatki_axis_data.Length);

                    Properties.Settings.Default.neshtatki_buffer_key_settings?.Clear();
                    Properties.Settings.Default.neshtatki_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.neshtatki_buffer_axis_settings2?.Clear();

                    if (Form1.neshtatki_wav_path_key_buffer != null) System.Array.Clear(Form1.neshtatki_wav_path_key_buffer, 0, Form1.neshtatki_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_neshtatki_wav_path_data_settings?.Clear();
                },

                ["2ES5K"] = () =>
                {
                    if (Form1.ES5K_key_buffer != null) System.Array.Clear(Form1.ES5K_key_buffer, 0, Form1.ES5K_key_buffer.Length);
                    if (Form1.ES5K_axis_buffer != null) System.Array.Clear(Form1.ES5K_axis_buffer, 0, Form1.ES5K_axis_buffer.Length);
                    if (sb_es5k_axis_data != null) System.Array.Clear(sb_es5k_axis_data, 0, sb_es5k_axis_data.Length);

                    Properties.Settings.Default.es5k_buffer_key_settings?.Clear();
                    Properties.Settings.Default.es5k_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.es5k_buffer_axis_settings2?.Clear();

                    if (Form1.es5k_wav_path_key_buffer != null) System.Array.Clear(Form1.es5k_wav_path_key_buffer, 0, Form1.es5k_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_es5k_wav_path_data_settings?.Clear();
                },

                ["EP1M"] = () =>
                {
                    if (Form1.EP1M_key_buffer != null) System.Array.Clear(Form1.EP1M_key_buffer, 0, Form1.EP1M_key_buffer.Length);
                    if (Form1.EP1M_axis_buffer != null) System.Array.Clear(Form1.EP1M_axis_buffer, 0, Form1.EP1M_axis_buffer.Length);
                    if (sb_ep1m_axis_data != null) System.Array.Clear(sb_ep1m_axis_data, 0, sb_ep1m_axis_data.Length);

                    Properties.Settings.Default.ep1m_buffer_key_settings?.Clear();
                    Properties.Settings.Default.ep1m_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.ep1m_buffer_axis_settings2?.Clear();

                    if (Form1.ep1m_wav_path_key_buffer != null) System.Array.Clear(Form1.ep1m_wav_path_key_buffer, 0, Form1.ep1m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ep1m_wav_path_data_settings?.Clear();
                },

                ["CHS2K"] = () =>
                {
                    if (Form1.CHS2K_key_buffer != null) System.Array.Clear(Form1.CHS2K_key_buffer, 0, Form1.CHS2K_key_buffer.Length);
                    if (Form1.CHS2K_axis_buffer != null) System.Array.Clear(Form1.CHS2K_axis_buffer, 0, Form1.CHS2K_axis_buffer.Length);
                    if (sb_chs2k_axis_data != null) System.Array.Clear(sb_chs2k_axis_data, 0, sb_chs2k_axis_data.Length);

                    Properties.Settings.Default.chs2k_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs2k_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs2k_buffer_axis_settings2?.Clear();

                    if (Form1.chs2k_wav_path_key_buffer != null) System.Array.Clear(Form1.chs2k_wav_path_key_buffer, 0, Form1.chs2k_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs2k_wav_path_data_settings?.Clear();
                },

                ["CHS4"] = () =>
                {
                    if (Form1.CHS4_key_buffer != null) System.Array.Clear(Form1.CHS4_key_buffer, 0, Form1.CHS4_key_buffer.Length);
                    if (Form1.CHS4_axis_buffer != null) System.Array.Clear(Form1.CHS4_axis_buffer, 0, Form1.CHS4_axis_buffer.Length);
                    if (sb_chs4_axis_data != null) System.Array.Clear(sb_chs4_axis_data, 0, sb_chs4_axis_data.Length);

                    Properties.Settings.Default.chs4_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs4_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs4_buffer_axis_settings2?.Clear();

                    if (Form1.chs4_wav_path_key_buffer != null) System.Array.Clear(Form1.chs4_wav_path_key_buffer, 0, Form1.chs4_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4_wav_path_data_settings?.Clear();
                },

                ["CHS4 KVR"] = () =>
                {
                    if (Form1.CHS4KVR_key_buffer != null) System.Array.Clear(Form1.CHS4KVR_key_buffer, 0, Form1.CHS4KVR_key_buffer.Length);
                    if (Form1.CHS4KVR_axis_buffer != null) System.Array.Clear(Form1.CHS4KVR_axis_buffer, 0, Form1.CHS4KVR_axis_buffer.Length);
                    if (sb_chs4kvr_axis_data != null) System.Array.Clear(sb_chs4kvr_axis_data, 0, sb_chs4kvr_axis_data.Length);

                    Properties.Settings.Default.chs4kvr_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings2?.Clear();

                    if (Form1.chs4kvr_wav_path_key_buffer != null) System.Array.Clear(Form1.chs4kvr_wav_path_key_buffer, 0, Form1.chs4kvr_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings?.Clear();
                },

                ["CHS4T"] = () =>
                {
                    if (Form1.CHS4T_key_buffer != null) System.Array.Clear(Form1.CHS4T_key_buffer, 0, Form1.CHS4T_key_buffer.Length);
                    if (Form1.CHS4T_axis_buffer != null) System.Array.Clear(Form1.CHS4T_axis_buffer, 0, Form1.CHS4T_axis_buffer.Length);
                    if (sb_chs4t_axis_data != null) System.Array.Clear(sb_chs4t_axis_data, 0, sb_chs4t_axis_data.Length);

                    Properties.Settings.Default.chs4t_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs4t_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs4t_buffer_axis_settings2?.Clear();

                    if (Form1.chs4t_wav_path_key_buffer != null) System.Array.Clear(Form1.chs4t_wav_path_key_buffer, 0, Form1.chs4t_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4t_wav_path_data_settings?.Clear();
                },

                ["CHS7"] = () =>
                {
                    if (Form1.CHS7_key_buffer != null) System.Array.Clear(Form1.CHS7_key_buffer, 0, Form1.CHS7_key_buffer.Length);
                    if (Form1.CHS7_axis_buffer != null) System.Array.Clear(Form1.CHS7_axis_buffer, 0, Form1.CHS7_axis_buffer.Length);
                    if (sb_chs7_axis_data != null) System.Array.Clear(sb_chs7_axis_data, 0, sb_chs7_axis_data.Length);

                    Properties.Settings.Default.chs7_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs7_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs7_buffer_axis_settings2?.Clear();

                    if (Form1.chs7_wav_path_key_buffer != null) System.Array.Clear(Form1.chs7_wav_path_key_buffer, 0, Form1.chs7_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs7_wav_path_data_settings?.Clear();
                },

                ["CHS8"] = () =>
                {
                    if (Form1.CHS8_key_buffer != null) System.Array.Clear(Form1.CHS8_key_buffer, 0, Form1.CHS8_key_buffer.Length);
                    if (Form1.CHS8_axis_buffer != null) System.Array.Clear(Form1.CHS8_axis_buffer, 0, Form1.CHS8_axis_buffer.Length);
                    if (sb_chs8_axis_data != null) System.Array.Clear(sb_chs8_axis_data, 0, sb_chs8_axis_data.Length);

                    Properties.Settings.Default.chs8_buffer_key_settings?.Clear();
                    Properties.Settings.Default.chs8_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.chs8_buffer_axis_settings2?.Clear();

                    if (Form1.chs8_wav_path_key_buffer != null) System.Array.Clear(Form1.chs8_wav_path_key_buffer, 0, Form1.chs8_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs8_wav_path_data_settings?.Clear();
                },

                ["VL11M"] = () =>
                {
                    if (Form1.VL11M_key_buffer != null) System.Array.Clear(Form1.VL11M_key_buffer, 0, Form1.VL11M_key_buffer.Length);
                    if (Form1.VL11M_axis_buffer != null) System.Array.Clear(Form1.VL11M_axis_buffer, 0, Form1.VL11M_axis_buffer.Length);
                    if (sb_vl11_axis_data != null) System.Array.Clear(sb_vl11_axis_data, 0, sb_vl11_axis_data.Length);

                    Properties.Settings.Default.vl11_buffer_key_settings?.Clear();
                    Properties.Settings.Default.vl11_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.vl11_buffer_axis_settings2?.Clear();

                    if (Form1.vl11_wav_path_key_buffer != null) System.Array.Clear(Form1.vl11_wav_path_key_buffer, 0, Form1.vl11_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl11_wav_path_data_settings?.Clear();
                },

                ["VL82M"] = () =>
                {
                    if (Form1.VL82M_key_buffer != null) System.Array.Clear(Form1.VL82M_key_buffer, 0, Form1.VL82M_key_buffer.Length);
                    if (Form1.VL82M_axis_buffer != null) System.Array.Clear(Form1.VL82M_axis_buffer, 0, Form1.VL82M_axis_buffer.Length);
                    if (sb_vl82_axis_data != null) System.Array.Clear(sb_vl82_axis_data, 0, sb_vl82_axis_data.Length);

                    Properties.Settings.Default.vl82_buffer_key_settings?.Clear();
                    Properties.Settings.Default.vl82_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.vl82_buffer_axis_settings2?.Clear();

                    if (Form1.vl82_wav_path_key_buffer != null) System.Array.Clear(Form1.vl82_wav_path_key_buffer, 0, Form1.vl82_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl82_wav_path_data_settings?.Clear();
                },

                ["VL80T"] = () =>
                {
                    if (Form1.VL80T_key_buffer != null) System.Array.Clear(Form1.VL80T_key_buffer, 0, Form1.VL80T_key_buffer.Length);
                    if (Form1.VL80T_axis_buffer != null) System.Array.Clear(Form1.VL80T_axis_buffer, 0, Form1.VL80T_axis_buffer.Length);
                    if (sb_vl80t_axis_data != null) System.Array.Clear(sb_vl80t_axis_data, 0, sb_vl80t_axis_data.Length);

                    Properties.Settings.Default.vl80t_buffer_key_settings?.Clear();
                    Properties.Settings.Default.vl80t_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.vl80t_buffer_axis_settings2?.Clear();

                    if (Form1.vl80t_wav_path_key_buffer != null) System.Array.Clear(Form1.vl80t_wav_path_key_buffer, 0, Form1.vl80t_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl80t_wav_path_data_settings?.Clear();
                },

                ["VL85"] = () =>
                {
                    if (Form1.VL85_key_buffer != null) System.Array.Clear(Form1.VL85_key_buffer, 0, Form1.VL85_key_buffer.Length);
                    if (Form1.VL85_axis_buffer != null) System.Array.Clear(Form1.VL85_axis_buffer, 0, Form1.VL85_axis_buffer.Length);
                    if (sb_vl85_axis_data != null) System.Array.Clear(sb_vl85_axis_data, 0, sb_vl85_axis_data.Length);

                    Properties.Settings.Default.vl85_buffer_key_settings?.Clear();
                    Properties.Settings.Default.vl85_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.vl85_buffer_axis_settings2?.Clear();

                    if (Form1.vl85_wav_path_key_buffer != null) System.Array.Clear(Form1.vl85_wav_path_key_buffer, 0, Form1.vl85_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl85_wav_path_data_settings?.Clear();
                },

                ["TEP70"] = () =>
                {
                    if (Form1.TEP70_key_buffer != null) System.Array.Clear(Form1.TEP70_key_buffer, 0, Form1.TEP70_key_buffer.Length);
                    if (Form1.TEP70_axis_buffer != null) System.Array.Clear(Form1.TEP70_axis_buffer, 0, Form1.TEP70_axis_buffer.Length);
                    if (sb_tep70_axis_data != null) System.Array.Clear(sb_tep70_axis_data, 0, sb_tep70_axis_data.Length);

                    Properties.Settings.Default.tep70_buffer_key_settings?.Clear();
                    Properties.Settings.Default.tep70_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.tep70_buffer_axis_settings2?.Clear();

                    if (Form1.tep70_wav_path_key_buffer != null) System.Array.Clear(Form1.tep70_wav_path_key_buffer, 0, Form1.tep70_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_tep70_wav_path_data_settings?.Clear();
                },

                ["2TE10U"] = () =>
                {
                    if (Form1.TE10U_key_buffer != null) System.Array.Clear(Form1.TE10U_key_buffer, 0, Form1.TE10U_key_buffer.Length);
                    if (Form1.TE10U_axis_buffer != null) System.Array.Clear(Form1.TE10U_axis_buffer, 0, Form1.TE10U_axis_buffer.Length);
                    if (sb_te10u_axis_data != null) System.Array.Clear(sb_te10u_axis_data, 0, sb_te10u_axis_data.Length);

                    Properties.Settings.Default.te10u_buffer_key_settings?.Clear();
                    Properties.Settings.Default.te10u_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.te10u_buffer_axis_settings2?.Clear();

                    if (Form1.te10u_wav_path_key_buffer != null) System.Array.Clear(Form1.te10u_wav_path_key_buffer, 0, Form1.te10u_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_te10u_wav_path_data_settings?.Clear();
                },

                ["M62"] = () =>
                {
                    if (Form1.M62_key_buffer != null) System.Array.Clear(Form1.M62_key_buffer, 0, Form1.M62_key_buffer.Length);
                    if (Form1.M62_axis_buffer != null) System.Array.Clear(Form1.M62_axis_buffer, 0, Form1.M62_axis_buffer.Length);
                    if (sb_m62_axis_data != null) System.Array.Clear(sb_m62_axis_data, 0, sb_m62_axis_data.Length);

                    Properties.Settings.Default.m62_buffer_key_settings?.Clear();
                    Properties.Settings.Default.m62_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.m62_buffer_axis_settings2?.Clear();

                    if (Form1.m62_wav_path_key_buffer != null) System.Array.Clear(Form1.m62_wav_path_key_buffer, 0, Form1.m62_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_m62_wav_path_data_settings?.Clear();
                },

                ["ED4M"] = () =>
                {
                    if (Form1.ED4M_key_buffer != null) System.Array.Clear(Form1.ED4M_key_buffer, 0, Form1.ED4M_key_buffer.Length);
                    if (Form1.ED4M_axis_buffer != null) System.Array.Clear(Form1.ED4M_axis_buffer, 0, Form1.ED4M_axis_buffer.Length);
                    if (sb_ed4m_axis_data != null) System.Array.Clear(sb_ed4m_axis_data, 0, sb_ed4m_axis_data.Length);

                    Properties.Settings.Default.ed4m_buffer_key_settings?.Clear();
                    Properties.Settings.Default.ed4m_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.ed4m_buffer_axis_settings2?.Clear();

                    if (Form1.ed4m_wav_path_key_buffer != null) System.Array.Clear(Form1.ed4m_wav_path_key_buffer, 0, Form1.ed4m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ed4m_wav_path_data_settings?.Clear();
                },

                ["ED9M"] = () =>
                {
                    if (Form1.ED9M_key_buffer != null) System.Array.Clear(Form1.ED9M_key_buffer, 0, Form1.ED9M_key_buffer.Length);
                    if (Form1.ED9M_axis_buffer != null) System.Array.Clear(Form1.ED9M_axis_buffer, 0, Form1.ED9M_axis_buffer.Length);
                    if (sb_ed9m_axis_data != null) System.Array.Clear(sb_ed9m_axis_data, 0, sb_ed9m_axis_data.Length);

                    Properties.Settings.Default.ed9m_buffer_key_settings?.Clear();
                    Properties.Settings.Default.ed9m_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.ed9m_buffer_axis_settings2?.Clear();

                    if (Form1.ed9m_wav_path_key_buffer != null) System.Array.Clear(Form1.ed9m_wav_path_key_buffer, 0, Form1.ed9m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ed9m_wav_path_data_settings?.Clear();
                },

                ["tem18"] = () =>
                {
                    if (Form1.tem18_key_buffer != null) System.Array.Clear(Form1.tem18_key_buffer, 0, Form1.tem18_key_buffer.Length);
                    if (Form1.tem18_axis_buffer != null) System.Array.Clear(Form1.tem18_axis_buffer, 0, Form1.tem18_axis_buffer.Length);
                    if (sb_tem18_axis_data != null) System.Array.Clear(sb_tem18_axis_data, 0, sb_tem18_axis_data.Length);

                    Properties.Settings.Default.tem18_buffer_key_settings?.Clear();
                    Properties.Settings.Default.tem18_buffer_axis_settings?.Clear();
                    Properties.Settings.Default.tem18_buffer_axis_settings2?.Clear();

                    if (Form1.tem18_wav_path_key_buffer != null) System.Array.Clear(Form1.tem18_wav_path_key_buffer, 0, Form1.tem18_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_tem18_wav_path_data_settings?.Clear();
                },
            };
        }

        //============================================================================
        // Кнопка "Сброс тек" служит для сброса настроек текущего локомотива
        //============================================================================
        private void button_zdsim_sbros_tek_Click(object sender, EventArgs e)
        {
            string loco = comboBox_zdsimLoco.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(loco))
                return;

            string title = "Удаление настроек текущего локомотива";
            string msg = "Настройки локомотива - " + loco + " будут удалены, уверены?";

            var result = MessageBox.Show(
                msg,
                title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.OK)
                return;

            EnsureLocoResetDefs();

            if (!_locoResetByName.TryGetValue(loco, out var resetAction))
            {
                MessageBox.Show(
                    "Для локомотива \"" + loco + "\" не описан сброс (нет в карте).",
                    "Сброс",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            resetAction();

            // твой общий хвост
            sbBufferSave();
            Form1.SaveBuffersSettings();
            Properties.Settings.Default.Save();
            KeyDataUpdate();
        }

        //============================================================================
        // Кнопка "Удал.кн." — удаление текущей кнопки или точки оси
        //============================================================================
        private void button_zdsim_sbros_key_Click(object sender, EventArgs e)
        {
            // удаляем только если стоим в колонке 1
            if (dataGridView_Zdsimulator.CurrentCell == null) return;
            if (dataGridView_Zdsimulator.CurrentRow == null) return;
            if (dataGridView_Zdsimulator.CurrentCell.ColumnIndex != 1) return;

            string loco = comboBox_zdsimLoco.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(loco)) return;

            int row = dataGridView_Zdsimulator.CurrentRow.Index;
            if (row < 0) return;

            EnsureLocoRowBindingDefs();

            if (!_locoRowBindingByName.TryGetValue(loco, out var def))
                return; // или показать MessageBox, если хочешь

            ClearBindingRow(def, row); // Единая функция «удалить привязку в строке»

            // общий хвост
            sbBufferSave();
            Form1.SaveBuffersSettings();
            Properties.Settings.Default.Save();
            KeyDataUpdate();

            // вернуть курсор и скролл на ту же строку
            if (row >= 0 && row < dataGridView_Zdsimulator.Rows.Count)
            {
                dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = row;
                dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[row].Cells[1];
            }
        }

        //============================================================================
        // Кнопка "Сбросить всё" служит для сброса настроек всех локомотивов
        //============================================================================
        private void button_zdsim_sbros_all_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Настройки всех локомотивов будут удалены, уверены?",
                "Удаление настроек всех локомотивов",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            EnsureLocoRowBindingDefs();
            EnsureSoundBindings();

            foreach (var def in _locoRowBindingByName.Values)
            {
                if (def.KeyBuffer != null)
                    Array.Clear(def.KeyBuffer, 0, def.KeyBuffer.Length);

                if (def.AxisBuffer != null)
                    Array.Clear(def.AxisBuffer, 0, def.AxisBuffer.Length);

                if (def.SbAxisData != null)
                    Array.Clear(def.SbAxisData, 0, def.SbAxisData.Length);

                int rows = def.AxisBuffer?.GetLength(0) ?? 0;

                ResetSizeFillZeros(def.KeySettings, def.KeyBuffer?.Length ?? 0);
                ResetSizeFillZeros(def.AxisSettings1, rows);
                ResetSizeFillZeros(def.AxisSettings2, rows);
            }

            foreach (var sound in _soundByLocoName.Values)
            {
                if (sound.WavPathBuffer != null)
                    Array.Clear(sound.WavPathBuffer, 0, sound.WavPathBuffer.Length);

                EnsureSizeAndFillNulls(sound.WavSettings, sound.WavPathBuffer?.Length ?? 0);
            }

            sbBufferSave();
            sbBufferWavPathSave();

            Form1.SaveBuffersSettings();
            Properties.Settings.Default.Save();
            KeyDataUpdate();
        }

        //============================================================================
        // Полный сброс коллекции: очистить и заполнить "0" заполнить нулями до нужной длины.
        //============================================================================
        private static void ResetSizeFillZeros(StringCollection sc, int size)
        {
            if (sc == null) return;
            sc.Clear();
            for (int i = 0; i < size; i++) sc.Add("0");
        }




        //============================================================================
        // Сброс буферов локомотивов
        //============================================================================
        private void ResetAllLocos()
        {
            EnsureLocoRowBindingDefs();

            foreach (var def in _locoRowBindingByName.Values)
            {
                if (def.KeyBuffer != null) Array.Clear(def.KeyBuffer, 0, def.KeyBuffer.Length);
                if (def.AxisBuffer != null) Array.Clear(def.AxisBuffer, 0, def.AxisBuffer.Length);
                if (def.SbAxisData != null) Array.Clear(def.SbAxisData, 0, def.SbAxisData.Length);

                // Settings — привести к ожидаемой длине
                if (def.KeyBuffer != null)
                    ResetSizeFillZeros(def.KeySettings, def.KeyBuffer.Length);

                int rows = (def.AxisBuffer != null) ? def.AxisBuffer.GetLength(0) : 0;
                ResetSizeFillZeros(def.AxisSettings1, rows);
                ResetSizeFillZeros(def.AxisSettings2, rows);
            }
        }

        //============================================================================
        // Сброс точек осей
        //============================================================================
        private void ResetAllAxisPoints()
        {
            EnsureAxisResetDefs();

            foreach (var def in _axisResetByUiName.Values)
            {
                def.ClearPointBuffer?.Invoke();
                def.ResetSettingToStart?.Invoke();
            }
        }

        //============================================================================
        // Кнопка "Сбросить всё" служит для сброса всех установленных настроек
        //============================================================================
        private void button_sbros_all_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "ВНИМАНИЕ! Все настройки, включая точки осей будут удалены, уверены?",
                    "Удаление ВСЕХ НАСТРОЕК !",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            //Cursor = Cursors.WaitCursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                ResetAllLocos();
                ResetAllAxisPoints();

                Properties.Settings.Default.Save();

                Form1.DeleteXmlBinFiles();
                DeleteAppDataFiles();
                DeleteAppDataFiles_local();

                Properties.Settings.Default.Save();
                Application.Restart();
                //Application.Exit();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        //удаление файла config
        public void DeleteAppDataFiles()
        {
            string baseUserFolder, templateAppdata;
            GetAppDataMyFolderPath(out baseUserFolder, out templateAppdata);

            var users = Directory.GetDirectories(baseUserFolder);
            foreach (var userPath in users)
            {
                try
                {
                    string myFolderPath = userPath + templateAppdata + "\\zdsimScanner";
                    if (Directory.Exists(myFolderPath))
                    {
                        Directory.Delete(myFolderPath, true);
                    }
                    
                }
                catch (Exception ex)
                {
                    //Logging.WriteLogEntry(System.Diagnostics.EventLogEntryType.Error, ex.ToString());
                }
            }
        }

        public void DeleteAppDataFiles_local()
        {
            string baseUserFolder, templateAppdata;
            GetAppDataMyFolderPath_local(out baseUserFolder, out templateAppdata);

            var users = Directory.GetDirectories(baseUserFolder);
            foreach (var userPath in users)
            {
                try
                {
                    string myFolderPath = userPath + templateAppdata + "\\zdsimScanner";
                    if (Directory.Exists(myFolderPath))
                    {
                        Directory.Delete(myFolderPath, true);
                    }
                }
                catch (Exception ex)
                {
                    //Logging.WriteLogEntry(System.Diagnostics.EventLogEntryType.Error, ex.ToString());
                }
            }
        }

        private void GetAppDataMyFolderPath(out string baseUserFolder, out string templateForAppDataPath)
        {
            string appdataFolder = Environment.ExpandEnvironmentVariables("%APPDATA%");
            // results in
            // "C:\Documents and Settings\<username>\Application Data" on XP
            // "C:\Users\<username>\AppData\Roaming" on Vista or above

            //from start to second \
            int scndBackslash = appdataFolder.IndexOf("\\", 5, StringComparison.Ordinal);
            baseUserFolder = appdataFolder.Substring(0, scndBackslash);
            //get rid of C:\Users\<username>
            //cant use Environment.UserName to replace, as it may be SYSTEM while in  %APPDATA% is current user
            templateForAppDataPath = appdataFolder.Substring(
                appdataFolder.IndexOf("\\", scndBackslash + 1, StringComparison.Ordinal));//start at third \
        }

        private void GetAppDataMyFolderPath_local(out string baseUserFolder, out string templateForAppDataPath)
        {
            string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // results in
            // "C:\Documents and Settings\<username>\Application Data" on XP
            // "C:\Users\<username>\AppData\Roaming" on Vista or above

            //from start to second \
            int scndBackslash = appdataFolder.IndexOf("\\", 5, StringComparison.Ordinal);
            baseUserFolder = appdataFolder.Substring(0, scndBackslash);
            //get rid of C:\Users\<username>
            //cant use Environment.UserName to replace, as it may be SYSTEM while in  %APPDATA% is current user
            templateForAppDataPath = appdataFolder.Substring(
                appdataFolder.IndexOf("\\", scndBackslash + 1, StringComparison.Ordinal));//start at third \
        }

        //============================================================================
        // Чек бокс "звуки в режиме передачи" служит для включения воспроизведения
        // звуков при перемещении рукояток
        //============================================================================
        private void checkBox_sound_peredacha_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_sound_peredacha.Checked == true)
            {
                Form1.i_sound_peredacha = 1;
                if (Form2.ActiveForm.CanFocus == true)
                {
                    MessageBox.Show("В режиме передачи звуков\nвозможно снижение производительности !");
                }
            }
            else Form1.i_sound_peredacha = 0;
        }

        //============================================================================
        // Чек бокс "откл.контр.дверей" служит для отключения контроля закрытия дверей 
        // в электропоездах при неисправных дверях
        //============================================================================
        private void checkBox_dvery_control_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_dvery_control.Checked == true) Form1.i_dvery_control_off_settings = 0;
            else Form1.i_dvery_control_off_settings = 1;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        //============================================================================
        // Вспомогательная функция для StringCollection:
        //============================================================================
        private static void EnsureSizeAndFillNulls(StringCollection sc, int size)
        {
            if (sc == null) return;

            while (sc.Count < size) sc.Add(null);
            while (sc.Count > size) sc.RemoveAt(sc.Count - 1);
        }

        //============================================================================
        // Кнопка "Удал.звук" служит для удаления звука установленного для органа управления
        //============================================================================
        private void button_zdsim_sbros_sound_Click(object sender, EventArgs e)
        {
            if (dataGridView_Zdsimulator.CurrentCell?.ColumnIndex != 2) return;
            if (dataGridView_Zdsimulator.CurrentRow == null) return;

            EnsureSoundBindings();
   
            string loco = comboBox_zdsimLoco.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(loco)) return;

            if (!_soundByLocoName.TryGetValue(loco, out var b)) return;

            int row = dataGridView_Zdsimulator.CurrentRow.Index;
            if ((uint)row >= (uint)b.WavPathBuffer.Length) return;

            b.WavPathBuffer[row] = null;

            // ВАЖНО: StringCollection по индексу требует, чтобы элемент существовал.
            // Если коллекция может быть короче — сначала нормализуем длину.
            EnsureSizeAtLeastFillNulls(b.WavSettings, b.WavPathBuffer.Length);
            //EnsureSizeAndFillNulls(b.WavSettings, b.WavPathBuffer.Length);
            b.WavSettings[row] = null;

            KeyDataUpdate();

            dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = row;
            dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[row].Cells[2];

            sbBufferWavPathSave();
            Form1.SaveBuffersSettings();
            Properties.Settings.Default.Save();
        }


    }
}