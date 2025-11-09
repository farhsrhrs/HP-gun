namespace HP_gun
{
    partial class PartnerEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartnerEditForm));
            label1 = new Label();
            label2 = new Label();
            cmbType = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txtName = new TextBox();
            txtDirector = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            btnSave = new Button();
            numRating = new NumericUpDown();
            maskedTextBoxPhone = new MaskedTextBox();
            maskedTxtINN = new MaskedTextBox();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)numRating).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 28);
            label1.Name = "label1";
            label1.Size = new Size(549, 37);
            label1.TabIndex = 0;
            label1.Text = "Добовление/Редактирование партнера";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(36, 98);
            label2.Name = "label2";
            label2.Size = new Size(120, 21);
            label2.TabIndex = 1;
            label2.Text = "Тип партнёра:";
            // 
            // cmbType
            // 
            cmbType.BackColor = Color.FromArgb(244, 232, 211);
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "ООО", "ЗАО", "ОАО", "ПАО", "ИП" });
            cmbType.Location = new Point(303, 98);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(434, 23);
            cmbType.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(36, 128);
            label3.Name = "label3";
            label3.Size = new Size(211, 21);
            label3.TabIndex = 3;
            label3.Text = "Наименование партнера:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(36, 157);
            label4.Name = "label4";
            label4.Size = new Size(91, 21);
            label4.TabIndex = 4;
            label4.Text = "Директор:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.Location = new Point(36, 186);
            label5.Name = "label5";
            label5.Size = new Size(245, 21);
            label5.TabIndex = 5;
            label5.Text = "Электронная почта партнера:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label6.Location = new Point(36, 215);
            label6.Name = "label6";
            label6.Size = new Size(161, 21);
            label6.TabIndex = 6;
            label6.Text = "Телефон партнера:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label7.Location = new Point(36, 244);
            label7.Name = "label7";
            label7.Size = new Size(255, 21);
            label7.TabIndex = 7;
            label7.Text = "Юридический адрес партнера:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label8.Location = new Point(36, 273);
            label8.Name = "label8";
            label8.Size = new Size(51, 21);
            label8.TabIndex = 8;
            label8.Text = "ИНН:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.Location = new Point(36, 300);
            label9.Name = "label9";
            label9.Size = new Size(77, 21);
            label9.TabIndex = 9;
            label9.Text = "Рейтинг:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(244, 232, 211);
            txtName.Location = new Point(303, 128);
            txtName.Name = "txtName";
            txtName.Size = new Size(434, 23);
            txtName.TabIndex = 10;
            // 
            // txtDirector
            // 
            txtDirector.BackColor = Color.FromArgb(244, 232, 211);
            txtDirector.Location = new Point(303, 157);
            txtDirector.Name = "txtDirector";
            txtDirector.Size = new Size(434, 23);
            txtDirector.TabIndex = 11;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(244, 232, 211);
            txtEmail.Location = new Point(303, 186);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(434, 23);
            txtEmail.TabIndex = 12;
            // 
            // txtAddress
            // 
            txtAddress.BackColor = Color.FromArgb(244, 232, 211);
            txtAddress.Location = new Point(303, 244);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(434, 23);
            txtAddress.TabIndex = 14;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(103, 186, 128);
            btnSave.FlatAppearance.BorderColor = Color.Black;
            btnSave.FlatAppearance.BorderSize = 2;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(303, 390);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(142, 48);
            btnSave.TabIndex = 17;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click_1;
            // 
            // numRating
            // 
            numRating.BackColor = Color.FromArgb(244, 232, 211);
            numRating.Location = new Point(303, 303);
            numRating.Name = "numRating";
            numRating.Size = new Size(434, 23);
            numRating.TabIndex = 18;
            // 
            // maskedTextBoxPhone
            // 
            maskedTextBoxPhone.BackColor = Color.FromArgb(244, 232, 211);
            maskedTextBoxPhone.Location = new Point(303, 215);
            maskedTextBoxPhone.Mask = "000 000 00 00";
            maskedTextBoxPhone.Name = "maskedTextBoxPhone";
            maskedTextBoxPhone.Size = new Size(434, 23);
            maskedTextBoxPhone.TabIndex = 19;
            // 
            // maskedTxtINN
            // 
            maskedTxtINN.BackColor = Color.FromArgb(244, 232, 211);
            maskedTxtINN.Location = new Point(303, 274);
            maskedTxtINN.Mask = "0000000000";
            maskedTxtINN.Name = "maskedTxtINN";
            maskedTxtINN.Size = new Size(434, 23);
            maskedTxtINN.TabIndex = 20;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(103, 186, 128);
            btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBack.Location = new Point(620, 390);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(146, 48);
            btnBack.TabIndex = 21;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click_1;
            // 
            // PartnerEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(maskedTxtINN);
            Controls.Add(maskedTextBoxPhone);
            Controls.Add(numRating);
            Controls.Add(btnSave);
            Controls.Add(txtAddress);
            Controls.Add(txtEmail);
            Controls.Add(txtDirector);
            Controls.Add(txtName);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbType);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PartnerEditForm";
            Text = "Добовление/Редактирование партнера";
            ((System.ComponentModel.ISupportInitialize)numRating).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbType;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtName;
        private TextBox txtDirector;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private Button btnSave;
        private NumericUpDown numRating;
        private MaskedTextBox maskedTextBoxPhone;
        private MaskedTextBox maskedTxtINN;
        private Button btnBack;
    }
}