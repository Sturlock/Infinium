using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public GameObject FlameWave;
    public GameObject target;
    private bool cooldown = false;
    Quaternion rot;

    GameObject clone;

    



    

    void Start()
    {
       var scollider = GetComponent<SphereCollider>();
        FlameWave.SetActive(false);
        scollider.enabled = false;
    }
    

    void Update()
    {
        Vector3 currentRot = new Vector3(90, 0, 0);
        rot.eulerAngles = currentRot;

        if (Input.GetKeyDown(KeyCode.W) && cooldown == false)
        {
            FlameWave.SetActive(true);
            clone = Instantiate(FlameWave, target.transform.position, rot);
            
            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
            
            
        }
           
       
    }

    private void OnEnable()
    {
        
    }
    void ResetCooldown()
    {
        cooldown = false;
        FlameWave.SetActive(false);
        
    }
        
    

}
