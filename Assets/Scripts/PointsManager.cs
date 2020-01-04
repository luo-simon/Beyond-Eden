using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int playerPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerPoints"))
        {
            PlayerPrefs.SetInt("PlayerPoints", playerPoints);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("PlayerPoints", playerPoints);
            Debug.Log("PlayerPoints: " + PlayerPrefs.GetInt("PlayerPoints"));
        }
    }

    public void ChangePoints(int points)
    {
        Debug.Log("PlayerPoints change: " + points);
        
        playerPoints = PlayerPrefs.GetInt("PlayerPoints") + points;
        PlayerPrefs.SetInt("PlayerPoints", playerPoints);

        Debug.Log("PlayerPoints now: " + PlayerPrefs.GetInt("PlayerPoints"));

        PlayerPrefs.Save();
    }
}
