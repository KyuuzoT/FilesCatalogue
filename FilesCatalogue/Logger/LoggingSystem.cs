using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesCatalogue.Logger
{
    class LoggingSystem
    {
        private static LoggingSystem _instance;

        private LoggingSystem()
        {
        }

        public LoggingSystem GetLoggingSystemInstance()
        {
            if(_instance.Equals(null))
            {
                _instance = new LoggingSystem();
            }

            return _instance;
        }

        public void CreateLogFile()
        {
        }
    }
}
