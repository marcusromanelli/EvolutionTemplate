using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Zenject;

public class PlayFabLoginController : ILoginController
{
    [Inject]
    IEventHandler _eventHandler;

    public const string PlayFabIdKey = "PlayFabId";

    public void Initialize()
    {
        Login();
    }

    public void Login()
    {
        if (HasStoredCredentials())
        {
            LoginWithStoredCredentials();
            return;
        }

        FirstTimeLogin();
    }

    private bool HasStoredCredentials()
    {
        var playFabId = PlayerPrefs.GetString(PlayFabIdKey, "");

        return playFabId != null && playFabId != "";
    }


    private void LoginWithStoredCredentials()
    {
        var playfabId = PlayerPrefs.GetString(PlayFabIdKey, "");

        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest();
        request.CreateAccount = true;
        request.CustomId = playfabId;

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucceed, OnLoginFailed);
    }
    private void FirstTimeLogin()
    {
        var newCredential = Guid.NewGuid().ToString();
        StoreCredential(newCredential);

        LoginWithStoredCredentials();
    }

    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        _eventHandler.LogOut();
    }

    public bool IsLoggedIn()
    {
        return PlayFabAuthenticationAPI.IsEntityLoggedIn();
    }

    private void OnLoginSucceed(LoginResult loginResult)
    {
        Debug.Log("Succeed");
        _eventHandler.LogIn();
    }

    private void OnLoginFailed(PlayFabError loginError)
    {
        Debug.Log("Failed");        
    }

    private void StoreCredential(string id)
    {
        PlayerPrefs.SetString(PlayFabIdKey, id);
    }
}
