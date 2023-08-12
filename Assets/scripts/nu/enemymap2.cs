using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemymap2 : MonoBehaviour
{
    public float enemyspeed = 1.9f;
    public float givedamage = 1f;
    float ehealth = 100f;
    float presentehealth;

    public scoremanager sm;

    public GameObject bloodeffect;
    public ParticleSystem muzzleflash;

    public AudioSource a;
    public AudioClip shootingsound;

    public Transform spawn;

    public Transform lookpoint;
    public GameObject shootingraycastarea;
    public NavMeshAgent enemybody;
    public Transform playerbody;
    public LayerMask playerlayer;
    public Transform enemycharacter;

    public float timebwshoot;
    bool previouslyshoot;
    public float visionradius;
    public float shootradius;
    bool invisionradius;
    bool inshootradius;
    // bool isplayer = false;
    // RaycastHit hitcheck;

    public Animator eanim;

    private void Awake()
    {
        enemybody = GetComponent<NavMeshAgent>();
        presentehealth = ehealth;
    }
    private void Update()
    {
        invisionradius = Physics.CheckSphere(transform.position, visionradius, playerlayer);
        inshootradius = Physics.CheckSphere(transform.position, shootradius, playerlayer);
        if(!inshootradius && !invisionradius)
        {
            eanim.SetBool("idle", true);
        }
        else if (invisionradius && !inshootradius)
        {
            pursueplayer();
        }

        //Physics.Raycast(shootingraycastarea.transform.position, shootingraycastarea.transform.forward, out hitcheck, shootradius);
        if (invisionradius && inshootradius)
        {

            shootplayer();
        }
    }
    private void pursueplayer()
    {
        if (enemybody.SetDestination(playerbody.position))
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
    private void shootplayer()
    {
        enemybody.SetDestination(transform.position);
        transform.LookAt(lookpoint);


        if (!previouslyshoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootingraycastarea.transform.position, shootingraycastarea.transform.forward, out hit, shootradius + 0.5f))
            {
                muzzleflash.Play();
                a.PlayOneShot(shootingsound);
                // Debug.Log("shooting " + hit.transform.name);
                player_movement player = hit.transform.GetComponent<player_movement>();
                if (player != null)
                {
                    player.playerhit(givedamage);
                    transform.LookAt(hit.transform);
                    GameObject blood = Instantiate(bloodeffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(blood, 1f);
                }
                playerai playerai = hit.transform.GetComponent<playerai>();
                if (playerai != null)
                {
                    playerai.paihit(givedamage);
                    transform.LookAt(hit.transform);
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
    public void enemyhit(float takedamage)
    {
        presentehealth -= takedamage;
        if (presentehealth <= 0)
        {
            StartCoroutine(respawn());
        }
    }
    IEnumerator respawn()
    {
        enemybody.SetDestination(transform.position);
        enemyspeed = 0f;
        inshootradius = false;
        invisionradius = false;
        shootradius = 0f;
        visionradius = 0f;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        eanim.SetBool("shooting", false);
        eanim.SetBool("running", false);
        eanim.SetBool("die", true);
        sm.pkills += 1;
        //Debug.Log("Died");
        //die ani
        yield return new WaitForSeconds(3f);
        // Debug.Log("respawned");
        enemycharacter.position = spawn.position;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        eanim.SetBool("shooting", false);
        eanim.SetBool("running", true);
        eanim.SetBool("die", false);
        enemyspeed = 1.9f;
        inshootradius = false;
        invisionradius = true;
        ehealth = 100f;
        shootradius = 10f;
        visionradius = 100f;
        pursueplayer();


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootradius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionradius);
    }

}
