using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ChangeLightColour : MonoBehaviour
{
    public Light2D lightSource;

    public Color dayColour;
    public Color nightColour;
    public float duration;

    [SerializeField]
    private Color currentColour;
    
    void Start()
    {
        lightSource = GetComponent<Light2D>();
        lightSource.color = nightColour;
    }

    void Update()
    {
        currentColour = lightSource.color;

        float t = Mathf.PingPong(Time.time, duration) / duration;
        lightSource.color = Color.Lerp(nightColour, dayColour, t);
    }

    void ChangeToDay()
    {
        lightSource.color -= (dayColour / 12f) * Time.deltaTime;
    }

    void ChangeToNight()
    {
        lightSource.color -= (nightColour / 12f) * Time.deltaTime;
    }
}
