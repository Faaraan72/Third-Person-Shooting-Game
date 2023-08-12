using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{   
    public float phealth =100f;
    public float currentphealth;

    public Text healthtxt;

    public float speed = 1.9f;
    public float currentspeed = 0f;
    public float sprint = 3f;
    public float currentsprint = 0f;
    public CharacterController cc;

    float gravity = -9.8f;
    public Animator anim;
    Vector3 velocity;
    public Transform surfacecheck;
    public float surfacedistance = 0.4f;
    bool onsurface;
    public LayerMask surfacemask;

  

    float turnangle = 0.1f;
    float turnvelocity;

    public Transform pcamera;
    float targetangle;
    private Vector3 aim;

    //public bool ismobile = false;
   // public FixedJoystick joystick;
    //public FixedJoystick spjoystick;
   // public float mousesen = 100f;

   // public Transform spine;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentphealth = phealth;
        healthtxt.text = ((int)phealth)+ "";
        
    }

    private void Update()
    {
        onsurface = Physics.CheckSphere(surfacecheck.position, surfacedistance, surfacemask);
        if(onsurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //gravity
         velocity.y += gravity * Time.deltaTime;
         cc.Move(velocity * Time.deltaTime);
        //float mouseY = Input.GetAxis("Mouse Y") * mousesen * Time.deltaTime;
        //spine.Rotate(Vector3.up * mouseY);



        pmove();
        psprint();
        jump();
        shootinganimation();
      

    }
    
    void shootinganimation()
    {
       
        if (Input.GetButton("Fire1"))
        {
            anim.SetBool("fire", true);
            
        }
        else
        {
            anim.SetBool("fire", false);
        }
    }
    void pmove()
    {
        
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v).normalized;
            if (dir.magnitude > 0.1f)

            {   //animations

                anim.SetBool("walk", true);
                anim.SetBool("running", false);
                anim.SetBool("idle", false);
                anim.SetTrigger("jump");
                //---------> // anim.SetBool("idleaim", false);
                //------------->// anim.SetBool("aimWalk", false);
                //changing angle of player while turning
                // Atan2 --> is A TAN 2(X/Y) 
                targetangle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + pcamera.eulerAngles.y;
                //Smoothdampangle --> gradually change angle to desired angle
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnvelocity, turnangle);
                transform.rotation = Quaternion.Euler(0f, angle, 0f); //apply  rotation on transform of obj
                                                                      //player rotation due to camera
                Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
                //movement
                cc.Move(movedir.normalized * speed * Time.deltaTime);
                currentspeed = speed;
            }
            else
            {
                anim.SetBool("idle", true);
                anim.SetBool("walk", false);
                anim.SetBool("running", false);
                anim.SetTrigger("jump");
                anim.SetBool("aimWalk", false);
                currentspeed = 0f;
            }
        }
    
    public float jumprange = 1f;
    void jump()
    {
        if(Input.GetButtonDown("Jump")&& onsurface)
        {
            anim.SetBool("walk", false);
            
            anim.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(jumprange * -2 * gravity);
        }
        else
        {
            anim.ResetTrigger("jump");
        }
    }


    void psprint()
    {
            if (Input.GetButton("sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onsurface)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                Vector3 dir = new Vector3(h, 0, v).normalized;
                if (dir.magnitude > 0.1f)

                {

                    anim.SetBool("walk", false);
                    anim.SetBool("running", true);
                    anim.SetBool("idle", false);
                    // anim.SetTrigger("Jump");
                    anim.SetBool("idleaim", false);
                    //anim.SetBool("aimwalk", false);

                    //changing angle of player while turning
                    // Atan2 --> is A TAN 2(X/Y) 
                    float targetangle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + pcamera.eulerAngles.y;
                    //Smoothdampangle --> gradually change angle to desired angle
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnvelocity, turnangle);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f); //apply  rotation on transform of obj
                                                                          //player rotation due to camera
                    Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
                    //movement
                    cc.Move(movedir.normalized * sprint * Time.deltaTime);
                    currentsprint = sprint;
                }
                else
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("idle", false);
                    currentsprint = 0f;
                }
            }
        }
    

   public void playerhit(float takedamage)
    {
        currentphealth -= takedamage;
        if (currentphealth >= 0)
        {
            healthtxt.text = ((int)currentphealth) + "";
        }
        if (currentphealth < 0)
        {
            healthtxt.text ="0";
            playerdie();
        }
    }
    public scoremanager sm;
    void playerdie()
    {
        Cursor.lockState = CursorLockMode.None;
        gameObject.GetComponent<CharacterController>().enabled = false;
        
        anim.SetBool("die", true);
        Invoke(nameof(enewins),2f);

    }
    public void enewins()
    {
        sm.enekills = 10;
    }
}
