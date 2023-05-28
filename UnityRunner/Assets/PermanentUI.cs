using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    //Player Stats
    //public int cherries = 0;
    public int health = 3;
    //public TextMeshProUGUI cherryText;
    //public TextMeshProUGUI healthAmount;

    public Animator anim;
    public GameObject HP2;
    public GameObject HP1;

    public static PermanentUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        //Singleton
        if (!perm)
        {
            perm = this; // This = current instance //
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {

        if (health == 3)
        {
            anim.speed = 1f;
            anim.enabled = (false);
            HP2.SetActive(true);
            HP1.SetActive(true);
        }
        if (health == 2)
        {
            HP2.SetActive(false);
            anim.enabled = (true);
        }
        if (health == 1)
        {
            HP1.SetActive(false);
            anim.speed = 2f;
        }
    }
    public void Reset()
    {
        //cherries = 0;
        //cherryText.text = cherries.ToString();
    }
}
