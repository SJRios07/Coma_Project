using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPuerta : MonoBehaviour
{
    public TextMesh textoFeedback;
    public string msgInserte;
    public string msgNokey;

    public SctPlayerController player;
    public bool hasKey;

    public bool isOnDoor;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Presiono E");
                if (hasKey == true)
                {//Si tiene la llave
                 //SwitchDoor(false);
                    door.GetComponent<Animator>().SetBool("opendoor", true);
                }
                else
                {//No tiene Llave
                    SetTextMessage(msgNokey, true);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<SctPlayerController>();

        if (player != null)
        {
            //Entro un player
            //isOnDoor = true;
            SetTextMessage(msgInserte, true);
   
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SctPlayerController>() == player)
        {

            door.GetComponent<Animator>().SetBool("opendoor", false);
            //isOnDoor = false;
            player = null;
            SetTextMessage("No player", false);
        }
    }


    void SetTextMessage(string message, bool isActive)
    {
        textoFeedback.text = message;
        textoFeedback.gameObject.SetActive(isActive);
    }

    void SwitchDoor(bool isOpen)
    {
        //door.SetActive(isOpen);
    }
}
