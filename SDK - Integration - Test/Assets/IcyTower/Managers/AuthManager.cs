using UnityEngine;
using UnityEngine.UI;
using Library.FaceBook;
using Library.Authentication.GooglePlay;
using Library.Authentication;
using System;
using UnityEngine.Purchasing;

public class AuthManager : LoadManager
{   
    private PlayfabCustomAuth _customAuth;

    private PlayfabFacebook _facebookAuth;

    private PlayFabGPGS _gpgsAuth;

    private bool authManagerStatus;

    /// <summary>
    /// /////////////////////////// SILINECEK
    /// </summary>

    [Header("Recover PopUpMenu Configuration")]
    public GameObject _recoverPopUpFB;

    public GameObject _recoverPopUpGPGS;

    public Text _recoverPopUpFBText;

    public Text _recoverPopUpGPGSText;

    // Constructor that initializes Action subscriptions.
    public AuthManager( )
    {
        // Subscribe AuthManager Action Events.
        HandleAuthManagerEvents(true);

        // Invoke First AuthManager's Process
        InternetConnectionResult();

    }

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
        if (ControlInternetConnection()) // Internet Connection Succeed
        {
            // Invoke Success Action Event Callback
            EventManager.current.StartInternetConnectionSuccess();
        }

        else { // Internet Connection Failed

            // Invoke Success Action Failed Callback
            EventManager.current.StartInternetConnectionFailed();
        }

    }

    // 1
    private void onStartInternetControlSucceed()
    {
        // Update GameMode State as ONLINEMODE
        loadState = LoadState.ONLINEMODE;
    }

    // 1
    private void onStartInternetControlFailed()
    {
        // Update GameMode State as OFFLINEMODE
        loadState = LoadState.OFFLINEMODE;

        // AuthManager Process Completed...
        authManagerStatus = true;
    }

    /***********************************************************************************************************/

    // 2
    private void onStartFacebookSucceed( )
    {
        
    }

    // 2
    private void onStartFacebookFailed()
    {

    }

    // 2
    private void onStartGooglePlaySucceed()
    {
        throw new NotImplementedException();
    }

    // 2
    private void onStartGooglePlayFailed()
    {
        throw new NotImplementedException();
    }

    // 3
    private void onStartAccountControl()
    {
        throw new NotImplementedException();
    }

    // 4
    private void onStartAutomaticLogin()
    {
        throw new NotImplementedException();
    }


    private void OnDisable()
    {
        HandleAuthManagerEvents(false);
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

            // Subscribe "onStartAccountControlEnter" action
            EventManager.current.onStartAccountControlEnter += onStartAccountControl;

            // Subscribe "onStartAutomaticLoginEnter" action
            EventManager.current.onStartAutomaticLoginEnter += onStartAutomaticLogin;

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

            // UnSubscribe "onStartAccountControlEnter" action
            EventManager.current.onStartAccountControlEnter -= onStartAccountControl;

            // UnSubscribe "onStartAutomaticLoginEnter" action
            EventManager.current.onStartAutomaticLoginEnter -= onStartAutomaticLogin;

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


    private bool StartServices()
    {
        _customAuth = new PlayfabCustomAuth();

        _facebookAuth = new PlayfabFacebook(_recoverPopUpFB);

        _gpgsAuth = new PlayFabGPGS(_recoverPopUpGPGS);

        return false;
    }

    // Define Process Status
    public bool ProcessComplete()
    {
        return authManagerStatus;
    }

    private void Start()
    {
        // LoggedIn before with GPGS
        if (_gpgsAuth.LoggedInBefore())
        {
            /// PlayFabGPGS handles automatically Login with GPGS
            Debug.Log("GPGS login in automatically...");
        }

        // Not LoggedIn before with GPGS
        else
        {
            /// Login as Guest with Unique DeviceID
            _customAuth.AnonymousLogin(false);

            Debug.Log("Mobile Device login in automatically...");
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
