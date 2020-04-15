using UnityEngine;
using UnityEngine.UI;
using Library.FaceBook;
using Library.Authentication.GooglePlay;
using Library.Authentication;
using System;

public class AuthManager : LoadManager
{   
    private PlayfabCustomAuth _customAuth;

    private PlayfabFacebook _facebookAuth;

    private PlayFabGPGS _gpgsAuth;

    // That's total Status
    private bool _authManagerStatus;

    public bool _startService;

    /// <summary>
    /// /////////////////////////// SILINECEK
    /// </summary>

    [Header("Recover PopUpMenu Configuration")]
    public GameObject _recoverPopUpFB;

    public GameObject _recoverPopUpGPGS;

    public Text _recoverPopUpFBText;

    public Text _recoverPopUpGPGSText;

    // Override Start function
    private void Start() { }

    // Constructor that initializes Action subscriptions.
    public void StartProcess( )
    {
        // Store service status
        _startService = true;

        // Subscribe AuthManager Action Events.
        HandleAuthManagerEvents(true);

        // Invoke First AuthManager's Process
        InternetConnectionResult();

    }

    #region FIRST CONTROL BLOCK

    /***********************************************************************************************************/
    // INTERNET CONNECTION WORKSPACE

    // Check Internet Connection Status
    private bool ControlInternetConnection()
    {
        return Internet.ConnectionTest() ;
    }

    // Receive Internet Connection Result
    private void InternetConnectionResult()
    {
        Debug.Log("[0] Control Internet Connection...");

        if (ControlInternetConnection()) // Internet Connection Succeed
        {
            // Invoke Event Action Succeed Callback
            EventManager.current.StartInternetConnectionSuccess();
        }

        else { // Internet Connection Failed

            // Invoke Event Action Failed Callback
            EventManager.current.StartInternetConnectionFailed();
        }

    }

    // 1
    private void onStartInternetControlSucceed()
    {
        // Update GameMode State as ONLINEMODE
        loadState = LoadState.ONLINEMODE;

        Debug.Log("[1] Internet Connection succeed.");

        // INITIALIZE SECOND CONTROL BLOCK
        // Invock StartServices that initializes Facebook, Googleplay services before post authentication request
        StartServices();
    }

    // 1
    private void onStartInternetControlFailed()
    {
        // Update GameMode State as OFFLINEMODE
        loadState = LoadState.OFFLINEMODE;

        // AuthManager Process Completed...
        _authManagerStatus = true;

        Debug.LogError("[1] Internet Connection failed.");

        Debug.Log("[2] Preparing Offline Game Mode...");
    }

    /***********************************************************************************************************/

    #endregion

    /***********************************************************************************************************/
    // AUTHENTICATION WORKSPACE - SECOND CONTROL BLOCK

    // INITIALIZE SERVICES

    // Facebook Service Status
    private bool _facebookReadyFlag;

    // Google Play Service Status
    private bool _googlePlayReadyFlag;

    // Make ready Services such as GooglePlay and Facebook
    private void StartServices()
    {
        _customAuth = new PlayfabCustomAuth();

        // Initialize Facebook Services
        _facebookAuth = new PlayfabFacebook(_recoverPopUpFB);

    }

    // 2.1
    private void onStartFacebookSucceed()
    {
        Debug.Log("[2] Initialized Facebook Services Succeed.");

        // Turn Facebook flag to true
        _facebookReadyFlag = true;

        // Facebook Initializion succeed.
        // Initialize Google Play Services
        _gpgsAuth = new PlayFabGPGS(_recoverPopUpGPGS);

        // Invoke GooglePlay Inıtialization Succeed
        EventManager.current.StartGooglePlayInitializionSucceed();

    }

    // 2.1
    private void onStartFacebookFailed()
    {
        Debug.LogError("[2] Initialized Facebook Services Failed.");

        // EXIT GAME
        Debug.LogError("[3] Unexpected Issue. Please Try Again Later");

        // Stop Loading Process for all control workspace
        stopCycle = true;

    }

    // 2.2
    private void onStartGooglePlaySucceed()
    {
        Debug.Log("[3] Initialized Google Play Services Succeed.");

        // Turn Google Play flag to true
        _googlePlayReadyFlag = true;

        // Google Play Initializion succeed.
        // Everything is ready for Authentication.
        StartAuthentication();
    }

    // 2.2
    private void onStartGooglePlayFailed()
    {
        Debug.LogError("[3] Initialized Google Play Services Failed.");

        // EXIT GAME
        Debug.LogError("[4] Unexpected Issue. Please Try Again Later");

        // Stop Loading Process for all control workspace
        stopCycle = true;
    }


    private void StartAuthentication()
    {
        // LoggedIn before with GPGS
        if (_gpgsAuth.LoggedInBefore())
        {
            Debug.Log("[4] A Registered Account Was Found.");

            // Login Google Acc. automaticly
            _gpgsAuth.LoginPlayGameService(false); 

        }

        // Not LoggedIn before with GPGS
        else
        {
            if (PlayerPrefs.HasKey("DISPLAYNAME_GUEST"))
            {
                Debug.Log("[4] A Registered Guest Account Was Found.");
            }

            else
            {
                Debug.Log("[4] Preparing The Game for First Time Launch.");
            }

            /// Login as Guest with Unique DeviceID
            _customAuth.AnonymousLogin(false);

        }

    }

    // 3.1
    private void onStartGuestLoginSucceedEnter()
    {
        if (PlayerPrefs.HasKey("DISPLAYNAME_GUEST"))
        {
            Debug.Log("[5] Login as " + PlayerPrefs.GetString("DISPLAYNAME_GUEST"));
        }

        else
        {
            Debug.Log("[5] Guest Account Succesfully Created.");
        }

        // AuthManager Process Completed...
        _authManagerStatus = true;
    }

    // 3.2
    private void onStartGuestLoginFailedEnter()
    {
        Debug.LogError("[5] Guest Account Creation Failed.");

        // EXIT GAME
        Debug.LogError("[6] Unexpected Issue. Please Try Again Later");

        // Stop Loading Process for all control workspace
        stopCycle = true;
    }

    // 4
    private void onGooglePlayLoginSucceedEnter()
    {
        Debug.Log("[5] Login Google Play Succeed.");

        // AuthManager Process Completed...
        _authManagerStatus = true;
    }


    private void onGooglePlayLoginFailedEnter()
    {
        Debug.LogError("[5] Google Play Login Failed.");

        // EXIT GAME
        Debug.LogError("[6] Unexpected Issue. Please Try Again Later");

        // Stop Loading Process for all control workspace
        stopCycle = true;
    }

    /***********************************************************************************************************/

    // Unsubcribe all action events on AuthManager
    public void OnDisable()
    {
        HandleAuthManagerEvents(false);
    }


    // Define Process Status
    public bool ProcessComplete()
    {
        return _authManagerStatus;
    }


    // Action event Handler
    private void HandleAuthManagerEvents(bool Active)
    {
        if (Active)
        {
            // Subscribe "onStartFacebookInitializionSucceedEnter" action
            EventManager.current.onStartFacebookInitializionSucceedEnter += onStartFacebookSucceed;

            // Subscribe "onStartFacebookInitializionFailedEnter" action
            EventManager.current.onStartFacebookInitializionFailedEnter += onStartFacebookFailed;

            // Subscribe "onStartGuestLoginSucceedEnter" action
            EventManager.current.onStartGuestLoginSucceedEnter += onStartGuestLoginSucceedEnter;

            // Subscribe "onStartGuestLoginFailedEnter" action
            EventManager.current.onStartGuestLoginFailedEnter += onStartGuestLoginFailedEnter;

            // Subscribe "onStartGooglePlayLoginSucceedEnter" action
            EventManager.current.onStartGooglePlayLoginSucceedEnter += onGooglePlayLoginSucceedEnter;

            // Subscribe "onStartGooglePlayLoginFailedEnter" action
            EventManager.current.onStartGooglePlayLoginFailedEnter += onGooglePlayLoginFailedEnter;

            // Subscribe "onStartGooglePlayInitializionSucceedEnter" action
            EventManager.current.onStartGooglePlayInitializionSucceedEnter += onStartGooglePlaySucceed;

            // Subscribe "onStartGooglePlayInitializionFailedEnter" action
            EventManager.current.onStartGooglePlayInitializionFailedEnter += onStartGooglePlayFailed;

            // Subscribe "onStartInternetConnectionSucceedEnter" action
            EventManager.current.onStartInternetConnectionSucceedEnter += onStartInternetControlSucceed;

            // Subscribe "onStartInternetConnectionFailedEnter" action
            EventManager.current.onStartInternetConnectionFailedEnter += onStartInternetControlFailed;
        }

        else
        {
            // UnSubscribe "onStartFacebookInitializionSucceedEnter" action
            EventManager.current.onStartFacebookInitializionSucceedEnter -= onStartFacebookSucceed;

            // UnSubscribe "onStartFacebookInitializionFailedEnter" action
            EventManager.current.onStartFacebookInitializionFailedEnter -= onStartFacebookFailed;

            // UnSubscribe "onStartGuestLoginSucceedEnter" action
            EventManager.current.onStartGuestLoginSucceedEnter -= onStartGuestLoginSucceedEnter;

            // UnSubscribe "onStartGuestLoginFailedEnter" action
            EventManager.current.onStartGuestLoginFailedEnter -= onStartGuestLoginFailedEnter;

            // UnSubscribe "onStartGooglePlayLoginSucceedEnter" action
            EventManager.current.onStartGooglePlayLoginSucceedEnter -= onGooglePlayLoginSucceedEnter;

            // UnSubscribe "onStartGooglePlayLoginFailedEnter" action
            EventManager.current.onStartGooglePlayLoginFailedEnter -= onGooglePlayLoginFailedEnter;

            // UnSubscribe "onStartGooglePlayInitializionSucceedEnter" action
            EventManager.current.onStartGooglePlayInitializionSucceedEnter -= onStartGooglePlaySucceed;

            // UnSubscribe "onStartGooglePlayInitializionFailedEnter" action
            EventManager.current.onStartGooglePlayInitializionFailedEnter -= onStartGooglePlayFailed;

            // UnSubscribe "onStartInternetConnectionSucceedEnter" action
            EventManager.current.onStartInternetConnectionSucceedEnter -= onStartInternetControlSucceed;

            // UnSubscribe "onStartInternetConnectionFailedEnter" action
            EventManager.current.onStartInternetConnectionFailedEnter -= onStartInternetControlFailed;

        }
    }

   





































    public void ConnectGPGS()
    {
        if (_gpgsAuth.GetLoggedIn()) // Loggedin with GPGS
        {
            // UnLink GPGS Acc.
            _gpgsAuth.UnLinkWithGooglePlayAccount();
        }

        else // Not Loggedin with GPGS, Connect with it.
        {
            // Link GPGS Acc.
            _gpgsAuth.LoginPlayGameService(true);
        }
    }

    public void ConnectFacebook()
    {
        if (_facebookAuth.GetLoggedIn()) // Loggedin with Facebook
        {
            // UnLink Facebook Acc.
            _facebookAuth.UnLinkWithFacebook();
        }

        else
        {
            // Link Facebook Acc.
            _facebookAuth.AuthLogin(true);
        }
    }

    public void RecoverAccountWithFacebook()
    {
        _facebookAuth.RecoverAccount();
    }

    public void DontRecoverAccountWithFacebook()
    {
        _facebookAuth.DontRecoverAccount();
    }

    public void RecoverAccountWithGPGS()
    {
        _gpgsAuth.RecoverAccount();
    }

    public void DontRecoverAccountWithGPGS()
    {
        _gpgsAuth.DontRecoverAccount();
    }


    public void GetRecoverWithGPGSText()
    {
        _recoverPopUpGPGSText.text = _gpgsAuth.GetRecoverPopUpText();
    }
}
