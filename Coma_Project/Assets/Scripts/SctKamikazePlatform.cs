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
    Quaternion rotIni;
    float deltaTime;
    Rigidbody platformRB;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fall = false;
        respawn = false;
        move = false;
        playerOn = false;
        posIni = transform.position;
        rotIni = transform.rotation;
        platformRB = gameObject.GetComponent<Rigidbody>();
        platformRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            platformRB.AddForce(transform.right * moveDir * speed, ForceMode.Force);
            if (playerOn)
            {
                deltaTime = Time.deltaTime * 500;
                if (Input.GetKey(KeyCode.D))
                {
                    platformRB.AddForce(transform.right * -player.gameObject.GetComponent<SctPlayerController>().groundSpeed * deltaTime, ForceMode.Force);
                } 
                if (Input.GetKey(KeyCode.A))
                {
                    platformRB.AddForce(transform.right * player.gameObject.GetComponent<SctPlayerController>().groundSpeed * deltaTime, ForceMode.Force);
                }
            }
        }
        if (fall && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                platformRB.constraints = RigidbodyConstraints.None;
                platformRB.useGravity = true;
                platformRB.AddForce(Vector3.down * 10, ForceMode.VelocityChange);
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
                transform.rotation = rotIni;
                platform.SetActive(true);
                //gameObject.GetComponent<BoxCollider>().enabled = true;
                platformRB.velocity = Vector3.zero;
                platformRB.constraints = RigidbodyConstraints.FreezeAll;
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
            { //if the top side hit something
                move = true;
                playerOn = true;
                platformRB.AddForce(transform.right * moveDir * speed, ForceMode.VelocityChange);
                player = collision.gameObject;
                platformRB.constraints -= RigidbodyConstraints.FreezePositionX;
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
        if (platformRB.velocity.x > maxSpeed)
        {
            platformRB.AddForce(transform.right * moveDir * speed, ForceMode.Force);
        }
        if (platformRB.velocity.x < -maxSpeed)
        {
            platformRB.AddForce(transform.right * moveDir * speed, ForceMode.Force);
        }
    }
}
