// Auth
window.authClientId = '8ad096fa-beef-4eff-97e6-9aa01a72fd46';
window.signUpSignIn = 'B2C_1_Rideshare_Signin';
window.authAuthority = 'https://ridesharecs.b2clogin.com/ridesharecs.onmicrosoft.com/B2C_1_Rideshare_Signin';
window.loginScopes = ['openid','https://ridesharecs.onmicrosoft.com/api/rideshare'];
window.apiScopes = ['https://ridesharecs.onmicrosoft.com/api/rideshare'];
window.authEnabled = false;
window.knownAuthority = 'ridesharecs.b2clogin.com'
window.redirectUri = window.location.protocol + '//' + window.location.host

// API endpoints
window.apiKey = 'b435115202f045b097369e72ffb74370';
window.apiBaseUrl = 'https://ridesharecsapim.azure-api.net';
window.apiDriversBaseUrl = `${window.apiBaseUrl}/d`;
window.apiTripsBaseUrl = `${window.apiBaseUrl}/t`;
window.apiPassengersBaseUrl = `${window.apiBaseUrl}/p`;