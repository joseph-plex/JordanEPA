using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using NLog;

namespace EPA.Functions
{

    public interface IDeepCloneable
    {
        object DeepClone();
    }
    public interface IDeepCloneable<T> : IDeepCloneable
    {
        T DeepClone();
    }


    public class Logger
    {
        //  private static NLog.Logger logger = LogManager.GetCurrentClassLogger();


        public static void Info(string message)
        {
            // TODO   logger.Info(message);
        }

        public static void Warn(string message)
        {
            // TODO   logger.Warn(message);
        }
        public static void Trace(string message)
        {
            // TODO logger.Trace(message);
        }
    }
}
