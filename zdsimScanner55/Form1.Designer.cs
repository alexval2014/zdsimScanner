namespace zdsimScanner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.but_settings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.console1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button_stop = new System.Windows.Forms.Button();
            this.timer_bdit = new System.Windows.Forms.Timer(this.components);
            this.combobox_Port = new System.Windows.Forms.ComboBox();
            this.button_about = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDown_delay_HID = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_skorCOM = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_delay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_time = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.timer_joystick_update = new System.Windows.Forms.Timer(this.components);
            this.timer_send_HID = new System.Windows.Forms.Timer(this.components);
            this.timer_delay_key_50 = new System.Windows.Forms.Timer(this.components);
            this.timer_dvery_delay = new System.Windows.Forms.Timer(this.components);
            this.timer_500ms = new System.Windows.Forms.Timer(this.components);
            this.timer_oborot_disel = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay_HID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_skorCOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_time)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Время до запуска - min";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(19, 585);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(1017, 585);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Пуск";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "COM порт";
            // 
            // but_settings
            // 
            this.but_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_settings.Location = new System.Drawing.Point(1008, 68);
            this.but_settings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.but_settings.Name = "but_settings";
            this.but_settings.Size = new System.Drawing.Size(123, 35);
            this.but_settings.TabIndex = 10;
            this.but_settings.Text = "Настройки";
            this.but_settings.UseVisualStyleBackColor = true;
            this.but_settings.Click += new System.EventHandler(this.but_settings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Скорость COM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(584, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Интервал передачи COM";
            // 
            // console1
            // 
            this.console1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console1.BackColor = System.Drawing.Color.Black;
            this.console1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.console1.Location = new System.Drawing.Point(18, 131);
            this.console1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.console1.Multiline = true;
            this.console1.Name = "console1";
            this.console1.ReadOnly = true;
            this.console1.Size = new System.Drawing.Size(1110, 423);
            this.console1.TabIndex = 17;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button_stop
            // 
            this.button_stop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_stop.Location = new System.Drawing.Point(513, 586);
            this.button_stop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(112, 35);
            this.button_stop.TabIndex = 19;
            this.button_stop.Text = "Стоп";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // timer_bdit
            // 
            this.timer_bdit.Enabled = true;
            this.timer_bdit.Tick += new System.EventHandler(this.timer_bdit_Tick);
            // 
            // combobox_Port
            // 
            this.combobox_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_Port.FormattingEnabled = true;
            this.combobox_Port.Location = new System.Drawing.Point(208, 68);
            this.combobox_Port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.combobox_Port.Name = "combobox_Port";
            this.combobox_Port.Size = new System.Drawing.Size(180, 28);
            this.combobox_Port.TabIndex = 7;
            this.toolTip1.SetToolTip(this.combobox_Port, "COM порт вашего устройства вывода или Arduino");
            this.combobox_Port.SelectedIndexChanged += new System.EventHandler(this.combobox_Port_SelectedIndexChanged);
            // 
            // button_about
            // 
            this.button_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_about.Location = new System.Drawing.Point(1072, 12);
            this.button_about.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_about.Name = "button_about";
            this.button_about.Size = new System.Drawing.Size(58, 35);
            this.button_about.TabIndex = 20;
            this.button_about.Text = "?";
            this.button_about.UseVisualStyleBackColor = true;
            this.button_about.Click += new System.EventHandler(this.button_about_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 1000;
            // 
            // numericUpDown_delay_HID
            // 
            this.numericUpDown_delay_HID.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::zdsimScanner.Properties.Settings.Default, "delay_send_HID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_delay_HID.Location = new System.Drawing.Point(804, 68);
            this.numericUpDown_delay_HID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_delay_HID.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.numericUpDown_delay_HID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_delay_HID.Name = "numericUpDown_delay_HID";
            this.numericUpDown_delay_HID.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown_delay_HID.TabIndex = 24;
            this.toolTip1.SetToolTip(this.numericUpDown_delay_HID, "Интервал передачи данных в игру\r\nс устройства ввода HID в мсек");
            this.numericUpDown_delay_HID.Value = global::zdsimScanner.Properties.Settings.Default.delay_send_HID;
            this.numericUpDown_delay_HID.ValueChanged += new System.EventHandler(this.numericUpDown_delay_HID_ValueChanged);
            // 
            // numericUpDown_skorCOM
            // 
            this.numericUpDown_skorCOM.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::zdsimScanner.Properties.Settings.Default, "skor_COM", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_skorCOM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_skorCOM.Location = new System.Drawing.Point(399, 68);
            this.numericUpDown_skorCOM.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_skorCOM.Maximum = new decimal(new int[] {
            256000,
            0,
            0,
            0});
            this.numericUpDown_skorCOM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_skorCOM.Name = "numericUpDown_skorCOM";
            this.numericUpDown_skorCOM.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown_skorCOM.TabIndex = 16;
            this.toolTip1.SetToolTip(this.numericUpDown_skorCOM, "Скорость передачи COM порта, \r\nпо прошивкам 56000, но можно\r\nрегулировать, если е" +
        "сть помехи при передаче");
            this.numericUpDown_skorCOM.Value = global::zdsimScanner.Properties.Settings.Default.skor_COM;
            this.numericUpDown_skorCOM.ValueChanged += new System.EventHandler(this.numericUpDown_skorCOM_ValueChanged);
            // 
            // numericUpDown_delay
            // 
            this.numericUpDown_delay.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::zdsimScanner.Properties.Settings.Default, "delay_send", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_delay.Location = new System.Drawing.Point(603, 68);
            this.numericUpDown_delay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_delay.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.numericUpDown_delay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_delay.Name = "numericUpDown_delay";
            this.numericUpDown_delay.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown_delay.TabIndex = 13;
            this.toolTip1.SetToolTip(this.numericUpDown_delay, "Интервал передачи данных на COM порт в мсек");
            this.numericUpDown_delay.Value = global::zdsimScanner.Properties.Settings.Default.delay_send;
            this.numericUpDown_delay.ValueChanged += new System.EventHandler(this.numericUpDown_delay_ValueChanged);
            // 
            // numericUpDown_time
            // 
            this.numericUpDown_time.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::zdsimScanner.Properties.Settings.Default, "Time", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown_time.Location = new System.Drawing.Point(19, 68);
            this.numericUpDown_time.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDown_time.Name = "numericUpDown_time";
            this.numericUpDown_time.Size = new System.Drawing.Size(180, 26);
            this.numericUpDown_time.TabIndex = 11;
            this.toolTip1.SetToolTip(this.numericUpDown_time, "Время до запуска трейнера\r\n(для того, чтобы потом не сворачивать игру)");
            this.numericUpDown_time.Value = global::zdsimScanner.Properties.Settings.Default.Time;
            this.numericUpDown_time.ValueChanged += new System.EventHandler(this.numericUpDown_time_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(800, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Интервал передачи HID";
            // 
            // timer_joystick_update
            // 
            this.timer_joystick_update.Interval = 5;
            this.timer_joystick_update.Tick += new System.EventHandler(this.timer_joystick_update_Tick);
            // 
            // timer_send_HID
            // 
            this.timer_send_HID.Tick += new System.EventHandler(this.timer_send_HID_Tick);
            // 
            // timer_delay_key_50
            // 
            this.timer_delay_key_50.Enabled = true;
            this.timer_delay_key_50.Interval = 50;
            this.timer_delay_key_50.Tick += new System.EventHandler(this.timer_delay_key_Tick);
            // 
            // timer_dvery_delay
            // 
            this.timer_dvery_delay.Interval = 1000;
            this.timer_dvery_delay.Tick += new System.EventHandler(this.timer_dvery_delay_Tick_1);
            // 
            // timer_500ms
            // 
            this.timer_500ms.Enabled = true;
            this.timer_500ms.Interval = 500;
            this.timer_500ms.Tick += new System.EventHandler(this.timer_500ms_Tick);
            // 
            // timer_oborot_disel
            // 
            this.timer_oborot_disel.Enabled = true;
            this.timer_oborot_disel.Interval = 120;
            this.timer_oborot_disel.Tick += new System.EventHandler(this.timer_oborot_disel_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(1149, 639);
            this.Controls.Add(this.numericUpDown_delay_HID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_about);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.console1);
            this.Controls.Add(this.numericUpDown_skorCOM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_delay);
            this.Controls.Add(this.numericUpDown_time);
            this.Controls.Add(this.but_settings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combobox_Port);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1152, 654);
            this.Name = "Form1";
            this.Text = "zdsim Scanner55 v7.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_Closed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay_HID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_skorCOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_time)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox combobox_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button but_settings;
        private System.Windows.Forms.NumericUpDown numericUpDown_time;
        private System.Windows.Forms.NumericUpDown numericUpDown_delay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_skorCOM;
        private System.Windows.Forms.TextBox console1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer_bdit;
        private System.Windows.Forms.Button button_about;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_delay_HID;
        private System.Windows.Forms.Timer timer_joystick_update;
        private System.Windows.Forms.Timer timer_send_HID;
        private System.Windows.Forms.Timer timer_delay_key_50;
        private System.Windows.Forms.Timer timer_dvery_delay;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Timer timer_500ms;
        private System.Windows.Forms.Timer timer_oborot_disel;
    }
}

