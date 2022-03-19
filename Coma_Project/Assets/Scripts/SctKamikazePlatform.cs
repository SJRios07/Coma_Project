using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctKamikazePlatform : MonoBehaviour
{
    public GameObject platform;
    GameObject player;
    public float fallTime;
    public float respawnTime;
    public float moveDir;
    public float speed;
    public float maxSpeed = 30f;
    float timer;
    bool fall;
    bool respawn;
    bool move;
    bool playerOn;
    Vector3 posIni;
    float deltaTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fall = false;
        respawn = false;
        move = false;
        playerOn = false;
        posIni = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * moveDir * speed, ForceMode.Force);
            if (playerOn)
            {
                deltaTime = Time.deltaTime * 500;
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -player.gameObject.GetComponent<SctPlayerController>().groundSpeed * deltaTime, ForceMode.Force);
                } 
                if (Input.GetKey(KeyCode.A))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.right * player.gameObject.GetComponent<SctPlayerController>().groundSpeed * deltaTime, ForceMode.Force);
                }
            }
        }
        if (fall && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                platform.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                fall = false;
                respawn = true;
                timer = respawnTime;
            }
        }
        else if (respawn && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                transform.position = posIni;
                platform.SetActive(true);
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                respawn = false;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !fall && !respawn)
        {
            var normal = collision.contacts[0].normal;
            if (normal.y < 0)
            { //if the bottom side hit something
                move = true;
                playerOn = true;
                gameObject.GetComponent<Rigidbody>().AddForce(transform.right * moveDir * speed, ForceMode.VelocityChange);
                player = collision.gameObject;
            }
        }
        if (collision.gameObject.tag == "Piso" && move)
        {
            timer = fallTime;
            fall = true;
            move = false;
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        playerOn = false;
    }

    public void checkVelocity()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.x > maxSpeed)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * moveDir * speed, ForceMode.Force);
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.x < -maxSpeed)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * moveDir * speed, ForceMode.Force);
        }
    }
}
