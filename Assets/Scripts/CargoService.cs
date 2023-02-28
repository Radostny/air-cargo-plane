using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoService : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duration = 2;
    private float _elapsedTime;
    private GameObject _cargo;
    private GameObject _plane;

    private int _debugCounter = 0;
    public void PickUpSmoothly(GameObject cargo, GameObject plane)
    {
        _cargo = cargo;
        _plane = plane;
        StartCoroutine(PositionLerping());        
    }

    IEnumerator PositionLerping()
    {
        for (int t = 0; t <= 10; t += 1)
        {
            _cargo.transform.position = Vector3.Lerp(_cargo.transform.position, _plane.transform.position, _curve.Evaluate(t / _duration));
            _cargo.transform.localScale = Vector3.Lerp(_cargo.transform.localScale, Vector3.zero, _curve.Evaluate(t / _duration));
            yield return null;
        }

        /*        while (_elapsedTime <= _duration)
                {
                    _elapsedTime += Time.deltaTime;
                    float percentageComplete = _elapsedTime / _duration;
                    _cargo.transform.position = Vector3.Lerp(_cargo.transform.position, _plane.transform.position, _curve.Evaluate(percentageComplete));
                    _cargo.transform.localScale = Vector3.Lerp(_cargo.transform.localScale, Vector3.zero, _curve.Evaluate(percentageComplete));

                    Debug.Log(_debugCounter += 1);

                    yield return null;
                }
                _elapsedTime = 0;*/
        Destroy(_cargo);
    }
}
