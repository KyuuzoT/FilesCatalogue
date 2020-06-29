using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesCatalogue.Logger
{
    class LoggerEntry
    {
        private LoggerEntry _entryInstance;
        private LoggingLevel _currentLevel;

        private LoggerEntry()
        {
        }

        protected LoggerEntry GetLoggerEntryInstance(LoggingLevel level)
        {
            if(_entryInstance.Equals(null))
            {
                _entryInstance = new LoggerEntry();
            }

            _currentLevel = level;

            return _entryInstance;
        }

        public void AddEntry(object entry)
        {
            switch (_currentLevel)
            {
                case LoggingLevel.Debug:
                    CreateFullExceptionMessage(entry);
                    break;
                case LoggingLevel.Info:
                    break;
                case LoggingLevel.Error:
                    break;
                default:
                    break;
            }
        }

        private object ReturnMessageType<T>(T entry)
        {
            switch (entry)
            {
                case Exception ex:
                    return ex;
                case string str:
                    return str;
                default:
                    return entry as string;
            }
        }


        private string CreateFullExceptionMessage<T>(T message)
        {
            var exception = message as Exception;
            StringBuilder sb = new StringBuilder();
            sb.Append("Error! Exception occured in: ")
                .Append(exception.Source)
                .Append("\nSTACK TRACE:\n")
                .Append(exception.StackTrace);
            return sb.ToString();
        }
    }
}
