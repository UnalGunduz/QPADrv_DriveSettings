using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIn.Contracts
{
    public interface IControlUnitItem
    {
        string Name { get; }
        List<IDriveItem> Drives { get; }

        void AddDrive(IDriveItem drive);
    }
}
