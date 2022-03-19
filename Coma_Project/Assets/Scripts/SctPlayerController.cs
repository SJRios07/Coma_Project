using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctPlayerController : MonoBehaviour
{
    public float groundSpeed;
    public float airSpeed;
    public float jumpForce;
    float speedPlayer;

    bool canJump;
    bool doubleJump;

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    Rigidbody playerRB;

    [HideInInspector]
    public Vector3 posIni;


    public Animator leerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        playerRB = gameObject.GetComponent<Rigidbody>();
        posIni = transform.position;
        speedPlayer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        speedPlayer = (canJump) ? groundSpeed:airSpeed;

        if (Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(transform.right * speedPlayer, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(-transform.right * speedPlayer, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S) && !canJump)
        {
            playerRB.AddForce(-transform.up * speedPlayer, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = posIni;
            playerRB.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }

        HandleAnimation();
    }

    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        playerRB.AddForce(gravity, ForceMode.Acceleration);


        
     }


    public void HandleAnimation()
    {
        if (playerRB.velocity.magnitude > 0.01f)
        {
            leerAnimator.SetBool("Running", true);
        }
        else
        {
            leerAnimator.SetBool("Running", false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            var normal = collision.contacts[0].normal;
            if (normal.y > 0)
            { //if the bottom side hit something 
                canJump = true;
                doubleJump = true;
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            canJump = false;
        }
    }
}
