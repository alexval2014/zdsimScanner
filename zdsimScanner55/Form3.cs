using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

//============================================================================
// Окно "Кнопки и оси джойстика"
//============================================================================
namespace zdsimScanner
{
    public partial class Form_joystick_control : Form
    {
        public Form_joystick_control()
        {
            InitializeComponent();
        }
        int[] b_temp;
        Device device;
        
        /*
        public string sig_ARx = "ARx ";
        public string sig_ARy = "ARy ";
        public string sig_ARz = "ARz ";
        public string sig_AX = "AX  ";
        public string sig_AY = "AY  ";
        public string sig_AZ = "AZ  ";
        public string sig_FRx = "FRx ";
        public string sig_FRy = "FRy ";
        public string sig_FRz = "FRz ";
        public string sig_FX = "FX  ";
        public string sig_FY = "FY  ";
        public string sig_FZ = "FZ  ";
        public string sig_Rx = "Rx  ";
        public string sig_Ry = "Ry  ";
        public string sig_Rz = "Rz  ";
        public string sig_VRx = "VRx ";
        public string sig_VRy = "VRy ";
        public string sig_VRz = "VRz ";
        public string sig_VX = "VX  ";
        public string sig_VY = "VY  ";
        public string sig_VZ = "VZ  ";
        public string sig_X = "X   ";
        public string sig_Y = "Y   ";
        public string sig_Z = "Z   ";
        public string sig_POV = "POV ";
        public string sig_Sld = "Sld ";
        public string sig_ASld = "ASld";
        public string sig_FSld = "FSld";
        public string sig_VSld = "VSld";
        */

        //============================================================================
        // Окно "Кнопки и оси джойстика"  (фрагмент внутри класса Form_joystick_control)
        //============================================================================

        #region Helpers: LocoMap + AxisDefs

        private sealed class LocoBuffers
        {
            public int[] Key;
            public int[,] Axis;
        }

        // ВАЖНО: это static readonly. Нормально, т.к. массивы Form1 — тоже static и уже созданы при старте.
        // Если у тебя бывают сценарии, когда Form1.*_buffer ещё не инициализированы к моменту открытия формы,
        // тогда надо будет сделать карту НЕ static (но в твоём коде это выглядит стабильным).
        private static readonly System.Collections.Generic.Dictionary<string, LocoBuffers> LocoMap =
            new System.Collections.Generic.Dictionary<string, LocoBuffers>(StringComparer.Ordinal)
            {
                ["Controls"] = new LocoBuffers { Key = Form1.Controls_key_buffer, Axis = Form1.Controls_axis_buffer },
                ["Neshtatki"] = new LocoBuffers { Key = Form1.Neshtatki_key_buffer, Axis = Form1.Neshtatki_axis_buffer },
                ["2ES5K"] = new LocoBuffers { Key = Form1.ES5K_key_buffer, Axis = Form1.ES5K_axis_buffer },
                ["EP1M"] = new LocoBuffers { Key = Form1.EP1M_key_buffer, Axis = Form1.EP1M_axis_buffer },
                ["CHS2K"] = new LocoBuffers { Key = Form1.CHS2K_key_buffer, Axis = Form1.CHS2K_axis_buffer },
                ["CHS4"] = new LocoBuffers { Key = Form1.CHS4_key_buffer, Axis = Form1.CHS4_axis_buffer },
                ["CHS4 KVR"] = new LocoBuffers { Key = Form1.CHS4KVR_key_buffer, Axis = Form1.CHS4KVR_axis_buffer },
                ["CHS4T"] = new LocoBuffers { Key = Form1.CHS4T_key_buffer, Axis = Form1.CHS4T_axis_buffer },
                ["CHS7"] = new LocoBuffers { Key = Form1.CHS7_key_buffer, Axis = Form1.CHS7_axis_buffer },
                ["CHS8"] = new LocoBuffers { Key = Form1.CHS8_key_buffer, Axis = Form1.CHS8_axis_buffer },
                ["VL11M"] = new LocoBuffers { Key = Form1.VL11M_key_buffer, Axis = Form1.VL11M_axis_buffer },
                ["VL82M"] = new LocoBuffers { Key = Form1.VL82M_key_buffer, Axis = Form1.VL82M_axis_buffer },
                ["VL80T"] = new LocoBuffers { Key = Form1.VL80T_key_buffer, Axis = Form1.VL80T_axis_buffer },
                ["VL85"] = new LocoBuffers { Key = Form1.VL85_key_buffer, Axis = Form1.VL85_axis_buffer },
                ["TEP70"] = new LocoBuffers { Key = Form1.TEP70_key_buffer, Axis = Form1.TEP70_axis_buffer },
                ["2TE10U"] = new LocoBuffers { Key = Form1.TE10U_key_buffer, Axis = Form1.TE10U_axis_buffer },
                ["M62"] = new LocoBuffers { Key = Form1.M62_key_buffer, Axis = Form1.M62_axis_buffer },
                ["ED4M"] = new LocoBuffers { Key = Form1.ED4M_key_buffer, Axis = Form1.ED4M_axis_buffer },
                ["ED9M"] = new LocoBuffers { Key = Form1.ED9M_key_buffer, Axis = Form1.ED9M_axis_buffer },
                ["tem18"] = new LocoBuffers { Key = Form1.tem18_key_buffer, Axis = Form1.tem18_axis_buffer },
            };

        private static bool TryGetCurrentLocoBuffers(out LocoBuffers b)
        {
            b = null;
            string loco = Convert.ToString(Form2.s_current_loco_select);
            if (string.IsNullOrEmpty(loco)) return false;
            return LocoMap.TryGetValue(loco, out b);
        }

        private static bool SetKeyAndClearAxis(LocoBuffers b, int row, int key1Based)
        {
            if (b == null || b.Key == null || b.Axis == null) return false;

            if ((uint)row >= (uint)b.Key.Length) return false;
            if ((uint)row >= (uint)b.Axis.GetLength(0)) return false;

            b.Key[row] = key1Based;
            b.Axis[row, 0] = 0;
            b.Axis[row, 1] = 0;
            return true;
        }

        private sealed class AxisDef
        {
            public int AxisId;                 // то, что пишем в *_axis_buffer[row,0]
            public string Sig4;                // "ARx ", "AX  ", ...
            public Func<int[]> GetPoints;      // Form1.joystick_*_point_buffer
            public Func<int> GetCurrent;       // текущее значение оси
        }

        private System.Collections.Generic.List<AxisDef> _axesOrdered;
        private System.Collections.Generic.Dictionary<string, AxisDef> _axisBySig4;

        private void EnsureAxisDefs()
        {
            if (_axesOrdered != null) return;

            _axesOrdered = new System.Collections.Generic.List<AxisDef>
    {
        new AxisDef { AxisId =  1, Sig4 = "ARx ", GetPoints = () => Form1.joystick_ARx_point_buffer, GetCurrent = () => device.CurrentJoystickState.ARx },
        new AxisDef { AxisId =  2, Sig4 = "ARy ", GetPoints = () => Form1.joystick_ARy_point_buffer, GetCurrent = () => device.CurrentJoystickState.ARy },
        new AxisDef { AxisId =  3, Sig4 = "ARz ", GetPoints = () => Form1.joystick_ARz_point_buffer, GetCurrent = () => device.CurrentJoystickState.ARz },
        new AxisDef { AxisId =  4, Sig4 = "AX  ", GetPoints = () => Form1.joystick_AX_point_buffer,  GetCurrent = () => device.CurrentJoystickState.AX  },
        new AxisDef { AxisId =  5, Sig4 = "AY  ", GetPoints = () => Form1.joystick_AY_point_buffer,  GetCurrent = () => device.CurrentJoystickState.AY  },
        new AxisDef { AxisId =  6, Sig4 = "AZ  ", GetPoints = () => Form1.joystick_AZ_point_buffer,  GetCurrent = () => device.CurrentJoystickState.AZ  },

        new AxisDef { AxisId =  7, Sig4 = "FRx ", GetPoints = () => Form1.joystick_FRx_point_buffer, GetCurrent = () => device.CurrentJoystickState.FRx },
        new AxisDef { AxisId =  8, Sig4 = "FRy ", GetPoints = () => Form1.joystick_FRy_point_buffer, GetCurrent = () => device.CurrentJoystickState.FRy },
        new AxisDef { AxisId =  9, Sig4 = "FRz ", GetPoints = () => Form1.joystick_FRz_point_buffer, GetCurrent = () => device.CurrentJoystickState.FRz },

        new AxisDef { AxisId = 10, Sig4 = "FX  ", GetPoints = () => Form1.joystick_FX_point_buffer,  GetCurrent = () => device.CurrentJoystickState.FX  },
        new AxisDef { AxisId = 11, Sig4 = "FY  ", GetPoints = () => Form1.joystick_FY_point_buffer,  GetCurrent = () => device.CurrentJoystickState.FY  },
        new AxisDef { AxisId = 12, Sig4 = "FZ  ", GetPoints = () => Form1.joystick_FZ_point_buffer,  GetCurrent = () => device.CurrentJoystickState.FZ  },

        new AxisDef { AxisId = 13, Sig4 = "Rx  ", GetPoints = () => Form1.joystick_Rx_point_buffer,  GetCurrent = () => device.CurrentJoystickState.Rx  },
        new AxisDef { AxisId = 14, Sig4 = "Ry  ", GetPoints = () => Form1.joystick_Ry_point_buffer,  GetCurrent = () => device.CurrentJoystickState.Ry  },
        new AxisDef { AxisId = 15, Sig4 = "Rz  ", GetPoints = () => Form1.joystick_Rz_point_buffer,  GetCurrent = () => device.CurrentJoystickState.Rz  },

        new AxisDef { AxisId = 16, Sig4 = "VRx ", GetPoints = () => Form1.joystick_VRx_point_buffer, GetCurrent = () => device.CurrentJoystickState.VRx },
        new AxisDef { AxisId = 17, Sig4 = "VRy ", GetPoints = () => Form1.joystick_VRy_point_buffer, GetCurrent = () => device.CurrentJoystickState.VRy },
        new AxisDef { AxisId = 18, Sig4 = "VRz ", GetPoints = () => Form1.joystick_VRz_point_buffer, GetCurrent = () => device.CurrentJoystickState.VRz },

        new AxisDef { AxisId = 19, Sig4 = "VX  ", GetPoints = () => Form1.joystick_VX_point_buffer,  GetCurrent = () => device.CurrentJoystickState.VX  },
        new AxisDef { AxisId = 20, Sig4 = "VY  ", GetPoints = () => Form1.joystick_VY_point_buffer,  GetCurrent = () => device.CurrentJoystickState.VY  },
        new AxisDef { AxisId = 21, Sig4 = "VZ  ", GetPoints = () => Form1.joystick_VZ_point_buffer,  GetCurrent = () => device.CurrentJoystickState.VZ  },

        new AxisDef { AxisId = 22, Sig4 = "X   ", GetPoints = () => Form1.joystick_X_point_buffer,   GetCurrent = () => device.CurrentJoystickState.X   },
        new AxisDef { AxisId = 23, Sig4 = "Y   ", GetPoints = () => Form1.joystick_Y_point_buffer,   GetCurrent = () => device.CurrentJoystickState.Y   },
        new AxisDef { AxisId = 24, Sig4 = "Z   ", GetPoints = () => Form1.joystick_Z_point_buffer,   GetCurrent = () => device.CurrentJoystickState.Z   },

        new AxisDef { AxisId = 25, Sig4 = "POV ", GetPoints = () => Form1.joystick_POV_point_buffer,     GetCurrent = () => device.CurrentJoystickState.GetPointOfView()[0] },

        new AxisDef { AxisId = 26, Sig4 = "Sld ", GetPoints = () => Form1.joystick_Slider_point_buffer,  GetCurrent = () => device.CurrentJoystickState.GetSlider()[0] },
        new AxisDef { AxisId = 27, Sig4 = "ASld", GetPoints = () => Form1.joystick_ASlider_point_buffer, GetCurrent = () => device.CurrentJoystickState.GetASlider()[0] },
        new AxisDef { AxisId = 28, Sig4 = "FSld", GetPoints = () => Form1.joystick_FSlider_point_buffer, GetCurrent = () => device.CurrentJoystickState.GetFSlider()[0] },
        new AxisDef { AxisId = 29, Sig4 = "VSld", GetPoints = () => Form1.joystick_VSlider_point_buffer, GetCurrent = () => device.CurrentJoystickState.GetVSlider()[0] },
    };

            _axisBySig4 = new System.Collections.Generic.Dictionary<string, AxisDef>(StringComparer.Ordinal);
            foreach (var a in _axesOrdered)
                _axisBySig4[a.Sig4] = a;
        }

        private static bool TryExtractPointIndex0(string text, out int idx0)
        {
            idx0 = -1;
            int n = 0;
            bool has = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c >= '0' && c <= '9')
                {
                    has = true;
                    n = (n * 10) + (c - '0');
                }
            }

            if (!has) return false;

            idx0 = n - 1;
            return idx0 >= 0;
        }

        #endregion

        //-----------------------------------------------------------------------------
        // ЗАМЕНА 1: UpdateKey()
        //-----------------------------------------------------------------------------
        private void UpdateKey()
        {
            Form2 f2 = this.Owner as Form2;
            if (f2 == null) return;

            if (f2.i_temp_datagird_select_f2 != 1) return;

            if (!TryGetCurrentLocoBuffers(out var lb))
                return;

            if (dataGridView_but.CurrentRow == null) return;

            int row = f2.i_temp_row_number_f2;
            int key = dataGridView_but.CurrentRow.Index + 1;

            SetKeyAndClearAxis(lb, row, key);
        }

        //-----------------------------------------------------------------------------
        // ЗАМЕНА 2: UpdateAxis()
        //-----------------------------------------------------------------------------
        private void UpdateAxis()
        {
            Form2 f2 = this.Owner as Form2;
            if (f2 == null) return;

            if (f2.i_temp_datagird_select_f2 != 1) return;

            if (dataGridView_osi.CurrentCell == null) return;

            string text = Convert.ToString(dataGridView_osi.CurrentCell.Value);
            if (string.IsNullOrEmpty(text) || text.Length < 4) return;

            if (device == null) return;
            EnsureAxisDefs();

            // 1) Ось по сигнатуре первых 4 символов
            string sig4 = text.Substring(0, 4);
            if (_axisBySig4 == null || !_axisBySig4.TryGetValue(sig4, out var axis))
                return;

            // 2) Индекс точки
            if (!TryExtractPointIndex0(text, out int pointIdx0))
                return;

            int[] points = axis.GetPoints();
            if (points == null) return;
            if ((uint)pointIdx0 >= (uint)points.Length) return;

            // 3) Буфер локомотива
            if (!TryGetCurrentLocoBuffers(out var lb))
                return;

            int row = f2.i_temp_row_number_f2;

            // 4) Сброс кнопки и запись оси
            if (lb.Key != null && (uint)row < (uint)lb.Key.Length)
                lb.Key[row] = 0;

            if (lb.Axis == null) return;
            if ((uint)row >= (uint)lb.Axis.GetLength(0)) return;

            lb.Axis[row, 0] = axis.AxisId;         // 1..29
            lb.Axis[row, 1] = points[pointIdx0];   // значение точки
        }

        //-----------------------------------------------------------------------------
        // ЗАМЕНА 3: Form_joystick_control_Load
        //-----------------------------------------------------------------------------
        private void Form_joystick_control_Load(object sender, EventArgs e)
        {
            Joystick_init();
            if (device == null) return;

            EnsureAxisDefs();

            // Кнопки
            int buttons = device.Caps.NumberButtons;

            dataGridView_but.Rows.Clear();
            dataGridView_but.Columns.Clear();
            dataGridView_but.Columns.Add(null, null);
            if (buttons > 0) dataGridView_but.Rows.Add(buttons);

            // Оси = сумма длин всех point_buffer
            int axisRows = 0;
            foreach (var a in _axesOrdered)
            {
                int[] p = a.GetPoints();
                if (p != null) axisRows += p.Length;
            }

            dataGridView_osi.Rows.Clear();
            dataGridView_osi.Columns.Clear();
            dataGridView_osi.Columns.Add(null, null);
            if (axisRows > 0) dataGridView_osi.Rows.Add(axisRows);

            timer1.Enabled = true;
        }

        //-----------------------------------------------------------------------------
        // ЗАМЕНА 4: timer1_Tick
        //-----------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (device == null) return;

            EnsureAxisDefs();
            UpdateJoystickState();

            // Кнопки
            for (int i = 0; i < dataGridView_but.Rows.Count; i++)
            {
                var cell = dataGridView_but.Rows[i].Cells[0];
                cell.Value = "Button " + (i + 1);

                bool pressed =
                    Form1.joystick_buttons_buffer != null &&
                    i < Form1.joystick_buttons_buffer.Length &&
                    Form1.joystick_buttons_buffer[i] != 0;

                cell.Selected = false;
                cell.Style.BackColor = pressed ? Color.Red : Color.Black;
            }

            // Оси (таблично)
            int row = 0;
            int noise = Form1.i_shum_joystick;

            foreach (var a in _axesOrdered)
            {
                int[] points = a.GetPoints();
                if (points == null) continue;

                int cur = a.GetCurrent();

                for (int i = 0; i < points.Length; i++)
                {
                    var cell = dataGridView_osi.Rows[row].Cells[0];

                    // подпись как в старом коде
                    cell.Value = a.Sig4 + (i + 1);

                    bool hit = (points[i] > cur - noise) && (points[i] < cur + noise);

                    cell.Selected = false;
                    cell.Style.BackColor = hit ? Color.Red : Color.Black;

                    row++;
                }
            }
        }

        //==============================================================================
        public void Joystick_init()
        {
            foreach (DeviceInstance instance in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                device = new Device(instance.ProductGuid);
                device.SetCooperativeLevel(null, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);

                foreach (DeviceObjectInstance doi in device.Objects)
                {
                    if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                    {
                        device.Properties.SetRange(
                                ParameterHow.ById,
                                doi.ObjectId,
                            //new InputRange(-32768, 32768));
                                new InputRange(0, 65535));
                    }
                }

                device.Acquire();
                Form1.i_joy_name = Convert.ToString(device.DeviceInformation.InstanceName);
            }
            if (device == null) Form1.i_joy_name = "";
        }

        public void UpdateJoystickState()
        {
            JoystickState j = device.CurrentJoystickState;

            Form1.joystick_axis_buffer[0] = j.ARx;//обычные оси
            Form1.joystick_axis_buffer[1] = j.ARy;//обычные оси
            Form1.joystick_axis_buffer[2] = j.ARz;//обычные оси
            Form1.joystick_axis_buffer[3] = j.AX;//обычные оси
            Form1.joystick_axis_buffer[4] = j.AY;//обычные оси
            Form1.joystick_axis_buffer[5] = j.AZ;//обычные оси
            Form1.joystick_axis_buffer[6] = j.FRx;//обычные оси
            Form1.joystick_axis_buffer[7] = j.FRy;//обычные оси
            Form1.joystick_axis_buffer[8] = j.FRz;//обычные оси
            Form1.joystick_axis_buffer[9] = j.FX;//обычные оси
            Form1.joystick_axis_buffer[10] = j.FY;//обычные оси
            Form1.joystick_axis_buffer[11] = j.FZ;//обычные оси
            Form1.joystick_axis_buffer[12] = j.Rx;//обычные оси
            Form1.joystick_axis_buffer[13] = j.Ry;//обычные оси
            Form1.joystick_axis_buffer[14] = j.Rz;//обычные оси
            Form1.joystick_axis_buffer[15] = j.VRx;//обычные оси
            Form1.joystick_axis_buffer[16] = j.VRy;//обычные оси
            Form1.joystick_axis_buffer[17] = j.VRz;//обычные оси
            Form1.joystick_axis_buffer[18] = j.VX;//обычные оси
            Form1.joystick_axis_buffer[19] = j.VY;//обычные оси
            Form1.joystick_axis_buffer[20] = j.VZ;//обычные оси
            Form1.joystick_axis_buffer[21] = j.X;//обычные оси
            Form1.joystick_axis_buffer[22] = j.Y;//обычные оси
            Form1.joystick_axis_buffer[23] = j.Z;//обычные оси
            b_temp = j.GetPointOfView();//хатка
            Form1.joystick_axis_buffer[24] = b_temp[0];
            b_temp = j.GetSlider();//газ
            Form1.joystick_axis_buffer[25] = b_temp[0];
            b_temp = j.GetASlider();//...
            Form1.joystick_axis_buffer[26] = b_temp[0];
            b_temp = j.GetFSlider();//...
            Form1.joystick_axis_buffer[27] = b_temp[0];
            b_temp = j.GetVSlider();//...
            Form1.joystick_axis_buffer[28] = b_temp[0];

            Form1.joystick_buttons_buffer = j.GetButtons();//кнопки

        }

        private void dataGridView_but_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 f2 = this.Owner as Form2;

           // f2.i_temp_row_number_f3 = Convert.ToInt16(dataGridView_but.CurrentRow.Index);

            //zdsim
            if (f2.i_temp_datagird_select_f2 == 1)
            {
                f2.dataGridView_Zdsimulator.Rows[f2.i_temp_row_number_f2].Cells[1].Value = Convert.ToString(dataGridView_but.CurrentCell.Value.ToString());
            }

            UpdateKey();
            timer1.Enabled = false;
            this.Close();
        }

        private void dataGridView_osi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 f2 = this.Owner as Form2;

            //f2.i_temp_row_number_f3 = Convert.ToInt16(dataGridView_but.CurrentRow.Index);

            //zdsim
            if (f2.i_temp_datagird_select_f2 == 1)
            {
                f2.dataGridView_Zdsimulator.Rows[f2.i_temp_row_number_f2].Cells[1].Value = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                if (f2.i_temp_loco_select == 0)
                {
                    f2.sb_controls_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 19)
                {
                    f2.sb_neshtatki_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 1)
                {
                    f2.sb_es5k_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 2)
                {
                    f2.sb_ep1m_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 3)
                {
                    f2.sb_chs2k_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 4)
                {
                    f2.sb_chs4_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 5)
                {
                    f2.sb_chs4kvr_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 6)
                {
                    f2.sb_chs4t_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 7)
                {
                    f2.sb_chs7_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 8)
                {
                    f2.sb_chs8_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 9)
                {
                    f2.sb_vl11_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 10)
                {
                    f2.sb_vl82_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 11)
                {
                    f2.sb_vl80t_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 12)
                {
                    f2.sb_vl85_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 13)
                {
                    f2.sb_tep70_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 14)
                {
                    f2.sb_te10u_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 15)
                {
                    f2.sb_m62_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 16)
                {
                    f2.sb_ed4m_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 17)
                {
                    f2.sb_ed9m_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
                if (f2.i_temp_loco_select == 18)
                {
                    f2.sb_tem18_axis_data[f2.i_temp_row_number_f2] = Convert.ToString(dataGridView_osi.CurrentCell.Value.ToString());
                }
            }

            UpdateAxis();
            timer1.Enabled = false;
            this.Close();
        }

        private void Form_joystick_control_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
//==============================================================================
/*
        private void UpdateKey()
        {
            Form2 f2 = this.Owner as Form2;
            if (f2.i_temp_datagird_select_f2 == 1)
            {
                if (Convert.ToString(Form2.s_current_loco_select) == "Controls")
                {
                    Form1.Controls_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "Neshtatki")
                {
                    Form1.Neshtatki_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "2ES5K")
                {
                    Form1.ES5K_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "EP1M")
                {
                    Form1.EP1M_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS2K")
                {
                    Form1.CHS2K_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4")
                {
                    Form1.CHS4_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4 KVR")
                {
                    Form1.CHS4KVR_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4T")
                {
                    Form1.CHS4T_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS7")
                {
                    Form1.CHS7_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS8")
                {
                    Form1.CHS8_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL11M")
                {
                    Form1.VL11M_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL82M")
                {
                    Form1.VL82M_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL80T")
                {
                    Form1.VL80T_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL85")
                {
                    Form1.VL85_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "TEP70")
                {
                    Form1.TEP70_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "2TE10U")
                {
                    Form1.TE10U_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "M62")
                {
                    Form1.M62_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "ED4M")
                {
                    Form1.ED4M_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "ED9M")
                {
                    Form1.ED9M_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "tem18")
                {
                    Form1.tem18_key_buffer[f2.i_temp_row_number_f2] = dataGridView_but.CurrentRow.Index + 1;
                    Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 0;
                    Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = 0;
                }
            }
        }

        private void UpdateAxis()
        {
            string s_temp;
            int i_temp = 0;

            Form2 f2 = this.Owner as Form2;
            s_temp = dataGridView_osi.CurrentCell.Value.ToString();
            i_temp = Convert.ToInt16(Regex.Replace(s_temp, @"[^\d]+", "")) - 1;

            if (f2.i_temp_datagird_select_f2 == 1)
            {
                if (Convert.ToString(Form2.s_current_loco_select) == "Controls")
                {
                    Form1.Controls_key_buffer[f2.i_temp_row_number_f2] = 0;

                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.Controls_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "Neshtatki")
                {
                    Form1.Neshtatki_key_buffer[f2.i_temp_row_number_f2] = 0;

                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.Neshtatki_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "2ES5K")
                {
                    Form1.ES5K_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.ES5K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "EP1M")
                {
                    Form1.EP1M_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.EP1M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS2K")
                {
                    Form1.CHS2K_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS2K_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4")
                {
                    Form1.CHS4_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS4_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4 KVR")
                {
                    Form1.CHS4KVR_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS4KVR_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS4T")
                {
                    Form1.CHS4T_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS4T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS7")
                {
                    Form1.CHS7_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS7_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "CHS8")
                {
                    Form1.CHS8_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.CHS8_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL11M")
                {
                    Form1.VL11M_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.VL11M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL82M")
                {
                    Form1.VL82M_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.VL82M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL80T")
                {
                    Form1.VL80T_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.VL80T_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "VL85")
                {
                    Form1.VL85_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.VL85_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "TEP70")
                {
                    Form1.TEP70_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.TEP70_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "2TE10U")
                {
                    Form1.TE10U_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.TE10U_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "M62")
                {
                    Form1.M62_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.M62_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "ED4M")
                {
                    Form1.ED4M_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.ED4M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "ED9M")
                {
                    Form1.ED9M_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.ED9M_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
                if (Convert.ToString(Form2.s_current_loco_select) == "tem18")
                {
                    Form1.tem18_key_buffer[f2.i_temp_row_number_f2] = 0;
                    if (Form1.joystick_ARx_point_buffer != null && s_temp.Substring(0, 4) == sig_ARx)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 1;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARy_point_buffer != null && s_temp.Substring(0, 4) == sig_ARy)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 2;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ARz_point_buffer != null && s_temp.Substring(0, 4) == sig_ARz)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 3;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ARz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AX_point_buffer != null && s_temp.Substring(0, 4) == sig_AX)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 4;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AY_point_buffer != null && s_temp.Substring(0, 4) == sig_AY)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 5;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_AZ_point_buffer != null && s_temp.Substring(0, 4) == sig_AZ)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 6;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_AZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRx_point_buffer != null && s_temp.Substring(0, 4) == sig_FRx)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 7;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRy_point_buffer != null && s_temp.Substring(0, 4) == sig_FRy)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 8;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FRz_point_buffer != null && s_temp.Substring(0, 4) == sig_FRz)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 9;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FX_point_buffer != null && s_temp.Substring(0, 4) == sig_FX)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 10;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FY_point_buffer != null && s_temp.Substring(0, 4) == sig_FY)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 11;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FZ_point_buffer != null && s_temp.Substring(0, 4) == sig_FZ)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 12;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rx_point_buffer != null && s_temp.Substring(0, 4) == sig_Rx)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 13;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Ry_point_buffer != null && s_temp.Substring(0, 4) == sig_Ry)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 14;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Ry_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Rz_point_buffer != null && s_temp.Substring(0, 4) == sig_Rz)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 15;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Rz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRx_point_buffer != null && s_temp.Substring(0, 4) == sig_VRx)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 16;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRx_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRy_point_buffer != null && s_temp.Substring(0, 4) == sig_VRy)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 17;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRy_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VRz_point_buffer != null && s_temp.Substring(0, 4) == sig_VRz)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 18;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VRz_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VX_point_buffer != null && s_temp.Substring(0, 4) == sig_VX)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 19;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VX_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VY_point_buffer != null && s_temp.Substring(0, 4) == sig_VY)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 20;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VY_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VZ_point_buffer != null && s_temp.Substring(0, 4) == sig_VZ)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 21;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VZ_point_buffer[i_temp];
                    }
                    if (Form1.joystick_X_point_buffer != null && s_temp.Substring(0, 4) == sig_X)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 22;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_X_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Y_point_buffer != null && s_temp.Substring(0, 4) == sig_Y)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 23;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Y_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Z_point_buffer != null && s_temp.Substring(0, 4) == sig_Z)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 24;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Z_point_buffer[i_temp];
                    }
                    if (Form1.joystick_POV_point_buffer != null && s_temp.Substring(0, 4) == sig_POV)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 25;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_POV_point_buffer[i_temp];
                    }
                    if (Form1.joystick_Slider_point_buffer != null && s_temp.Substring(0, 4) == sig_Sld)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 26;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_Slider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_ASlider_point_buffer != null && s_temp.Substring(0, 4) == sig_ASld)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 27;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_ASlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_FSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_FSld)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 28;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_FSlider_point_buffer[i_temp];
                    }
                    if (Form1.joystick_VSlider_point_buffer != null && s_temp.Substring(0, 4) == sig_VSld)
                    {
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 0] = 29;
                        Form1.tem18_axis_buffer[f2.i_temp_row_number_f2, 1] = Form1.joystick_VSlider_point_buffer[i_temp];
                    }
                }
            }
        }

        private void Form_joystick_control_Load(object sender, EventArgs e)
        {
            Joystick_init();

            JoystickState j = device.CurrentJoystickState;

            byte buttons_number = Convert.ToByte(device.Caps.NumberButtons);

            dataGridView_but.Rows.Clear();
            dataGridView_but.Columns.Clear();
            dataGridView_but.Columns.Add(null, null);           
            for (int i = 0; i < buttons_number; i++)
            {
                dataGridView_but.Rows.Add();
            }


            int axis_number = 0;

            if (Form1.joystick_ARx_point_buffer != null) axis_number += Form1.joystick_ARx_point_buffer.Length;
            if (Form1.joystick_ARy_point_buffer != null) axis_number += Form1.joystick_ARy_point_buffer.Length;
            if (Form1.joystick_ARz_point_buffer != null) axis_number += Form1.joystick_ARz_point_buffer.Length;
            if (Form1.joystick_AX_point_buffer != null) axis_number += Form1.joystick_AX_point_buffer.Length;
            if (Form1.joystick_AY_point_buffer != null) axis_number += Form1.joystick_AY_point_buffer.Length;
            if (Form1.joystick_AZ_point_buffer != null) axis_number += Form1.joystick_AZ_point_buffer.Length;
            if (Form1.joystick_FRx_point_buffer != null) axis_number += Form1.joystick_FRx_point_buffer.Length;
            if (Form1.joystick_FRy_point_buffer != null) axis_number += Form1.joystick_FRy_point_buffer.Length;
            if (Form1.joystick_FRz_point_buffer != null) axis_number += Form1.joystick_FRz_point_buffer.Length;
            if (Form1.joystick_FX_point_buffer != null) axis_number += Form1.joystick_FX_point_buffer.Length;
            if (Form1.joystick_FY_point_buffer != null) axis_number += Form1.joystick_FY_point_buffer.Length;
            if (Form1.joystick_FZ_point_buffer != null) axis_number += Form1.joystick_FZ_point_buffer.Length;
            if (Form1.joystick_Rx_point_buffer != null) axis_number += Form1.joystick_Rx_point_buffer.Length;
            if (Form1.joystick_Ry_point_buffer != null) axis_number += Form1.joystick_Ry_point_buffer.Length;
            if (Form1.joystick_Rz_point_buffer != null) axis_number += Form1.joystick_Rz_point_buffer.Length;
            if (Form1.joystick_VRx_point_buffer != null) axis_number += Form1.joystick_VRx_point_buffer.Length;
            if (Form1.joystick_VRy_point_buffer != null) axis_number += Form1.joystick_VRy_point_buffer.Length;
            if (Form1.joystick_VRz_point_buffer != null) axis_number += Form1.joystick_VRz_point_buffer.Length;
            if (Form1.joystick_VX_point_buffer != null) axis_number += Form1.joystick_VX_point_buffer.Length;
            if (Form1.joystick_VY_point_buffer != null) axis_number += Form1.joystick_VY_point_buffer.Length;
            if (Form1.joystick_VZ_point_buffer != null) axis_number += Form1.joystick_VZ_point_buffer.Length;
            if (Form1.joystick_X_point_buffer != null) axis_number += Form1.joystick_X_point_buffer.Length;
            if (Form1.joystick_Y_point_buffer != null) axis_number += Form1.joystick_Y_point_buffer.Length;
            if (Form1.joystick_Z_point_buffer != null) axis_number += Form1.joystick_Z_point_buffer.Length;
            if (Form1.joystick_POV_point_buffer != null) axis_number += Form1.joystick_POV_point_buffer.Length;
            if (Form1.joystick_Slider_point_buffer != null) axis_number += Form1.joystick_Slider_point_buffer.Length;
            if (Form1.joystick_ASlider_point_buffer != null) axis_number += Form1.joystick_ASlider_point_buffer.Length;
            if (Form1.joystick_FSlider_point_buffer != null) axis_number += Form1.joystick_FSlider_point_buffer.Length;
            if (Form1.joystick_VSlider_point_buffer != null) axis_number += Form1.joystick_VSlider_point_buffer.Length;

            dataGridView_osi.Rows.Clear();
            dataGridView_osi.Columns.Clear();
            dataGridView_osi.Columns.Add(null, null);
            for (int i = 0; i < axis_number; i++)
            {
                dataGridView_osi.Rows.Add();
            }

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            UpdateJoystickState();

            for (int i = 0; i < dataGridView_but.Rows.Count; i++)
            {
                dataGridView_but.Rows[i].Cells[0].Value = Convert.ToString("Button " + (i + 1));
                if (Form1.joystick_buttons_buffer[i] != 0)
                {
                    dataGridView_but.Rows[i].Cells[0].Selected = false;
                    dataGridView_but.Rows[i].Cells[0].Style.BackColor = Color.Red;
                }
                else dataGridView_but.Rows[i].Cells[0].Style.BackColor = Color.Black;
            }

            int j = 0;

            if (Form1.joystick_ARx_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_ARx_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("ARx " + (i + 1));
                    if (Form1.joystick_ARx_point_buffer[i] > device.CurrentJoystickState.ARx - Form1.i_shum_joystick &&
                        Form1.joystick_ARx_point_buffer[i] < device.CurrentJoystickState.ARx + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }

            if (Form1.joystick_ARy_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_ARy_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("ARy " + (i + 1));
                    if (Form1.joystick_ARy_point_buffer[i] > device.CurrentJoystickState.ARy - Form1.i_shum_joystick &&
                        Form1.joystick_ARy_point_buffer[i] < device.CurrentJoystickState.ARy + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_ARz_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_ARz_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("ARz " + (i + 1));
                    if (Form1.joystick_ARz_point_buffer[i] > device.CurrentJoystickState.ARz - Form1.i_shum_joystick &&
                        Form1.joystick_ARz_point_buffer[i] < device.CurrentJoystickState.ARz + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_AX_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_AX_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("AX  " + (i + 1));
                    if (Form1.joystick_AX_point_buffer[i] > device.CurrentJoystickState.AX - Form1.i_shum_joystick &&
                        Form1.joystick_AX_point_buffer[i] < device.CurrentJoystickState.AX + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_AY_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_AY_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("AY  " + (i + 1));
                    if (Form1.joystick_AY_point_buffer[i] > device.CurrentJoystickState.AY - Form1.i_shum_joystick &&
                        Form1.joystick_AY_point_buffer[i] < device.CurrentJoystickState.AY + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_AZ_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_AZ_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("AZ  " + (i + 1));
                    if (Form1.joystick_AZ_point_buffer[i] > device.CurrentJoystickState.AZ - Form1.i_shum_joystick &&
                        Form1.joystick_AZ_point_buffer[i] < device.CurrentJoystickState.AZ + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FRx_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FRx_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FRx " + (i + 1));
                    if (Form1.joystick_FRx_point_buffer[i] > device.CurrentJoystickState.FRx - Form1.i_shum_joystick &&
                        Form1.joystick_FRx_point_buffer[i] < device.CurrentJoystickState.FRx + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FRy_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FRy_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FRy " + (i + 1));
                    if (Form1.joystick_FRy_point_buffer[i] > device.CurrentJoystickState.FRy - Form1.i_shum_joystick &&
                        Form1.joystick_FRy_point_buffer[i] < device.CurrentJoystickState.FRy + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FRz_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FRz_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FRz " + (i + 1));
                    if (Form1.joystick_FRz_point_buffer[i] > device.CurrentJoystickState.FRz - Form1.i_shum_joystick &&
                        Form1.joystick_FRz_point_buffer[i] < device.CurrentJoystickState.FRz + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FX_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FX_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FX  " + (i + 1));
                    if (Form1.joystick_FX_point_buffer[i] > device.CurrentJoystickState.FX - Form1.i_shum_joystick &&
                        Form1.joystick_FX_point_buffer[i] < device.CurrentJoystickState.FX + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FY_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FY_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FY  " + (i + 1));
                    if (Form1.joystick_FY_point_buffer[i] > device.CurrentJoystickState.FY - Form1.i_shum_joystick &&
                        Form1.joystick_FY_point_buffer[i] < device.CurrentJoystickState.FY + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FZ_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FZ_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FZ  " + (i + 1));
                    if (Form1.joystick_FZ_point_buffer[i] > device.CurrentJoystickState.FZ - Form1.i_shum_joystick &&
                        Form1.joystick_FZ_point_buffer[i] < device.CurrentJoystickState.FZ + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Rx_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Rx_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Rx  " + (i + 1));
                    if (Form1.joystick_Rx_point_buffer[i] > device.CurrentJoystickState.Rx - Form1.i_shum_joystick &&
                        Form1.joystick_Rx_point_buffer[i] < device.CurrentJoystickState.Rx + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Ry_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Ry_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Ry  " + (i + 1));
                    if (Form1.joystick_Ry_point_buffer[i] > device.CurrentJoystickState.Ry - Form1.i_shum_joystick &&
                        Form1.joystick_Ry_point_buffer[i] < device.CurrentJoystickState.Ry + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Rz_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Rz_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Rz  " + (i + 1));
                    if (Form1.joystick_Rz_point_buffer[i] > device.CurrentJoystickState.Rz - Form1.i_shum_joystick &&
                        Form1.joystick_Rz_point_buffer[i] < device.CurrentJoystickState.Rz + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VRx_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VRx_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VRx " + (i + 1));
                    if (Form1.joystick_VRx_point_buffer[i] > device.CurrentJoystickState.VRx - Form1.i_shum_joystick &&
                        Form1.joystick_VRx_point_buffer[i] < device.CurrentJoystickState.VRx + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VRy_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VRy_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VRy " + (i + 1));
                    if (Form1.joystick_VRy_point_buffer[i] > device.CurrentJoystickState.VRy - Form1.i_shum_joystick &&
                        Form1.joystick_VRy_point_buffer[i] < device.CurrentJoystickState.VRy + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VRz_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VRz_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VRz " + (i + 1));
                    if (Form1.joystick_VRz_point_buffer[i] > device.CurrentJoystickState.VRz - Form1.i_shum_joystick &&
                        Form1.joystick_VRz_point_buffer[i] < device.CurrentJoystickState.VRz + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VX_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VX_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VX  " + (i + 1));
                    if (Form1.joystick_VX_point_buffer[i] > device.CurrentJoystickState.VX - Form1.i_shum_joystick &&
                        Form1.joystick_VX_point_buffer[i] < device.CurrentJoystickState.VX + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VY_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VY_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VY  " + (i + 1));
                    if (Form1.joystick_VY_point_buffer[i] > device.CurrentJoystickState.VY - Form1.i_shum_joystick &&
                        Form1.joystick_VY_point_buffer[i] < device.CurrentJoystickState.VY + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VZ_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VZ_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VZ  " + (i + 1));
                    if (Form1.joystick_VZ_point_buffer[i] > device.CurrentJoystickState.VZ - Form1.i_shum_joystick &&
                        Form1.joystick_VZ_point_buffer[i] < device.CurrentJoystickState.VZ + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_X_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_X_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("X   " + (i + 1));
                    if (Form1.joystick_X_point_buffer[i] > device.CurrentJoystickState.X - Form1.i_shum_joystick &&
                        Form1.joystick_X_point_buffer[i] < device.CurrentJoystickState.X + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Y_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Y_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Y   " + (i + 1));
                    if (Form1.joystick_Y_point_buffer[i] > device.CurrentJoystickState.Y - Form1.i_shum_joystick &&
                        Form1.joystick_Y_point_buffer[i] < device.CurrentJoystickState.Y + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Z_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Z_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Z   " + (i + 1));
                    if (Form1.joystick_Z_point_buffer[i] > device.CurrentJoystickState.Z - Form1.i_shum_joystick &&
                        Form1.joystick_Z_point_buffer[i] < device.CurrentJoystickState.Z + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_POV_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_POV_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("POV " + (i + 1));
                    b_temp = device.CurrentJoystickState.GetPointOfView();
                    if (Form1.joystick_POV_point_buffer[i] > b_temp[0] - Form1.i_shum_joystick &&
                        Form1.joystick_POV_point_buffer[i] < b_temp[0] + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_Slider_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_Slider_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("Sld " + (i + 1));
                    b_temp = device.CurrentJoystickState.GetSlider();
                    if (Form1.joystick_Slider_point_buffer[i] > b_temp[0] - Form1.i_shum_joystick &&
                        Form1.joystick_Slider_point_buffer[i] < b_temp[0] + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_ASlider_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_ASlider_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("ASld" + (i + 1));
                    b_temp = device.CurrentJoystickState.GetASlider();
                    if (Form1.joystick_ASlider_point_buffer[i] > b_temp[0] - Form1.i_shum_joystick &&
                        Form1.joystick_ASlider_point_buffer[i] < b_temp[0] + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_FSlider_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_FSlider_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("FSld" + (i + 1));
                    b_temp = device.CurrentJoystickState.GetFSlider();
                    if (Form1.joystick_FSlider_point_buffer[i] > b_temp[0] - Form1.i_shum_joystick &&
                        Form1.joystick_FSlider_point_buffer[i] < b_temp[0] + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }
            if (Form1.joystick_VSlider_point_buffer != null)
            {
                for (int i = 0; i < Form1.joystick_VSlider_point_buffer.Length; i++)
                {
                    dataGridView_osi.Rows[j].Cells[0].Value = Convert.ToString("VSld" + (i + 1));
                    b_temp = device.CurrentJoystickState.GetVSlider();
                    if (Form1.joystick_VSlider_point_buffer[i] > b_temp[0] - Form1.i_shum_joystick &&
                        Form1.joystick_VSlider_point_buffer[i] < b_temp[0] + Form1.i_shum_joystick)
                    {
                        dataGridView_osi.Rows[j].Cells[0].Selected = false;
                        dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Red;
                    }
                    else dataGridView_osi.Rows[j].Cells[0].Style.BackColor = Color.Black;
                    j++;
                }
            }

        }
 */