namespace ServerlessMicroservices.Shared.Services
{
    public interface ISettingService
    {
        string GetSiteName();

        // Global
        bool EnableAuth();

        // Storage
        string GetStorageAccount();
        string GetTripManagersQueueName();
        string GetTripMonitorsQueueName();
        string GetTripDemosQueueName();
        string GetTripDriversQueueName();

        // Management    
        bool IsRunningInContainer();
        bool IsEnqueueToOrchestrators();
        bool IsPersistDirectly();
        int GetDriversAcknowledgeMaxWaitPeriodInSeconds();
        double GetDriversLocationRadiusInMiles();
        int GetTripMonitorIntervalInSeconds();
        int GetTripMonitorMaxIterations();

        // App Insights
        string GetApplicationInsightsConnectionString();

        // Cosmos
        string GetDocDbEndpointUri();
        string GetDocDbApiKey();
        string GetDocDbConnectionString();
        string GetDocDbRideShareDatabaseName();
        string GetDocDbMainCollectionName();
        int? GetDocDbThroughput();

        // Sql
        string GetSqlConnectionString();

        // Orchestrators
        string GetStartTripManagerOrchestratorBaseUrl();
        string GetStartTripManagerOrchestratorApiKey();
        string GetStartTripDemoOrchestratorBaseUrl();
        string GetStartTripDemoOrchestratorApiKey();
        string GetTerminateTripManagerOrchestratorBaseUrl();
        string GetTerminateTripManagerOrchestratorApiKey();
        string GetTerminateTripMonitorOrchestratorBaseUrl();
        string GetTerminateTripMonitorOrchestratorApiKey();

        // Event Grid Urls
        string GetTripExternalizationsEventGridTopicUrl();
        string GetTripExternalizationsEventGridTopicApiKey();

        // B2C Settings
        string GetAuthorityUrl();
        string GetApiApplicationId();
        string GetApiScopeName();
        
        // Graph API Settings
        string GetGraphTenantId();
        string GetGraphClientId();
        string GetGraphClientSecret();
    }
}
