using AddIn.Contracts;
using AddIn.UI;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.MC.Drives;
using Siemens.Engineering.SW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;
using static SDRhelper.StartdriveHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QPADrv_DriveSettings
{
    public class AddInController : IAddInController
    {

        DriveObject myDriveObject;
        public LogForm logForm;
        public string cuNameS120;
        public string cuNameG120;
        private readonly List<IControlUnitItemS120> _controlUnitsS120;
        private readonly List<IDriveItemG120> _driveItemG120;

        public AddInController()
        {
            _controlUnitsS120 = new List<IControlUnitItemS120>();
            _driveItemG120 = new List<IDriveItemG120>();
            logForm = new LogForm();
        }

        #region LogForm
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
        #endregion

        public List<IControlUnitItemS120> GetControlUnitsS120()
        {
            return _controlUnitsS120;
        }
        public List<IDriveItemG120> GetDriveItemG120()
        {
            return _driveItemG120;
        }

        public void AddControlUnitS120(string controlUnitName)
        {
            if (!_controlUnitsS120.Any(cu => cu.Name == controlUnitName))
            {
                _controlUnitsS120.Add(new ControlUnitItemS120(controlUnitName));
            }
        }
        public void AddDriveToControlUnitS120(string controlUnitName, string driveName, DeviceItem deviceItem)
        {
            var controlUnit = _controlUnitsS120.FirstOrDefault(cu => cu.Name == controlUnitName);
            if (controlUnit != null && !controlUnit.Drives.Any(d => d.Name == driveName))
            {
                controlUnit.AddDriveS120(new DriveItemS120(driveName, deviceItem));
            }
        }
        public void AddDriveG120(string driveName, DeviceItem deviceItem)
        {
            if (!_driveItemG120.Any(cu => cu.Name == driveName))
            {
                _driveItemG120.Add(new DriveItemG120(driveName, deviceItem));
            }
        }

        public void HandleSelectedDrive(IDriveItemS120 drive)
        {
            WriteLog($"Seçilen Drive: {drive.Name}");
        }
        public string ReadParameter(DeviceItem deviceItem)
        {

            DriveObject myDriveObject = null;

            myDriveObject = deviceItem.GetService<DriveObjectContainer>().DriveObjects[0];

            return $"Parameter : {deviceItem.Name} => {ReadParameterValue(myDriveObject, "205")}";
        }

        public void ProjectAddIn(TiaPortal tiaportal)
        {

            var project = tiaportal.Projects[0]; // İlk projeyi al
            WriteLog($"TIA Portal Projesi: {project.Name}");

            // TIA Portal içindeki tüm cihazları listele
            foreach (Device device in project.Devices)
            {
                WriteLog($"Device: {device.Name} + {device.TypeIdentifier}");
            }
        }

        public void SelectedDeviceAddIn(IEnumerable<DeviceItem> selection)
        {

            //change parameters for each selected drive in TIA Portal
            foreach (DeviceItem selectedDeviceItem in selection)
            {
                // Get selected device item to get the device
                // Check if the selected device is a G120 or S120 drive
                try
                {
                    Device device = (Device)selectedDeviceItem.Parent;

                    WriteLog($"SelectedDeviceItem: {selectedDeviceItem.Name} => {selectedDeviceItem.TypeIdentifier}");
                    WriteLog($"Device: {device.Name} => {device.TypeIdentifier.ToString()}");

                    // If the device is a S120 drive
                    if (device.TypeIdentifier == "System:Device.S120")
                    {
                        cuNameS120 = selectedDeviceItem.Name;
                        AddControlUnitS120(cuNameS120);

                        WriteLog($"Control Unit: {cuNameS120} => {selectedDeviceItem.TypeIdentifier}");

                        // Find each drive unit under the control unit
                        foreach (DeviceItem subDeviceItem in device.DeviceItems)
                        {
                            WriteLog($"subDeviceItem.TypeIdentifier: {cuNameS120} => {subDeviceItem.TypeIdentifier}");
                            if (subDeviceItem.TypeIdentifier.Contains("System:Rack"))
                            {
                                AddDriveToControlUnitS120(cuNameS120, subDeviceItem.Name, subDeviceItem);

                                WriteLog($"📌 Drive Unit: {subDeviceItem.Name} + {subDeviceItem.TypeIdentifier}");
                            }
                        }
                    }

                    // if the device is a G120 drive
                    if (device.TypeIdentifier == "System:Device.G120-2")
                    {

                        AddDriveG120(selectedDeviceItem.Name, selectedDeviceItem);
                        WriteLog($"Control Unit G120: {selectedDeviceItem.Name} => {selectedDeviceItem.TypeIdentifier}");

                        //// Find each drive unit under the control unit
                        //foreach (DeviceItem subDeviceItem in device.DeviceItems)
                        //{
                        //    if (subDeviceItem.TypeIdentifier == "System:Rack")
                        //    {
                        //        AddDriveG120(subDeviceItem.Name);

                        //        WriteLog($"📌 Drive Unit G120: {subDeviceItem.Name} + {subDeviceItem.TypeIdentifier}");
                        //    }
                        //}
                    }


                }




                catch (Exception ex)
                {
                    WriteLog($"DriveObject alınamadı: {ex.Message}");
                }
                //Start Code to adjust SINAMICS drive parameters 
                //if (myDriveObject != null)
                //{
                //    ListDrive(myDriveObject);
                //}
            }
        }
    }
}
