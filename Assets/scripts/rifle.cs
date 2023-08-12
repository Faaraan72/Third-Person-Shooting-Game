using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rifle : MonoBehaviour
{  
    public player_movement player;
    public Camera cam;
    public float givedamage = 5f;
    public float shootrange = 100f;

    public Text ammotxt;
    public Text magtxt;

    public float firecharge = 15f;
    public float nexttimetoshoot = 0f;

    public ParticleSystem muzzlespark;
    public GameObject woodeffect;
    public GameObject bloodeffect;

    public Animator anim;

    public AudioSource a;
    public AudioClip shootingsound;
    public AudioClip reloadingsound;

    public int mag = 15;
    private int maxammo = 30;
    private int presentammo;
    private float reloadingtime =1.3f;
    bool reloading = false;
    private Transform playertransform;

    //public float mousesen = 100f;
    //public Transform spine;

    private void Awake()
    {
        presentammo = maxammo;
        playertransform = GetComponent<Transform>();
        ammotxt.text = "Ammo: " + (presentammo);
        magtxt.text = "Mag: " + (mag);
    }
    private void Update()
    {
        if (reloading) return;
        if(presentammo <= 0)
        {
            StartCoroutine(reload()); // to call an IEnumerator function we use StartCoroutine
            return;
        }      
        if (Input.GetButton("Fire1") && Time.time > nexttimetoshoot)
        {
           anim.SetBool("fire", true);
            anim.SetBool("idle", false);
            nexttimetoshoot = Time.time + 1f / firecharge;
            shoot();
        }
        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("firewalk", true);
            anim.SetBool("idle", false);
           // float mouseY = Input.GetAxis("Mouse Y") * mousesen * Time.deltaTime;
           // spine.Rotate(Vector3.up * mouseY);

        }
        else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            anim.SetBool("firewalk", true);
            anim.SetBool("idle", false);
            anim.SetBool("idleaim", true);
            anim.SetBool("reloading", false);
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("firewalk", false);
            anim.SetBool("fire", false);
            anim.SetBool("idle", true);
            
            
        }
    }
    void shoot()
    {
        if (mag == 0) { magtxt.text = "Mag: 0" ;  }//Debug.Log("No Ammo Left"); 
        presentammo--;
        
        if (presentammo == 0) { mag--; }
        ammotxt.text = "Ammo: " + (presentammo);
        magtxt.text = "Mag: " + (mag);
        
        
        muzzlespark.Play();
        a.PlayOneShot(shootingsound);
        RaycastHit hitinfo;
        if(Physics.Raycast(cam.transform.position , cam.transform.forward,out hitinfo,shootrange))
        {
           // Debug.Log(hitinfo.transform.name);
            

            Objhealth obj = hitinfo.transform.GetComponent<Objhealth>();
            enemy enemy = hitinfo.transform.GetComponent<enemy>();
            if(obj != null)
            {
               // obj.damage(0f);
                GameObject woodgo = Instantiate(woodeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(woodgo, 1f);
            }
            if(enemy != null)
            {
                enemy.enemyhit(givedamage);
                GameObject blood = Instantiate(bloodeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(blood, 1f);
            }
        }
    }
    //IEnumerator type methods stop a process and then perform thier func() then resume from where thy left
    IEnumerator reload()
    {
        player.speed = 0f;
        player.sprint = 0f;
        reloading = true;
       // Debug.Log("Reloading...");
        anim.SetBool("reloading", true);
        a.PlayOneShot(reloadingsound);
        yield return new WaitForSeconds(reloadingtime);
        anim.SetBool("reloading", false);
        presentammo = maxammo;
        player.speed = 1.9f;
        player.sprint = 3f;
        reloading = false;


    }
}
