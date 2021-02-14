using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CigController : MonoBehaviour
{
    public GameObject SMOKE;
    public Text GUI_TEXT;
    public AudioSource CIG_SOUND;

    private bool isInside;
    private bool isTextDisplayed;
    private bool isSoundPlayed;

    void Start()
    {
        this.isTextDisplayed = false;
        this.isSoundPlayed = false;
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
            isInside = true; //If player object enters cig collider set flag to true
        }
        if (!isTextDisplayed)
        {
            // Display helper message if it hasn't been displayed allready
            isTextDisplayed = true;
            StartCoroutine(ShowMessage("Press 'e' To Put Out Cigarette..."));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false; //If player object exits cig collider set flag to false
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            if (!isSoundPlayed)
            {
                // Only play the extiguish cig sound if it hasn't allready been played
                isSoundPlayed = true;
                CIG_SOUND.Play();
            }
            SMOKE.SetActive(false); //Disable the smoke particle effect
        }
    }
}
