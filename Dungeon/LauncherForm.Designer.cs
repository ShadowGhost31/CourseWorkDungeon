﻿namespace Dungeon
{
    partial class LauncherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
            this.button_left = new System.Windows.Forms.Button();
            this.label_main = new System.Windows.Forms.Label();
            this.textBox_nick = new System.Windows.Forms.TextBox();
            this.label_nick = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_right = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_left
            // 
            this.button_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button_left.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_left.ForeColor = System.Drawing.Color.Black;
            this.button_left.Location = new System.Drawing.Point(14, 339);
            this.button_left.Name = "button_left";
            this.button_left.Size = new System.Drawing.Size(280, 50);
            this.button_left.TabIndex = 0;
            this.button_left.Text = "Реєстрація";
            this.button_left.UseVisualStyleBackColor = false;
            this.button_left.Click += new System.EventHandler(this.button_left_Click);
            // 
            // label_main
            // 
            this.label_main.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_main.ForeColor = System.Drawing.Color.White;
            this.label_main.Location = new System.Drawing.Point(12, 129);
            this.label_main.Name = "label_main";
            this.label_main.Size = new System.Drawing.Size(603, 43);
            this.label_main.TabIndex = 2;
            this.label_main.Text = "Авторизація";
            this.label_main.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_nick
            // 
            this.textBox_nick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_nick.Location = new System.Drawing.Point(12, 218);
            this.textBox_nick.MaxLength = 32;
            this.textBox_nick.Name = "textBox_nick";
            this.textBox_nick.Size = new System.Drawing.Size(603, 31);
            this.textBox_nick.TabIndex = 3;
            this.textBox_nick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_nick.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_nick.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label_nick
            // 
            this.label_nick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_nick.ForeColor = System.Drawing.Color.White;
            this.label_nick.Location = new System.Drawing.Point(12, 172);
            this.label_nick.Name = "label_nick";
            this.label_nick.Size = new System.Drawing.Size(603, 34);
            this.label_nick.TabIndex = 4;
            this.label_nick.Text = "Ім\'я:";
            this.label_nick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_password
            // 
            this.label_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_password.ForeColor = System.Drawing.Color.White;
            this.label_password.Location = new System.Drawing.Point(24, 252);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(591, 34);
            this.label_password.TabIndex = 6;
            this.label_password.Text = "Пароль:";
            this.label_password.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_password.Location = new System.Drawing.Point(12, 289);
            this.textBox_password.MaxLength = 32;
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(603, 31);
            this.textBox_password.TabIndex = 5;
            this.textBox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // button_right
            // 
            this.button_right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button_right.Enabled = false;
            this.button_right.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_right.ForeColor = System.Drawing.Color.Black;
            this.button_right.Location = new System.Drawing.Point(335, 339);
            this.button_right.Name = "button_right";
            this.button_right.Size = new System.Drawing.Size(280, 50);
            this.button_right.TabIndex = 7;
            this.button_right.Text = "Вхід";
            this.button_right.UseVisualStyleBackColor = false;
            this.button_right.Click += new System.EventHandler(this.button_right_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(151, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(336, 127);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(627, 401);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_right);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_nick);
            this.Controls.Add(this.textBox_nick);
            this.Controls.Add(this.label_main);
            this.Controls.Add(this.button_left);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Підземелля - Авторизація";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LauncherForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_left;
        private System.Windows.Forms.Label label_main;
        private System.Windows.Forms.TextBox textBox_nick;
        private System.Windows.Forms.Label label_nick;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_right;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}