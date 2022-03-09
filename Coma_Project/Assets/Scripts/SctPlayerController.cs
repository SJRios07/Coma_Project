using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctPlayerController : MonoBehaviour
{
    public float speedPlayer;
    public float jumpForce;
    bool canJump;
    bool doubleJump;
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
            Debug.Log("Tierra firrrrrrme!");
            canJump = true;
            doubleJump = true;
        }
        Debug.Log(collision.gameObject.tag);
    }
}
