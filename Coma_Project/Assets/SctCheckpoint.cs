using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctCheckpoint : MonoBehaviour
{
    public Light spotlight;
    public Color colorInicial;
    public Color colorActivado;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spotlight.color = colorInicial;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<SctPlayerController>().posIni != transform.position)
        {
            spotlight.color = colorInicial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<SctPlayerController>().posIni = transform.position;
            spotlight.color = colorActivado;
        }
    }

}
