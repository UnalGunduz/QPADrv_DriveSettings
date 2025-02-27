using Siemens.Engineering.HW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIn.Contracts
{
    public interface IDriveItemS120
    {
        string Name { get; }
        DeviceItem DeviceItem { get; }
    }
}
