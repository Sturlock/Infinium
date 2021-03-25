using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnamedQuestConfig", menuName ="Infinium/Quest Config")]
public class QuestScriptable : ScriptableObject
{
    [SerializeField]
    string[] tasks;

    public IEnumerable<string> GetTasks()
    {
        yield return "Task 1";
        Debug.Log("Do some work");
        yield return "Task 2";
    }
}
