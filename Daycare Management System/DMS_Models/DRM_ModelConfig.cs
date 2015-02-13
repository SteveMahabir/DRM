using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Runtime.Serialization;

namespace DMS_Model
{
    public class DMS_ModelConfig
    {
        /// <summary>
        /// ErrorRoutine Checks - Re-throw or throws execption when an error has occured.
        /// </summary>
        /// <param name="e">Exception Class</param>
        /// <param name="obj">Object that was being passed</param>
        /// <param name="method">Which method threw the exception</param>
        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            // debug to console to get around privilege issues with writing to log file during development

            if (e.InnerException != null)
            {
                Debug.WriteLine("Error in DMS_Model, object=" + obj +
                    ", method=" + method + ", inner exception message=" + e.InnerException, EventLogEntryType.Error);
                throw e.InnerException;
            }
            else
            {
                Debug.WriteLine("Error in DMS_Model, object=" + obj +
                ", method=" + method + " , message=" + e.Message, EventLogEntryType.Error);
                throw e;
            }
        }
        
        /// <summary>
        /// Serializer - changes objects to a byte[]
        /// </summary>
        /// <param name="inObject">Object to be transferred through a data stream</param>
        /// <returns>Byte Array</returns>
        public static byte[] Serializer(Object inObject)
        {
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream();
            frm.Serialize(strm, inObject);
            byte[] ByteArrayObject = strm.ToArray();
            return ByteArrayObject;
        }

        /// <summary>
        /// Deserializes a byte[] into the original object
        /// </summary>
        /// <param name="ByteArrayIn">Serailized Object</param>
        /// <returns>Original Object</returns>
        public static Object Deserializer(byte[] ByteArrayIn)
        {
            BinaryFormatter frm = new BinaryFormatter();
            MemoryStream strm = new MemoryStream(ByteArrayIn);
            Object returnObject = frm.Deserialize(strm);
            return returnObject;
        }

    }
}
