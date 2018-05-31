using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsCSharp
{
    /// <summary>
    /// DBNullUtil provides some static methods for handling DBNull values when working with tables containing
    /// columns with allowable nulls.
    /// Author: David Stelter
    /// </summary>
    /// <remarks>
    /// DataReaders return a System.DBNull value when you select data from a field that has "NULL" as the value
    /// in the database. If you try to cast this as a string, for example, you get an InvalidCastException. This provides
    /// an easy workaround.
    /// </remarks>
    public class DBNullUtil
    {
        #region Scrub Input

        /// <summary>
        /// Returns the input cast as a string. If the input is a System.DBNull or a regular null, returns "".
        /// </summary>
        /// <param name="input">an object fetched from a database.</param>
        /// <returns>The input object cast as a string value, or "" if input value is a null or DBNull.</returns>
        /// <exeption>
        /// Throws an InvalidCastException if the input object is not a System.DBNull and cannot 
        /// be cast as a string.
        /// </exeption>
        public static string ScrubStringFromDB(object input)
        {
            string retString;
            if ((input == System.DBNull.Value) || input == null)
            {
                retString = "";
            }
            else
            {
                retString = (string)input;
            }
            return retString;
        }
        /// <summary>
        /// Returns the input cast as an int. If the input is a System.DBNull, returns null.
        /// </summary>
        /// <param name="input">an object fetched from a database.</param>
        /// <returns>The input object cast as an int, or Int32.MinValue if input value is a DBNull.</returns>
        /// <exeption>
        /// Throws an InvalidCastException if the input object is not a System.DBNull and cannot 
        /// be cast as an int.
        /// </exeption>
        public static int ScrubInt32FromDB(object input)
        {
            int retInt;
            if (input == System.DBNull.Value)
            {
                retInt = Int32.MinValue;
            }
            else
            {
                retInt = (int)input;
            }
            return retInt;
        }
        /// <summary>
        /// Returns the input cast as a double. If the input is a System.DBNull, returns Double.MinValue.
        /// </summary>
        /// <param name="input">an object fetched from a database.</param>
        /// <returns>The input object cast as a double, or Double.MinValue if input value is a DBNull.</returns>
        /// <exeption>
        /// Throws an InvalidCastException if the input object is not a System.DBNull and cannot 
        /// be cast as a double.
        /// </exeption>
        public static double ScrubDoubleFromDB(object input)
        {
            double retDouble;
            if (input == System.DBNull.Value)
            {
                retDouble = Double.MinValue;
            }
            else
            {
                retDouble = (double)input;
            }
            return retDouble;
        }

        /// <summary>
        /// Returns the input cast as a long. If the input is a System.DBNull, returns Long.MinValue.
        /// </summary>
        /// <param name="input">an object fetched from a database.</param>
        /// <returns>The input object cast as a long, or Long.MinValue if input value is a DBNull.</returns>
        /// <exeption>
        /// Throws an InvalidCastException if the input object is not a System.DBNull and cannot 
        /// be cast as a long.
        /// </exeption>
        public static long ScrubInt64FromDB(object input)
        {
            long retLong;
            if (input == System.DBNull.Value)
            {
                retLong = Int64.MinValue;
            }
            else
            {
                retLong = (long)input;
            }
            return retLong;
        }

        /// <summary>
        /// Returns the input cast as a boolean false. 
        /// If the input is a System.DBNull, returns false.
        /// </summary>
        /// <param name="input">an object fetched from a database.</param>
        /// <returns>The input object cast as a boolean, or false if input value is a DBNull.</returns>
        /// <exeption>
        /// Throws an InvalidCastException if the input object is not a System.DBNull and cannot 
        /// be cast as a long.
        /// </exeption>
        public static bool ScrubBooleanFromDB(object input)
        {
            bool retBoolean;
            if (input == System.DBNull.Value)
            {
                retBoolean = false;
            }
            else
            {
                retBoolean = (bool)input;
            }
            return retBoolean;
        }

        public static DateTime ScrubDateTimeFromDB(object input)
        {
            DateTime ret;
            if (input == System.DBNull.Value)
            {
                ret = DateTime.MinValue;
            }
            else
            {
                ret = (DateTime)input;
            }
            return ret;
        }
        #endregion Scrub Input

        #region Scrub Output
        /// <summary>
        /// Returns the input as a System.Object.  If the input is null or is equal to "",
        /// a System.DBNull is returned.
        /// </summary>
        /// <param name="input">a string input value to be "scrubbed"</param>
        /// <returns>a "scrubbed" object</returns>
        public static object ScrubStringToDB(string input)
        {
            object retObj;
            if ((input == null) || (input == ""))
            {
                retObj = System.DBNull.Value;
            }
            else
            {
                retObj = input;
            }
            return retObj;
        }
        /// <summary>
        /// Returns the input as a System.Object.  If the input is a System.Int32.MinValue,
        /// a System.DBNull is returned.
        /// </summary>
        /// <param name="input">an int input value to be "scrubbed"</param>
        /// <returns>a "scrubbed" object</returns>
        public static object ScrubInt32ToDB(int input)
        {
            object retObj;
            if (input == System.Int32.MinValue)
            {
                retObj = System.DBNull.Value;
            }
            else
            {
                retObj = input;
            }
            return retObj;
        }
        /// <summary>
        /// Returns the input as a System.Object.  If the input is System.Double.MinValue,
        /// a System.DBNull is returned.
        /// </summary>
        /// <param name="input">a double input value to be "scrubbed"</param>
        /// <returns>a "scrubbed" object</returns>
        public static object ScrubDoubleToDB(double input)
        {
            object retObj;
            if ((input == System.Double.MinValue))
            {
                retObj = System.DBNull.Value;
            }
            else
            {
                retObj = input;
            }
            return retObj;
        }

        /// <summary>
        /// Returns the input as a System.Object.  If the input is System.Long.MinValue,
        /// a System.DBNull is returned.
        /// </summary>
        /// <param name="input">a long input value to be "scrubbed"</param>
        /// <returns>a "scrubbed" object</returns>
        public static object ScrubInt64ToDB(long input)
        {
            object retObj;
            if ((input == System.Int64.MinValue))
            {
                retObj = System.DBNull.Value;
            }
            else
            {
                retObj = input;
            }
            return retObj;
        }

        public static object ScrubDateTimeToDB(DateTime input)
        {
            object retObj;
            if ((input == System.DateTime.MinValue))
            {
                retObj = System.DBNull.Value;
            }
            else
            {
                retObj = input;
            }
            return retObj;
        }
        #endregion Scrub Output
    }
}
