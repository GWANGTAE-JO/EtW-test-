using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public float shakePower;

    private float shakeTime;

    Vector3 initialPosition;

    public void VibrateForTime(float time) {
        shakeTime = time;
    }
    void Start()
    {
        initialPosition = new Vector3(0f, 0f, -10f);
    }

    void Update()
    {
        if (shakeTime > 0) {
            transform.position = Random.insideUnitSphere * shakePower + initialPosition;
            shakeTime -= Time.deltaTime;
        } else {
            shakeTime = 0f;
            transform.position = initialPosition;
        }
    }
}
