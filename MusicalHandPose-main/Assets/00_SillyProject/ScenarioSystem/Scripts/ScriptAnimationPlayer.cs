using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScriptAnimationPlayer : MonoBehaviour
{
    [System.Serializable]
    public class AnimationAction{
        public Animator animator;
        public string name;
    }
    [SerializeField]
    private List<AnimationAction> aniActionList;
    

    private void Start()
    {
        
        Debug.Log("ani awake");
    }

    public void CallAnimation(string para_name)
    {
        AnimationAction targetAction = aniActionList.Find(ani => ani.name == para_name);
        
        try{
            
            targetAction.animator.Play(targetAction.name);
            Debug.Log(targetAction.animator+" paly "+targetAction.name);
        }
        catch(Exception ex){
            Debug.Log("fail to paly "+targetAction.name+ " "+ ex.Message);
        }
        
    
    }
}
