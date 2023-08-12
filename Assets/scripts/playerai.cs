using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class playerai : MonoBehaviour
{ 
    public float paispeed = 1.9f;
    public float givedamage = 1f;
    float paihealth = 80f;
    float presentpaihealth;

    public scoremanager sm;

    public GameObject bloodeffect;
    public ParticleSystem muzzlespark;

    public Transform spawn;

    public Transform lookpoint;
    public GameObject shootingraycastarea;
    public NavMeshAgent paibody;
    public Transform enemybody;
    public LayerMask enemylayer;
    public Transform paicharacter;

    public AudioSource a;
    public AudioClip shootingsound;

    public float timebwshoot;
    bool previouslyshoot;
    public float visionradius;
    public float shootradius;
    bool invisionradius;
    bool inshootradius;
    //bool isplayer = false;
    // RaycastHit hitcheck;

    public Animator eanim;

    private void Awake()
    {
        paibody = GetComponent<NavMeshAgent>();
        presentpaihealth = paihealth;
    }
    private void Update()
    {
        invisionradius = Physics.CheckSphere(transform.position, visionradius, enemylayer);
        inshootradius = Physics.CheckSphere(transform.position, shootradius, enemylayer);
        if (invisionradius && !inshootradius)
        {
            pursueenemy();
        }

        //Physics.Raycast(shootingraycastarea.transform.position, shootingraycastarea.transform.forward, out hitcheck, shootradius);
        if (invisionradius && inshootradius)
        {

            shootenemy();
        }
    }
    private void pursueenemy()
    {
        if (paibody.SetDestination(enemybody.position))
        {
            eanim.SetBool("running", true);
            eanim.SetBool("shooting", false);
        }
        else
        {
            eanim.SetBool("running", false);
            eanim.SetBool("shooting", false);
        }
    }
    private void shootenemy()
    {
        paibody.SetDestination(transform.position);
        transform.LookAt(lookpoint);
        

        if (!previouslyshoot)
        {
            muzzlespark.Play();
            a.PlayOneShot(shootingsound);
            RaycastHit hit;
            if (Physics.Raycast(shootingraycastarea.transform.position, shootingraycastarea.transform.forward, out hit, shootradius))
            {
                // Debug.Log("shooting " + hit.transform.name);
                enemy enemy = hit.transform.GetComponent<enemy>();
                if (enemy != null)
                {
                    enemy.enemyhit(givedamage);
                    GameObject blood = Instantiate(bloodeffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(blood, 1f);
                }
            }
            eanim.SetBool("shooting", true);
            eanim.SetBool("running", false);
            previouslyshoot = true;
            Invoke(nameof(reshoot), timebwshoot);
        }
    }
    void reshoot()
    {
        previouslyshoot = false;
    }
    public void paihit(float takedamage)
    {
        presentpaihealth -= takedamage;
        if (presentpaihealth <= 0)
        {
            StartCoroutine(respawn());
        }
    }
    IEnumerator respawn()
    {
       paibody.SetDestination(transform.position);
        paispeed = 0f;
        inshootradius = false;
        invisionradius = false;
        shootradius = 0f;
        visionradius = 0f;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        sm.enekills += 1;
        eanim.SetBool("shooting", false);
        eanim.SetBool("running", false);
        eanim.SetBool("die", true);
        
        //Debug.Log("Died");
        //die ani
        yield return new WaitForSeconds(3f);
        paicharacter.position = spawn.position;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        // Debug.Log("respawned");
        eanim.SetBool("shooting", false);
        eanim.SetBool("running", true);
        eanim.SetBool("die", false);
        paispeed = 1.9f;
        inshootradius = false;
        invisionradius = true;
        paihealth = 80f;
        shootradius = 10f;
        visionradius = 100f;
        
        pursueenemy();


    }

}
