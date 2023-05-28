using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitPlay()
    {
        Application.Quit();
    }

    public void Restart()
    {
        PermanentUI.perm.health = 3;
        //PermanentUI.perm.healthAmount.text = PermanentUI.perm.health.ToString();
        //PermanentUI.perm.cherries = 0;
        //PermanentUI.perm.cherryText.text = PermanentUI.perm.cherries.ToString();
        SceneManager.LoadScene("Level 1");
    }

    public void References()
    {
        SceneManager.LoadScene("Ref Level");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
