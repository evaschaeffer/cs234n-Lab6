using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
            	MDIMain frame;
				FormLogin logDialog = new FormLogin();
				DialogResult res = logDialog.ShowDialog();
				if (res == DialogResult.OK)
				{
                    UserConfiguration settings = logDialog.Settings;
					logDialog.Dispose();
					frame = new MDIMain(settings);
					Application.Run(frame);
				}
				else
					logDialog.Dispose();
			}

			catch (Exception ex)
			{
				MessageBox.Show( "Error: " + ex.Message,  "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
        }
    }
}
