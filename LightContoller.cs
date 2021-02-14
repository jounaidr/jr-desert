using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightContoller : MonoBehaviour
{

    public GameObject LIGHT_SOURCE;
    public Text GUI_TEXT;
    public AudioSource LIGHT_SOUND;

    private bool isOn;
    private bool isInside;
    private bool isTextDisplayed;

    void Start()
    {
        isOn = LIGHT_SOURCE.GetComponent<Light>().enabled; // Get current light status
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
            isInside = true; //If player object enters light collider set flag to true
        }
        if (!isTextDisplayed)
        {
            // Display helper message if it hasn't been displayed allready
            isTextDisplayed = true;
            StartCoroutine(ShowMessage("Press 'e' To Use Lamp..."));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false; //If player object exits light collider set flag to false
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            // On e-key press when inside trigger collider, play light sound...
            LIGHT_SOUND.Play();
            if (!isOn)
            {
                // If light is not on, turn it on
                isOn = true;
                LIGHT_SOURCE.GetComponent<Light>().enabled = isOn;
            }
            else
            {
                // If light is on, turn it off
                isOn = false;
                LIGHT_SOURCE.GetComponent<Light>().enabled = isOn;
            }
        }
    }
}
