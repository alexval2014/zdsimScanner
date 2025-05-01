namespace zdsimScanner
{
    partial class Form_joystick_control
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_osi = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridView_but = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_osi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_but)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_osi
            // 
            this.dataGridView_osi.AllowUserToAddRows = false;
            this.dataGridView_osi.AllowUserToDeleteRows = false;
            this.dataGridView_osi.AllowUserToResizeColumns = false;
            this.dataGridView_osi.AllowUserToResizeRows = false;
            this.dataGridView_osi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_osi.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView_osi.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_osi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_osi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_osi.ColumnHeadersVisible = false;
            this.dataGridView_osi.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_osi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_osi.GridColor = System.Drawing.Color.Silver;
            this.dataGridView_osi.Location = new System.Drawing.Point(13, 55);
            this.dataGridView_osi.MultiSelect = false;
            this.dataGridView_osi.Name = "dataGridView_osi";
            this.dataGridView_osi.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_osi.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_osi.RowHeadersVisible = false;
            this.dataGridView_osi.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_osi.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_osi.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView_osi.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Gold;
            this.dataGridView_osi.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange;
            this.dataGridView_osi.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView_osi.RowTemplate.DividerHeight = 1;
            this.dataGridView_osi.RowTemplate.ReadOnly = true;
            this.dataGridView_osi.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_osi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_osi.ShowCellToolTips = false;
            this.dataGridView_osi.ShowEditingIcon = false;
            this.dataGridView_osi.Size = new System.Drawing.Size(160, 522);
            this.dataGridView_osi.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dataGridView_osi, "Выберите точки осей");
            this.dataGridView_osi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_osi_CellDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 2000;
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 2000;
            this.toolTip1.ReshowDelay = 400;
            // 
            // dataGridView_but
            // 
            this.dataGridView_but.AllowUserToAddRows = false;
            this.dataGridView_but.AllowUserToDeleteRows = false;
            this.dataGridView_but.AllowUserToResizeColumns = false;
            this.dataGridView_but.AllowUserToResizeRows = false;
            this.dataGridView_but.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_but.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView_but.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_but.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_but.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_but.ColumnHeadersVisible = false;
            this.dataGridView_but.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_but.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_but.GridColor = System.Drawing.Color.Silver;
            this.dataGridView_but.Location = new System.Drawing.Point(184, 55);
            this.dataGridView_but.MultiSelect = false;
            this.dataGridView_but.Name = "dataGridView_but";
            this.dataGridView_but.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_but.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_but.RowHeadersVisible = false;
            this.dataGridView_but.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_but.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_but.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView_but.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Gold;
            this.dataGridView_but.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange;
            this.dataGridView_but.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView_but.RowTemplate.DividerHeight = 1;
            this.dataGridView_but.RowTemplate.ReadOnly = true;
            this.dataGridView_but.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_but.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_but.ShowCellToolTips = false;
            this.dataGridView_but.ShowEditingIcon = false;
            this.dataGridView_but.Size = new System.Drawing.Size(160, 522);
            this.dataGridView_but.TabIndex = 1;
            this.toolTip1.SetToolTip(this.dataGridView_but, "Выберите кнопку");
            this.dataGridView_but.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_but_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(56, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Точки осей";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(240, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кнопки";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form_joystick_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 594);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_but);
            this.Controls.Add(this.dataGridView_osi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(364, 614);
            this.Name = "Form_joystick_control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Кнопки и оси джойстика";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_joystick_control_FormClosed);
            this.Load += new System.EventHandler(this.Form_joystick_control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_osi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_but)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_osi;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dataGridView_but;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
    }
}