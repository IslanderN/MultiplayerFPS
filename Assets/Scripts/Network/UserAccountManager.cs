using UnityEngine;
using System.Collections;
using DatabaseControl;
using UnityEngine.SceneManagement;


// Log out and log in need to improve
public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager instance;

    //These store the username and password of the player when they have logged in
    public static string playerUsername { get; protected set; }
    public static string playerPassword { get; protected set; }

    //public static string LoggedIn_Data { get; protected set; }

    public static bool isLoggedIn { get; protected set; }

    public string loggedInSceneName = "Lobby";
    public string loggedOutSceneName = "LoginMenu";

    public delegate void OnDataReceivedCallback(string data);

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void LogOut()
    {
        playerUsername = "";
        playerPassword = "";

        isLoggedIn = false;

        SceneManager.LoadScene(loggedOutSceneName);
    }

    public void LogIn(string username, string password)
    {
        playerUsername = username;
        playerPassword = password;

        isLoggedIn = true;
        

    }

    public void SuccessLogInChangeScene()
    {
        if (isLoggedIn)
        {
            SceneManager.LoadScene(loggedInSceneName);
        }
    }

    public void SendData(string data)
    {
        if (isLoggedIn)
        {
            StartCoroutine(SendDataRequest(playerUsername, playerPassword, data));
        }
    }

    public void GetData(OnDataReceivedCallback onDataReceived)
    {
        if (isLoggedIn)
        {
            StartCoroutine(GetDataRequest(playerUsername, playerPassword, onDataReceived));
            
        }
    }

    public IEnumerator SendDataRequest(string username, string password, string data)
    {
        IEnumerator e = DCF.SetUserData(username, password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //...
        }
        else
        {
            //...
        }
    }

    public IEnumerator GetDataRequest(string username, string password, OnDataReceivedCallback onDataReceived)
    {
        
        IEnumerator e = DCF.GetUserData(username, password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            Debug.Log("Data Uploda Error.");
        }
        else
        {
           //...
        }

        if (onDataReceived != null)
        {
            onDataReceived.Invoke(response);
        }
    }

    public IEnumerator LoginUserRequest(string username, string password)
    {
        IEnumerator e = DCF.Login(username, password); // << Send request to login, providing username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if(response == "Success")
        {
            //...
        }
        else
        {
            //...
        }
    }

    public IEnumerator RegisterUserRequest(string username, string password, string data)
    {
        IEnumerator e = DCF.RegisterUser(username, password, data); // << Send request to register a new user, providing submitted username and password. It also provides an initial value for the data string on the account, which is "Hello World".
        while (e.MoveNext())
        {
            yield return e.Current;
        }

        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //...
        }
        else
        {
            //...
        }
    }
}
