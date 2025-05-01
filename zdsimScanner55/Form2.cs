using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;


namespace zdsimScanner
{
    public partial class Form2 : Form
    {

        public int i_temp_row_number_f2;
        public int i_temp_row_number_f3;
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


        /************************************zdsim*****************************************/
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
        /************************************zdsim*****************************************/
        Device device;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox_zdsimLoco.Text = comboBox_zdsimLoco.Items[0].ToString();           
            comboBox_osi_select.SelectedIndex = 0;

            this.textBox1.Text = (Properties.Settings.Default.skor_tek);
            this.textBox2.Text = (Properties.Settings.Default.tok_ept);
            this.textBox3.Text = (Properties.Settings.Default.napr_ks);
            this.textBox4.Text = (Properties.Settings.Default.napr_td);
            this.textBox5.Text = (Properties.Settings.Default.tok);
            this.textBox6.Text = (Properties.Settings.Default.pnevmatika);
            this.numericUpDown7.Value = Properties.Settings.Default.delay_motor;
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
        //загрузка отрисовки осей
        private void sbBufferUpdate()
        {
            //загрузка буфера отрисовки zdsim
            if (Properties.Settings.Default.sb_controls_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_controls_axis_data.Length; i++)
                {
                    sb_controls_axis_data[i] = Properties.Settings.Default.sb_controls_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_neshtatki_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_neshtatki_axis_data.Length; i++)
                {
                    sb_neshtatki_axis_data[i] = Properties.Settings.Default.sb_neshtatki_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_es5k_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_es5k_axis_data.Length; i++)
                {
                    sb_es5k_axis_data[i] = Properties.Settings.Default.sb_es5k_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ep1m_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_ep1m_axis_data.Length; i++)
                {
                    sb_ep1m_axis_data[i] = Properties.Settings.Default.sb_ep1m_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs2k_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs2k_axis_data.Length; i++)
                {
                    sb_chs2k_axis_data[i] = Properties.Settings.Default.sb_chs2k_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs4_axis_data.Length; i++)
                {
                    sb_chs4_axis_data[i] = Properties.Settings.Default.sb_chs4_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4kvr_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs4kvr_axis_data.Length; i++)
                {
                    sb_chs4kvr_axis_data[i] = Properties.Settings.Default.sb_chs4kvr_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4t_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs4t_axis_data.Length; i++)
                {
                    sb_chs4t_axis_data[i] = Properties.Settings.Default.sb_chs4t_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs7_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs7_axis_data.Length; i++)
                {
                    sb_chs7_axis_data[i] = Properties.Settings.Default.sb_chs7_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs8_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_chs8_axis_data.Length; i++)
                {
                    sb_chs8_axis_data[i] = Properties.Settings.Default.sb_chs8_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl11_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_vl11_axis_data.Length; i++)
                {
                    sb_vl11_axis_data[i] = Properties.Settings.Default.sb_vl11_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl82_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_vl82_axis_data.Length; i++)
                {
                    sb_vl82_axis_data[i] = Properties.Settings.Default.sb_vl82_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl80t_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_vl80t_axis_data.Length; i++)
                {
                    sb_vl80t_axis_data[i] = Properties.Settings.Default.sb_vl80t_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl85_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_vl85_axis_data.Length; i++)
                {
                    sb_vl85_axis_data[i] = Properties.Settings.Default.sb_vl85_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_tep70_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_tep70_axis_data.Length; i++)
                {
                    sb_tep70_axis_data[i] = Properties.Settings.Default.sb_tep70_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_te10u_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_te10u_axis_data.Length; i++)
                {
                    sb_te10u_axis_data[i] = Properties.Settings.Default.sb_te10u_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_m62_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_m62_axis_data.Length; i++)
                {
                    sb_m62_axis_data[i] = Properties.Settings.Default.sb_m62_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ed4m_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_ed4m_axis_data.Length; i++)
                {
                    sb_ed4m_axis_data[i] = Properties.Settings.Default.sb_ed4m_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ed9m_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_ed9m_axis_data.Length; i++)
                {
                    sb_ed9m_axis_data[i] = Properties.Settings.Default.sb_ed9m_axis_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_tem18_axis_data_settings[0] != "start")
            {
                for (int i = 0; i < sb_tem18_axis_data.Length; i++)
                {
                    sb_tem18_axis_data[i] = Properties.Settings.Default.sb_tem18_axis_data_settings[i];
                }
            }

             Properties.Settings.Default.Save();
        }
        //сохранение отрисовки осей
        private void sbBufferSave()
        {
            //сохраняем буфер отрисовки datagridzdsim
            Properties.Settings.Default.sb_controls_axis_data_settings.Clear();
            for (int i = 0; i < sb_controls_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_controls_axis_data_settings.Add(Convert.ToString(sb_controls_axis_data[i]));
            }
            Properties.Settings.Default.sb_neshtatki_axis_data_settings.Clear();
            for (int i = 0; i < sb_neshtatki_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_neshtatki_axis_data_settings.Add(Convert.ToString(sb_neshtatki_axis_data[i]));
            }
            Properties.Settings.Default.sb_es5k_axis_data_settings.Clear();
            for (int i = 0; i < sb_es5k_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_es5k_axis_data_settings.Add(Convert.ToString(sb_es5k_axis_data[i]));
            }
            Properties.Settings.Default.sb_ep1m_axis_data_settings.Clear();
            for (int i = 0; i < sb_ep1m_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_ep1m_axis_data_settings.Add(Convert.ToString(sb_ep1m_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs2k_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs2k_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs2k_axis_data_settings.Add(Convert.ToString(sb_chs2k_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs4_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs4_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs4_axis_data_settings.Add(Convert.ToString(sb_chs4_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs4kvr_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs4kvr_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs4kvr_axis_data_settings.Add(Convert.ToString(sb_chs4kvr_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs4t_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs4t_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs4t_axis_data_settings.Add(Convert.ToString(sb_chs4t_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs7_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs7_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs7_axis_data_settings.Add(Convert.ToString(sb_chs7_axis_data[i]));
            }
            Properties.Settings.Default.sb_chs8_axis_data_settings.Clear();
            for (int i = 0; i < sb_chs8_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_chs8_axis_data_settings.Add(Convert.ToString(sb_chs8_axis_data[i]));
            }
            Properties.Settings.Default.sb_vl11_axis_data_settings.Clear();
            for (int i = 0; i < sb_vl11_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_vl11_axis_data_settings.Add(Convert.ToString(sb_vl11_axis_data[i]));
            }
            Properties.Settings.Default.sb_vl82_axis_data_settings.Clear();
            for (int i = 0; i < sb_vl82_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_vl82_axis_data_settings.Add(Convert.ToString(sb_vl82_axis_data[i]));
            }
            Properties.Settings.Default.sb_vl80t_axis_data_settings.Clear();
            for (int i = 0; i < sb_vl80t_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_vl80t_axis_data_settings.Add(Convert.ToString(sb_vl80t_axis_data[i]));
            }
            Properties.Settings.Default.sb_vl85_axis_data_settings.Clear();
            for (int i = 0; i < sb_vl85_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_vl85_axis_data_settings.Add(Convert.ToString(sb_vl85_axis_data[i]));
            }
            Properties.Settings.Default.sb_tep70_axis_data_settings.Clear();
            for (int i = 0; i < sb_tep70_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_tep70_axis_data_settings.Add(Convert.ToString(sb_tep70_axis_data[i]));
            }
            Properties.Settings.Default.sb_te10u_axis_data_settings.Clear();
            for (int i = 0; i < sb_te10u_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_te10u_axis_data_settings.Add(Convert.ToString(sb_te10u_axis_data[i]));
            }
            Properties.Settings.Default.sb_m62_axis_data_settings.Clear();
            for (int i = 0; i < sb_m62_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_m62_axis_data_settings.Add(Convert.ToString(sb_m62_axis_data[i]));
            }
            Properties.Settings.Default.sb_ed4m_axis_data_settings.Clear();
            for (int i = 0; i < sb_ed4m_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_ed4m_axis_data_settings.Add(Convert.ToString(sb_ed4m_axis_data[i]));
            }
            Properties.Settings.Default.sb_ed9m_axis_data_settings.Clear();
            for (int i = 0; i < sb_ed9m_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_ed9m_axis_data_settings.Add(Convert.ToString(sb_ed9m_axis_data[i]));
            }
            Properties.Settings.Default.sb_tem18_axis_data_settings.Clear();
            for (int i = 0; i < sb_tem18_axis_data.Length; i++)
            {
                Properties.Settings.Default.sb_tem18_axis_data_settings.Add(Convert.ToString(sb_tem18_axis_data[i]));
            }
         }
        //сохранение отрисовки звуков
        private void sbBufferWavPathSave()
        {
            //сохраняем буфер отрисовки звуков zdsim
            Properties.Settings.Default.sb_controls_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.controls_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_controls_wav_path_data_settings.Add(Form1.controls_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_neshtatki_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.neshtatki_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_neshtatki_wav_path_data_settings.Add(Form1.neshtatki_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_es5k_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.es5k_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_es5k_wav_path_data_settings.Add(Form1.es5k_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.ep1m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Add(Form1.ep1m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs2k_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Add(Form1.chs2k_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs4_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4_wav_path_data_settings.Add(Form1.chs4_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs4kvr_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Add(Form1.chs4kvr_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs4t_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Add(Form1.chs4t_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs7_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs7_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs7_wav_path_data_settings.Add(Form1.chs7_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs8_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.chs8_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs8_wav_path_data_settings.Add(Form1.chs8_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl11_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.vl11_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl11_wav_path_data_settings.Add(Form1.vl11_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl82_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.vl82_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl82_wav_path_data_settings.Add(Form1.vl82_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.vl80t_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Add(Form1.vl80t_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl85_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.vl85_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl85_wav_path_data_settings.Add(Form1.vl85_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_tep70_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.tep70_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_tep70_wav_path_data_settings.Add(Form1.tep70_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_te10u_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.te10u_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_te10u_wav_path_data_settings.Add(Form1.te10u_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_m62_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.m62_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_m62_wav_path_data_settings.Add(Form1.m62_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.ed4m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Add(Form1.ed4m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.ed9m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Add(Form1.ed9m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_tem18_wav_path_data_settings.Clear();
            for (int i = 0; i < Form1.tem18_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_tem18_wav_path_data_settings.Add(Form1.tem18_wav_path_key_buffer[i]);
            }
         }

        public string Joystick_init()
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
                                new InputRange(0,65535));
                    }
                }

                device.Acquire();
                Form1.i_joy_name = Convert.ToString(device.DeviceInformation.InstanceName);
            }
            if (device == null) Form1.i_joy_name = "";
            return Form1.i_joy_name;
        }

        public void UpdateJoystickState()
        {
            int[] b_temp;

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

            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARx")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.ARx);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.ARx);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARy")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.ARy);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.ARy);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARz")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.ARz);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.ARz);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AX")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.AX);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.AX);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AY")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.AY);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.AY);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AZ")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.AZ);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.AZ);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRx")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FRx);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FRx);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRy")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FRy);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FRy);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRz")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FRz);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FRz);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FX")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FX);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FX);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FY")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FY);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FY);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FZ")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.FZ);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.FZ);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Rx")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.Rx);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.Rx);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Ry")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.Ry);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.Ry);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Rz")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.Rz);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.Rz);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRx")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VRx);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VRx);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRy")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VRy);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VRy);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRz")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VRz);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VRz);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VX")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VX);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VX);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VY")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VY);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VY);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VZ")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.VZ);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.VZ);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "X")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.X);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.X);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Y")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.Y);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.Y);
            } 
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Z")
            {
                progressBar_osi.Value = Convert.ToUInt16(device.CurrentJoystickState.Z);
                label_progressbar_osi.Text = Convert.ToString(device.CurrentJoystickState.Z);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "POV")
            {
                b_temp = device.CurrentJoystickState.GetPointOfView();
                if (b_temp[0] == -1) b_temp[0] = 65535;
                if (b_temp[0] == 0) b_temp[0] = 1;
                progressBar_osi.Value = Convert.ToUInt16(b_temp[0]);
                label_progressbar_osi.Text = Convert.ToString(b_temp[0]);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Slider")
            {
                b_temp = device.CurrentJoystickState.GetSlider();
                progressBar_osi.Value = Convert.ToUInt16(b_temp[0]);
                label_progressbar_osi.Text = Convert.ToString(b_temp[0]);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ASlider")
            {
                b_temp = device.CurrentJoystickState.GetASlider();
                progressBar_osi.Value = Convert.ToUInt16(b_temp[0]);
                label_progressbar_osi.Text = Convert.ToString(b_temp[0]);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FSlider")
            {
                b_temp = device.CurrentJoystickState.GetFSlider();
                progressBar_osi.Value = Convert.ToUInt16(b_temp[0]);
                label_progressbar_osi.Text = Convert.ToString(b_temp[0]);
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VSlider")
            {
                b_temp = device.CurrentJoystickState.GetVSlider();
                progressBar_osi.Value = Convert.ToUInt16(b_temp[0]);
                label_progressbar_osi.Text = Convert.ToString(b_temp[0]);
            }



        }

        public string[] LocoKeyData = new string[]{

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

        public void KeyDataUpdate()
        {
            s_current_loco_select = comboBox_zdsimLoco.Text;
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "Controls")
            {
                i_temp_loco_select = 0;
                int j = 0;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);//имя
                dataGridView_Zdsimulator.Columns.Add(null, null);//кнопка или ось
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 34; i++)
                {                   
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_controls_axis_data != null && sb_controls_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_controls_axis_data[i];
                    }
                    if (Form1.Controls_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.Controls_key_buffer[i]);
                    }
                    if (Form1.controls_wav_path_key_buffer != null && Form1.controls_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.controls_wav_path_key_buffer[i].Substring(Form1.controls_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "2ES5K")
            {
                i_temp_loco_select = 1;
                int j = 34;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 109; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_es5k_axis_data != null && sb_es5k_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_es5k_axis_data[i];
                    }
                    if (Form1.ES5K_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.ES5K_key_buffer[i]);
                    }
                    if (Form1.es5k_wav_path_key_buffer != null && Form1.es5k_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.es5k_wav_path_key_buffer[i].Substring(Form1.es5k_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "EP1M")
            {
                i_temp_loco_select = 2;
                int j = 143;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 112; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_ep1m_axis_data != null && sb_ep1m_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_ep1m_axis_data[i];
                    }
                    if (Form1.EP1M_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.EP1M_key_buffer[i]);
                    }
                    if (Form1.ep1m_wav_path_key_buffer != null && Form1.ep1m_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.ep1m_wav_path_key_buffer[i].Substring(Form1.ep1m_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS2K")
            {
                i_temp_loco_select = 3;
                int j = 255;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 32; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs2k_axis_data != null && sb_chs2k_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs2k_axis_data[i];
                    }
                    if (Form1.CHS2K_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS2K_key_buffer[i]);
                    }
                    if (Form1.chs2k_wav_path_key_buffer != null && Form1.chs2k_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs2k_wav_path_key_buffer[i].Substring(Form1.chs2k_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4")
            {
                i_temp_loco_select = 4;
                int j = 287;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 55; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs4_axis_data != null && sb_chs4_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs4_axis_data[i];
                    }
                    if (Form1.CHS4_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS4_key_buffer[i]);
                    }
                    if (Form1.chs4_wav_path_key_buffer != null && Form1.chs4_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs4_wav_path_key_buffer[i].Substring(Form1.chs4_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4 KVR")
            {
                i_temp_loco_select = 5;
                int j = 342;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 55; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs4kvr_axis_data != null && sb_chs4kvr_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs4kvr_axis_data[i];
                    }
                    if (Form1.CHS4KVR_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS4KVR_key_buffer[i]);
                    }
                    if (Form1.chs4kvr_wav_path_key_buffer != null && Form1.chs4kvr_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs4kvr_wav_path_key_buffer[i].Substring(Form1.chs4kvr_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS4T")
            {
                i_temp_loco_select = 6;
                int j = 397;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 52; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs4t_axis_data != null && sb_chs4t_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs4t_axis_data[i];
                    }
                    if (Form1.CHS4T_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS4T_key_buffer[i]);
                    }
                    if (Form1.chs4t_wav_path_key_buffer != null && Form1.chs4t_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs4t_wav_path_key_buffer[i].Substring(Form1.chs4t_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS7")
            {
                i_temp_loco_select = 7;
                int j = 451;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 46; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs7_axis_data != null && sb_chs7_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs7_axis_data[i];
                    }
                    if (Form1.CHS7_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS7_key_buffer[i]);
                    }
                    if (Form1.chs7_wav_path_key_buffer != null && Form1.chs7_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs7_wav_path_key_buffer[i].Substring(Form1.chs7_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "CHS8")
            {
                i_temp_loco_select = 8;
                int j = 497;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 63; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_chs8_axis_data != null && sb_chs8_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_chs8_axis_data[i];
                    }
                    if (Form1.CHS8_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.CHS8_key_buffer[i]);
                    }
                    if (Form1.chs8_wav_path_key_buffer != null && Form1.chs8_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.chs8_wav_path_key_buffer[i].Substring(Form1.chs8_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL11M")
            {
                i_temp_loco_select = 9;
                int j = 560;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 83; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_vl11_axis_data != null && sb_vl11_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_vl11_axis_data[i];
                    }
                    if (Form1.VL11M_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.VL11M_key_buffer[i]);
                    }
                    if (Form1.vl11_wav_path_key_buffer != null && Form1.vl11_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.vl11_wav_path_key_buffer[i].Substring(Form1.vl11_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL82M")
            {
                i_temp_loco_select = 10;
                int j = 643;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 83; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_vl82_axis_data != null && sb_vl82_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_vl82_axis_data[i];
                    }
                    if (Form1.VL82M_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.VL82M_key_buffer[i]);
                    }
                    if (Form1.vl82_wav_path_key_buffer != null && Form1.vl82_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.vl82_wav_path_key_buffer[i].Substring(Form1.vl82_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL80T")
            {
                i_temp_loco_select = 11;
                int j = 726;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 49; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_vl80t_axis_data != null && sb_vl80t_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_vl80t_axis_data[i];
                    }
                    if (Form1.VL80T_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.VL80T_key_buffer[i]);
                    }
                    if (Form1.vl80t_wav_path_key_buffer != null && Form1.vl80t_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.vl80t_wav_path_key_buffer[i].Substring(Form1.vl80t_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "VL85")
            {
                i_temp_loco_select = 12;
                int j = 775;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 77; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_vl85_axis_data != null && sb_vl85_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_vl85_axis_data[i];
                    }
                    if (Form1.VL85_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.VL85_key_buffer[i]);
                    }
                    if (Form1.vl85_wav_path_key_buffer != null && Form1.vl85_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.vl85_wav_path_key_buffer[i].Substring(Form1.vl85_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "TEP70")
            {
                i_temp_loco_select = 13;
                int j = 852;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 36; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_tep70_axis_data != null && sb_tep70_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_tep70_axis_data[i];
                    }
                    if (Form1.TEP70_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.TEP70_key_buffer[i]);
                    }
                    if (Form1.tep70_wav_path_key_buffer != null && Form1.tep70_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.tep70_wav_path_key_buffer[i].Substring(Form1.tep70_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "2TE10U")
            {
                i_temp_loco_select = 14;
                int j = 888;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 47; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_te10u_axis_data != null && sb_te10u_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_te10u_axis_data[i];
                    }
                    if (Form1.TE10U_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.TE10U_key_buffer[i]);
                    }
                    if (Form1.te10u_wav_path_key_buffer != null && Form1.te10u_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.te10u_wav_path_key_buffer[i].Substring(Form1.te10u_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "M62")
            {
                i_temp_loco_select = 15;
                int j = 935;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 36; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_m62_axis_data != null && sb_m62_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_m62_axis_data[i];
                    }
                    if (Form1.M62_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.M62_key_buffer[i]);
                    }
                    if (Form1.m62_wav_path_key_buffer != null && Form1.m62_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.m62_wav_path_key_buffer[i].Substring(Form1.m62_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "ED9M")
            {
                i_temp_loco_select = 17;
                int j = 971;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 30; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_ed9m_axis_data != null && sb_ed9m_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_ed9m_axis_data[i];
                    }
                    if (Form1.ED9M_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.ED9M_key_buffer[i]);
                    }
                    if (Form1.ed9m_wav_path_key_buffer != null && Form1.ed9m_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.ed9m_wav_path_key_buffer[i].Substring(Form1.ed9m_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "ED4M")
            {
                i_temp_loco_select = 16;
                int j = 1001;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 33; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_ed4m_axis_data != null && sb_ed4m_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_ed4m_axis_data[i];
                    }
                    if (Form1.ED4M_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.ED4M_key_buffer[i]);
                    }
                    if (Form1.ed4m_wav_path_key_buffer != null && Form1.ed4m_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.ed4m_wav_path_key_buffer[i].Substring(Form1.ed4m_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "tem18")
            {
                i_temp_loco_select = 18;
                int j = 1034;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 32; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_tem18_axis_data != null && sb_tem18_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_tem18_axis_data[i];
                    }
                    if (Form1.tem18_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.tem18_key_buffer[i]);
                    }
                    if (Form1.tem18_wav_path_key_buffer != null && Form1.tem18_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.tem18_wav_path_key_buffer[i].Substring(Form1.tem18_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
            if (Convert.ToString(comboBox_zdsimLoco.Text) == "Neshtatki")
            {
                i_temp_loco_select = 19;
                int j = 1066;
                dataGridView_Zdsimulator.Rows.Clear();
                dataGridView_Zdsimulator.Columns.Clear();
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);
                dataGridView_Zdsimulator.Columns.Add(null, null);//звук
                for (int i = 0; i < 100; i++)
                {
                    dataGridView_Zdsimulator.Rows.Add();
                    dataGridView_Zdsimulator.Rows[i].Cells[0].Value = Convert.ToString(LocoKeyData[j]);
                    if (sb_neshtatki_axis_data != null && sb_neshtatki_axis_data[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = sb_neshtatki_axis_data[i];
                    }
                    if (Form1.Neshtatki_key_buffer[i] != 0)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[1].Value = Convert.ToString("Button " + Form1.Neshtatki_key_buffer[i]);
                    }
                    if (Form1.neshtatki_wav_path_key_buffer != null && Form1.neshtatki_wav_path_key_buffer[i] != null)
                    {
                        dataGridView_Zdsimulator.Rows[i].Cells[2].Value = Form1.neshtatki_wav_path_key_buffer[i].Substring(Form1.neshtatki_wav_path_key_buffer[i].LastIndexOf("\\") + 1);
                    }
                    j++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.Init_pribor();
            textBox1.Text = Convert.ToString(1.475);
            textBox2.Text = Convert.ToString(26.2);
            textBox3.Text = Convert.ToString(11.5);
            textBox4.Text = Convert.ToString(2.6);
            textBox5.Text = Convert.ToString(6.5);
            textBox6.Text = Convert.ToString(38.8);
            numericUpDown7.Value = 3300;
            numericUpDown8.Value = 500;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Loco.i_skor_tek_convert = Convert.ToSingle(textBox1.Text);
            Loco.i_tok_ept_convert = Convert.ToSingle(textBox2.Text);
            Loco.i_napruga_ks_convert = Convert.ToSingle(textBox3.Text);
            Loco.i_napruga_td_convert = Convert.ToSingle(textBox4.Text);
            Loco.i_tok_convert = Convert.ToSingle(textBox5.Text);
            Loco.i_pnevmo_convert = Convert.ToSingle(textBox6.Text);
            Form1.i_delay_motor = Convert.ToUInt16(numericUpDown7.Value);
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

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0") || (textBox1.Text == "")) textBox1.Text = "1";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if ((textBox2.Text == "0") || (textBox2.Text == "")) textBox2.Text = "1";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if ((textBox3.Text == "0") || (textBox3.Text == "")) textBox3.Text = "1";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if ((textBox4.Text == "0") || (textBox4.Text == "")) textBox4.Text = "1";
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if ((textBox5.Text == "0") || (textBox5.Text == "")) textBox5.Text = "1";
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if ((textBox6.Text == "0") || (textBox6.Text == "")) textBox6.Text = "1";
        }

        private void textBox_joy_name_Enter(object sender, EventArgs e)
        {
            label7.Focus();
        }

        private void radio_priem_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            tabControl1.Visible = false;
            button_metka_osi.Enabled = false;
            button_sbros_metok.Enabled = false;
            Form1.i_priem_peredacha = 1;
        }

        private void radio_peredacha_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
            tabControl1.Visible = true;
            button_metka_osi.Enabled = true;
            button_sbros_metok.Enabled = true;
            Form1.i_priem_peredacha = 2;
        }

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
            openFileDialog1.Filter = "Wav files (*.wav)|*.wav";//только wav
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;//восстанавливать прежний путь

            if (i_temp_column_number_f2 == 2)
            {
                string strFileName = "";
                string strFilePath = "";
                string strAllPath = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fInfo = new System.IO.FileInfo(openFileDialog1.FileName);
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

        private void numericUpDown_shum_ValueChanged(object sender, EventArgs e)
        {
            Form1.i_shum_joystick = Convert.ToInt16(numericUpDown_shum.Value);
        }

        private void button_metka_osi_Click(object sender, EventArgs e)
        {
            int[] b_temp;
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARx")
            {
                if (Form1.joystick_ARx_point_buffer == null)
                {
                    Form1.joystick_ARx_point_buffer = new int[1];
                    Form1.joystick_ARx_point_buffer[Form1.joystick_ARx_point_buffer.Length - 1] = device.CurrentJoystickState.ARx;
                }
                else
                {
                    b_temp = Form1.joystick_ARx_point_buffer;
                    Form1.joystick_ARx_point_buffer = new int[Form1.joystick_ARx_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_ARx_point_buffer, b_temp.Length);
                    Form1.joystick_ARx_point_buffer[Form1.joystick_ARx_point_buffer.Length - 1] = device.CurrentJoystickState.ARx;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARy")
            {
                if (Form1.joystick_ARy_point_buffer == null)
                {
                    Form1.joystick_ARy_point_buffer = new int[1];
                    Form1.joystick_ARy_point_buffer[Form1.joystick_ARy_point_buffer.Length - 1] = device.CurrentJoystickState.ARy;
                }
                else
                {
                    b_temp = Form1.joystick_ARy_point_buffer;
                    Form1.joystick_ARy_point_buffer = new int[Form1.joystick_ARy_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_ARy_point_buffer, b_temp.Length);
                    Form1.joystick_ARy_point_buffer[Form1.joystick_ARy_point_buffer.Length - 1] = device.CurrentJoystickState.ARy;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ARz")
            {
                if (Form1.joystick_ARz_point_buffer == null)
                {
                    Form1.joystick_ARz_point_buffer = new int[1];
                    Form1.joystick_ARz_point_buffer[Form1.joystick_ARz_point_buffer.Length - 1] = device.CurrentJoystickState.ARz;
                }
                else
                {
                    b_temp = Form1.joystick_ARz_point_buffer;
                    Form1.joystick_ARz_point_buffer = new int[Form1.joystick_ARz_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_ARz_point_buffer, b_temp.Length);
                    Form1.joystick_ARz_point_buffer[Form1.joystick_ARz_point_buffer.Length - 1] = device.CurrentJoystickState.ARz;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AX")
            {
                if (Form1.joystick_AX_point_buffer == null)
                {
                    Form1.joystick_AX_point_buffer = new int[1];
                    Form1.joystick_AX_point_buffer[Form1.joystick_AX_point_buffer.Length - 1] = device.CurrentJoystickState.AX;
                }
                else
                {
                    b_temp = Form1.joystick_AX_point_buffer;
                    Form1.joystick_AX_point_buffer = new int[Form1.joystick_AX_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_AX_point_buffer, b_temp.Length);
                    Form1.joystick_AX_point_buffer[Form1.joystick_AX_point_buffer.Length - 1] = device.CurrentJoystickState.AX;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AY")
            {
                if (Form1.joystick_AY_point_buffer == null)
                {
                    Form1.joystick_AY_point_buffer = new int[1];
                    Form1.joystick_AY_point_buffer[Form1.joystick_AY_point_buffer.Length - 1] = device.CurrentJoystickState.AY;
                }
                else
                {
                    b_temp = Form1.joystick_AY_point_buffer;
                    Form1.joystick_AY_point_buffer = new int[Form1.joystick_AY_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_AY_point_buffer, b_temp.Length);
                    Form1.joystick_AY_point_buffer[Form1.joystick_AY_point_buffer.Length - 1] = device.CurrentJoystickState.AY;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "AZ")
            {
                if (Form1.joystick_AZ_point_buffer == null)
                {
                    Form1.joystick_AZ_point_buffer = new int[1];
                    Form1.joystick_AZ_point_buffer[Form1.joystick_AZ_point_buffer.Length - 1] = device.CurrentJoystickState.AZ;
                }
                else
                {
                    b_temp = Form1.joystick_AZ_point_buffer;
                    Form1.joystick_AZ_point_buffer = new int[Form1.joystick_AZ_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_AZ_point_buffer, b_temp.Length);
                    Form1.joystick_AZ_point_buffer[Form1.joystick_AZ_point_buffer.Length - 1] = device.CurrentJoystickState.AZ;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRx")
            {
                if (Form1.joystick_FRx_point_buffer == null)
                {
                    Form1.joystick_FRx_point_buffer = new int[1];
                    Form1.joystick_FRx_point_buffer[Form1.joystick_FRx_point_buffer.Length - 1] = device.CurrentJoystickState.FRx;
                }
                else
                {
                    b_temp = Form1.joystick_FRx_point_buffer;
                    Form1.joystick_FRx_point_buffer = new int[Form1.joystick_FRx_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FRx_point_buffer, b_temp.Length);
                    Form1.joystick_FRx_point_buffer[Form1.joystick_FRx_point_buffer.Length - 1] = device.CurrentJoystickState.FRx;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRy")
            {
                if (Form1.joystick_FRy_point_buffer == null)
                {
                    Form1.joystick_FRy_point_buffer = new int[1];
                    Form1.joystick_FRy_point_buffer[Form1.joystick_FRy_point_buffer.Length - 1] = device.CurrentJoystickState.FRy;
                }
                else
                {
                    b_temp = Form1.joystick_FRy_point_buffer;
                    Form1.joystick_FRy_point_buffer = new int[Form1.joystick_FRy_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FRy_point_buffer, b_temp.Length);
                    Form1.joystick_FRy_point_buffer[Form1.joystick_FRy_point_buffer.Length - 1] = device.CurrentJoystickState.FRy;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FRz")
            {
                if (Form1.joystick_FRz_point_buffer == null)
                {
                    Form1.joystick_FRz_point_buffer = new int[1];
                    Form1.joystick_FRz_point_buffer[Form1.joystick_FRz_point_buffer.Length - 1] = device.CurrentJoystickState.FRz;
                }
                else
                {
                    b_temp = Form1.joystick_FRz_point_buffer;
                    Form1.joystick_FRz_point_buffer = new int[Form1.joystick_FRz_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FRz_point_buffer, b_temp.Length);
                    Form1.joystick_FRz_point_buffer[Form1.joystick_FRz_point_buffer.Length - 1] = device.CurrentJoystickState.FRz;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FX")
            {
                if (Form1.joystick_FX_point_buffer == null)
                {
                    Form1.joystick_FX_point_buffer = new int[1];
                    Form1.joystick_FX_point_buffer[Form1.joystick_FX_point_buffer.Length - 1] = device.CurrentJoystickState.FX;
                }
                else
                {
                    b_temp = Form1.joystick_FX_point_buffer;
                    Form1.joystick_FX_point_buffer = new int[Form1.joystick_FX_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FX_point_buffer, b_temp.Length);
                    Form1.joystick_FX_point_buffer[Form1.joystick_FX_point_buffer.Length - 1] = device.CurrentJoystickState.FX;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FY")
            {
                if (Form1.joystick_FY_point_buffer == null)
                {
                    Form1.joystick_FY_point_buffer = new int[1];
                    Form1.joystick_FY_point_buffer[Form1.joystick_FY_point_buffer.Length - 1] = device.CurrentJoystickState.FY;
                }
                else
                {
                    b_temp = Form1.joystick_FY_point_buffer;
                    Form1.joystick_FY_point_buffer = new int[Form1.joystick_FY_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FY_point_buffer, b_temp.Length);
                    Form1.joystick_FY_point_buffer[Form1.joystick_FY_point_buffer.Length - 1] = device.CurrentJoystickState.FY;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FZ")
            {
                if (Form1.joystick_FZ_point_buffer == null)
                {
                    Form1.joystick_FZ_point_buffer = new int[1];
                    Form1.joystick_FZ_point_buffer[Form1.joystick_FZ_point_buffer.Length - 1] = device.CurrentJoystickState.FZ;
                }
                else
                {
                    b_temp = Form1.joystick_FZ_point_buffer;
                    Form1.joystick_FZ_point_buffer = new int[Form1.joystick_FZ_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FZ_point_buffer, b_temp.Length);
                    Form1.joystick_FZ_point_buffer[Form1.joystick_FZ_point_buffer.Length - 1] = device.CurrentJoystickState.FZ;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Rx")
            {
                if (Form1.joystick_Rx_point_buffer == null)
                {
                    Form1.joystick_Rx_point_buffer = new int[1];
                    Form1.joystick_Rx_point_buffer[Form1.joystick_Rx_point_buffer.Length - 1] = device.CurrentJoystickState.Rx;
                }
                else
                {
                    b_temp = Form1.joystick_Rx_point_buffer;
                    Form1.joystick_Rx_point_buffer = new int[Form1.joystick_Rx_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Rx_point_buffer, b_temp.Length);
                    Form1.joystick_Rx_point_buffer[Form1.joystick_Rx_point_buffer.Length - 1] = device.CurrentJoystickState.Rx;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Ry")
            {
                if (Form1.joystick_Ry_point_buffer == null)
                {
                    Form1.joystick_Ry_point_buffer = new int[1];
                    Form1.joystick_Ry_point_buffer[Form1.joystick_Ry_point_buffer.Length - 1] = device.CurrentJoystickState.Ry;
                }
                else
                {
                    b_temp = Form1.joystick_Ry_point_buffer;
                    Form1.joystick_Ry_point_buffer = new int[Form1.joystick_Ry_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Ry_point_buffer, b_temp.Length);
                    Form1.joystick_Ry_point_buffer[Form1.joystick_Ry_point_buffer.Length - 1] = device.CurrentJoystickState.Ry;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Rz")
            {
                if (Form1.joystick_Rz_point_buffer == null)
                {
                    Form1.joystick_Rz_point_buffer = new int[1];
                    Form1.joystick_Rz_point_buffer[Form1.joystick_Rz_point_buffer.Length - 1] = device.CurrentJoystickState.Rz;
                }
                else
                {
                    b_temp = Form1.joystick_Rz_point_buffer;
                    Form1.joystick_Rz_point_buffer = new int[Form1.joystick_Rz_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Rz_point_buffer, b_temp.Length);
                    Form1.joystick_Rz_point_buffer[Form1.joystick_Rz_point_buffer.Length - 1] = device.CurrentJoystickState.Rz;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRx")
            {
                if (Form1.joystick_VRx_point_buffer == null)
                {
                    Form1.joystick_VRx_point_buffer = new int[1];
                    Form1.joystick_VRx_point_buffer[Form1.joystick_VRx_point_buffer.Length - 1] = device.CurrentJoystickState.VRx;
                }
                else
                {
                    b_temp = Form1.joystick_VRx_point_buffer;
                    Form1.joystick_VRx_point_buffer = new int[Form1.joystick_VRx_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VRx_point_buffer, b_temp.Length);
                    Form1.joystick_VRx_point_buffer[Form1.joystick_VRx_point_buffer.Length - 1] = device.CurrentJoystickState.VRx;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRy")
            {
                if (Form1.joystick_VRy_point_buffer == null)
                {
                    Form1.joystick_VRy_point_buffer = new int[1];
                    Form1.joystick_VRy_point_buffer[Form1.joystick_VRy_point_buffer.Length - 1] = device.CurrentJoystickState.VRy;
                }
                else
                {
                    b_temp = Form1.joystick_VRy_point_buffer;
                    Form1.joystick_VRy_point_buffer = new int[Form1.joystick_VRy_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VRy_point_buffer, b_temp.Length);
                    Form1.joystick_VRy_point_buffer[Form1.joystick_VRy_point_buffer.Length - 1] = device.CurrentJoystickState.VRy;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VRz")
            {
                if (Form1.joystick_VRz_point_buffer == null)
                {
                    Form1.joystick_VRz_point_buffer = new int[1];
                    Form1.joystick_VRz_point_buffer[Form1.joystick_VRz_point_buffer.Length - 1] = device.CurrentJoystickState.VRz;
                }
                else
                {
                    b_temp = Form1.joystick_VRz_point_buffer;
                    Form1.joystick_VRz_point_buffer = new int[Form1.joystick_VRz_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VRz_point_buffer, b_temp.Length);
                    Form1.joystick_VRz_point_buffer[Form1.joystick_VRz_point_buffer.Length - 1] = device.CurrentJoystickState.VRz;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VX")
            {
                if (Form1.joystick_VX_point_buffer == null)
                {
                    Form1.joystick_VX_point_buffer = new int[1];
                    Form1.joystick_VX_point_buffer[Form1.joystick_VX_point_buffer.Length - 1] = device.CurrentJoystickState.VX;
                }
                else
                {
                    b_temp = Form1.joystick_VX_point_buffer;
                    Form1.joystick_VX_point_buffer = new int[Form1.joystick_VX_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VX_point_buffer, b_temp.Length);
                    Form1.joystick_VX_point_buffer[Form1.joystick_VX_point_buffer.Length - 1] = device.CurrentJoystickState.VX;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VY")
            {
                if (Form1.joystick_VY_point_buffer == null)
                {
                    Form1.joystick_VY_point_buffer = new int[1];
                    Form1.joystick_VY_point_buffer[Form1.joystick_VY_point_buffer.Length - 1] = device.CurrentJoystickState.VY;
                }
                else
                {
                    b_temp = Form1.joystick_VY_point_buffer;
                    Form1.joystick_VY_point_buffer = new int[Form1.joystick_VY_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VY_point_buffer, b_temp.Length);
                    Form1.joystick_VY_point_buffer[Form1.joystick_VY_point_buffer.Length - 1] = device.CurrentJoystickState.VY;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VZ")
            {
                if (Form1.joystick_VZ_point_buffer == null)
                {
                    Form1.joystick_VZ_point_buffer = new int[1];
                    Form1.joystick_VZ_point_buffer[Form1.joystick_VZ_point_buffer.Length - 1] = device.CurrentJoystickState.VZ;
                }
                else
                {
                    b_temp = Form1.joystick_VZ_point_buffer;
                    Form1.joystick_VZ_point_buffer = new int[Form1.joystick_VZ_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VZ_point_buffer, b_temp.Length);
                    Form1.joystick_VZ_point_buffer[Form1.joystick_VZ_point_buffer.Length - 1] = device.CurrentJoystickState.VZ;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "X")
            {
                if (Form1.joystick_X_point_buffer == null)
                {
                    Form1.joystick_X_point_buffer = new int[1];
                    Form1.joystick_X_point_buffer[Form1.joystick_X_point_buffer.Length - 1] = device.CurrentJoystickState.X;
                }
                else
                {
                    b_temp = Form1.joystick_X_point_buffer;
                    Form1.joystick_X_point_buffer = new int[Form1.joystick_X_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_X_point_buffer, b_temp.Length);
                    Form1.joystick_X_point_buffer[Form1.joystick_X_point_buffer.Length - 1] = device.CurrentJoystickState.X;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Y")
            {
                if (Form1.joystick_Y_point_buffer == null)
                {
                    Form1.joystick_Y_point_buffer = new int[1];
                    Form1.joystick_Y_point_buffer[Form1.joystick_Y_point_buffer.Length - 1] = device.CurrentJoystickState.Y;
                }
                else
                {
                    b_temp = Form1.joystick_Y_point_buffer;
                    Form1.joystick_Y_point_buffer = new int[Form1.joystick_Y_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Y_point_buffer, b_temp.Length);
                    Form1.joystick_Y_point_buffer[Form1.joystick_Y_point_buffer.Length - 1] = device.CurrentJoystickState.Y;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Z")
            {
                if (Form1.joystick_Z_point_buffer == null)
                {
                    Form1.joystick_Z_point_buffer = new int[1];
                    Form1.joystick_Z_point_buffer[Form1.joystick_Z_point_buffer.Length - 1] = device.CurrentJoystickState.Z;
                }
                else
                {
                    b_temp = Form1.joystick_Z_point_buffer;
                    Form1.joystick_Z_point_buffer = new int[Form1.joystick_Z_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Z_point_buffer, b_temp.Length);
                    Form1.joystick_Z_point_buffer[Form1.joystick_Z_point_buffer.Length - 1] = device.CurrentJoystickState.Z;
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "POV")
            {
                if (Form1.joystick_POV_point_buffer == null)
                {
                    Form1.joystick_POV_point_buffer = new int[1];
                    b_temp = device.CurrentJoystickState.GetPointOfView();
                    Form1.joystick_POV_point_buffer[Form1.joystick_POV_point_buffer.Length - 1] = b_temp[0];
                }
                else
                {
                    b_temp = Form1.joystick_POV_point_buffer;
                    Form1.joystick_POV_point_buffer = new int[Form1.joystick_POV_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_POV_point_buffer, b_temp.Length);
                    b_temp = device.CurrentJoystickState.GetPointOfView();
                    Form1.joystick_POV_point_buffer[Form1.joystick_POV_point_buffer.Length - 1] = b_temp[0];
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "Slider")
            {
                if (Form1.joystick_Slider_point_buffer == null)
                {
                    Form1.joystick_Slider_point_buffer = new int[1];
                    b_temp = device.CurrentJoystickState.GetSlider();
                    Form1.joystick_Slider_point_buffer[Form1.joystick_Slider_point_buffer.Length - 1] = b_temp[0];
                }
                else
                {
                    b_temp = Form1.joystick_Slider_point_buffer;
                    Form1.joystick_Slider_point_buffer = new int[Form1.joystick_Slider_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_Slider_point_buffer, b_temp.Length);
                    b_temp = device.CurrentJoystickState.GetSlider();
                    Form1.joystick_Slider_point_buffer[Form1.joystick_Slider_point_buffer.Length - 1] = b_temp[0];
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "ASlider")
            {
                if (Form1.joystick_ASlider_point_buffer == null)
                {
                    Form1.joystick_ASlider_point_buffer = new int[1];
                    b_temp = device.CurrentJoystickState.GetASlider();
                    Form1.joystick_ASlider_point_buffer[Form1.joystick_ASlider_point_buffer.Length - 1] = b_temp[0];
                }
                else
                {
                    b_temp = Form1.joystick_ASlider_point_buffer;
                    Form1.joystick_ASlider_point_buffer = new int[Form1.joystick_ASlider_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_ASlider_point_buffer, b_temp.Length);
                    b_temp = device.CurrentJoystickState.GetASlider();
                    Form1.joystick_ASlider_point_buffer[Form1.joystick_ASlider_point_buffer.Length - 1] = b_temp[0];
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "FSlider")
            {
                if (Form1.joystick_FSlider_point_buffer == null)
                {
                    Form1.joystick_FSlider_point_buffer = new int[1];
                    b_temp = device.CurrentJoystickState.GetFSlider();
                    Form1.joystick_FSlider_point_buffer[Form1.joystick_FSlider_point_buffer.Length - 1] = b_temp[0];
                }
                else
                {
                    b_temp = Form1.joystick_FSlider_point_buffer;
                    Form1.joystick_FSlider_point_buffer = new int[Form1.joystick_FSlider_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_FSlider_point_buffer, b_temp.Length);
                    b_temp = device.CurrentJoystickState.GetFSlider();
                    Form1.joystick_FSlider_point_buffer[Form1.joystick_FSlider_point_buffer.Length - 1] = b_temp[0];
                }
            }
            if (Convert.ToString(comboBox_osi_select.SelectedItem) == "VSlider")
            {
                if (Form1.joystick_VSlider_point_buffer == null)
                {
                    Form1.joystick_VSlider_point_buffer = new int[1];
                    b_temp = device.CurrentJoystickState.GetVSlider();
                    Form1.joystick_VSlider_point_buffer[Form1.joystick_VSlider_point_buffer.Length - 1] = b_temp[0];
                }
                else
                {
                    b_temp = Form1.joystick_VSlider_point_buffer;
                    Form1.joystick_VSlider_point_buffer = new int[Form1.joystick_VSlider_point_buffer.Length + 1];
                    Array.Copy(b_temp, Form1.joystick_VSlider_point_buffer, b_temp.Length);
                    b_temp = device.CurrentJoystickState.GetVSlider();
                    Form1.joystick_VSlider_point_buffer[Form1.joystick_VSlider_point_buffer.Length - 1] = b_temp[0];
                }
            }
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

        private void sbros_metok_clear_buffer(int i_sbros_metok_axis_number)
        {
            for (int i = 0; i < Form1.Controls_axis_buffer.Length / 2; i++)
            {
                if (Form1.Controls_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.Controls_axis_buffer[i, 0] = 0;
                    Form1.Controls_axis_buffer[i, 1] = 0;
                    sb_controls_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.ES5K_axis_buffer.Length / 2; i++)
            {
                if (Form1.ES5K_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.ES5K_axis_buffer[i, 0] = 0;
                    Form1.ES5K_axis_buffer[i, 1] = 0;
                    sb_es5k_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.EP1M_axis_buffer.Length / 2; i++)
            {
                if (Form1.EP1M_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.EP1M_axis_buffer[i, 0] = 0;
                    Form1.EP1M_axis_buffer[i, 1] = 0;
                    sb_ep1m_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS2K_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS2K_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS2K_axis_buffer[i, 0] = 0;
                    Form1.CHS2K_axis_buffer[i, 1] = 0;
                    sb_chs2k_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS4_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS4_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS4_axis_buffer[i, 0] = 0;
                    Form1.CHS4_axis_buffer[i, 1] = 0;
                    sb_chs4_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS4KVR_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS4KVR_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS4KVR_axis_buffer[i, 0] = 0;
                    Form1.CHS4KVR_axis_buffer[i, 1] = 0;
                    sb_chs4kvr_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS4T_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS4T_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS4T_axis_buffer[i, 0] = 0;
                    Form1.CHS4T_axis_buffer[i, 1] = 0;
                    sb_chs4t_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS7_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS7_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS7_axis_buffer[i, 0] = 0;
                    Form1.CHS7_axis_buffer[i, 1] = 0;
                    sb_chs7_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.CHS8_axis_buffer.Length / 2; i++)
            {
                if (Form1.CHS8_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.CHS8_axis_buffer[i, 0] = 0;
                    Form1.CHS8_axis_buffer[i, 1] = 0;
                    sb_chs8_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.VL11M_axis_buffer.Length / 2; i++)
            {
                if (Form1.VL11M_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.VL11M_axis_buffer[i, 0] = 0;
                    Form1.VL11M_axis_buffer[i, 1] = 0;
                    sb_vl11_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.VL82M_axis_buffer.Length / 2; i++)
            {
                if (Form1.VL82M_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.VL82M_axis_buffer[i, 0] = 0;
                    Form1.VL82M_axis_buffer[i, 1] = 0;
                    sb_vl82_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.VL80T_axis_buffer.Length / 2; i++)
            {
                if (Form1.VL80T_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.VL80T_axis_buffer[i, 0] = 0;
                    Form1.VL80T_axis_buffer[i, 1] = 0;
                    sb_vl80t_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.VL85_axis_buffer.Length / 2; i++)
            {
                if (Form1.VL85_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.VL85_axis_buffer[i, 0] = 0;
                    Form1.VL85_axis_buffer[i, 1] = 0;
                    sb_vl85_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.TEP70_axis_buffer.Length / 2; i++)
            {
                if (Form1.TEP70_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.TEP70_axis_buffer[i, 0] = 0;
                    Form1.TEP70_axis_buffer[i, 1] = 0;
                    sb_tep70_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.TE10U_axis_buffer.Length / 2; i++)
            {
                if (Form1.TE10U_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.TE10U_axis_buffer[i, 0] = 0;
                    Form1.TE10U_axis_buffer[i, 1] = 0;
                    sb_te10u_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.M62_axis_buffer.Length / 2; i++)
            {
                if (Form1.M62_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.M62_axis_buffer[i, 0] = 0;
                    Form1.M62_axis_buffer[i, 1] = 0;
                    sb_m62_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.ED4M_axis_buffer.Length / 2; i++)
            {
                if (Form1.ED4M_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.ED4M_axis_buffer[i, 0] = 0;
                    Form1.ED4M_axis_buffer[i, 1] = 0;
                    sb_ed4m_axis_data[i] = null;
                }
            }
            for (int i = 0; i < Form1.ED9M_axis_buffer.Length / 2; i++)
            {
                if (Form1.ED9M_axis_buffer[i, 0] == i_sbros_metok_axis_number)
                {
                    Form1.ED9M_axis_buffer[i, 0] = 0;
                    Form1.ED9M_axis_buffer[i, 1] = 0;
                    sb_ed9m_axis_data[i] = null;
                }
            }
        }

        private void button_sbros_metok_Click(object sender, EventArgs e)
        {
            string s_name = "Удаление точек оси";
            string s1 = "Точки текущей оси будут удалены, уверены?";
            DialogResult result = MessageBox.Show(s1, s_name, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                if (comboBox_osi_select.SelectedItem.ToString() == "ARx")
                {
                    Form1.joystick_ARx_point_buffer = null;
                    Properties.Settings.Default.ARx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(1);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "ARy")
                {
                    Form1.joystick_ARy_point_buffer = null;
                    Properties.Settings.Default.ARy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(2);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "ARz")
                {
                    Form1.joystick_ARz_point_buffer = null;
                    Properties.Settings.Default.ARz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(3);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "AX")
                {
                    Form1.joystick_AX_point_buffer = null;
                    Properties.Settings.Default.AX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(4);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "AY")
                {
                    Form1.joystick_AY_point_buffer = null;
                    Properties.Settings.Default.AY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(5);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "AZ")
                {
                    Form1.joystick_AZ_point_buffer = null;
                    Properties.Settings.Default.AZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(6);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FRx")
                {
                    Form1.joystick_FRx_point_buffer = null;
                    Properties.Settings.Default.FRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(7);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FRy")
                {
                    Form1.joystick_FRy_point_buffer = null;
                    Properties.Settings.Default.FRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(8);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FRz")
                {
                    Form1.joystick_FRz_point_buffer = null;
                    Properties.Settings.Default.FRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(9);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FX")
                {
                    Form1.joystick_FX_point_buffer = null;
                    Properties.Settings.Default.FX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(10);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FY")
                {
                    Form1.joystick_FY_point_buffer = null;
                    Properties.Settings.Default.FY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(11);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "FZ")
                {
                    Form1.joystick_FZ_point_buffer = null;
                    Properties.Settings.Default.FZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(12);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Rx")
                {
                    Form1.joystick_Rx_point_buffer = null;
                    Properties.Settings.Default.Rx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(13);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Ry")
                {
                    Form1.joystick_Ry_point_buffer = null;
                    Properties.Settings.Default.Ry_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(14);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Rz")
                {
                    Form1.joystick_Rz_point_buffer = null;
                    Properties.Settings.Default.Rz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(15);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VRx")
                {
                    Form1.joystick_VRx_point_buffer = null;
                    Properties.Settings.Default.VRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(16);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VRy")
                {
                    Form1.joystick_VRy_point_buffer = null;
                    Properties.Settings.Default.VRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(17);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VRz")
                {
                    Form1.joystick_VRz_point_buffer = null;
                    Properties.Settings.Default.VRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(18);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VX")
                {
                    Form1.joystick_VX_point_buffer = null;
                    Properties.Settings.Default.VX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(19);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VY")
                {
                    Form1.joystick_VY_point_buffer = null;
                    Properties.Settings.Default.VY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(20);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "VZ")
                {
                    Form1.joystick_VZ_point_buffer = null;
                    Properties.Settings.Default.VZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(21);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "X")
                {
                    Form1.joystick_X_point_buffer = null;
                    Properties.Settings.Default.X_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(22);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Y")
                {
                    Form1.joystick_Y_point_buffer = null;
                    Properties.Settings.Default.Y_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(23);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Z")
                {
                    Form1.joystick_Z_point_buffer = null;
                    Properties.Settings.Default.Z_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(24);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "POV")
                {
                    Form1.joystick_POV_point_buffer = null;
                    Properties.Settings.Default.POV_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(25);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "Slider")
                {
                    Form1.joystick_Slider_point_buffer = null;
                    Properties.Settings.Default.Slider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(26);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "SliderAS")
                {
                    Form1.joystick_ASlider_point_buffer = null;
                    Properties.Settings.Default.ASlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(27);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "SliderFS")
                {
                    Form1.joystick_FSlider_point_buffer = null;
                    Properties.Settings.Default.FSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(28);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                if (comboBox_osi_select.SelectedItem.ToString() == "SliderVS")
                {
                    Form1.joystick_VSlider_point_buffer = null;
                    Properties.Settings.Default.VSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                    sbros_metok_clear_buffer(29);
                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    KeyDataUpdate();
                }
                Properties.Settings.Default.Save();
            }

        }

        private void button_zdsim_sbros_tek_Click(object sender, EventArgs e)
        {
            string s_name = "Удаление настроек текущего локомотива";
            string s1 = "Настройки локомотива - ";
            string s2 = " будут удалены, уверены?";
            DialogResult result = MessageBox.Show((s1 + comboBox_zdsimLoco.SelectedItem.ToString() + s2), s_name, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Controls")
                {
                    //кнопки, оси
                    Array.Clear(Form1.Controls_key_buffer, 0, Form1.Controls_key_buffer.Length);
                    Array.Clear(Form1.Controls_axis_buffer, 0, Form1.Controls_axis_buffer.Length);
                    Array.Clear(sb_controls_axis_data, 0, sb_controls_axis_data.Length);
                    Properties.Settings.Default.controls_buffer_key_settings.Clear();
                    Properties.Settings.Default.controls_buffer_axis_settings.Clear();
                    Properties.Settings.Default.controls_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.controls_wav_path_key_buffer, 0, Form1.controls_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_controls_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Neshtatki")
                {
                    Array.Clear(Form1.Neshtatki_key_buffer, 0, Form1.Neshtatki_key_buffer.Length);
                    Array.Clear(Form1.Neshtatki_axis_buffer, 0, Form1.Neshtatki_axis_buffer.Length);
                    Array.Clear(sb_neshtatki_axis_data, 0, sb_neshtatki_axis_data.Length);
                    Properties.Settings.Default.neshtatki_buffer_key_settings.Clear();
                    Properties.Settings.Default.neshtatki_buffer_axis_settings.Clear();
                    Properties.Settings.Default.neshtatki_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.neshtatki_wav_path_key_buffer, 0, Form1.neshtatki_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_neshtatki_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2ES5K")
                {
                    Array.Clear(Form1.ES5K_key_buffer, 0, Form1.ES5K_key_buffer.Length);
                    Array.Clear(Form1.ES5K_axis_buffer, 0, Form1.ES5K_axis_buffer.Length);
                    Array.Clear(sb_es5k_axis_data, 0, sb_es5k_axis_data.Length);
                    Properties.Settings.Default.es5k_buffer_key_settings.Clear();
                    Properties.Settings.Default.es5k_buffer_axis_settings.Clear();
                    Properties.Settings.Default.es5k_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.es5k_wav_path_key_buffer, 0, Form1.es5k_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_es5k_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "EP1M")
                {
                    Array.Clear(Form1.EP1M_key_buffer, 0, Form1.EP1M_key_buffer.Length);
                    Array.Clear(Form1.EP1M_axis_buffer, 0, Form1.EP1M_axis_buffer.Length);
                    Array.Clear(sb_ep1m_axis_data, 0, sb_ep1m_axis_data.Length);
                    Properties.Settings.Default.ep1m_buffer_key_settings.Clear();
                    Properties.Settings.Default.ep1m_buffer_axis_settings.Clear();
                    Properties.Settings.Default.ep1m_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.ep1m_wav_path_key_buffer, 0, Form1.ep1m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS2K")
                {
                    Array.Clear(Form1.CHS2K_key_buffer, 0, Form1.CHS2K_key_buffer.Length);
                    Array.Clear(Form1.CHS2K_axis_buffer, 0, Form1.CHS2K_axis_buffer.Length);
                    Array.Clear(sb_chs2k_axis_data, 0, sb_chs2k_axis_data.Length);
                    Properties.Settings.Default.chs2k_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs2k_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs2k_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs2k_wav_path_key_buffer, 0, Form1.chs2k_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4")
                {
                    Array.Clear(Form1.CHS4_key_buffer, 0, Form1.CHS4_key_buffer.Length);
                    Array.Clear(Form1.CHS4_axis_buffer, 0, Form1.CHS4_axis_buffer.Length);
                    Array.Clear(sb_chs4_axis_data, 0, sb_chs4_axis_data.Length);
                    Properties.Settings.Default.chs4_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs4_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs4_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs4_wav_path_key_buffer, 0, Form1.chs4_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4 KVR")
                {
                    Array.Clear(Form1.CHS4KVR_key_buffer, 0, Form1.CHS4KVR_key_buffer.Length);
                    Array.Clear(Form1.CHS4KVR_axis_buffer, 0, Form1.CHS4KVR_axis_buffer.Length);
                    Array.Clear(sb_chs4kvr_axis_data, 0, sb_chs4kvr_axis_data.Length);
                    Properties.Settings.Default.chs4kvr_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs4kvr_wav_path_key_buffer, 0, Form1.chs4kvr_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4T")
                {
                    Array.Clear(Form1.CHS4T_key_buffer, 0, Form1.CHS4T_key_buffer.Length);
                    Array.Clear(Form1.CHS4T_axis_buffer, 0, Form1.CHS4T_axis_buffer.Length);
                    Array.Clear(sb_chs4t_axis_data, 0, sb_chs4t_axis_data.Length);
                    Properties.Settings.Default.chs4t_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs4t_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs4t_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs4t_wav_path_key_buffer, 0, Form1.chs4t_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS7")
                {
                    Array.Clear(Form1.CHS7_key_buffer, 0, Form1.CHS7_key_buffer.Length);
                    Array.Clear(Form1.CHS7_axis_buffer, 0, Form1.CHS7_axis_buffer.Length);
                    Array.Clear(sb_chs7_axis_data, 0, sb_chs7_axis_data.Length);
                    Properties.Settings.Default.chs7_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs7_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs7_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs7_wav_path_key_buffer, 0, Form1.chs7_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs7_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS8")
                {
                    Array.Clear(Form1.CHS8_key_buffer, 0, Form1.CHS8_key_buffer.Length);
                    Array.Clear(Form1.CHS8_axis_buffer, 0, Form1.CHS8_axis_buffer.Length);
                    Array.Clear(sb_chs8_axis_data, 0, sb_chs8_axis_data.Length);
                    Properties.Settings.Default.chs8_buffer_key_settings.Clear();
                    Properties.Settings.Default.chs8_buffer_axis_settings.Clear();
                    Properties.Settings.Default.chs8_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.chs8_wav_path_key_buffer, 0, Form1.chs8_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_chs8_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL11M")
                {
                    Array.Clear(Form1.VL11M_key_buffer, 0, Form1.VL11M_key_buffer.Length);
                    Array.Clear(Form1.VL11M_axis_buffer, 0, Form1.VL11M_axis_buffer.Length);
                    Array.Clear(sb_vl11_axis_data, 0, sb_vl11_axis_data.Length);
                    Properties.Settings.Default.vl11_buffer_key_settings.Clear();
                    Properties.Settings.Default.vl11_buffer_axis_settings.Clear();
                    Properties.Settings.Default.vl11_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.vl11_wav_path_key_buffer, 0, Form1.vl11_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl11_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL82M")
                {
                    Array.Clear(Form1.VL82M_key_buffer, 0, Form1.VL82M_key_buffer.Length);
                    Array.Clear(Form1.VL82M_axis_buffer, 0, Form1.VL82M_axis_buffer.Length);
                    Array.Clear(sb_vl82_axis_data, 0, sb_vl82_axis_data.Length);
                    Properties.Settings.Default.vl82_buffer_key_settings.Clear();
                    Properties.Settings.Default.vl82_buffer_axis_settings.Clear();
                    Properties.Settings.Default.vl82_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.vl82_wav_path_key_buffer, 0, Form1.vl82_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl82_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL80T")
                {
                    Array.Clear(Form1.VL80T_key_buffer, 0, Form1.VL80T_key_buffer.Length);
                    Array.Clear(Form1.VL80T_axis_buffer, 0, Form1.VL80T_axis_buffer.Length);
                    Array.Clear(sb_vl80t_axis_data, 0, sb_vl80t_axis_data.Length);
                    Properties.Settings.Default.vl80t_buffer_key_settings.Clear();
                    Properties.Settings.Default.vl80t_buffer_axis_settings.Clear();
                    Properties.Settings.Default.vl80t_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.vl80t_wav_path_key_buffer, 0, Form1.vl80t_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL85")
                {
                    Array.Clear(Form1.VL85_key_buffer, 0, Form1.VL85_key_buffer.Length);
                    Array.Clear(Form1.VL85_axis_buffer, 0, Form1.VL85_axis_buffer.Length);
                    Array.Clear(sb_vl85_axis_data, 0, sb_vl85_axis_data.Length);
                    Properties.Settings.Default.vl85_buffer_key_settings.Clear();
                    Properties.Settings.Default.vl85_buffer_axis_settings.Clear();
                    Properties.Settings.Default.vl85_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.vl85_wav_path_key_buffer, 0, Form1.vl85_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_vl85_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "TEP70")
                {
                    Array.Clear(Form1.TEP70_key_buffer, 0, Form1.TEP70_key_buffer.Length);
                    Array.Clear(Form1.TEP70_axis_buffer, 0, Form1.TEP70_axis_buffer.Length);
                    Array.Clear(sb_tep70_axis_data, 0, sb_tep70_axis_data.Length);
                    Properties.Settings.Default.tep70_buffer_key_settings.Clear();
                    Properties.Settings.Default.tep70_buffer_axis_settings.Clear();
                    Properties.Settings.Default.tep70_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.tep70_wav_path_key_buffer, 0, Form1.tep70_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_tep70_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2TE10U")
                {
                    Array.Clear(Form1.TE10U_key_buffer, 0, Form1.TE10U_key_buffer.Length);
                    Array.Clear(Form1.TE10U_axis_buffer, 0, Form1.TE10U_axis_buffer.Length);
                    Array.Clear(sb_te10u_axis_data, 0, sb_te10u_axis_data.Length);
                    Properties.Settings.Default.te10u_buffer_key_settings.Clear();
                    Properties.Settings.Default.te10u_buffer_axis_settings.Clear();
                    Properties.Settings.Default.te10u_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.te10u_wav_path_key_buffer, 0, Form1.te10u_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_te10u_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "M62")
                {
                    Array.Clear(Form1.M62_key_buffer, 0, Form1.M62_key_buffer.Length);
                    Array.Clear(Form1.M62_axis_buffer, 0, Form1.M62_axis_buffer.Length);
                    Array.Clear(sb_m62_axis_data, 0, sb_m62_axis_data.Length);
                    Properties.Settings.Default.m62_buffer_key_settings.Clear();
                    Properties.Settings.Default.m62_buffer_axis_settings.Clear();
                    Properties.Settings.Default.m62_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.m62_wav_path_key_buffer, 0, Form1.m62_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_m62_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED4M")
                {
                    Array.Clear(Form1.ED4M_key_buffer, 0, Form1.ED4M_key_buffer.Length);
                    Array.Clear(Form1.ED4M_axis_buffer, 0, Form1.ED4M_axis_buffer.Length);
                    Array.Clear(sb_ed4m_axis_data, 0, sb_ed4m_axis_data.Length);
                    Properties.Settings.Default.ed4m_buffer_key_settings.Clear();
                    Properties.Settings.Default.ed4m_buffer_axis_settings.Clear();
                    Properties.Settings.Default.ed4m_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.ed4m_wav_path_key_buffer, 0, Form1.ed4m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED9M")
                {
                    Array.Clear(Form1.ED9M_key_buffer, 0, Form1.ED9M_key_buffer.Length);
                    Array.Clear(Form1.ED9M_axis_buffer, 0, Form1.ED9M_axis_buffer.Length);
                    Array.Clear(sb_ed9m_axis_data, 0, sb_ed9m_axis_data.Length);
                    Properties.Settings.Default.ed9m_buffer_key_settings.Clear();
                    Properties.Settings.Default.ed9m_buffer_axis_settings.Clear();
                    Properties.Settings.Default.ed9m_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.ed9m_wav_path_key_buffer, 0, Form1.ed9m_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Clear();
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "tem18")
                {
                    Array.Clear(Form1.tem18_key_buffer, 0, Form1.tem18_key_buffer.Length);
                    Array.Clear(Form1.tem18_axis_buffer, 0, Form1.tem18_axis_buffer.Length);
                    Array.Clear(sb_tem18_axis_data, 0, sb_tem18_axis_data.Length);
                    Properties.Settings.Default.tem18_buffer_key_settings.Clear();
                    Properties.Settings.Default.tem18_buffer_axis_settings.Clear();
                    Properties.Settings.Default.tem18_buffer_axis_settings2.Clear();
                    //звук
                    Array.Clear(Form1.tem18_wav_path_key_buffer, 0, Form1.tem18_wav_path_key_buffer.Length);
                    Properties.Settings.Default.sb_tem18_wav_path_data_settings.Clear();
                }
                sbBufferSave();
                Form1.SaveBuffersSettings();
                Properties.Settings.Default.Save();
                KeyDataUpdate();
                
            }

        }

        private void button_zdsim_sbros_key_Click(object sender, EventArgs e)
        {
            if (dataGridView_Zdsimulator.CurrentCell.ColumnIndex == 1)
            {
                int i = 0;
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Controls")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.Controls_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.Controls_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.Controls_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_controls_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.controls_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.controls_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.controls_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Neshtatki")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.Neshtatki_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.Neshtatki_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.Neshtatki_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_neshtatki_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.neshtatki_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.neshtatki_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.neshtatki_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2ES5K")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ES5K_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.ES5K_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.ES5K_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_es5k_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.es5k_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.es5k_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.es5k_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "EP1M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.EP1M_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.EP1M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.EP1M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_ep1m_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.ep1m_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ep1m_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ep1m_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS2K")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS2K_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS2K_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS2K_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs2k_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs2k_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs2k_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs2k_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS4_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS4_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS4_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs4_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs4_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4 KVR")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS4KVR_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS4KVR_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS4KVR_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs4kvr_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs4kvr_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4kvr_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4T")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS4T_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS4T_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS4T_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs4t_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs4t_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4t_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs4t_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS7")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS7_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS7_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS7_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs7_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs7_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs7_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs7_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS8")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.CHS8_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.CHS8_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.CHS8_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_chs8_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.chs8_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs8_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.chs8_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL11M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.VL11M_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.VL11M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.VL11M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_vl11_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.vl11_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl11_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl11_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL82M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.VL82M_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.VL82M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.VL82M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_vl82_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.vl82_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl82_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl82_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL80T")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.VL80T_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.VL80T_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.VL80T_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_vl80t_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.vl80t_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl80t_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl80t_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL85")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.VL85_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.VL85_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.VL85_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_vl85_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.vl85_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl85_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.vl85_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "TEP70")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.TEP70_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.TEP70_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.TEP70_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_tep70_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.tep70_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.tep70_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.tep70_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2TE10U")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.TE10U_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.TE10U_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.TE10U_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_te10u_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.te10u_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.te10u_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.te10u_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "M62")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.M62_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.M62_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.M62_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_m62_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.m62_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.m62_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.m62_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED4M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ED4M_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.ED4M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.ED4M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_ed4m_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.ed4m_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ed4m_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ed4m_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED9M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ED9M_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.ED9M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.ED9M_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_ed9m_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.ed9m_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ed9m_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.ed9m_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "tem18")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.tem18_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = 0;
                    Form1.tem18_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 0] = 0;
                    Form1.tem18_axis_buffer[dataGridView_Zdsimulator.CurrentRow.Index, 1] = 0;
                    sb_tem18_axis_data[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.tem18_buffer_key_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.tem18_buffer_axis_settings[dataGridView_Zdsimulator.CurrentRow.Index] = "0";
                    Properties.Settings.Default.tem18_buffer_axis_settings2[dataGridView_Zdsimulator.CurrentRow.Index] = "0";

                    sbBufferSave();
                    Form1.SaveBuffersSettings();
                    Properties.Settings.Default.Save();
                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[1];

                }
            }

        }

        private void button_zdsim_sbros_sound_Click(object sender, EventArgs e)
        {
            if (dataGridView_Zdsimulator.CurrentCell.ColumnIndex == 2)
            {
                int i = 0;
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Controls")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.controls_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_controls_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2ES5K")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.es5k_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_es5k_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "EP1M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ep1m_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_ep1m_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS2K")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs2k_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs2k_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs4_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs4_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4 KVR")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs4kvr_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS4T")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs4t_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs4t_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS7")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs7_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs7_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "CHS8")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.chs8_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_chs8_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL11M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.vl11_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_vl11_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL82M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.vl82_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_vl82_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL80T")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.vl80t_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_vl80t_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "VL85")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.vl85_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_vl85_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "TEP70")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.tep70_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_tep70_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "2TE10U")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.te10u_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_te10u_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "M62")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.m62_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_m62_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED4M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ed4m_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_ed4m_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "ED9M")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.ed9m_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_ed9m_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "tem18")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.tem18_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_tem18_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
                if (comboBox_zdsimLoco.SelectedItem.ToString() == "Neshtatki")
                {
                    i = dataGridView_Zdsimulator.CurrentRow.Index;
                    Form1.neshtatki_wav_path_key_buffer[dataGridView_Zdsimulator.CurrentRow.Index] = null;
                    Properties.Settings.Default.sb_neshtatki_wav_path_data_settings[dataGridView_Zdsimulator.CurrentRow.Index] = null;

                    KeyDataUpdate();
                    //перевод курсора на выделенную позицию
                    dataGridView_Zdsimulator.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_Zdsimulator.CurrentCell = dataGridView_Zdsimulator.Rows[i].Cells[2];
 
                }
            }
        }

        private void button_zdsim_sbros_all_Click(object sender, EventArgs e)
        {
            string s_name = "Удаление настроек всех локомотивов";
            string s1 = "Настройки всех локомотивов будут удалены, уверены?";
            DialogResult result = MessageBox.Show(s1, s_name, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                Array.Clear(Form1.Controls_key_buffer, 0, Form1.Controls_key_buffer.Length);
                Array.Clear(Form1.Controls_axis_buffer, 0, Form1.Controls_axis_buffer.Length);
                Array.Clear(sb_controls_axis_data, 0, sb_controls_axis_data.Length);
                Properties.Settings.Default.controls_buffer_key_settings.Clear();
                Properties.Settings.Default.controls_buffer_axis_settings.Clear();
                Properties.Settings.Default.controls_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.controls_wav_path_key_buffer, 0, Form1.controls_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_controls_wav_path_data_settings.Clear();

                Array.Clear(Form1.Neshtatki_key_buffer, 0, Form1.Neshtatki_key_buffer.Length);
                Array.Clear(Form1.Neshtatki_axis_buffer, 0, Form1.Neshtatki_axis_buffer.Length);
                Array.Clear(sb_neshtatki_axis_data, 0, sb_neshtatki_axis_data.Length);
                Properties.Settings.Default.neshtatki_buffer_key_settings.Clear();
                Properties.Settings.Default.neshtatki_buffer_axis_settings.Clear();
                Properties.Settings.Default.neshtatki_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.neshtatki_wav_path_key_buffer, 0, Form1.neshtatki_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_neshtatki_wav_path_data_settings.Clear();

                Array.Clear(Form1.ES5K_key_buffer, 0, Form1.ES5K_key_buffer.Length);
                Array.Clear(Form1.ES5K_axis_buffer, 0, Form1.ES5K_axis_buffer.Length);
                Array.Clear(sb_es5k_axis_data, 0, sb_es5k_axis_data.Length);
                Properties.Settings.Default.es5k_buffer_key_settings.Clear();
                Properties.Settings.Default.es5k_buffer_axis_settings.Clear();
                Properties.Settings.Default.es5k_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.es5k_wav_path_key_buffer, 0, Form1.es5k_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_es5k_wav_path_data_settings.Clear();

                Array.Clear(Form1.EP1M_key_buffer, 0, Form1.EP1M_key_buffer.Length);
                Array.Clear(Form1.EP1M_axis_buffer, 0, Form1.EP1M_axis_buffer.Length);
                Array.Clear(sb_ep1m_axis_data, 0, sb_ep1m_axis_data.Length);
                Properties.Settings.Default.ep1m_buffer_key_settings.Clear();
                Properties.Settings.Default.ep1m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ep1m_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.ep1m_wav_path_key_buffer, 0, Form1.ep1m_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS2K_key_buffer, 0, Form1.CHS2K_key_buffer.Length);
                Array.Clear(Form1.CHS2K_axis_buffer, 0, Form1.CHS2K_axis_buffer.Length);
                Array.Clear(sb_chs2k_axis_data, 0, sb_chs2k_axis_data.Length);
                Properties.Settings.Default.chs2k_buffer_key_settings.Clear();
                Properties.Settings.Default.chs2k_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs2k_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs2k_wav_path_key_buffer, 0, Form1.chs2k_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS4_key_buffer, 0, Form1.CHS4_key_buffer.Length);
                Array.Clear(Form1.CHS4_axis_buffer, 0, Form1.CHS4_axis_buffer.Length);
                Array.Clear(sb_chs4_axis_data, 0, sb_chs4_axis_data.Length);
                Properties.Settings.Default.chs4_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs4_wav_path_key_buffer, 0, Form1.chs4_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs4_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS4KVR_key_buffer, 0, Form1.CHS4KVR_key_buffer.Length);
                Array.Clear(Form1.CHS4KVR_axis_buffer, 0, Form1.CHS4KVR_axis_buffer.Length);
                Array.Clear(sb_chs4kvr_axis_data, 0, sb_chs4kvr_axis_data.Length);
                Properties.Settings.Default.chs4kvr_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4kvr_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4kvr_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs4kvr_wav_path_key_buffer, 0, Form1.chs4kvr_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS4T_key_buffer, 0, Form1.CHS4T_key_buffer.Length);
                Array.Clear(Form1.CHS4T_axis_buffer, 0, Form1.CHS4T_axis_buffer.Length);
                Array.Clear(sb_chs4t_axis_data, 0, sb_chs4t_axis_data.Length);
                Properties.Settings.Default.chs4t_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4t_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4t_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs4t_wav_path_key_buffer, 0, Form1.chs4t_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS7_key_buffer, 0, Form1.CHS7_key_buffer.Length);
                Array.Clear(Form1.CHS7_axis_buffer, 0, Form1.CHS7_axis_buffer.Length);
                Array.Clear(sb_chs7_axis_data, 0, sb_chs7_axis_data.Length);
                Properties.Settings.Default.chs7_buffer_key_settings.Clear();
                Properties.Settings.Default.chs7_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs7_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs7_wav_path_key_buffer, 0, Form1.chs7_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs7_wav_path_data_settings.Clear();

                Array.Clear(Form1.CHS8_key_buffer, 0, Form1.CHS8_key_buffer.Length);
                Array.Clear(Form1.CHS8_axis_buffer, 0, Form1.CHS8_axis_buffer.Length);
                Array.Clear(sb_chs8_axis_data, 0, sb_chs8_axis_data.Length);
                Properties.Settings.Default.chs8_buffer_key_settings.Clear();
                Properties.Settings.Default.chs8_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs8_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.chs8_wav_path_key_buffer, 0, Form1.chs8_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_chs8_wav_path_data_settings.Clear();

                Array.Clear(Form1.VL11M_key_buffer, 0, Form1.VL11M_key_buffer.Length);
                Array.Clear(Form1.VL11M_axis_buffer, 0, Form1.VL11M_axis_buffer.Length);
                Array.Clear(sb_vl11_axis_data, 0, sb_vl11_axis_data.Length);
                Properties.Settings.Default.vl11_buffer_key_settings.Clear();
                Properties.Settings.Default.vl11_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl11_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.vl11_wav_path_key_buffer, 0, Form1.vl11_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_vl11_wav_path_data_settings.Clear();

                Array.Clear(Form1.VL82M_key_buffer, 0, Form1.VL82M_key_buffer.Length);
                Array.Clear(Form1.VL82M_axis_buffer, 0, Form1.VL82M_axis_buffer.Length);
                Array.Clear(sb_vl82_axis_data, 0, sb_vl82_axis_data.Length);
                Properties.Settings.Default.vl82_buffer_key_settings.Clear();
                Properties.Settings.Default.vl82_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl82_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.vl82_wav_path_key_buffer, 0, Form1.vl82_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_vl82_wav_path_data_settings.Clear();

                Array.Clear(Form1.VL80T_key_buffer, 0, Form1.VL80T_key_buffer.Length);
                Array.Clear(Form1.VL80T_axis_buffer, 0, Form1.VL80T_axis_buffer.Length);
                Array.Clear(sb_vl80t_axis_data, 0, sb_vl80t_axis_data.Length);
                Properties.Settings.Default.vl80t_buffer_key_settings.Clear();
                Properties.Settings.Default.vl80t_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl80t_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.vl80t_wav_path_key_buffer, 0, Form1.vl80t_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Clear();

                Array.Clear(Form1.VL85_key_buffer, 0, Form1.VL85_key_buffer.Length);
                Array.Clear(Form1.VL85_axis_buffer, 0, Form1.VL85_axis_buffer.Length);
                Array.Clear(sb_vl85_axis_data, 0, sb_vl85_axis_data.Length);
                Properties.Settings.Default.vl85_buffer_key_settings.Clear();
                Properties.Settings.Default.vl85_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl85_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.vl85_wav_path_key_buffer, 0, Form1.vl85_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_vl85_wav_path_data_settings.Clear();

                Array.Clear(Form1.TEP70_key_buffer, 0, Form1.TEP70_key_buffer.Length);
                Array.Clear(Form1.TEP70_axis_buffer, 0, Form1.TEP70_axis_buffer.Length);
                Array.Clear(sb_tep70_axis_data, 0, sb_tep70_axis_data.Length);
                Properties.Settings.Default.tep70_buffer_key_settings.Clear();
                Properties.Settings.Default.tep70_buffer_axis_settings.Clear();
                Properties.Settings.Default.tep70_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.tep70_wav_path_key_buffer, 0, Form1.tep70_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_tep70_wav_path_data_settings.Clear();

                Array.Clear(Form1.TE10U_key_buffer, 0, Form1.TE10U_key_buffer.Length);
                Array.Clear(Form1.TE10U_axis_buffer, 0, Form1.TE10U_axis_buffer.Length);
                Array.Clear(sb_te10u_axis_data, 0, sb_te10u_axis_data.Length);
                Properties.Settings.Default.te10u_buffer_key_settings.Clear();
                Properties.Settings.Default.te10u_buffer_axis_settings.Clear();
                Properties.Settings.Default.te10u_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.te10u_wav_path_key_buffer, 0, Form1.te10u_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_te10u_wav_path_data_settings.Clear();

                Array.Clear(Form1.M62_key_buffer, 0, Form1.M62_key_buffer.Length);
                Array.Clear(Form1.M62_axis_buffer, 0, Form1.M62_axis_buffer.Length);
                Array.Clear(sb_m62_axis_data, 0, sb_m62_axis_data.Length);
                Properties.Settings.Default.m62_buffer_key_settings.Clear();
                Properties.Settings.Default.m62_buffer_axis_settings.Clear();
                Properties.Settings.Default.m62_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.m62_wav_path_key_buffer, 0, Form1.m62_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_m62_wav_path_data_settings.Clear();

                Array.Clear(Form1.ED4M_key_buffer, 0, Form1.ED4M_key_buffer.Length);
                Array.Clear(Form1.ED4M_axis_buffer, 0, Form1.ED4M_axis_buffer.Length);
                Array.Clear(sb_ed4m_axis_data, 0, sb_ed4m_axis_data.Length);
                Properties.Settings.Default.ed4m_buffer_key_settings.Clear();
                Properties.Settings.Default.ed4m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ed4m_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.ed4m_wav_path_key_buffer, 0, Form1.ed4m_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Clear();

                Array.Clear(Form1.ED9M_key_buffer, 0, Form1.ED9M_key_buffer.Length);
                Array.Clear(Form1.ED9M_axis_buffer, 0, Form1.ED9M_axis_buffer.Length);
                Array.Clear(sb_ed9m_axis_data, 0, sb_ed9m_axis_data.Length);
                Properties.Settings.Default.ed9m_buffer_key_settings.Clear();
                Properties.Settings.Default.ed9m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ed9m_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.ed9m_wav_path_key_buffer, 0, Form1.ed9m_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Clear();

                Array.Clear(Form1.tem18_key_buffer, 0, Form1.tem18_key_buffer.Length);
                Array.Clear(Form1.tem18_axis_buffer, 0, Form1.tem18_axis_buffer.Length);
                Array.Clear(sb_tem18_axis_data, 0, sb_tem18_axis_data.Length);
                Properties.Settings.Default.tem18_buffer_key_settings.Clear();
                Properties.Settings.Default.tem18_buffer_axis_settings.Clear();
                Properties.Settings.Default.tem18_buffer_axis_settings2.Clear();
                //звук
                Array.Clear(Form1.tem18_wav_path_key_buffer, 0, Form1.tem18_wav_path_key_buffer.Length);
                Properties.Settings.Default.sb_tem18_wav_path_data_settings.Clear();


                sbBufferSave();
                Form1.SaveBuffersSettings();
                Properties.Settings.Default.Save();
                KeyDataUpdate();
            }
        }

        private void button_sbros_all_Click(object sender, EventArgs e)
        {
            string s_name = "Удаление ВСЕХ НАСТРОЕК !";
            string s1 = "ВНИМАНИЕ! Все настройки, включая точки осей будут удалены, уверены?";
            DialogResult result = MessageBox.Show(s1, s_name, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                //удаляем все буфера кнопок и осей
                Array.Clear(Form1.Controls_key_buffer, 0, Form1.Controls_key_buffer.Length);
                Array.Clear(Form1.Controls_axis_buffer, 0, Form1.Controls_axis_buffer.Length);
                Properties.Settings.Default.controls_buffer_key_settings.Clear();
                Properties.Settings.Default.controls_buffer_axis_settings.Clear();
                Properties.Settings.Default.controls_buffer_axis_settings2.Clear();
                Array.Clear(Form1.Neshtatki_key_buffer, 0, Form1.Neshtatki_key_buffer.Length);
                Array.Clear(Form1.Neshtatki_axis_buffer, 0, Form1.Neshtatki_axis_buffer.Length);
                Array.Clear(sb_neshtatki_axis_data, 0, sb_neshtatki_axis_data.Length);
                Properties.Settings.Default.neshtatki_buffer_key_settings.Clear();
                Properties.Settings.Default.neshtatki_buffer_axis_settings.Clear();
                Properties.Settings.Default.neshtatki_buffer_axis_settings2.Clear();
                Array.Clear(Form1.ES5K_key_buffer, 0, Form1.ES5K_key_buffer.Length);
                Array.Clear(Form1.ES5K_axis_buffer, 0, Form1.ES5K_axis_buffer.Length);
                Properties.Settings.Default.es5k_buffer_key_settings.Clear();
                Properties.Settings.Default.es5k_buffer_axis_settings.Clear();
                Properties.Settings.Default.es5k_buffer_axis_settings2.Clear();
                Array.Clear(Form1.EP1M_key_buffer, 0, Form1.EP1M_key_buffer.Length);
                Array.Clear(Form1.EP1M_axis_buffer, 0, Form1.EP1M_axis_buffer.Length);
                Properties.Settings.Default.ep1m_buffer_key_settings.Clear();
                Properties.Settings.Default.ep1m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ep1m_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS2K_key_buffer, 0, Form1.CHS2K_key_buffer.Length);
                Array.Clear(Form1.CHS2K_axis_buffer, 0, Form1.CHS2K_axis_buffer.Length);
                Properties.Settings.Default.chs2k_buffer_key_settings.Clear();
                Properties.Settings.Default.chs2k_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs2k_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS4_key_buffer, 0, Form1.CHS4_key_buffer.Length);
                Array.Clear(Form1.CHS4_axis_buffer, 0, Form1.CHS4_axis_buffer.Length);
                Properties.Settings.Default.chs4_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS4KVR_key_buffer, 0, Form1.CHS4KVR_key_buffer.Length);
                Array.Clear(Form1.CHS4KVR_axis_buffer, 0, Form1.CHS4KVR_axis_buffer.Length);
                Properties.Settings.Default.chs4kvr_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4kvr_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4kvr_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS4T_key_buffer, 0, Form1.CHS4T_key_buffer.Length);
                Array.Clear(Form1.CHS4T_axis_buffer, 0, Form1.CHS4T_axis_buffer.Length);
                Properties.Settings.Default.chs4t_buffer_key_settings.Clear();
                Properties.Settings.Default.chs4t_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs4t_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS7_key_buffer, 0, Form1.CHS7_key_buffer.Length);
                Array.Clear(Form1.CHS7_axis_buffer, 0, Form1.CHS7_axis_buffer.Length);
                Properties.Settings.Default.chs7_buffer_key_settings.Clear();
                Properties.Settings.Default.chs7_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs7_buffer_axis_settings2.Clear();
                Array.Clear(Form1.CHS8_key_buffer, 0, Form1.CHS8_key_buffer.Length);
                Array.Clear(Form1.CHS8_axis_buffer, 0, Form1.CHS8_axis_buffer.Length);
                Properties.Settings.Default.chs8_buffer_key_settings.Clear();
                Properties.Settings.Default.chs8_buffer_axis_settings.Clear();
                Properties.Settings.Default.chs8_buffer_axis_settings2.Clear();
                Array.Clear(Form1.VL11M_key_buffer, 0, Form1.VL11M_key_buffer.Length);
                Array.Clear(Form1.VL11M_axis_buffer, 0, Form1.VL11M_axis_buffer.Length);
                Properties.Settings.Default.vl11_buffer_key_settings.Clear();
                Properties.Settings.Default.vl11_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl11_buffer_axis_settings2.Clear();
                Array.Clear(Form1.VL82M_key_buffer, 0, Form1.VL82M_key_buffer.Length);
                Array.Clear(Form1.VL82M_axis_buffer, 0, Form1.VL82M_axis_buffer.Length);
                Properties.Settings.Default.vl82_buffer_key_settings.Clear();
                Properties.Settings.Default.vl82_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl82_buffer_axis_settings2.Clear();
                Array.Clear(Form1.VL80T_key_buffer, 0, Form1.VL80T_key_buffer.Length);
                Array.Clear(Form1.VL80T_axis_buffer, 0, Form1.VL80T_axis_buffer.Length);
                Properties.Settings.Default.vl80t_buffer_key_settings.Clear();
                Properties.Settings.Default.vl80t_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl80t_buffer_axis_settings2.Clear();
                Array.Clear(Form1.VL85_key_buffer, 0, Form1.VL85_key_buffer.Length);
                Array.Clear(Form1.VL85_axis_buffer, 0, Form1.VL85_axis_buffer.Length);
                Properties.Settings.Default.vl85_buffer_key_settings.Clear();
                Properties.Settings.Default.vl85_buffer_axis_settings.Clear();
                Properties.Settings.Default.vl85_buffer_axis_settings2.Clear();
                Array.Clear(Form1.TEP70_key_buffer, 0, Form1.TEP70_key_buffer.Length);
                Array.Clear(Form1.TEP70_axis_buffer, 0, Form1.TEP70_axis_buffer.Length); 
                Properties.Settings.Default.tep70_buffer_key_settings.Clear();
                Properties.Settings.Default.tep70_buffer_axis_settings.Clear();
                Properties.Settings.Default.tep70_buffer_axis_settings2.Clear();
                Array.Clear(Form1.TE10U_key_buffer, 0, Form1.TE10U_key_buffer.Length);
                Array.Clear(Form1.TE10U_axis_buffer, 0, Form1.TE10U_axis_buffer.Length); 
                Properties.Settings.Default.te10u_buffer_key_settings.Clear();
                Properties.Settings.Default.te10u_buffer_axis_settings.Clear();
                Properties.Settings.Default.te10u_buffer_axis_settings2.Clear();
                Array.Clear(Form1.M62_key_buffer, 0, Form1.M62_key_buffer.Length);
                Array.Clear(Form1.M62_axis_buffer, 0, Form1.M62_axis_buffer.Length); 
                Properties.Settings.Default.m62_buffer_key_settings.Clear();
                Properties.Settings.Default.m62_buffer_axis_settings.Clear();
                Properties.Settings.Default.m62_buffer_axis_settings2.Clear();
                Array.Clear(Form1.ED4M_key_buffer, 0, Form1.ED4M_key_buffer.Length);
                Array.Clear(Form1.ED4M_axis_buffer, 0, Form1.ED4M_axis_buffer.Length); 
                Properties.Settings.Default.ed4m_buffer_key_settings.Clear();
                Properties.Settings.Default.ed4m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ed4m_buffer_axis_settings2.Clear();
                Array.Clear(Form1.ED9M_key_buffer, 0, Form1.ED9M_key_buffer.Length);
                Array.Clear(Form1.ED9M_axis_buffer, 0, Form1.ED9M_axis_buffer.Length); 
                Properties.Settings.Default.ed9m_buffer_key_settings.Clear();
                Properties.Settings.Default.ed9m_buffer_axis_settings.Clear();
                Properties.Settings.Default.ed9m_buffer_axis_settings2.Clear();
                Array.Clear(Form1.tem18_key_buffer, 0, Form1.tem18_key_buffer.Length);
                Array.Clear(Form1.tem18_axis_buffer, 0, Form1.tem18_axis_buffer.Length);
                Properties.Settings.Default.tem18_buffer_key_settings.Clear();
                Properties.Settings.Default.tem18_buffer_axis_settings.Clear();
                Properties.Settings.Default.tem18_buffer_axis_settings2.Clear();

 
                //удаляем точки осей
                Form1.joystick_ARx_point_buffer = null;
                Properties.Settings.Default.ARx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_ARy_point_buffer = null;
                Properties.Settings.Default.ARy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_ARz_point_buffer = null;
                Properties.Settings.Default.ARz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_AX_point_buffer = null;
                Properties.Settings.Default.AX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_AY_point_buffer = null;
                Properties.Settings.Default.AY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_AZ_point_buffer = null;
                Properties.Settings.Default.AZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FRx_point_buffer = null;
                Properties.Settings.Default.FRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FRy_point_buffer = null;
                Properties.Settings.Default.FRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FRz_point_buffer = null;
                Properties.Settings.Default.FRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FX_point_buffer = null;
                Properties.Settings.Default.FX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FY_point_buffer = null;
                Properties.Settings.Default.FY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FZ_point_buffer = null;
                Properties.Settings.Default.FZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Rx_point_buffer = null;
                Properties.Settings.Default.Rx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Ry_point_buffer = null;
                Properties.Settings.Default.Ry_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Rz_point_buffer = null;
                Properties.Settings.Default.Rz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VRx_point_buffer = null;
                Properties.Settings.Default.VRx_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VRy_point_buffer = null;
                Properties.Settings.Default.VRy_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VRz_point_buffer = null;
                Properties.Settings.Default.VRz_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VX_point_buffer = null;
                Properties.Settings.Default.VX_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VY_point_buffer = null;
                Properties.Settings.Default.VY_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VZ_point_buffer = null;
                Properties.Settings.Default.VZ_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_X_point_buffer = null;
                Properties.Settings.Default.X_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Y_point_buffer = null;
                Properties.Settings.Default.Y_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Z_point_buffer = null;
                Properties.Settings.Default.Z_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_POV_point_buffer = null;
                Properties.Settings.Default.POV_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_Slider_point_buffer = null;
                Properties.Settings.Default.Slider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_ASlider_point_buffer = null;
                Properties.Settings.Default.ASlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_FSlider_point_buffer = null;
                Properties.Settings.Default.FSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                Form1.joystick_VSlider_point_buffer = null;
                Properties.Settings.Default.VSlider_point_buffer_settings = Properties.Settings.Default.temp_string_buffer_start;
                
                Properties.Settings.Default.Save();
                Form1.DeleteXmlBinFiles();
                DeleteAppDataFiles();
                DeleteAppDataFiles_local();
                Thread.Sleep(3000);
                this.Cursor = Cursors.Default;
                Application.Restart();
                
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

        private void checkBox_dvery_control_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_dvery_control.Checked == true) Form1.i_dvery_control_off_settings = 0;
            else Form1.i_dvery_control_off_settings = 1;
        }





        
        

        

        

        

    }
}
