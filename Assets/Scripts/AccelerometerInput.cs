using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour {

    private Rigidbody rigid;
    private float previousYacceleration = 0.0f;
    private float maxHeight = 2.0f;
    private float minHeight = 2.0f;
    private float step = 10.0f;
    private float a = 0.0f;

    // Low pass filter
    static float AccelerometerUpdateInterval = 1.0f / 60.0f;
    static float LowPassKernelWidthInSeconds = 1.0f;

    float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
    Vector3 lowPassValue = Vector3.zero;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Screen.orientation = ScreenOrientation.Portrait;
        previousYacceleration = Input.acceleration.z;
        a = Input.acceleration.z;

        lowPassValue = Input.acceleration;
    }

    float speed = 300.0f;
    private void LateUpdate()
    {
        Vector3 dir = Vector3.zero;

        dir.y = (LowPassFilterAccelerometer().y - previousYacceleration) * -1.0f;
        previousYacceleration = LowPassFilterAccelerometer().y;

        Debug.Log(dir.y);

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);

        //Vector3 acc = Input.acceleration;
        //if (acc.sqrMagnitude > 1) {
        //    acc.Normalize();
        //}

        //Debug.Log(acc.z);

        //float currentHeight = transform.position.y;
        //float wantedHeight = acc.z * -1.0f * maxHeight;
        //float y = Mathf.Lerp(currentHeight, wantedHeight, step * Time.deltaTime);

        //previousYacceleration = acc.z;

        //transform.position = new Vector3(transform.position.x,
        //y,
        //transform.position.z);
    }

    Vector3 LowPassFilterAccelerometer() {
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
        return lowPassValue;
    }

    //void FixedUpdate()
    //{
        //if (collidedPlane) {
        //    return;
        //}

        //if (rigid.position.y < 5 && !wasThrown)
        //{
        //    rigid.useGravity = false;
        //}

        //Vector3 tilt = Input.acceleration;

        //bool up = tilt.y < 0;
        //if (up) {
        //    Debug.Log(tilt);
        //    rigid.AddForce(0, -20 * tilt.y, 0);
        //}
        //else {
        //    if (rigid.position.y < 5 || collidedPlane) {
        //        return;
        //    }

        //    rigid.AddForce(0, -300 * tilt.y, 0);
        //    rigid.useGravity = true;
        //    wasThrown = true;
        //}
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log(collision.collider.name);
    //    if (collision.collider.name == "Plane") {
    //        collidedPlane = wasThrown && true;
    //    }
    //}
}
