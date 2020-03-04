using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [HEADER("Player One")]                              //Hierarchy Attribute for the Inspector
    //<-------------------------------- Player -------------------------------->
    public PlayerController Player1;                    //Instance of the Player Controller on the Player
    private Rigidbody2D rb;                             //Instance of the Rigidbody Object on the Player
    private Animator anim;                              //Instance of the Animator on the Player
    [Space]

    [HEADER("Player Movement Logic")]                   //Hierarchy Attribute for the Inspector
    //<-------------------------------- Player Movement -------------------------------->
    private float mvSpeed = 5f;                         //Default Player Move Speed
    public float horizontalMove = 0f;
    private Vector3 velocity;                           //Vector in which the Player moves
    [Space]

    [HEADER("Player Jump Logic")]                       //Hierarchy Attribute for the Inspector
    //<-------------------------------- Player Jump -------------------------------->
    public float jumpForce = 150f;                      //Default Player Jump Force
    private bool grounded;                              //Boolean to set Player True/False depending on if grounded
    public Transform groundCheck;                       //GameObject to check for the Ground
    public LayerMask groundLayer;                       //LayerMask to check for an Object on Layer tagged Ground

    //<-------------------------------- Awake -------------------------------->
    void Awake() {
        rb = GetComponent<Rigidbody2D>();               //On Awake, grab the Rigidbody2D Component
        anim = GetComponent<Animator>();                //On Awake, grab the Animator Component
        Player1 = GetComponent<PlayerController>();     //On Awake, grab the Player Controller 
    }

    //<-------------------------------- Update -------------------------------->
    void Update() {
        //Player One Controls
        horizontalMove = Input.GetAxisRaw("Horizontal") * mvSpeed;
        anim.SetFloat("Horizontal", horizontalMove);

        if(grounded && Input.GetKeyDown("Space")) {
            grounded = false;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            
        }
        anim.SetFloat("Speed", velocity.sqrMagnitude);
    }

    //<-------------------------------- Fixed Update -------------------------------->
    void FixedUpdate() {
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = targetVelocity;

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
    }
}