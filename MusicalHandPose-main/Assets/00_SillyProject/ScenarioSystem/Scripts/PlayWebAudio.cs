using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayWebAudio : MonoBehaviour
{
    public ScenarioSystem scenarioSystem;
    public AudioSource audioSource;
    public string baseUrl = "https://github.com/hsiehchenwei/DataSpace/raw/main/";
    public string audioFile = "Bolero_A_Intro";
    public List<string> clipNames;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private int loadingCount =0;
    public UnityEvent OnLoadComplete;
    void Start()
    {
       scenarioSystem.LoadSoundClipsName((clipNames) => {
           this.clipNames = clipNames;
           DownloadClips();
       });
    }
    void DownloadClips()
    {
        Debug.Log("開始下載音樂"+clipNames.Count+"首");
        loadingCount=0;
        foreach (string name in clipNames)
        {
            StartCoroutine(DownloadClip(name));
        }
    }
    string getFileUrl(string filename)
    {
        return baseUrl + filename + ".wav";
    }
    IEnumerator DownloadClip(string clipname)
    {
        string url = getFileUrl(clipname);
        using (WWW www = new WWW(getFileUrl(clipname)))
        {
            yield return www;

            if (www.error == null)
            {
                audioClips[clipname] = www.GetAudioClip();
                Debug.Log(clipname+"下載完成");
                loadingCount++;
                if(loadingCount==clipNames.Count)
                {
                    Debug.Log("全部音樂下載完成");
                    OnLoadComplete.Invoke();
                }
            }
            else
            {
                Debug.LogError("咦？出錯了！確定這網址沒問題嗎？" + url);
            }
        }
    }

    public void PlaySound(string clipNameToPlay)
    {
        if (audioClips.ContainsKey(clipNameToPlay))
        {
            audioSource.clip = audioClips[clipNameToPlay];
            audioSource.Play();
            Debug.Log("音樂 " + clipNameToPlay + " 播放中！");
        }
        else
        {
            Debug.LogError("這首 " + clipNameToPlay + " 沒下載到！");
        }
    }
}
