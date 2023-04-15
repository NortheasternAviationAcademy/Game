using System;
using FirebaseWebGL.Scripts.FirebaseBridge;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public const string dragonCode = "PUWC";
    public const string flareCode = "JBBP";
    public const string meteorCode = "MYFL";
    public const string pollenCode = "YUTZ";
    public const string fireCode = "CHPW";
    public const string shieldCode = "GPGJ";
    public const string laserCode = "NFDK";
    public const string speedCode = "TZBL";

    public const string stableCode = "FWWD";

    // Start is called before the first frame update
    public static bool aliensFlared;

    public  bool stabilized = false;
    public  bool scaly = false;
    public  bool shielded  = false;
    public  bool boosted = false;
    public  bool laser = false;
    public  bool fire = false;
    public  bool pollen = false;
    public  bool meteor = false;
    public  bool flare = false;

    public static bool validcode;
    public string username;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void checkCode(string code)
    {
        validcode = true;
        switch (code)
        {
            case dragonCode:
                scaly = true;
                break;
            case flareCode:
                aliensFlared = true;
                break;
            case meteorCode:
                meteor = true;
                break;
            case pollenCode:
                pollen = true;
                break;
            case fireCode:
                fire = true;
                break;
            case shieldCode:
                shielded = true;
                break;
            case laserCode:
                laser = true;
                break;
            case speedCode:
                boosted = true;
                break;
            case stableCode:
                stabilized = true;
                break;
            default:
                validcode = false;
                break;
        }
        if (validcode)
        {
            //update firebase
            
        }
    }
    
    public void packageplayerdata()
    {
        GameObject globalref = GameObject.Find("GlobalVars");
        GlobalVars globalvars = globalref.GetComponent<GlobalVars>();
        string json = JsonUtility.ToJson(globalvars);
        print(json);
    }
    public void updateplayerdata(string json)
    {
        FirebaseFirestore.AddDocument("gameplay", json, username, "DisplayInfo", "DisplayErrorObject");
        
    }
}