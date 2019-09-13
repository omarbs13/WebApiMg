using System;
using System.IO;
using System.Reflection;
using MdbApi.Application.Models;
using Microsoft.Extensions.Logging;

namespace MdbApi.Application.Main
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            //LogWrite(message, "Information");
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
            //LogWrite(message, "Warning");
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
           // LogWrite(message, "Error");
        }

        private void LogWrite(string logMessage, string type)
        {
            var m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w, type);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter, string type)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry (" + type + ") : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
