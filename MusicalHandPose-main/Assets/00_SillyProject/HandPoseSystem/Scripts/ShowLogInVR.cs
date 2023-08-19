using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowLogInVR : MonoBehaviour
{
    public Text logText;
    List<string> logList = new List<string>();
    public int DisplayLines = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if(type==LogType.Log)
        {
            if(logString.Contains("Check:"))
            {
                logList.Add(logString);
                if(logList.Count>DisplayLines)
                {
                    logList.RemoveAt(0);
                }
            }
            for(int i = 0; i < DisplayLines; i++)
            {
                if (logList.Count > i)
                {
                    logText.text += logList[logList.Count - 1 - i] + "\n";
                }
            }
            logText.text = logString;
        }
         
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
