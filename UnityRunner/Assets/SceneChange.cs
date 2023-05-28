using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource levelComplete;

    // Time in seconds to delay (five seconds for example):
    public float delayTime = 5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelComplete.Play();
            Invoke(nameof(DelayedAction), delayTime);
        }
    }

    void DelayedAction()
    {
        SceneManager.LoadScene(sceneName);
    }

}
