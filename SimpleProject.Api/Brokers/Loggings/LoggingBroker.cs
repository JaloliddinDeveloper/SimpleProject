//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

namespace SimpleProject.Api.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;

        public LoggingBroker(ILogger logger)
        {
            this.logger = logger;
        }
        public void LogError(Exception exception)
        {
            this.logger.LogError(exception,message :exception.Message);
        }

        public void LogCritical(Exception exception)
        {
            this.logger.LogCritical(exception,message :exception.Message);
        }
    }
}
