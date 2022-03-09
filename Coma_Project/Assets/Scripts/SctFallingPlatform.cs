using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctFallingPlatform : MonoBehaviour
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
                respawn = false;
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerSJ" && !fall && !respawn)
        {
            timer = fallTime;
            fall = true;
        }
    }
}
