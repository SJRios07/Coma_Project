using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAScript : MonoBehaviour
{
    public SctPlayerController player;
    public AudioSource portalAsource;
    public AudioClip portalClip;

    // Start is called before the first frame update
    void Start()
    {
        portalAsource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<SctPlayerController>();

            if (player != null)
            {
                Debug.Log("Activao");
                portalAsource.clip = portalClip;
                portalAsource.PlayOneShot(portalClip);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SctPlayerController>() == player)
        {

            player = null;
            portalAsource.Stop();
        }
    }

}
