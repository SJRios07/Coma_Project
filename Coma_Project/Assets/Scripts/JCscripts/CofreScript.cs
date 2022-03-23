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

    public bool claimedKey;

    bool keyCreated;

    public AudioSource cofreAsource;
    public AudioClip cofreClip;


    // Start is called before the first frame update
    void Start()
    {
        SetTextMessage(msgabrircofre, false);
        keyCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && Chest.GetComponent<Animator>().GetBool("OpenChest") && !claimedKey)
            {
                keyCopy.SetActive(false);
                cofreAsource.clip = cofreClip;
                cofreAsource.PlayOneShot(cofreClip);
                player.GetComponent<SctPlayerController>().hasKey = true;
                claimedKey = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && Chest.GetComponent<Animator>().GetBool("OpenChest") == false )
            {
                cofreAsource.clip = cofreClip;
                cofreAsource.PlayOneShot(cofreClip);
                Chest.GetComponent<Animator>().SetBool("OpenChest", true);
                SetTextMessage(msgabrircofre, true);

                if (!claimedKey && !keyCreated)
                {
                    keyCopy = Instantiate(prefabKey);
                    keyCopy.transform.position = aimerkey.position;
                    keyCreated = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<SctPlayerController>();

            if (player != null)
            {
                //Entro un player
                isOnChest = true;
                SetTextMessage(msgabrircofre, true);
            }
        }
       
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