using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAitry : MonoBehaviour
{
    public NavMeshAgent agent;
    // public AudioSource fts;
    public Transform player;
    public Animator eanim;
    

    public LayerMask whatIsGround, whatIsPlayer;

    public float health=100f;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    // bool alreadyAttacked = false;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        player = GameObject.Find("player1").transform;
        agent = GetComponent<NavMeshAgent>();
        // pd=player.GetComponent<playerdie>();
    }
     public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && health>0)
        {
             Patroling();
              eanim.SetBool("Attrange", false);
              }
        if (playerInSightRange && !playerInAttackRange && health>0)
         {
            ChasePlayer();
            // fts.Play();
             eanim.SetBool("Attrange", false);
             eanim.SetBool("seen", true);
             }
        if (playerInAttackRange && playerInSightRange && health>0)
        {
             AttackPlayer();
            //  fts.Play();
              }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (health > 0) { agent.SetDestination(player.position); }
    }
    
    public float launchVelocity = 700f;
    private void AttackPlayer()
    {
     //playerdie p =  player.GetComponent<playerdie>();
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        eanim.SetBool("Attrange", true);
            // p.takedamage();
             

        // if (!alreadyAttacked)
        // {  
            //   a.Play();
            //   eshoot();
            // /Attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // /End of attack code
            // Destroy(rb,1f);
            //  GameObject ebullet = Instantiate(projectile, transform.position, transform.rotation);
            //  ebullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0,0,launchVelocity));
            //  Destroy(ball,1f);
             
            // alreadyAttacked = true;
            // pd.takedamage();
        //    Invoke(nameof(ResetAttack), timeBetweenAttacks);

          
    //    }
    }
    // private void ResetAttack()
    // {
    //     alreadyAttacked = false;
    //   eanim.SetBool("Attrange", false);
    // }

   
    // private void DestroyEnemy()
    // {   eanim.SetBool("die" , true);
    //     Destroy(gameObject , 3f);
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
