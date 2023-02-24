using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _planet;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _yawSpeed = 40f;

    private Vector3 _rotationAxis = Vector3.right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float yaw = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _rotationAxis = Quaternion.Euler(0, 0, 15) * _rotationAxis;
            transform.Rotate(new Vector3(0, -15f, 0), Space.Self);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _rotationAxis = Quaternion.Euler(0, 0, -15) * _rotationAxis;
            transform.Rotate(new Vector3(0, 15f, 0), Space.Self);
        }
        Debug.Log("Axis is: " + _rotationAxis);
        Debug.Log("Plane right is: " + transform.right);

        Debug.DrawRay(_planet.position, _rotationAxis * 15, Color.red);
        Debug.DrawRay(_planet.position, -_rotationAxis * 15, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * 15, Color.yellow);
        //transform.RotateAround(_planet.position, Vector3.right, _speed * Time.deltaTime);
        

        transform.RotateAround(_planet.position, _rotationAxis * Time.deltaTime, _speed * Time.deltaTime);
    }
}
