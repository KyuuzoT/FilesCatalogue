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
        private static string _logFolderName = "Log Files";
        private static string _logfilesPath;
        private static string _logfileExtension = ".txt";
        private static string _currentLogName;
        private static long _maxLogSize = 30 * 1024;
        private static LoggingLevel _level = LoggingLevel.Info;

        private LoggingSystem()
        {
        }

        public static LoggingSystem GetLoggingSystemInstance()
        {
            if(_instance.Equals(null))
            {
                _instance = new LoggingSystem();
            }

            return _instance;
        }

        public LoggingLevel SetLevel
        {
            set
            {
                _level = value;
            }
        }

        public void GenerateLog()
        {
            CreateLogFolder();
            CreateLogFile();
        }

        private void CreateLogFile()
        {
            DateTime currentDate = DateTime.Now;
            string logFileNameDate = $"log {currentDate.ToString("yyyy-MM-dd")}";
            string logFileName = logFileNameDate;

            int n = 1;
            while (File.Exists(_logfilesPath + logFileName + _logfileExtension))
            {
                logFileName = logFileName.Substring(0, logFileNameDate.Length);
                logFileName += $"_{n}";
                n++;
            }

            File.Create(_logfilesPath + logFileName + _logfileExtension);
            _currentLogName = _logfilesPath + logFileName + _logfileExtension;
        }

        public string CreateLogFile(string name)
        {
            string result = name;
            string logName = name;
            int n = 1;
            while (File.Exists(_logfilesPath + logName + _logfileExtension))
            {
                logName = logName.Substring(0, logName.Length);
                logName += $"_{n}";
                n++;
            }
            result = _logfilesPath + logName + _logfileExtension;
            _currentLogName = result;
            File.Create(result);
            return result;
        }

        private void CreateLogFolder()
        {
            if(!Directory.Exists(_logFolderName))
            {
                Directory.CreateDirectory(_logFolderName);
            }

            _logfilesPath = Directory.GetParent(_logFolderName).FullName;
        }

        public void AddEntry(string logEntry)
        {
            if(!CheckLogFileLength())
            {
                CreateLogFile();
            }
            using (StreamWriter sw = new StreamWriter(_currentLogName,append:true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy")} at {DateTime.Now.ToString("HH:mm:ss.ff")}: {logEntry}");
            }
        }

        private bool CheckLogFileLength()
        {
            FileInfo log = new FileInfo(_currentLogName);
            long logSize = log.Length;
            if(logSize >= _maxLogSize)
            {
                return false;
            }

            return true;
        }
    }
}
