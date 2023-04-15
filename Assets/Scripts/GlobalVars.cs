using System.Collections.Generic;
using FirebaseWebGL.Scripts.FirebaseBridge;
using Newtonsoft.Json;
using Unity.VisualScripting;
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

    public static bool validcode;

    public bool stabilized;
    public bool scaly;
    public bool shielded;
    public bool boosted;
    public bool laser;
    public bool fire;
    public bool pollen;
    public bool meteor;
    public bool flare;
    public string username;

    private string Data;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnRequestSuccess(string data)
    {
        Data = data;
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
            var json = packageplayerdata();
            updateplayerdata(json);
        }
    }

    public string packageplayerdata()
    {
        var globalref = GameObject.Find("GlobalVars");
        var globalvars = globalref.GetComponent<GlobalVars>();
        var json = JsonUtility.ToJson(globalvars);
        return json;
    }

    public void updateplayerdata(string json)
    {
        FirebaseFirestore.AddDocument("gameplay", json, this.gameObject.name, "OnRequestSuccess", "DisplayErrorObject");
    }
    public void downloadplayerdata()
    {
        FirebaseFirestore.GetDocumentsInCollection("gameplay", this.gameObject.name, "OnRequestSuccess", "DisplayErrorObject");
        print(Data);
        var parseData = JsonConvert.DeserializeObject<List<GlobalVars>>(Data);
        print(parseData);
        foreach (var item in parseData)
        {
            if (item.username == username)
            {
                scaly = item.scaly;
                shielded = item.shielded;
                boosted = item.boosted;
                laser = item.laser;
                fire = item.fire;
                pollen = item.pollen;
                meteor = item.meteor;
                flare = item.flare;
                stabilized = item.stabilized;
                break;
            }
            else
            {
                scaly = false;
                shielded = false;
                boosted = false;
                laser = false;
                fire = false;
                pollen = false;
                meteor = false;
                flare = false;
                stabilized = false;
            }
        }
    }
}