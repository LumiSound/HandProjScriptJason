using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

using UnityEngine.Networking;
public class ScenarioSystem : MonoBehaviour
{
    [System.Serializable]
    public class TimeEvent
    {
        public float time;
        public List<EventAction> actions;
    }

    [System.Serializable]
    public class EventAction
    {
        public string actionType;
        public string param;
    }

    [System.Serializable]
    public class UnityTimeEvent
    {
        public float time;
        public UnityEvent events;
    }

    [SerializeField]
    private ScriptAnimationPlayer aniPlayer;
    [SerializeField]
    private PlayWebAudio audioPlayer;

    public string eventsUrl = "https://raw.githubusercontent.com/LumiSound/HandProjScriptJason/main/Luniscript.json"; // jason on team repo
    public List<UnityTimeEvent> unityTimeEvents;

    private List<TimeEvent> jsonTimeEvents = new List<TimeEvent>();

    public bool IsAutoPlay = false;
    private const string PlaySoundAction = "playSound";
    private const string ToggleObjectAction = "toggleObject";
    private const string PlayAnimationAction = "playAnimation";

    private void Start()
    {
        if (IsAutoPlay)
        {
            StartPlay();
        }

    }
    public void StartPlay()
    {
        StartCoroutine(LoadJsonEvents());
        StartUnityEvents();
    }

    private IEnumerator LoadJsonEvents()
    {
        yield return StartCoroutine(LoadJsonFromUrl(eventsUrl));

        foreach (var timeEvent in jsonTimeEvents)
        {
            StartCoroutine(TriggerJsonEvent(timeEvent));
        }
    }

    private IEnumerator LoadJson(string url, Action<string> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;
                Debug.Log(json);
                onSuccess?.Invoke(json);
            }
            else
            {
                Debug.LogError(www.error);
                onError?.Invoke(www.error);
            }
        }
    }
    private IEnumerator LoadJsonFromUrl(string url)
    {
        yield return LoadJson(url,
            json => { jsonTimeEvents = JsonHelper.FromJson<TimeEvent>(json); },
            error => { Debug.Log(error); }
        );
    }
    public IEnumerator LoadJsonFromUrl(string url, Action< List<TimeEvent>> callback)
    {
        yield return LoadJson(url,
            json =>
            {
                List<TimeEvent> timeEvents = JsonHelper.FromJson<TimeEvent>(json);
                callback?.Invoke(timeEvents);
            },
            error => { callback?.Invoke(null); }
        );
    }
    private List<string> ExtractPlaySoundClips(List<TimeEvent> timeEvents)
    {
        List<string> soundClips = new List<string>();

        foreach (var timeEvent in timeEvents)
        {
            foreach (var action in timeEvent.actions)
            {
                if (action.actionType == PlaySoundAction)
                {
                    soundClips.Add(action.param);
                }
            }
        }
        return soundClips;
    }
    public void LoadSoundClipsName(Action<List<string>> callback)
    {
        StartCoroutine(LoadJsonFromUrl(eventsUrl, timeEvents =>
        {
            List<string> soundClips = ExtractPlaySoundClips(timeEvents);
            callback?.Invoke(soundClips);
        }));
    }


    private void StartUnityEvents()
    {
        foreach (var timeEvent in unityTimeEvents)
        {
            StartCoroutine(TriggerUnityEvent(timeEvent));
        }
    }

    private IEnumerator TriggerJsonEvent(TimeEvent timeEvent)
    {
        yield return new WaitForSeconds(timeEvent.time);

        foreach (var action in timeEvent.actions)
        {
            switch (action.actionType)
            {
                case PlaySoundAction:
                    audioPlayer.PlaySound(action.param);
                   
                    break;

                case ToggleObjectAction:
                    //ToggleObject(action.param);
                    break;

                case PlayAnimationAction:
                    aniPlayer.CallAnimation(action.param);
                  
                    break;
                case "playParticals":
                    // if needed
                    break;

                case "PostProcessing":
                    // if needed
                    break;

                case "customEvent":
                    // custom composition of various event types
                    break;

                    // Add more case statements for other action types...
            }
        }
    }

    private IEnumerator TriggerUnityEvent(UnityTimeEvent timeEvent)
    {
        yield return new WaitForSeconds(timeEvent.time);
        timeEvent.events.Invoke();
    }
}
public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        string newJson = "{ \"items\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
    }
}
