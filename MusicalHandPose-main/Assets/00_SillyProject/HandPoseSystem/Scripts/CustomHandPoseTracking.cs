using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CustomHandPoseTracking : MonoBehaviour
{
     public OVRHand targetHand;
     public OVRSkeleton targetHandSkeleton;
     public HandGestureData CurrentHandGestureData;

    public List<HandPoseTrackingPaire> handPoseTrackingPaires;

    [System.Serializable]
    public class HandPoseTrackingPaire
    {
        public string HandTOAction;
        public HandGestureData handGestureData;
        public UnityEvent OnHandPoseDetected;
    }
    [System.Serializable]
    public class HandGestureData
    {
        public string gestureName;
        public List<BoneData> gestureData;
        public void AddBoneData(string boneName, Vector3 bonePosition, Quaternion boneRotation)
        {
            if (gestureData == null)
            {
                gestureData = new List<BoneData>();
            }
            gestureData.Add(new BoneData(boneName, bonePosition, boneRotation));
        }
    }
    public class BoneData
    {
        public string boneName;
        public Vector3 bonePosition;
        public Quaternion boneRotation;
        public BoneData(string boneName, Vector3 bonePosition, Quaternion boneRotation)
        {
            this.boneName = boneName;
            this.bonePosition = bonePosition;
            this.boneRotation = boneRotation;
        }
    }
   
   HandPoseTrackingPaire previosHandPoseTrackingPaire;
   float ResetTraskingTimer=5;
    // Start is called before the first frame update
    void Start()
    {
        previosHandPoseTrackingPaire = null;
        InvokeRepeating("ResetTracking", 0, ResetTraskingTimer);
    }
    void ResetTracking()
    {
        previosHandPoseTrackingPaire = null;
    }

    // Update is called once per frame

   void Update()
    {
        if(targetHand.IsTracked)
        {
            if(targetHand.HandConfidence == OVRHand.TrackingConfidence.High)
            {
                RecognizeHandPoseBySimularity();

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    RecordHandPose();
                
                }
            }
        }
    }
    void RecordHandPose()
    {
        HandGestureData handGestureData = new HandGestureData();
        handGestureData.gestureName = "New Gesture";
        
        for (int i = 0; i < targetHandSkeleton.Bones.Count; i++)
        {
           string boneID= targetHandSkeleton.Bones[i].Id.ToString();
           Vector3 PosToRoot = targetHandSkeleton.transform.InverseTransformPoint(targetHandSkeleton.Bones[i].Transform.position);
           Quaternion localRotation = targetHandSkeleton.Bones[i].Transform.rotation;//Quaternion.Inverse(skeleton.Bones[i].Transform.rotation) * skeleton.transform.rotation;
            handGestureData.AddBoneData(boneID, PosToRoot, localRotation);
        }

        CurrentHandGestureData=handGestureData;
    }
    void SaveToJson(HandGestureData handGestureData )
    {
        string json = JsonUtility.ToJson(handGestureData);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/"+handGestureData.gestureName+"_HG.json", json);
    }
    
    float CompareHandPoseSimularity(HandGestureData handGestureData1, HandGestureData handGestureData2)
    {
        float simularity = 0;
        if (handGestureData1.gestureData.Count != handGestureData2.gestureData.Count)
        {
            return 0;
        }
        for (int i = 0; i < handGestureData1.gestureData.Count; i++)
        {
            simularity += Vector3.Distance(handGestureData1.gestureData[i].bonePosition, handGestureData2.gestureData[i].bonePosition);
        }
        return simularity;
    }
    HandPoseTrackingPaire GetMostLikeHandPose(HandGestureData curHand)
    {
        HandPoseTrackingPaire mostLikeHandPose = null;
        float minSimularity = 1000000;
        for (int i = 0; i < handPoseTrackingPaires.Count; i++)
        {
            float simularity = CompareHandPoseSimularity(curHand, handPoseTrackingPaires[i].handGestureData);
            if (simularity < minSimularity)
            {
                minSimularity = simularity;
                mostLikeHandPose = handPoseTrackingPaires[i];
            }
        }
        return mostLikeHandPose;
    }
    void RecognizeHandPoseBySimularity()
    {
        HandPoseTrackingPaire mostLikeHandPose = GetMostLikeHandPose(CurrentHandGestureData);
        if(previosHandPoseTrackingPaire!=mostLikeHandPose)
        {
            if (previosHandPoseTrackingPaire != null)
            {
                previosHandPoseTrackingPaire.OnHandPoseDetected.Invoke();
            }
            previosHandPoseTrackingPaire = mostLikeHandPose;
        }
    }
    // Start is called before the first frame update
   

   
}
