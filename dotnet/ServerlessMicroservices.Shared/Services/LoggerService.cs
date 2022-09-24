using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;

namespace ServerlessMicroservices.Shared.Services
{
    public class LoggerService : ILoggerService
    {
        private ISettingService _settingService;

        public LoggerService(ISettingService setting)
        {
            _settingService = setting;
        }

        public void Log(string message)
        {
            GetAppInsightsClient().TrackTrace(message);
        }

        public void Log(string eventName, Dictionary<string, string> props, Dictionary<string, double> measurements = null)
        {
            GetAppInsightsClient().TrackEvent(eventName, props, measurements);
        }

        public void Log(string metricName, double metricValue)
        {
            GetAppInsightsClient().TrackMetric(metricName, metricValue);
        }

        public void Log(Exception e)
        {
            GetAppInsightsClient().TrackException(e);
        }

        // *** PRIVATE *** //
        private TelemetryClient _appInsightsClient;
        private TelemetryClient GetAppInsightsClient()
        {
            if (_appInsightsClient == null)
            {
                var config = TelemetryConfiguration.CreateDefault();
                config.ConnectionString = _settingService.GetApplicationInsightsConnectionString();
                _appInsightsClient = new TelemetryClient(config);                   
            }

            return _appInsightsClient;
        }
    }
}
