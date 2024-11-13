using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LoginRegister : MonoBehaviour
{
    [HideInInspector]
    public string playFabId;

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI displayText;
    public UnityEvent onLoggedIn;

    public static LoginRegister instance;
    void Awake() { instance = this; }
    public void OnRegister()
    {
        RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest
        {
            Username = usernameInput.text,
            DisplayName = usernameInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest,
        result =>
        {
            SetDisplayText(result.PlayFabId, Color.green);
        },
        error =>
        {
            SetDisplayText(error.ErrorMessage, Color.red);
        }
        );
    }
    public void OnLogin ()
    {
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest
        {
            Username = usernameInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest,
        result =>
        {
            playFabId = result.PlayFabId;
            SetDisplayText("Logged in as: " + result.PlayFabId, Color.green);
            if (onLoggedIn != null)
                onLoggedIn.Invoke();
        },
        error => Debug.Log(error.ErrorMessage)
        );
        
    }
    void SetDisplayText(string text, Color color)
    {
        displayText.text = text;
        displayText.color = color;
    }
    public void LoadGame()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Game");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
