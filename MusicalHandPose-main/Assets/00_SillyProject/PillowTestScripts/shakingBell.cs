using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakingBell : PlayByhandPose
{   
    [Header("Sub Class Custom Properties")]
    public Transform LeftHandTransform ;
    public Transform RightHandTransform ; 
    public float shakeThreshold = 0.01f;
    public float rotateThreshold = 30f;
    private Vector3 lastLeftHandPosition;
    private Vector3 lastRightHandPosition;
    private Quaternion PreLeftRot ;
    private Quaternion PreRightRot ;


    

   

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
        var isLeftMiddleFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        var isLeftIndexFingerPinching = TargetLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        var isRightMiddleFingerPinching = TargetRightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        var isRightIndexFingerPinching = TargetRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
       
        Vector3 LeftOffset = LeftHandTransform.position - lastLeftHandPosition;
        Vector3 RightOffset = RightHandTransform.position - lastRightHandPosition;
        //calculate the rotation scale
        Quaternion LeftRotChange = LeftHandTransform.rotation * Quaternion.Inverse(PreLeftRot);
        Quaternion RightRotChange = RightHandTransform.rotation * Quaternion.Inverse(PreRightRot);
        float left_angle = Quaternion.Angle(Quaternion.identity,LeftRotChange);
        float right_angle = Quaternion.Angle(Quaternion.identity,RightRotChange);
        float shake_amount_left = LeftOffset.magnitude;
        float shake_amount_right = RightOffset.magnitude;
        Debug.Log("mL: "+shake_amount_left + "\nmR: "+ shake_amount_right +"\naL: " + left_angle +"\naR: " + right_angle);
        
        // update for next frame

        lastRightHandPosition = RightHandTransform.position;
        lastLeftHandPosition = LeftHandTransform.position;
        PreLeftRot = LeftHandTransform.rotation;
        PreRightRot = RightHandTransform.rotation;
        //end update

        // moving detection 
        if((shake_amount_left > shakeThreshold && (isLeftMiddleFingerPinching || isLeftIndexFingerPinching))|| (shake_amount_right > shakeThreshold && (isRightMiddleFingerPinching || isRightIndexFingerPinching))){
            
            isPoseStateCurrent = true;
            return true;
        }

        // rotating detection
        if((left_angle > rotateThreshold && (isLeftMiddleFingerPinching || isLeftIndexFingerPinching))|| (right_angle > rotateThreshold && (isRightMiddleFingerPinching || isRightIndexFingerPinching))){
            
            isPoseStateCurrent = true;
            return true;
        }

        
        isPoseStateCurrent = false;
        return false;

    }
    
    void Start()
    {
        
       lastRightHandPosition = RightHandTransform.position;
       lastLeftHandPosition = LeftHandTransform.position;
       PreLeftRot = LeftHandTransform.rotation;
       PreRightRot = RightHandTransform.rotation;
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
