using AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPADrv_DriveSettings
{
    public class ControlUnitItemS120 : IControlUnitItemS120
    {
        public string Name { get; }
        public List<IDriveItemS120> Drives { get; }

        public ControlUnitItemS120(string name)
        {
            Name = name;
            Drives = new List<IDriveItemS120>();
        }
        public void AddDriveS120(IDriveItemS120 drive)
        {
            if (!Drives.Exists(d => d.Name == drive.Name))
            {
                Drives.Add(drive);
            }
        }
    }
}
