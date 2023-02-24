using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _yawSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yaw = Input.GetAxis("Horizontal");
    //    transform.Rotate(new Vector3(0, 0, 1f), yaw * Time.deltaTime * _yawSpeed, Space.World);
    //    transform.Rotate(new Vector3(-1f, 0, 0), Time.deltaTime * _speed, Space.World);
        
    }
}
