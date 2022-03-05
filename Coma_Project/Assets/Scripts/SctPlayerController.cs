using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctPlayerController : MonoBehaviour
{
    public float speedPlayer;
    public float jumpForce;
    public bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
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
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump!");
            canJump = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Piso")
        {
            Debug.Log("Tierra firrrrrrme!");
            canJump = true;
        }
        Debug.Log(collision.gameObject.name);
    }
}
