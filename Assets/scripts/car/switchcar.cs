using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchcar : MonoBehaviour
{
    bool nearcar = false;
    public float dist = 2f;
    public LayerMask car;
    public GameObject playercar;
    public Camera cam;
    bool outsidecar = true;
   // public GameObject speedom;
    public GameObject playerui;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nearcar= Physics.CheckSphere(transform.position, dist, car);
        if(nearcar && Input.GetKey(KeyCode.F) && outsidecar)
        {
            Debug.Log("sit");
            outsidecar = false;
            playercar.GetComponent<movement>().enabled = true;
            playercar.GetComponent<switchtoplayer>().enabled = true;
            playercar.GetComponent<antirollbar>().enabled = true;
            playercar.GetComponent<AudioSource>().enabled = true;
            cam.GetComponent<camerafollow>().enabled = true;
            cam.GetComponent<CinemachineBrain>().enabled = false;
            gameObject.GetComponent<player_movement>().enabled = false;
            gameObject.SetActive(false);
            //speedom.SetActive(true);
            playerui.SetActive(false);
        }
       
    }
}
