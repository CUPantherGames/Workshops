using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //<-------------------------------- Player -------------------------------->
    public PlayerController Player;                    //Instance of the Player Controller on the Player
    private Rigidbody2D rb;                             //Instance of the Rigidbody Object on the Player
    private Animator anim;                              //Instance of the Animator on the Player
    [Space]
    [Header("Movement Logic")]                          //Hierarchy Attribute for the Inspector
    //<-------------------------------- Player Movement -------------------------------->
    private float mvSpeed = 20f;                         //Default Player Move Speed
    public float horizontalMove;
    private Vector3 velocity;                           //Vector in which the Player moves
    bool facingRight = true;
    [Space]

    [Header("Jump Logic")]                              //Hierarchy Attribute for the Inspector
    //<-------------------------------- Player Jump -------------------------------->
    public float jumpForce = 150f;                      //Default Player Jump Force
    private bool grounded;                              //Boolean to set Player True/False depending on if grounded
    public Transform groundCheck;                       //GameObject to check for the Ground
    public LayerMask groundLayer;                       //LayerMask to check for an Object on Layer tagged Ground

    //<-------------------------------- Awake -------------------------------->
    void Awake() {
        rb = GetComponent<Rigidbody2D>();               //On Awake, grab the Rigidbody2D Component
        anim = GetComponent<Animator>();                //On Awake, grab the Animator Component
        Player = GetComponent<PlayerController>();     //On Awake, grab the Player Controller
    }

    //<-------------------------------- Update -------------------------------->
    void Update() {
        //Player One Controls
        horizontalMove = Input.GetAxisRaw("Horizontal") * mvSpeed;
        anim.SetFloat("Horizontal", Mathf.Abs(horizontalMove));

        if(grounded && Input.GetKeyDown("space")) {
            grounded = false;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));

        }
        anim.SetFloat("Speed", velocity.sqrMagnitude);

        if(horizontalMove > 0 && !facingRight) {
		      Flip();
			  }
			  else if(horizontalMove < 0 && facingRight) {
		      Flip();
			  }
    }

    //<-------------------------------- Fixed Update -------------------------------->
    void FixedUpdate() {
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = targetVelocity;

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        anim.SetBool("Grounded", grounded);
    }

    //<-------------------------------- Flip -------------------------------->
    void Flip() {
  		facingRight = !facingRight;
  		Vector3 theScale = transform.localScale;
  		theScale.x *= -1;
  		transform.localScale = theScale;
	  }
}
