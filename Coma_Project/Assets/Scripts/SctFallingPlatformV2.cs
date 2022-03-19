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
    Vector3 posIni;
    Quaternion rotIni;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        fall = false;
        respawn = false;
        posIni = transform.position;
        rotIni = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (fall && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                //platform.SetActive(false);
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                fall = false;
                respawn = true;
                timer = respawnTime;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 10, ForceMode.VelocityChange);
            }
        }
        else if (respawn && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                //gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                transform.position = posIni;
                transform.rotation = rotIni;
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
                timer = fallTime;
                fall = true;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        var normal = collision.contacts[0].normal;
        if (normal.y < 0 && !fall && collision.gameObject.tag == "Player")
        { 
            timer = fallTime;
            fall = true;
        }
    }
}
