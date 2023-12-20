using System;
using System.Collections.Generic;
using Thresh.Core.Interface;

namespace Thresh.Unity.Global
{
    public enum LogLevel
    {
        None = 0,
        Trace = 1,
        Debug = 2,
        Info = 3,
        Warn = 4,
        Error = 5,
        Fatal = 6,
    }
    
    public class LogAssert
    {
        private static Dictionary<string, ILog> _Loggers = new Dictionary<string, ILog>();

        public static LogLevel Level = LogLevel.None;

        private static ILog _UtilLog = null;

        public static ILog Util
        {
            get
            {
                if (_UtilLog == null)
                {
                    _UtilLog = GetLog("Util");
                }

                return _UtilLog;
            }
        }

        public static ILog GetLog(string name)
        {
            ILog log = null;

            if (!_Loggers.TryGetValue(name, out log))
            {
                log = new UnityLog(name);
                _Loggers.Add(name, log);
            }

            return log;
        }
    }

    public class UnityLog : ILog
    {
        public string Name { get; }
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception ex, string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}