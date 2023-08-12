using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
   

    private float csa;//current steering angle
    private float cbf; // current breaking force
    private bool isbreaking;
    private Rigidbody rb;

    [SerializeField] private float motorforce, breakForce, maxSteeringangle;
    [SerializeField] private WheelCollider frontrightwc, frontleftwc, backrightwc, backleftwc;
    [SerializeField] private Transform frontrightTransforms, frontleftTransforms, backrightTransforms, backleftTransforms;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        GetInput();
        HandelMotor();
        HandelSteering();
       
        updateWheels();
        died();

    }
   
        
    public Vector3 pos;
    public Quaternion rot;
    private void died()
    {
        pos = rb.transform.position;
        rot = rb.transform.rotation;

        if(rb.rotation.x > 90 || rb.rotation.x < -90 || rb.rotation.z > 90 || rb.rotation.z < -90)
        {
            pos = rb.transform.position;
            Debug.Log("Died");
            Invoke(nameof(again), 2f);
            
        }

    }
    private void again()
    {
        Debug.Log("restarting");
        rb.transform.position = new Vector3(pos.x, pos.y + 5, pos.z + 5);
       
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isbreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandelMotor()
    {
        frontleftwc.motorTorque = verticalInput * motorforce;
        frontrightwc.motorTorque = verticalInput * motorforce;
        cbf = isbreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontleftwc.brakeTorque = cbf;
        frontrightwc.brakeTorque = cbf;
        backleftwc.brakeTorque = cbf;
        backrightwc.brakeTorque = cbf;

    }

    private void HandelSteering()
    {
        csa = maxSteeringangle * horizontalInput;
        frontleftwc.steerAngle = csa;
        frontrightwc.steerAngle = csa;

    }

    private void updateWheels()
    {
        UpdateSingleWheel(frontleftwc, frontleftTransforms);
        UpdateSingleWheel(frontrightwc, frontrightTransforms);
        UpdateSingleWheel(backleftwc, backleftTransforms);
        UpdateSingleWheel(backrightwc, backrightTransforms);

    }

    private void UpdateSingleWheel(WheelCollider wc, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wc.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

}
