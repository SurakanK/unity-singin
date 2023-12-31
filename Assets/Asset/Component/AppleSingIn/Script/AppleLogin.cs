using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class AppleLogin : MonoBehaviour
{
    [SerializeField]
    public Button ButtonSignInWithApple;

    private const string AppleUserIdKey = "AppleUserId";
    private IAppleAuthManager _appleAuthManager;

    private void Start()
    {
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this._appleAuthManager = new AppleAuthManager(deserializer);
        }

        this.InitializeLoginMenu();
        this.OnMessage();
    }

    private void OnMessage()
    {
        this.ButtonSignInWithApple.onClick.AddListener(this.OnClickSignInWithApple);
    }

    private void Update()
    {
        if (this._appleAuthManager != null)
        {
            this._appleAuthManager.Update();
        }
    }

    public void OnClickSignInWithApple()
    {
        this.SetupLoginMenuForAppleSignIn();
        this.SignInWithApple();
    }

    private void InitializeLoginMenu()
    {
        // Check if the current platform supports Sign In With Apple
        if (this._appleAuthManager == null)
        {
            this.SetupLoginMenuForUnsupportedPlatform();
            return;
        }

        // If at any point we receive a credentials revoked notification, we delete the stored User ID, and go back to login
        this._appleAuthManager.SetCredentialsRevokedCallback(result =>
        {
            Debug.Log("Received revoked callback " + result);
            this.SetupLoginMenuForSignInWithApple();
            PlayerPrefs.DeleteKey(AppleUserIdKey);
        });

        // If we have an Apple User Id available, get the credential status for it
        if (PlayerPrefs.HasKey(AppleUserIdKey))
        {
            var storedAppleUserId = PlayerPrefs.GetString(AppleUserIdKey);
            this.SetupLoginMenuForCheckingCredentials();
            this.CheckCredentialStatusForUserId(storedAppleUserId);
        }
        // If we do not have an stored Apple User Id, attempt a quick login
        else
        {
            this.SetupLoginMenuForQuickLoginAttempt();
            this.AttemptQuickLogin();
        }
    }

    private void SetupLoginMenuForUnsupportedPlatform()
    {
        //
    }

    private void SetupLoginMenuForSignInWithApple()
    {
        //
    }

    private void SetupLoginMenuForCheckingCredentials()
    {
        //
    }

    private void SetupLoginMenuForQuickLoginAttempt()
    {
        //
    }

    private void SetupLoginMenuForAppleSignIn()
    {
        //
    }

    private void SetupGameMenu(string appleUserId, ICredential credential)
    {
        var appleIdCredential = credential as IAppleIDCredential;
        var passwordCredential = credential as IPasswordCredential;
        if (appleIdCredential != null)
        {
            if (appleIdCredential.IdentityToken != null)
            {
                var identityToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken, 0, appleIdCredential.IdentityToken.Length);
                Debug.Log("Token:" + identityToken);
                Debug.Log("User:" + appleIdCredential.User);
            }
        }
        else if (passwordCredential != null)
        {
            Debug.Log("User:" + passwordCredential.User);
            Debug.Log("Password:" + passwordCredential.Password);
        }
    }

    private void CheckCredentialStatusForUserId(string appleUserId)
    {
        // If there is an apple ID available, we should check the credential state
        this._appleAuthManager.GetCredentialState(
            appleUserId,
            state =>
            {
                switch (state)
                {
                    // If it's authorized, login with that user id
                    case CredentialState.Authorized:
                        this.SetupGameMenu(appleUserId, null);
                        return;

                    // If it was revoked, or not found, we need a new sign in with apple attempt
                    // Discard previous apple user id
                    case CredentialState.Revoked:
                    case CredentialState.NotFound:
                        this.SetupLoginMenuForSignInWithApple();
                        PlayerPrefs.DeleteKey(AppleUserIdKey);
                        return;
                }
            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Error while trying to get credential state " + authorizationErrorCode.ToString() + " " + error.ToString());
                this.SetupLoginMenuForSignInWithApple();
            });
    }

    private void AttemptQuickLogin()
    {
        var quickLoginArgs = new AppleAuthQuickLoginArgs();

        // Quick login should succeed if the credential was authorized before and not revoked
        this._appleAuthManager.QuickLogin(
            quickLoginArgs,
            credential =>
            {
                // If it's an Apple credential, save the user ID, for later logins
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                }

                this.SetupGameMenu(credential.User, credential);
            },
            error =>
            {
                // If Quick Login fails, we should show the normal sign in with apple menu, to allow for a normal Sign In with apple
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Quick Login Failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                this.SetupLoginMenuForSignInWithApple();
            });
    }

    private void SignInWithApple()
    {
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this._appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                // If a sign in with apple succeeds, we should have obtained the credential with the user id, name, and email, save it
                PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                this.SetupGameMenu(credential.User, credential);
            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Sign in with Apple failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                this.SetupLoginMenuForSignInWithApple();
            });
    }
}
