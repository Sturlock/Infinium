using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public QuestScriptable quest;
    void Start()
    {
        
        foreach (string task in quest.GetTasks())
        {
            Debug.Log($"Has Task: {task}");
        }
    }


}
