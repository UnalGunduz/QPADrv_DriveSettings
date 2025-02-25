using AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPADrv_DriveSettings
{
    public class ControlUnitItem : IControlUnitItem
    {
        public string Name { get; }
        public List<IDriveItem> Drives { get; }

        public ControlUnitItem(string name)
        {
            Name = name;
            Drives = new List<IDriveItem>();
        }
        public void AddDrive(IDriveItem drive)
        {
            if (!Drives.Exists(d => d.Name == drive.Name))
            {
                Drives.Add(drive);
            }
        }
    }
}
