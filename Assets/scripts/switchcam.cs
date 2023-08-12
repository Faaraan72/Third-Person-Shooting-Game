using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcam : MonoBehaviour
{
    public GameObject aimcam;
    public GameObject aimcanvas;
    public GameObject tpcam;
    public GameObject tpcanvas;
    public Transform player;
    public Animator anim;

    private void Update()
    {
        if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("aimWalk");
            anim.SetBool("aimWalk",true);
            anim.SetBool("idle", false);
            anim.SetBool("idleaim",true);
            anim.SetBool("walk",false);
            
            tpcam.SetActive(false);
            tpcanvas.SetActive(false);
            aimcanvas.SetActive(true);
            aimcam.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("idle", false);
            anim.SetBool("idleaim", true);
            anim.SetBool("aimWalk", true);
            anim.SetBool("walk", false);

            tpcam.SetActive(false);
            tpcanvas.SetActive(false);
            aimcanvas.SetActive(true);
            aimcam.SetActive(true);
        }
        else
        {
            anim.SetBool("idle", true);
            anim.SetBool("idleaim", false);
            anim.SetBool("aimWalk", false);

            tpcam.SetActive(true);
            tpcanvas.SetActive(true);
            aimcanvas.SetActive(false);
            aimcam.SetActive(false);
        }
    }
}
