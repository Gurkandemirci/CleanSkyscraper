using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Space"))
        {
            PlayerControl.Instance.Check(!PlayerControl.Instance.player.leftright);
        }
    }
}
