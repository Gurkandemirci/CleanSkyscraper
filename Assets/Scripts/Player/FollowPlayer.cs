using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    void LateUpdate()
    {
        if (player.transform.position.y < transform.position.y)
        {
            transform.DOMoveY(player.transform.position.y, .4f);
        }
    }
}
