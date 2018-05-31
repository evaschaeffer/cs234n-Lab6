using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsCSharp
{
    /// <summary>
    /// Summary description for IReadDB.
    /// </summary>
    public interface IReadDB
    {
        IBaseProps Retrieve(Object key);
        object RetrieveAll(Type type);
    }
}
