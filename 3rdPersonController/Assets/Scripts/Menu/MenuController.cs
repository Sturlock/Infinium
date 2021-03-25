using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject startCanvas;


    public void StartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextMenu(GameObject nxtCanvas)

    {
        nxtCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}
