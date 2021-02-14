using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject TELEPORT_LOCATION;
    public GameObject PLAYER;
    public Text GUI_TEXT;
    public AudioSource TELE_SOUND;

    private bool isInside;
    private bool isTextDisplayed;

    void Start()
    {
        isTextDisplayed = false;
    }

    IEnumerator ShowMessage(string message)
    {
        GUI_TEXT.text = message; //Set the text message

        for (float t = 0.01f; t < 1; t += Time.deltaTime)
        {
            //Fade text in from clear
            GUI_TEXT.color = Color.Lerp(Color.clear, Color.white, Mathf.Min(1, t / 1));
            yield return null;
        }

        yield return new WaitForSeconds(3); //Show text for 3 seconds
        GUI_TEXT.color = Color.clear; //Reset the colour to clear
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = true; //If player object enters teleporter collider set flag to true
        }
        if (!isTextDisplayed)
        {
            // Display helper message if it hasn't been displayed allready
            isTextDisplayed = true;
            StartCoroutine(ShowMessage("Press 'e' To Activate Tablet..."));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false; //If player object exits teleporter collider set flag to false
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            TELE_SOUND.Play(); //Play teleport sound

            isInside = false; //Player will leave the collider

            CharacterController characterController = PLAYER.GetComponent<CharacterController>();

            characterController.enabled = false; //Temporarily disable the character controller
            PLAYER.transform.position = TELEPORT_LOCATION.transform.position;
            characterController.enabled = true; //Re enable character controller
        }
    }
}
