using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public GameObject main;
    public GameObject last;
    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(true);
        last.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        main.SetActive(false);
        last.SetActive(true);
    }
}
