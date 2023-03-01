using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Transform _planet;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _yawSpeed = 40f;

    [SerializeField] private GameObject _cargoPrefab;
    [SerializeField] private GameObject _cityPrefab;

    [SerializeField] GameObject _cargoServiceGameObject;
    private CargoService _cargoService;

    private void Awake()
    {
        _cargoService = _cargoServiceGameObject.GetComponent<CargoService>();
    }

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
                
        //Debug.Log("Coordinates: " + latitude + " : " + longitude);
    }

    enum PinType {City, Cargo};

    void Pinpoint(PinType obj, float latitude, float longitude, int color = 0)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = Geo2Xyz(latitude, longitude, 2);
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

    void PinRandomCargo()
    {
        float latitude = Random.Range(-90, 90);
        float longitude = Random.Range(-180, 180);
        Vector3 location = Geo2Xyz(latitude, longitude, 0.3f);
        Quaternion normal = Quaternion.FromToRotation(Vector3.up, location.normalized);
        GameObject orange = Instantiate(_cargoPrefab, location, normal);
    }

    void PinRandomCity()
    {
        float latitude = Random.Range(-90, 90);
        float longitude = Random.Range(-180, 180);
        Vector3 location = Geo2Xyz(latitude, longitude);
        Quaternion normal = Quaternion.FromToRotation(Vector3.up, location.normalized);
        GameObject city = Instantiate(_cityPrefab, location, normal);
    }

    public Vector3 Geo2Xyz(float latitude, float longitude, float altitude = 0)
    {
        if ((Mathf.Abs(latitude) > 90) || (Mathf.Abs(longitude) > 180))
        {
            throw new ArgumentException("Coordinate parameter (latitude or longitude) exceed acceptable value");
        }

        float R = 5; // Planet Radius
        float r = R * Mathf.Cos(latitude * Mathf.Deg2Rad);
        float x = r * Mathf.Sin(longitude * Mathf.Deg2Rad);
        float y = R * Mathf.Sin(latitude * Mathf.Deg2Rad);
        float zModulus = r * Mathf.Cos(longitude * Mathf.Deg2Rad);
        float z = longitude < 90 ? -zModulus : -zModulus;
        Vector3 pointOnSurface = new Vector3(x, y, z);

        return altitude == 0 ? pointOnSurface : Vector3.Normalize(pointOnSurface) * (R + altitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cargo")
        {
            Debug.Log("Wow! I got Cargo!");
            //other.gameObject.active = false;
            _cargoService.PickUpSmoothly(other.gameObject, gameObject);
            PinRandomCargo();
        }
    }

    private void Start()
    {
        PinRandomCargo();
        PinRandomCity();
        PinRandomCity();
        /*        for (int i = -90; i <= 90; i = i + 15)
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
                }*/
    }
}
