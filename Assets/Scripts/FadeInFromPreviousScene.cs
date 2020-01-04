using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;  

public class FadeInFromPreviousScene : MonoBehaviour
{
    public Light2D lightSource;
    public float time;

    private bool update = true;

    void Awake()
    {
        lightSource = GetComponentInParent<Light2D>();
        lightSource.color = Color.black;
    }

    void Start()
    {
        StartCoroutine(ToWhite());
    }

    void Update()
    {
        if(update) lightSource.color += (Color.white / time) * Time.deltaTime;
    }

    IEnumerator ToWhite()
    {
        yield return new WaitForSeconds(time);
        update = false;
    }
}
