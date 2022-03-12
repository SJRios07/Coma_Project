using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctFallingPlatformV2 : MonoBehaviour
{
    public GameObject platform;
    public float fallTime;
    public float respawnTime;
    float timer;
    bool fall;
    bool respawn;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fall = false;
        respawn = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                platform.SetActive(true);
                gameObject.GetComponent<BoxCollider>().enabled = true;
                respawn = false;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerSJ" && !fall && !respawn)
        {
            var normal = collision.contacts[0].normal;
            if (normal.y < 0)
            { //if the bottom side hit something
                timer = fallTime;
                fall = true;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        var normal = collision.contacts[0].normal;
        if (normal.y < 0 && !fall)
        { //if the bottom side hit something
            timer = fallTime;
            fall = true;
        }
    }
}
