import { PublicClientApplication, LogLevel, InteractionRequiredAuthError } from "@azure/msal-browser";

const msalConfig = {
  auth: {
    clientId: window.authClientId,
    authority: window.authAuthority,
    knownAuthorities: [window.knownAuthority],
    redirectUri: window.redirectUri,
  },
  cache: {
    cacheLocation: "sessionStorage",
    storeAuthStateInCookie: false,
  },
  system: {
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) {
          return;
        }
        switch (level) {
          case LogLevel.Error:
            console.error(message);
            return;
          case LogLevel.Info:
            console.info(message);
            return;
          case LogLevel.Verbose:
            console.debug(message);
            return;
          case LogLevel.Warning:
            console.warn(message);
            return;
        }
      }
    }
  }
};

const myMSALObj = new PublicClientApplication(msalConfig);

const loginRequest = {
  scopes: window.loginScopes
};

const tokenRequest = {
  scopes: window.apiScopes,
  forceRefresh: false
};

let accountId = "";

function setAccount(account) {
  accountId = account.homeAccountId;
}

function selectAccount() {
  const currentAccounts = myMSALObj.getAllAccounts();
  if (currentAccounts.length < 1) {
    return;
  } else if (currentAccounts.length > 1) {
    const accounts = currentAccounts.filter(account =>
      account.homeAccountId.toUpperCase().includes(window.signUpSignIn.toUpperCase())
      &&
      account.idTokenClaims.iss.toUpperCase().includes(window.knownAuthority.toUpperCase())
      &&
      account.idTokenClaims.aud === msalConfig.auth.clientId
    );

    if (accounts.length > 1) {
      if (accounts.every(account => account.localAccountId === accounts[0].localAccountId)) {
        setAccount(accounts[0]);
      } else {
        logout();
      };
    } else if (accounts.length === 1) {
      setAccount(accounts[0]);
    }

  } else if (currentAccounts.length === 1) {
    setAccount(currentAccounts[0]);
  }
}

selectAccount();

function handleResponse(response) {
  if (response !== null) {
    setAccount(response.account);
  } else {
    selectAccount();
  }
}

export function getUser() {
  let user = null;
  if (accountId) {
    user = myMSALObj.getAccountByHomeId(accountId);
  }
  return user;
}

export function signIn() {
  return myMSALObj.loginPopup(loginRequest)
    .then(
      response => {
        handleResponse(response)
        return response.account;
      },
      () => {
        return null;
      }
    ).catch(error => {
      console.log(error);
    });
}

export function signOut() {
  const logoutRequest = {
    postLogoutRedirectUri: msalConfig.auth.redirectUri,
    mainWindowRedirectUri: msalConfig.auth.redirectUri
  };
  myMSALObj.logoutPopup(logoutRequest).then(() => {
    window.location.replace('/');
  });
}

export function getAccessToken(request) {
  tokenRequest.account = myMSALObj.getAccountByHomeId(accountId);
  return myMSALObj.acquireTokenSilent(tokenRequest)
    .then((response) => {
      if (!response.accessToken || response.accessToken === "") {
        throw new InteractionRequiredAuthError;
      }
      return response.accessToken;
    })
    .catch(error => {
      console.log("Silent token acquisition fails. Acquiring token using popup. \n", error);
      if (error instanceof InteractionRequiredAuthError) {
        return myMSALObj.acquireTokenPopup(tokenRequest)
          .then(response => {
            console.log(response);
            return response.accessToken;
          }).catch(error => {
            console.log(error);
          });
      } else {
        console.log(error);
      }
    });
}

export function authenticated() {
  return !!getUser();
}

export function requireAuth(to, from, next) {
  if (!authenticated()) {
    next({
      path: '/no-auth',
      query: { redirect: to.fullPath }
    });
  } else {
    next();
  }
}
