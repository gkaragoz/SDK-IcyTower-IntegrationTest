using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region SINGLETON

    public static EventManager current;

    private void Awake()
    {
        if (current == null) { current = this; }

        else if (current != this) { Destroy(gameObject); }         
    }

    #endregion

    #region GAME LAUNCH ACTIONS

    //1
    #region AUTHENTICATION STATE

    // Called when internet connection completed succesfully.
    public event Action onStartInternetConnectionSucceedEnter;

    // Called when internet connection completed with error.
    public event Action onStartInternetConnectionFailedEnter;

    // Called when facebook service initialized succesfully.
    public event Action onStartFacebookInitializionSucceedEnter;

    // Called when facebook service initialized with error.
    public event Action onStartFacebookInitializionFailedEnter;

    // Called when google play service initialized succesfully
    public event Action onStartGooglePlayInitializionSucceedEnter;

    // Called when google play service initialized with error.
    public event Action onStartGooglePlayInitializionFailedEnter;

    // Called when existing account controls completed.
    public event Action onStartAccountControlEnter;

    // Called when automatic login completed.
    public event Action onStartAutomaticLoginEnter;


    // Invoke when subscribed "onStartFacebookInitializionSucceedEnter" action
    public void StartFacebookInitializionSucceed( )
    {
        onStartFacebookInitializionSucceedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartFacebookInitializionFailedEnter" action
    public void StartFacebookInitializionFailed()
    {
        onStartFacebookInitializionFailedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartInternetConnectionSuccessEnter" action
    public void StartInternetConnectionSuccess()
    {
        onStartInternetConnectionSucceedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartInternetConnectionFailedEnter" action
    public void StartInternetConnectionFailed()
    {
        onStartInternetConnectionFailedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartGooglePlayEnter" action
    public void StartGooglePlayInitializionSucceed()
    {
        onStartGooglePlayInitializionSucceedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartGooglePlayInitializionFailedEnter" action
    public void StartGooglePlayInitializionFailed()
    {
        onStartGooglePlayInitializionFailedEnter?.Invoke();
    }

    // Invoke when subscribed "onStartAccountControlEnter" action
    public void StartAccountControlEnter()
    {
        onStartAccountControlEnter?.Invoke();
    }

    // Invoke when subscribed "onStartAutomaticLoginEnter" action
    public void StartAutomaticLoginEnter()
    {
        onStartAutomaticLoginEnter?.Invoke();
    }

    #endregion

    //2
    #region GAME MODE STATE



    #endregion

    #endregion

}
