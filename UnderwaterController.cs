using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterController : MonoBehaviour
{
    public float WATER_LEVEL;
    public AudioSource UNDERWATER_SOUNDS;
    public AudioSource DESERT_SOUNDS;

    private bool isUnderwater;

    private Color abovewaterColour;
    private Color underwaterColour;
    

    // Start is called before the first frame update
    void Start()
    {
        isUnderwater = false;

        abovewaterColour = ConvertColor(206, 123, 60); // Set initial above water colour to desert fog
        underwaterColour = ConvertColor(0, 101, 111); // Set underwater colour to blue
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < WATER_LEVEL && !isUnderwater) // If under water level and isUnderwater variable not set
        {
            isUnderwater = true;
            SetUnderwater();
        }
        if (transform.position.y > WATER_LEVEL && isUnderwater) // If above water level and isUnderwater variable set to underwater
        {
            isUnderwater = false;
            SetAbovewater();
        }
    }

    void SetAbovewater()
    {
        // Set fog colour and density to normal settings
        RenderSettings.fogColor = abovewaterColour;
        RenderSettings.fogDensity = 0.003f;

        // Stop underwater abient noise and replay the desert noise
        UNDERWATER_SOUNDS.Stop();
        DESERT_SOUNDS.Play();
    }

    void SetUnderwater()
    {
        // Set fog colour and density to underwater settings
        RenderSettings.fogColor = underwaterColour;
        RenderSettings.fogDensity = 0.08f;


        // Stop the desert sounds and play the underwater ambient noise
        DESERT_SOUNDS.Stop();
        UNDERWATER_SOUNDS.Play();
    }

    Color ConvertColor(int r, int g, int b)
    {
    // Helper method to return Color object from integer RGB values
    return new Color(r/255.0f, g/255.0f, b/255.0f);
    }
}
