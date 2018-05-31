using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;

namespace HWSUI
{
	/// <summary>
	/// Summary description for HWSLogin.
	/// </summary>
	public class HWSLogin : System.Windows.Forms.Form
	{
		private Login logObj;
		private System.Windows.Forms.Button submitBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TextBox pwTxtBox;
		private System.Windows.Forms.Label pwLbl;
		private System.Windows.Forms.TextBox userTxtBox;
		private System.Windows.Forms.Label UserLbl;
		private System.Windows.Forms.PictureBox loginPic;
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Testing Constructor for HWSLogin (0 parameters)
		/// </summary>
		public HWSLogin()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HWSLogin));
			this.submitBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.pwTxtBox = new System.Windows.Forms.TextBox();
			this.pwLbl = new System.Windows.Forms.Label();
			this.userTxtBox = new System.Windows.Forms.TextBox();
			this.UserLbl = new System.Windows.Forms.Label();
			this.loginPic = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// submitBtn
			// 
			this.submitBtn.BackColor = System.Drawing.SystemColors.Control;
			this.submitBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.submitBtn.Location = new System.Drawing.Point(112, 160);
			this.submitBtn.Name = "submitBtn";
			this.submitBtn.Size = new System.Drawing.Size(88, 23);
			this.submitBtn.TabIndex = 2;
			this.submitBtn.Text = "OK";
			this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.BackColor = System.Drawing.SystemColors.Control;
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cancelBtn.Location = new System.Drawing.Point(216, 160);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(88, 23);
			this.cancelBtn.TabIndex = 3;
			this.cancelBtn.Text = "Cancel";
			// 
			// pwTxtBox
			// 
			this.pwTxtBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pwTxtBox.Location = new System.Drawing.Point(288, 88);
			this.pwTxtBox.Name = "pwTxtBox";
			this.pwTxtBox.PasswordChar = '*';
			this.pwTxtBox.Size = new System.Drawing.Size(114, 23);
			this.pwTxtBox.TabIndex = 1;
			this.pwTxtBox.Text = "";
			// 
			// pwLbl
			// 
			this.pwLbl.BackColor = System.Drawing.SystemColors.Control;
			this.pwLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pwLbl.Location = new System.Drawing.Point(192, 88);
			this.pwLbl.Name = "pwLbl";
			this.pwLbl.Size = new System.Drawing.Size(88, 24);
			this.pwLbl.TabIndex = 0;
			this.pwLbl.Text = "Password";
			this.pwLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// userTxtBox
			// 
			this.userTxtBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.userTxtBox.Location = new System.Drawing.Point(288, 48);
			this.userTxtBox.Name = "userTxtBox";
			this.userTxtBox.Size = new System.Drawing.Size(112, 23);
			this.userTxtBox.TabIndex = 0;
			this.userTxtBox.Text = "";
			// 
			// UserLbl
			// 
			this.UserLbl.BackColor = System.Drawing.SystemColors.Control;
			this.UserLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UserLbl.Location = new System.Drawing.Point(192, 48);
			this.UserLbl.Name = "UserLbl";
			this.UserLbl.Size = new System.Drawing.Size(88, 23);
			this.UserLbl.TabIndex = 0;
			this.UserLbl.Text = "User Name";
			this.UserLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// loginPic
			// 
			this.loginPic.Image = ((System.Drawing.Image)(resources.GetObject("loginPic.Image")));
			this.loginPic.Location = new System.Drawing.Point(13, 14);
			this.loginPic.Name = "loginPic";
			this.loginPic.Size = new System.Drawing.Size(179, 130);
			this.loginPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.loginPic.TabIndex = 17;
			this.loginPic.TabStop = false;
			// 
			// HWSLogin
			// 
			this.AcceptButton = this.submitBtn;
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(410, 200);
			this.ControlBox = false;
			this.Controls.Add(this.loginPic);
			this.Controls.Add(this.pwTxtBox);
			this.Controls.Add(this.userTxtBox);
			this.Controls.Add(this.pwLbl);
			this.Controls.Add(this.UserLbl);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.submitBtn);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HWSLogin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HWS Login";
			this.ResumeLayout(false);

		}
		#endregion
		
		public Login GetLoginObject()
		{
			return logObj;
		}

		private void submitBtn_Click(object sender, System.EventArgs e)
		{
			string userID = userTxtBox.Text.Trim();
			string password = pwTxtBox.Text.Trim();

			try
			{

				logObj = new Login(userID, password);
				string localCnString = logObj.ConnectionString;
				string etConnString = logObj.ETConnectionString;

				SqlConnection cn = new SqlConnection(localCnString);
				cn.Open();
				cn.Close();
				cn.Dispose();

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Invalid user id or password.  Please re-enter.\n" + ex.Message);
				this.DialogResult = DialogResult.None;
				this.userTxtBox.Focus();
			}
		}

		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

	}
}
