using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float mousesen =100f;

    public Transform playerBody;
    // public Transform spine;

    float xRotation =0f;
    // float zRotation =0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") *mousesen *Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") *mousesen *Time.deltaTime;


        xRotation-= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        xRotation = Mathf.Clamp(xRotation, 0f,20f);

        playerBody.Rotate(Vector3.up *mouseX);
        // spine.Rotate(Vector3.up *mouseY);




        // zRotation-= mouseX;
        // transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        // zRotation = Mathf.Clamp(zRotation, 0f,20f);

        // playerBody.Rotate(Vector3.down *mouseY);

    }
}
