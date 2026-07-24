namespace Projet_Fin_Formation
{
    partial class Form_Login
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.R = new System.Windows.Forms.ComboBox();
            this.nu = new System.Windows.Forms.TextBox();
            this.mp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.Location = new System.Drawing.Point(51, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Valider";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Crimson;
            this.button2.Location = new System.Drawing.Point(280, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "Fermer";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // R
            // 
            this.R.FormattingEnabled = true;
            this.R.Location = new System.Drawing.Point(175, 68);
            this.R.Name = "R";
            this.R.Size = new System.Drawing.Size(140, 21);
            this.R.TabIndex = 2;
            this.R.SelectedIndexChanged += new System.EventHandler(this.R_SelectedIndexChanged);
            // 
            // nu
            // 
            this.nu.Location = new System.Drawing.Point(147, 119);
            this.nu.Name = "nu";
            this.nu.Size = new System.Drawing.Size(201, 20);
            this.nu.TabIndex = 3;
            // 
            // mp
            // 
            this.mp.Location = new System.Drawing.Point(147, 168);
            this.mp.Name = "mp";
            this.mp.Size = new System.Drawing.Size(201, 20);
            this.mp.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Role de L\'utilisater : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nom_utilisater : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mots de passe :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(237, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Encadreur : Professeur Mustafa Laghzil";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(237, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Realisateur : Amine Jebbouj";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(172, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Projet : Gestion de Stock";
            // 
            // Form_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(461, 336);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mp);
            this.Controls.Add(this.nu);
            this.Controls.Add(this.R);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form_Login";
            this.Text = "Form_Login";
            this.Load += new System.EventHandler(this.Form_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox R;
        private System.Windows.Forms.TextBox nu;
        private System.Windows.Forms.TextBox mp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}