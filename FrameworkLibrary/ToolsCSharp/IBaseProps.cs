using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBDataReader = System.Data.SqlClient.SqlDataReader;

namespace ToolsCSharp
{
    /// <summary>
    /// Summary description for BaseProps.
    /// </summary>
    public interface IBaseProps: ICloneable
    {
        string GetState();

        void SetState(string xml);
        void SetState(DBDataReader dr);
    }
}
