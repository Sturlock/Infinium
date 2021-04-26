using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantGameObjects : MonoBehaviour
{
    [SerializeField] GameObject[] persistantObjects;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (GameObject i in persistantObjects)
        {
            DontDestroyOnLoad(i);
        }
    }
}
