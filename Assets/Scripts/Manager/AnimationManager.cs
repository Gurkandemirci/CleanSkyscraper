using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimationManager : LocalSingleton<AnimationManager>
{
    public AnimancerComponent character1;
    public AnimancerComponent character2;

    public AnimationClip idle;
    public AnimationClip turnIdle;
    public AnimationClip unTurnCharacter;
    public AnimationClip run;
    public AnimationClip dance;
    public AnimationClip falling;
    public AnimationClip jumping;




    public void PlayAnim(AnimancerComponent comp,AnimationClip clip, float speed = 1f,float fade = .3f)
    {
        var state = comp.Play(clip, fade);
        state.Speed = speed;
    }
}
