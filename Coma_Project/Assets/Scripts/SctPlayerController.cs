using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctPlayerController : MonoBehaviour
{
    public float groundSpeed = 10f;
    public float airSpeed = 5f;
    public float jumpForce = 20f;
    public float maxSpeed = 40f;
    float speedPlayer;

    bool canJump;
    bool doubleJump;

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    Rigidbody playerRB;
    public bool hasKey;

    [HideInInspector]
    public Vector3 posIni;
    public bool dead;

    GUIManager guimanager;
    float deltaTime;

    public Animator leerAnimator;
    public GameObject playerGeo;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        playerRB = gameObject.GetComponent<Rigidbody>();
        posIni = transform.position;
        speedPlayer = 0;
        hasKey = false;
        dead = false;
        guimanager = FindObjectOfType<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        speedPlayer = (canJump) ? groundSpeed:airSpeed;
        deltaTime = Time.deltaTime * 500;

        if (!dead)
        {
            if (Input.GetKey(KeyCode.D))
            {
                playerRB.AddForce(transform.right * speedPlayer * deltaTime, ForceMode.Force);
                playerGeo.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerRB.AddForce(-transform.right * speedPlayer * deltaTime, ForceMode.Force);
                playerGeo.transform.eulerAngles = new Vector3(0, 270, 0);
            }
            if (Input.GetKey(KeyCode.S) && !canJump)
            {
                playerRB.AddForce(-transform.up * speedPlayer * deltaTime, ForceMode.Force);
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
        }
        else
        {
            playerRB.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = posIni;
            playerRB.AddForce(Vector3.zero, ForceMode.VelocityChange);
            guimanager.AddLife(100);
            playerRB.constraints = RigidbodyConstraints.FreezeAll;
            playerRB.constraints -= RigidbodyConstraints.FreezePositionX;
            playerRB.constraints -= RigidbodyConstraints.FreezePositionY;
            dead = false;
        }

        //The step size is equal to speed times frame time.
        var step = speedPlayer * Time.deltaTime;

        // Rotate our transform a step closer to the target's.

        checkVelocity();
        HandleAnimation();
    }

    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        playerRB.AddForce(gravity, ForceMode.Acceleration);
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

    public void HandleAnimation()
    {
        //leerAnimator.SetBool("IsGrounded", canJump);

        if (playerRB.velocity.x > 0.01f || playerRB.velocity.x < -0.01f)
        {
            leerAnimator.SetBool("Running", true);
        }
        else
        {
            leerAnimator.SetBool("Running", false);
        }
        if ((playerRB.velocity.y > 0.01f || playerRB.velocity.y < -0.01f) && !canJump)
        {
            leerAnimator.SetBool("Jummping", true);
        }
        else
        {
            leerAnimator.SetBool("Jummping", false);
        }
    }

    public void checkVelocity()
    {
        if (playerRB.velocity.x > maxSpeed)
        {
            playerRB.AddForce(transform.right * -speedPlayer * deltaTime, ForceMode.Force);
        }
        if (playerRB.velocity.x < -maxSpeed)
        {
            playerRB.AddForce(transform.right * speedPlayer * deltaTime, ForceMode.Force);
        }
    }
}
