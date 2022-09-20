// Auth
window.authClientId = '';
window.authAuthority = 'https://ridesharecs.b2clogin.com/tfp/ridesharecs.onmicrosoft.com/B2C_1_Rideshare_Signin/v2.0';
window.authScopes = ['https://ridesharecs.onmicrosoft.com/api/rideshare'];
window.authEnabled = false;

// API endpoints
window.apiKey = 'b435115202f045b097369e72ffb74370';
window.apiBaseUrl = 'https://ridesharecsapim.azure-api.net';
window.apiDriversBaseUrl = `${window.apiBaseUrl}/d`;
window.apiTripsBaseUrl = `${window.apiBaseUrl}/t`;
window.apiPassengersBaseUrl = `${window.apiBaseUrl}/p`;
window.signalrInfoUrl = 'https://ridesharecstrips.azurewebsites.net/api/GetSignalRInfo';