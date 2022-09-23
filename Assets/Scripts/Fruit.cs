using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [HideInInspector]public Transform myParent;

    private void Start()
    {
        myParent = transform.parent;
    }
}
