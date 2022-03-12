using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctKamikazePlatform : MonoBehaviour
{
    public GameObject platform;
    public float fallTime;
    public float respawnTime;
    public float moveDir;
    public float speed;
    float timer;
    bool fall;
    bool respawn;
    bool move;
    Vector3 posIni;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fall = false;
        respawn = false;
        move = false;
        posIni = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            //transform.Translate(moveDir * speed, 0, 0);
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * moveDir * speed, ForceMode.Force);
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
                respawn = false;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Carajo" + collision.gameObject.tag);
        if (collision.gameObject.name == "PlayerSJ" && !fall && !respawn)
        {
            var normal = collision.contacts[0].normal;
            if (normal.y < 0)
            { //if the bottom side hit something
                move = true;
            }
        }
        if (collision.gameObject.tag == "Piso" && move)
        {
            timer = fallTime;
            fall = true;
            Debug.Log("Golpeo");
            move = false;
        }

    }

    public void OnCollisionStay(Collision collision)
    {

    }
}
