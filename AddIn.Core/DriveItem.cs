using AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPADrv_DriveSettings
{
    public class DriveItem : IDriveItem
    {
        public string Name { get; }

        public DriveItem(string name)
        {
            Name = name;
        }
    }
}
