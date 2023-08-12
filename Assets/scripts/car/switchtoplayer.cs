
 using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchtoplayer : MonoBehaviour
{
    //public GameObject speedom;
    public GameObject playerui;
   
    public float dist = 2f;
    public GameObject player;
    public Camera cam;
    bool insidecar = true;
    public GameObject offset;
    // Update is called once per frame
    void Update()
    {
        
         if (insidecar && Input.GetKey(KeyCode.K))
        {
            gameObject.GetComponent<movement>().enabled = false;
            gameObject.GetComponent<antirollbar>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = false;
            cam.GetComponent<camerafollow>().enabled = false;
            cam.GetComponent<CinemachineBrain>().enabled = true;
            player.SetActive(true);
            player.transform.position =  offset.transform.position;
            player.GetComponent<player_movement>().enabled = true;
            player.GetComponent<switchcar>().enabled = true;
           // speedom.SetActive(false);
            playerui.SetActive(true);


        }
    }
}


