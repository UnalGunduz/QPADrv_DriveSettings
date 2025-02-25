using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QPADrv_DriveSettings
{
    public partial class LogForm: Form
    {
        public LogForm()
        {
            InitializeComponent();
        }
        public void AppendLog(string message)
        {
            if(!string.IsNullOrWhiteSpace(message))
            {
                textBoxLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}" + Environment.NewLine);
            }
        }

    }
}
