﻿using System.Collections.Generic;
using Models;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class MainScript : MonoBehaviour
{
    private readonly string basePath = "https://jsonplaceholder.typicode.com";
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void Get()
    {
        // We can add default request headers for all requests
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer ...";

        RequestHelper requestOptions = null;

        RestClient.GetArray<Post>(basePath + "/posts").Then(res =>
        {
            LogMessage("Posts", JsonHelper.ArrayToJsonString(res, true));
            return RestClient.GetArray<Todo>(basePath + "/todos");
        }).Then(res =>
        {
            LogMessage("Todos", JsonHelper.ArrayToJsonString(res, true));
            return RestClient.GetArray<User>(basePath + "/users");
        }).Then(res =>
        {
            LogMessage("Users", JsonHelper.ArrayToJsonString(res, true));

            // We can add specific options and override default headers for a request
            requestOptions = new RequestHelper
            {
                Uri = basePath + "/photos",
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", "Other token..." }
                },
                EnableDebug = true
            };
            return RestClient.GetArray<Photo>(requestOptions);
        }).Then(res =>
        {
            LogMessage("Header", requestOptions.GetHeader("Authorization"));

            // And later we can clear the default headers for all requests
            RestClient.ClearDefaultHeaders();
        }).Catch(err => LogMessage("Error", err.Message));
    }

    public void Post()
    {
        // We can add default query string params for all requests
        RestClient.DefaultRequestParams["param1"] = "My first param";
        RestClient.DefaultRequestParams["param3"] = "My other param";

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/posts",
            Params = new Dictionary<string, string>
            {
                { "param1", "value 1" },
                { "param2", "value 2" }
            },
            Body = new Post
            {
                title = "foo",
                body = "bar",
                userId = 1
            },
            EnableDebug = true
        };
        RestClient.Post<Post>(currentRequest)
            .Then(res =>
            {
                // And later we can clear the default query string params for all requests
                RestClient.ClearDefaultParams();

                LogMessage("Success", JsonUtility.ToJson(res, true));
            })
            .Catch(err => LogMessage("Error", err.Message));
    }

    public void Put()
    {
        currentRequest = new RequestHelper
        {
            Uri = basePath + "/posts/1",
            Body = new Post
            {
                title = "foo",
                body = "bar",
                userId = 1
            },
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) =>
            {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        };
        RestClient.Put<Post>(currentRequest, (err, res, body) =>
        {
            if (err != null)
                LogMessage("Error", err.Message);
            else
                LogMessage("Success", JsonUtility.ToJson(body, true));
        });
    }

    public void Delete()
    {
        RestClient.Delete(basePath + "/posts/1", (err, res) =>
        {
            if (err != null)
                LogMessage("Error", err.Message);
            else
                LogMessage("Success", "Status: " + res.StatusCode);
        });
    }

    public void AbortRequest()
    {
        if (currentRequest != null)
        {
            currentRequest.Abort();
            currentRequest = null;
        }
    }

    public void DownloadFile()
    {
        var fileUrl = "https://raw.githubusercontent.com/IonDen/ion.sound/master/sounds/bell_ring.ogg";
        var fileType = AudioType.OGGVORBIS;

        RestClient.Get(new RequestHelper
        {
            Uri = fileUrl,
            DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType)
        }).Then(res =>
        {
            var audio = GetComponent<AudioSource>();
            audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
            audio.Play();
        }).Catch(err => { LogMessage("Error", err.Message); });
    }
}