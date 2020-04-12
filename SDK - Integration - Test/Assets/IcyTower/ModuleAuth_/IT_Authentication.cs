using UnityEngine;
using UnityEngine.UI;
using Library.FaceBook;
using Library.Authentication.GooglePlay;
using Library.Authentication;

public class IT_Authentication : MonoBehaviour
{
    private PlayfabCustomAuth _customAuth;

    private PlayfabFacebook _facebookAuth;

    private PlayFabGPGS _gpgsAuth;

    [Header("Recover PopUpMenu Configuration")]
    public GameObject _recoverPopUpFB;

    public GameObject _recoverPopUpGPGS;

    public Text _recoverPopUpFBText;

    public Text _recoverPopUpGPGSText;

    private void Awake()
    {
        _customAuth = new PlayfabCustomAuth();

        _facebookAuth = new PlayfabFacebook(_recoverPopUpFB);

        _gpgsAuth = new PlayFabGPGS(_recoverPopUpGPGS);
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
