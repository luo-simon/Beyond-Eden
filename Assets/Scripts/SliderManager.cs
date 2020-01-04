using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private TextMeshProUGUI text;

    public float maxPlayerScore;
    private float totalScore;

    public string[] titles = new string[4];

    public bool addPoints = false;
    public bool removePoints = false;


    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        totalScore = maxPlayerScore * 2;
}
    
    void Update()
    {
        UpdateSlider();

        if (slider.value < 0.25)
        {
            text.text = titles[0];
        } else if (slider.value < 0.5)
        {
            text.text = titles[1];
        } else if (slider.value > 0.75)
        {
            text.text = titles[3];
        } else
        {
            text.text = titles[2];
        }

        if (addPoints) ChangePoints(10);
        if (removePoints) ChangePoints(-10);
    }

    void UpdateSlider()
    {
        int points = PlayerPrefs.GetInt("PlayerPoints");
        float result = (float)points / totalScore;
        float value = 0.5f + result;
        slider.value = -value + 1f;
    }

    void ChangePoints(int points)
    {
        PlayerPrefs.SetInt("PlayerPoints", PlayerPrefs.GetInt("PlayerPoints") + points);
        addPoints = false;
        removePoints = false;
    }
}
