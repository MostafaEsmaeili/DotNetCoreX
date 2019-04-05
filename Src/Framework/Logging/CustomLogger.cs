using NLog;
using NLog.Web;

namespace Framework.Logging
{
    public class CustomLogger 
    {
        private readonly Logger _logger;

        public CustomLogger(string name)
        {
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetLogger(name);

        }
        public void ErrorException(string message,System.Exception exception)
        {
            var logEvent = new LogEventInfo(LogLevel.Error, _logger.Name, message)
            {
                Exception = exception
            };
            _logger.Log(typeof(CustomLogger), logEvent);
        }

        public void Error(string message)
        {
            var logEvent = new LogEventInfo(LogLevel.Error, _logger.Name, message)
            {
                Exception = new System.Exception(message)
            };
            _logger.Log(typeof(CustomLogger), logEvent);
        }
        public void Info(string message)
        {
            var logEvent = new LogEventInfo(LogLevel.Info, _logger.Name, message)
            {
                Exception = new System.Exception(message)
            };
            _logger.Log(typeof(CustomLogger), logEvent);
        }

    }
}