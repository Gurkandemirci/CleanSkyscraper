using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    HingeJoint joint;
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        joint.connectedBody = transform.parent.gameObject.GetComponent<Rigidbody>();
    }
}
