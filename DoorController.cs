using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public Animator DOOR_ANIMATOR;
    public Text GUI_TEXT;
    public AudioSource DOOR_SOUND;

    private bool openDoor;
    private bool isInside;
    private bool isTextDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        DOOR_ANIMATOR = GetComponent<Animator>();

        this.openDoor = false;
        this.isTextDisplayed = false;
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
        if(other.gameObject.tag == "Player")
        {
            // Set trigger collider flag to true
            isInside = true;
        }
        if (!isTextDisplayed)
        {
            // Display helper message if it hasn't been displayed allready
            isTextDisplayed = true;
            StartCoroutine(ShowMessage("Press 'e' To Open..."));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Set trigger collider flag to false
            isInside = false;
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            if (!openDoor)
            {
                // If door is closed, set openDoor flag to true locally and in animator (this will cause the open door animation to play...)
                openDoor = true;
                DOOR_ANIMATOR.SetBool("openDoor", openDoor);
                DOOR_SOUND.Play();
            }
            else
            {
                // If door is open, set openDoor flag to false locally and in animator (this will cause the close door animation to play...)
                openDoor = false;
                DOOR_ANIMATOR.SetBool("openDoor", openDoor);
                DOOR_SOUND.Play();
            }
        }
    }
}
