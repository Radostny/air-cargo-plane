using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMeasureService : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(t1.position, t1.forward * 4, Color.red);
        Debug.DrawRay(t2.position, t2.forward * 4, Color.blue);
    //    Debug.Log("Angle: " + Vector3.Angle(t1.forward, t2.forward));
        Debug.Log("Dot Product: " + Vector3.Dot(t1.forward, t2.forward));
    }
}
