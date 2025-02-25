using AddIn.Contracts;
using AddIn.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QPADrv_DriveSettings
{
    public class AddInController : IAddInController
    {

        public LogForm logForm;
        private readonly List<IControlUnitItem> _controlUnits;

        public AddInController()
        {
            _controlUnits = new List<IControlUnitItem>();
            logForm = new LogForm();
        }
        public void ShowLogForm()
        {
            if (logForm.IsDisposed)
            {
                logForm = new LogForm();
                WriteLog("Yeniden olusturuldu");
            }
            logForm.Show();
            logForm.BringToFront();
            WriteLog("Logform gosterildi");
        }

        public void WriteLog(string message)
        {
            try
            {
                if (logForm.InvokeRequired)
                {
                    logForm.Invoke(new Action(() => logForm.AppendLog(message)));
                }
                else
                {
                    logForm.AppendLog(message);
                }
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("Log form kapatıldıktan sonra log yazılamaz. Lütfen formu tekrar açın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Log yazılırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<IControlUnitItem> GetControlUnits()
        {
            return _controlUnits;
        }

        public void AddControlUnit(string controlUnitName)
        {
            if (!_controlUnits.Any(cu => cu.Name == controlUnitName))
            {
                _controlUnits.Add(new ControlUnitItem(controlUnitName));
            }
        }
        public void AddDriveToControlUnit(string controlUnitName, string driveName)
        {
            var controlUnit = _controlUnits.FirstOrDefault(cu => cu.Name == controlUnitName);
            if (controlUnit != null && !controlUnit.Drives.Any(d => d.Name == driveName))
            {
                controlUnit.AddDrive(new DriveItem(driveName));
            }
        }
        public void HandleSelectedDrive(IDriveItem drive)
        {
            //WriteLog($"Seçilen Drive: {drive.Name}");
        }
        public void WriteLogText()
        {
            ButtonTest_1();
        }

    }
}
