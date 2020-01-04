using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string TargetSceneName;
    public bool changeOnCollision = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && other.CompareTag("Player") && !changeOnCollision)
        {
            SceneManager.LoadScene(TargetSceneName);
        }
        
        if (changeOnCollision)
        {
            SceneManager.LoadScene(TargetSceneName);
        }
    }

}
