using AddIn.Contracts;
using AddIn.UI;
using Siemens.Engineering.HW;
using Siemens.Engineering.MC.Drives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SDRhelper.StartdriveHelper;

namespace QPADrv_DriveSettings
{
    public class AddInController : IAddInController
    {

        DriveObject myDriveObject;
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

        public void GetActDeviceItem(IEnumerable<DeviceItem> selection)
        {

            //change parameters for each selected drive in TIA Portal
            foreach (DeviceItem actDeviceItem in selection)
            {
                /*
                 * get the SINAMICS DriveObject 
                 * S120, S120 Integrated, G120, G115D, G110M
                 */
                try
                {
                    myDriveObject =
                    actDeviceItem.GetService<DriveObjectContainer>().DriveObjects[0];
                }
                /*
                 * get the SINAMICS DriveObject 
                 * S210
                 */
                catch
                {
                    Device drive_unit =
                        (Device)actDeviceItem.Parent;

                    if (drive_unit.TypeIdentifier.ToString().
                        Contains("S210"))
                    {
                        foreach (DeviceItem deviceItems
                            in drive_unit.DeviceItems)
                        {
                            if (deviceItems.Classification ==
                                DeviceItemClassifications.None)
                            {
                                myDriveObject =
                                deviceItems.GetService<DriveObjectContainer>().DriveObjects[0];
                            }
                        }
                    }
                    //no SINAMICS drive found
                    else
                    {
                        myDriveObject = null;
                    }

                }
                //Start Code to adjust SINAMICS drive parameters 
                if (myDriveObject != null)
                {
                    ListDrive(myDriveObject);
                }
            }
        }
            //DeviceItem test =
            //       (DeviceItem)myDriveObject.Parent;
            //_logText = test.Name.ToString();
        public void ListDrive(DriveObject actDriveObject)
        {
            /*
             * the Drive Object of the Control Unit of 
             * the selected drive axis in TIA
             */
            DriveObject myControlUnit = null;

            /*
             * the Drive Object of any other axis in 
             * the same device
             */
            //DriveObject DriveAxis1 = null;

            /*
             * the Drive Object of the Infeed of 
             * the selected drive axis in TIA
             */
            //DriveObject myInfeed = null;
            //get the device unit
            DeviceItem actDeviceItem =
                   (DeviceItem)actDriveObject.Parent.Parent;

            String nameOfactDeviceItem = actDeviceItem.Name.ToString();
            String nameOfCU = null;

            //In case of S120 devices
            if (actDeviceItem.TypeIdentifier == "System:Rack")
            {
                //get Control Unit Drive Object
                myControlUnit = GetControlUnit(actDriveObject);
                nameOfCU = actDriveObject.ToString();

                AddControlUnit(nameOfCU);
                AddDriveToControlUnit(nameOfCU, nameOfactDeviceItem);

                //get Infeed Drive Object
                //myInfeed = GetInfeedAxis(actDriveObject);

                /*
                 * to access any other DriveAxis, replace the string
                 * by the name of the other drive axis 
                 */
                //DriveAxis1 = GetDriveAxisByName(actDriveObject,
                //    "Other_Drive_axis_name");
            }
            //controller.AddControlUnit("Test Control Unit");
            //_controller.AddControlUnit("Test Control Unit_1");
            //_controller.AddDriveToControlUnit("Test Control Unit_1", "Test Drive_1");
            //_controller.AddDriveToControlUnit("Test Control Unit_1", "Test Drive_2");
            ////controller.AddDrive("Test Drive_2");

        }
        public void ButtonTest_1()
        {
            DriveObject myControlUnit = null;
            DeviceItem actDeviceItem =
                   (DeviceItem)myDriveObject.Parent.Parent;
            String nameOfactDeviceItem = actDeviceItem.Name.ToString();
            String nameOfCU = null;

            WriteLog("Button basildi");
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
