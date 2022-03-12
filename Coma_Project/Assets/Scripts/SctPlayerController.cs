using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctPlayerController : MonoBehaviour
{
    public float speedPlayer;
    public float jumpForce;
    bool canJump;
    bool doubleJump;
    Vector3 posIni;
    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    Rigidbody m_rb;
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        m_rb = gameObject.GetComponent<Rigidbody>();
        posIni = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speedPlayer * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speedPlayer * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && !canJump)
        {
            transform.Translate(0, -speedPlayer * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump!");
            canJump = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump!");
            doubleJump = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = posIni;
        }
    }

    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
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
                Debug.Log("Yay!");
            }
        }
        Debug.Log(collision.gameObject.name);

    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Piso")
        {
            canJump = false;
        }
    }
}
