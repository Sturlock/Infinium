using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuController : MonoBehaviour
{

    [SerializeField] GameObject questUI;
    private bool questOpen = false;

    // Start is called before the first frame update
    void Awake()
    {
        questUI = GameObject.FindGameObjectWithTag("QuestUI");
        questUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowQuestUI();
    }

    private void ShowQuestUI()
    {
        if (Input.GetKeyDown(KeyCode.J) && !questOpen)
        {
            questUI.SetActive(true);
            questOpen = true;
            return;
        }
        if (Input.GetKeyDown(KeyCode.J) && questOpen)
        {
            questUI.SetActive(false);
            questOpen = false;
            return;
        }
    }
}
