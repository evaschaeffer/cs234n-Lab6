using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsCSharp
{
    /// <summary>
    /// Summary description for IWriteDB.
    /// </summary>
    public interface IWriteDB
    {
        IBaseProps Create(IBaseProps props);
        bool Update(IBaseProps props);
        bool Delete(IBaseProps props);

    }
}
