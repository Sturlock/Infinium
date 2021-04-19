using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        if(gameOverCanvas != null)
            gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        GetComponent<PlayerController>().enabled = false;
        //GetComponent<Shooting>().enabled = false;
        //gameOverCanvas.enabled = true;
        

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
