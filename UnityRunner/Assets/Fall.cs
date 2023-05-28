using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerFell;

    // Time in seconds to delay (five seconds for example):
    public float delayTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerFell.Play();
            Invoke(nameof(DelayedAction), delayTime);
            PermanentUI.perm.Reset();
        }
    }
    void DelayedAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
