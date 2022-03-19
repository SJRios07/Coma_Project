using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBotiquin : MonoBehaviour
{
    public int healthAmount;
    public SctPlayerController player;

    public GUIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)          
    {
        if (other.gameObject.GetComponent<SctPlayerController>())
        {
            manager.AddLife(healthAmount);
            this.gameObject.SetActive(false);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SctPlayerController>() == player)
        {
        }
     
    }

}






  