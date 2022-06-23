
namespace Slot13_FristWindowForm
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
            this.lbId = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lbPhone = new System.Windows.Forms.Label();
            this.txt_phone = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDegree = new System.Windows.Forms.Label();
            this.degree = new System.Windows.Forms.ComboBox();
            this.lbGender = new System.Windows.Forms.Label();
            this.radio_male = new System.Windows.Forms.RadioButton();
            this.radio_female = new System.Windows.Forms.RadioButton();
            this.cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbId.Location = new System.Drawing.Point(60, 114);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(121, 22);
            this.lbId.TabIndex = 0;
            this.lbId.Text = "Employee ID";
            // 
            // txt_id
            // 
            this.txt_id.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_id.Location = new System.Drawing.Point(213, 105);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(333, 36);
            this.txt_id.TabIndex = 1;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(60, 178);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(151, 22);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "Employee Name";
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.Location = new System.Drawing.Point(213, 169);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(333, 36);
            this.txt_name.TabIndex = 3;
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhone.Location = new System.Drawing.Point(60, 238);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(65, 22);
            this.lbPhone.TabIndex = 4;
            this.lbPhone.Text = "Phone";
            // 
            // txt_phone
            // 
            this.txt_phone.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_phone.Location = new System.Drawing.Point(213, 229);
            this.txt_phone.Name = "txt_phone";
            this.txt_phone.Size = new System.Drawing.Size(333, 36);
            this.txt_phone.TabIndex = 5;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.save.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save.Location = new System.Drawing.Point(167, 435);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(138, 44);
            this.save.TabIndex = 6;
            this.save.Text = "&Save";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 35);
            this.label1.TabIndex = 7;
            this.label1.Text = "Employee Details";
            // 
            // lbDegree
            // 
            this.lbDegree.AutoSize = true;
            this.lbDegree.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDegree.Location = new System.Drawing.Point(60, 360);
            this.lbDegree.Name = "lbDegree";
            this.lbDegree.Size = new System.Drawing.Size(74, 22);
            this.lbDegree.TabIndex = 8;
            this.lbDegree.Text = "Degree";
            // 
            // degree
            // 
            this.degree.AllowDrop = true;
            this.degree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.degree.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degree.FormattingEnabled = true;
            this.degree.ItemHeight = 21;
            this.degree.Items.AddRange(new object[] {
            "University",
            "High school"});
            this.degree.SelectedIndex = 0;
            this.degree.Location = new System.Drawing.Point(213, 360);
            this.degree.Name = "degree";
            this.degree.Size = new System.Drawing.Size(272, 29);
            this.degree.TabIndex = 9;
            // 
            // lbGender
            // 
            this.lbGender.AutoSize = true;
            this.lbGender.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGender.Location = new System.Drawing.Point(60, 295);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(74, 22);
            this.lbGender.TabIndex = 10;
            this.lbGender.Text = "Gender";
            // 
            // radio_male
            // 
            this.radio_male.AutoSize = true;
            this.radio_male.Checked = true;
            this.radio_male.Location = new System.Drawing.Point(19, 31);
            this.radio_male.Name = "radio_male";
            this.radio_male.Size = new System.Drawing.Size(59, 20);
            this.radio_male.TabIndex = 11;
            this.radio_male.TabStop = true;
            this.radio_male.Text = "Male";
            this.radio_male.UseVisualStyleBackColor = true;
            // 
            // radio_female
            // 
            this.radio_female.AutoSize = true;
            this.radio_female.Location = new System.Drawing.Point(116, 31);
            this.radio_female.Name = "radio_female";
            this.radio_female.Size = new System.Drawing.Size(76, 20);
            this.radio_female.TabIndex = 12;
            this.radio_female.Text = "Female";
            this.radio_female.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.Location = new System.Drawing.Point(375, 435);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(138, 44);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radio_female);
            this.panel1.Controls.Add(this.radio_male);
            this.panel1.Location = new System.Drawing.Point(213, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 57);
            this.panel1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 517);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.degree);
            this.Controls.Add(this.lbDegree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.save);
            this.Controls.Add(this.txt_phone);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.lbId);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.TextBox txt_phone;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbDegree;
        private System.Windows.Forms.ComboBox degree;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.RadioButton radio_male;
        private System.Windows.Forms.RadioButton radio_female;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Panel panel1;
    }
}

