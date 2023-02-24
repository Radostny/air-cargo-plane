using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Transform _planet;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _yawSpeed = 40f;

    void Update()
    {
        float yaw = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, yaw * _yawSpeed * Time.deltaTime);

        Vector3 rotationAxis = transform.right;
        transform.RotateAround(_planet.position, rotationAxis, _speed * Time.deltaTime);

        Vector3 equator = Vector3.ProjectOnPlane(transform.position, _planet.up);
        int hemisphereNorthSouth = transform.position.y >= _planet.position.y ? 1 : -1;
        float latitude = Vector3.Angle(equator, transform.position) * hemisphereNorthSouth;

        Vector3 primeMeridian = Vector3.ProjectOnPlane(transform.position, _planet.right);
        int hemisphereWestEast = transform.position.x >= _planet.position.x ? 1 : -1;
        bool isFrontHemisphere = transform.position.z < _planet.position.z;
        float acuteAngle = Vector3.Angle(primeMeridian, transform.position);
        float longitude = (isFrontHemisphere ? acuteAngle : 180 - acuteAngle) * hemisphereWestEast;
                
        Debug.Log("Coordinates: " + latitude + " : " + longitude);
    }

    enum PinType {City, Cargo};

    void Pinpoint(PinType obj, float latitude, float longitude, int color = 0)
    {
        if ((Mathf.Abs(latitude) > 90) || (Mathf.Abs(longitude) > 180))
        {
            throw new ArgumentException("Coordinate parameter (latitude or longitude) exceed acceptable value");
        }

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        float R = 5; // Planet Radius
        float r = R * Mathf.Cos(latitude * Mathf.Deg2Rad);
        float x = r * Mathf.Sin(longitude * Mathf.Deg2Rad);
        float y = R * Mathf.Sin(latitude * Mathf.Deg2Rad);
        float zModulus = r * Mathf.Cos(longitude * Mathf.Deg2Rad);
        float z = longitude < 90 ? -zModulus : -zModulus;

        sphere.transform.position = new Vector3(x, y, z);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


        MeshRenderer sphereRenderer = sphere.GetComponent<MeshRenderer>();
        switch (color)
        {
            case 0:
                sphereRenderer.material.color = Color.green;
                break;
            case 1:
                sphereRenderer.material.color = Color.yellow;
                break;
            case 2:
                sphereRenderer.material.color = Color.magenta;
                break;
        }
        


    }

    private void Start()
    {
        for (int i = -90; i <= 90; i = i + 15)
        {
            for (int j = -180; j <= 180; j = j + 3)
            {
                Pinpoint(PinType.City, i, j);
            }
        }

        for (int i = -90; i <= 90; i = i + 3)
        {
            for (int j = -180; j <= 180; j = j + 15)
            {
                Pinpoint(PinType.City, i, j, 1);
            }
        }
    }
}
