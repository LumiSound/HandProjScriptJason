using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This is the template subclass for objects that needs to detect a single hand pose
/// The detailed judgement of a pose should be written here completely instead of the father class
/// </summary>
public class HandPose1 : PlayByhandPose
{   
    // transform of left hand, you should get "HandGrabInteractor" of the target hand you want in the scene in inspector 
    public Transform LeftHandTransform ;
    public Transform RightHandTransform ; 
    

   

    protected override bool CheckPose()
    {   
        
        /// <summary>
        ///  This is the template code of a hand tracking determination
        /// </summary>

        //get the needed finger for your gesture
        /*
        isPoseState = false;
        var isIndexFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        var isMiddleFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        var isRingFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        var isPinkyFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);
        var isThumbFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);

        var indexS= TargetLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        var middleS = TargetLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        var ringS = TargetLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        var pinkyS = TargetLeftHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);


        if(something happens){
            isPoseStateCurrent = true;
            return true;
        }


        */

        isPoseStateCurrent = false;
        return false;

    }
    
    void Start()
    {
        
       
    }

   
    protected override void StartPose()
    {
        base.StartPose();
        //Debug.Log("success to get pose in sub");
        
    }
    
    protected override void Pose()
    {
        base.Pose();
       
        
        
        //Debug.Log("this is pose going in sub");
        
    }
    
    protected override void ReleasePose()
    {
        base.ReleasePose();
        //Debug.Log("this is release pose in sub");
       
    }
    
}
