namespace FacebookMarketing_Project_CuoiKy.GUI
{
    partial class FormLoginFacebook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoginFacebook));
            this.btnLogin_FormLoginFacebook = new System.Windows.Forms.Button();
            this.btnCancel_FormLoginFacebook = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername_FormLoginFacebook = new System.Windows.Forms.TextBox();
            this.txtPassword_FromLoginFacebook = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLogin_FormLoginFacebook
            // 
            this.btnLogin_FormLoginFacebook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin_FormLoginFacebook.Location = new System.Drawing.Point(178, 149);
            this.btnLogin_FormLoginFacebook.Margin = new System.Windows.Forms.Padding(5);
            this.btnLogin_FormLoginFacebook.Name = "btnLogin_FormLoginFacebook";
            this.btnLogin_FormLoginFacebook.Size = new System.Drawing.Size(131, 42);
            this.btnLogin_FormLoginFacebook.TabIndex = 0;
            this.btnLogin_FormLoginFacebook.Text = "Login";
            this.btnLogin_FormLoginFacebook.UseVisualStyleBackColor = true;
            this.btnLogin_FormLoginFacebook.Click += new System.EventHandler(this.btnLogin_FormLoginFacebook_Click);
            // 
            // btnCancel_FormLoginFacebook
            // 
            this.btnCancel_FormLoginFacebook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel_FormLoginFacebook.Location = new System.Drawing.Point(370, 149);
            this.btnCancel_FormLoginFacebook.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel_FormLoginFacebook.Name = "btnCancel_FormLoginFacebook";
            this.btnCancel_FormLoginFacebook.Size = new System.Drawing.Size(131, 42);
            this.btnCancel_FormLoginFacebook.TabIndex = 1;
            this.btnCancel_FormLoginFacebook.Text = "Cancel";
            this.btnCancel_FormLoginFacebook.UseVisualStyleBackColor = true;
            this.btnCancel_FormLoginFacebook.Click += new System.EventHandler(this.btnCancel_FormLoginFacebook_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // txtUsername_FormLoginFacebook
            // 
            this.txtUsername_FormLoginFacebook.Location = new System.Drawing.Point(178, 29);
            this.txtUsername_FormLoginFacebook.Margin = new System.Windows.Forms.Padding(5);
            this.txtUsername_FormLoginFacebook.Name = "txtUsername_FormLoginFacebook";
            this.txtUsername_FormLoginFacebook.Size = new System.Drawing.Size(323, 34);
            this.txtUsername_FormLoginFacebook.TabIndex = 4;
            // 
            // txtPassword_FromLoginFacebook
            // 
            this.txtPassword_FromLoginFacebook.Location = new System.Drawing.Point(178, 89);
            this.txtPassword_FromLoginFacebook.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword_FromLoginFacebook.Name = "txtPassword_FromLoginFacebook";
            this.txtPassword_FromLoginFacebook.Size = new System.Drawing.Size(323, 34);
            this.txtPassword_FromLoginFacebook.TabIndex = 5;
            // 
            // FormLoginFacebook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(525, 207);
            this.Controls.Add(this.txtPassword_FromLoginFacebook);
            this.Controls.Add(this.txtUsername_FormLoginFacebook);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel_FormLoginFacebook);
            this.Controls.Add(this.btnLogin_FormLoginFacebook);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormLoginFacebook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormLoginFacebook";
            this.Load += new System.EventHandler(this.FormLoginFacebook_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtPassword_FromLoginFacebook;
        public System.Windows.Forms.Button btnLogin_FormLoginFacebook;
        public System.Windows.Forms.Button btnCancel_FormLoginFacebook;
        public System.Windows.Forms.TextBox txtUsername_FormLoginFacebook;
    }
}