using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveScript : MonoBehaviour
{

    public float wait;
    public float force;
    public Vector3 dir;

    private float lastTime;
    private bool forward;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = 0f;
        forward = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (forward)
            rb.AddForce(force * dir * Time.deltaTime);
        else
            rb.AddForce(-force * dir * Time.deltaTime);
        if (Time.time - lastTime > wait)
        {
            lastTime = Time.time;
            forward = !forward;
        }
    }
}
