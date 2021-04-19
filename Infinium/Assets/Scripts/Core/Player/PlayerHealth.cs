using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void PlayerTakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
