using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class Worker : MonoBehaviour
{
    public AnimancerComponent worker;

    public AnimationClip idle;
    public AnimationClip workerJoy;
    public AnimationClip workerTurn;


    void Start()
    {
        PlayAnim(worker,idle);
    }

    public void PlayAnim(AnimancerComponent comp, AnimationClip clip, float speed = 1f, float fade = .3f)
    {
        var state = comp.Play(clip, fade);
        state.Speed = speed;
    }

    public void PlayTurnAnim()
    {
        PlayAnim(worker, workerTurn);
    }

    public void PlayFinishAnim()
    {
        PlayAnim(worker, workerJoy);
    }
    
    
}
