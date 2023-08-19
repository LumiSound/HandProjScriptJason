using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayByhandPose : MonoBehaviour
{   

    /// <summary>
    /// father class of the interaction objects needing hand pose trigger
    /// the class is only used for the framework it self 
    /// detailed judgement of hand poses should be written in sub class
    /// </summary>





    public OVRHand TargetLeftHand;
    public OVRHand TargetRightHand;

    public UnityEvent OnStartPose;
    public UnityEvent OnPose;
    public UnityEvent OnReleasePose;

    protected bool isPoseStateCurrent = false;

    protected virtual void StartPose()
    {
        //Debug.Log("Start Pose");
        OnStartPose.Invoke();
        isPoseStateCurrent = true;
       
    }

    protected virtual void Pose()
    {
        
        //Debug.Log("Pose base");
        OnPose.Invoke();
    }

    protected virtual void ReleasePose()
    {
        //Debug.Log("Release Pose");
        OnReleasePose.Invoke();
        isPoseStateCurrent = false;
    }

    protected abstract bool CheckPose();

    protected virtual void Update()
    {
        if (TargetLeftHand.IsTracked || TargetRightHand.IsTracked)
        {   
            ;

            if(!isPoseStateCurrent && CheckPose()){
                StartPose();
            }
            else if(isPoseStateCurrent && CheckPose()){
                Pose();
            }

            else if (isPoseStateCurrent&& !CheckPose())
            {
               ReleasePose();
            }
            
        }
    }
}