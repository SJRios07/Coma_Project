using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreScript : MonoBehaviour
{
    public TextMesh textoFeedback;
    public string msgabrircofre;

    public SctPlayerController player;

    public GameObject prefabKey;
    private GameObject keyCopy;

    public Transform aimerkey;

    public bool isOnChest;

    public GameObject Chest;

    public bool hasKey;
    

    // Start is called before the first frame update
    void Start()
    {
        SetTextMessage(msgabrircofre, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            if (Input.GetKeyDown(KeyCode.E) && Chest.GetComponent<Animator>().GetBool("OpenChest") == true && hasKey == false)

            {
                Debug.Log("ENTRANDO SIN RAZPN");
                Destroy(keyCopy);
                keyCopy.SetActive(false);
                hasKey = true;
                //Chest.GetComponent<Animator>().SetBool("OpenChest", false);
                //keyTemp.GetComponent<Renderer>().enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && Chest.GetComponent<Animator>().GetBool("OpenChest") == false )
            {
                Debug.Log("Presiono E");
                Chest.GetComponent<Animator>().SetBool("OpenChest", true);
                SetTextMessage(msgabrircofre, true);

                if (hasKey == false)

                {
                    keyCopy = Instantiate(prefabKey);
                    keyCopy.transform.position = aimerkey.position;

                }
              

                //Destroy(key);

               

            }

        
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<SctPlayerController>();

        if (player != null)
        {
            

            //Entro un player
            isOnChest = true;
            SetTextMessage(msgabrircofre, true);


        }


        Debug.Log ("Entrounplayer");
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SctPlayerController>() == player)
        {

            Chest.GetComponent<Animator>().SetBool("OpenChest", false);
            isOnChest = false;
            player = null;
            SetTextMessage(msgabrircofre, false);
            
        }
    }


    void SetTextMessage(string message, bool isActive)
    {
        textoFeedback.text = message;
        textoFeedback.gameObject.SetActive(isActive);
    }

}