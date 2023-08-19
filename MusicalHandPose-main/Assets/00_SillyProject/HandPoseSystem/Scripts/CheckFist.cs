using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckFist : MonoBehaviour
{
    
    public OVRHand TargetHand;

    public UnityEvent OnStartFist;
    public UnityEvent OnFist;
    public UnityEvent OnNotFist;
    public UnityEvent OnReleaseFist;

    public List<FirstCheck> firstPinchChecks;

    [System.Serializable]
    public class FirstCheck{
        public string Title;

        public OVRHand.HandFinger finger;
        private bool isPinched=false;
        public float PinchStrength;
        public UnityEvent OnPinch;
        public UnityEvent OnReleasePinch;
        public void Check(OVRHand TargetHand)
        {
            var isFingerPinching = TargetHand.GetFingerIsPinching(finger);
            PinchStrength = TargetHand.GetFingerPinchStrength(finger);
            if (isFingerPinching)
            {
               if(!isPinched)
                {
                    OnPinch.Invoke();
                    isPinched = true;
                }
            }
            if(!isFingerPinching)
            {
                if(isPinched)
                {
                    OnReleasePinch.Invoke();
                    isPinched = false;
                }
            }
        }
    }

    public Text log;

    bool isFIstState=false;
    void DoWhenStartFist()
    {
        Debug.Log("Check:Start Fist");
        OnStartFist.Invoke();
    }
    void DoWhenFist()
    {
      //  Debug.Log("Fist");
        isFIstState = true;
        OnFist.Invoke();
    }
    void DoWhenNotFist()
    {
        isFIstState = false;
      //  Debug.Log("Not Fist");
        OnNotFist.Invoke();
    }
    void DoWhenReleaseFist()
    {

        Debug.Log("Check:Release Fist");
        OnReleaseFist.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetHand.IsTracked)
        {
            foreach(var obj in firstPinchChecks)
            {
                obj.Check(TargetHand);
            }
            var isIndexFingerPinching = TargetHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            var isMiddleFingerPinching = TargetHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
            var isRingFingerPinching = TargetHand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
            var isPinkyFingerPinching = TargetHand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

            var isThumbFingerPinching = TargetHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);
           var indexS= TargetHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            var middleS = TargetHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
            var ringS = TargetHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
            var pinkyS = TargetHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

            string logText = "Index :"+ isIndexFingerPinching+":"+indexS+System.Environment.NewLine+
            "Middle :" + isMiddleFingerPinching+":"+middleS+System.Environment.NewLine+
            "Ring :" + isRingFingerPinching+":"+ringS+System.Environment.NewLine+
            "Pinky :" + isPinkyFingerPinching+":"+pinkyS+System.Environment.NewLine+
            "Thumb :" + isThumbFingerPinching+System.Environment.NewLine;

            
            

            if(log!=null)
                log.text = logText;
            if (isIndexFingerPinching)
            {
                if(!isFIstState)
                {
                    DoWhenStartFist();
                }
                DoWhenFist();
            }
            else
            {
                if(isFIstState)
                {
                    DoWhenReleaseFist();
                }
                DoWhenNotFist();
            }
        }
    }
}
