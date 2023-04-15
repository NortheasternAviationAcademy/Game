using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // input fields
    public InputField username;
    public InputField code;

    private GameObject globalVars;


    private GlobalVars GlobalVars;

    private void Awake()
    {
        // create gameobject to hold global variables
        if (GameObject.Find("GlobalVars") == null)
        {
            globalVars = new GameObject("GlobalVars");
            globalVars.AddComponent<GlobalVars>();
        }
        else
        {
            globalVars = GameObject.Find("GlobalVars");
        }

        GlobalVars = globalVars.GetComponent<GlobalVars>();
        if (GlobalVars.username != null)
        {
            //disable the username
            username.GetComponent<InputField>().text = GlobalVars.username;
            username.interactable = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        GlobalVars.username = username.GetComponent<InputField>().text;
    }

    public void startGame()
    {
        GlobalVars.checkCode(code.GetComponent<InputField>().text);
        if (username.GetComponent<InputField>().text != "")
        {
            print("Username: " + username.GetComponent<InputField>().text);
            GlobalVars.username = username.GetComponent<InputField>().text;
            // check if the user has entered a code
            if (code.GetComponent<InputField>().text != "")
            {
                print("Code: " + code.GetComponent<InputField>().text);
                // check if the code is valid
                if (GlobalVars.validcode)
                    // load the game
                    SceneManager.LoadScene("MainScene");
                else
                    // display error message
                    Debug.Log("Invalid Code");
            }
            else
            {
                // load the game
                SceneManager.LoadScene("MainScene");
            }
        }
        else
        {
            // display error message
            Debug.Log("Invalid Username");
        }
    }
}