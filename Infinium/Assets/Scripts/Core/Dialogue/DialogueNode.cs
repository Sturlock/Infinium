using Infinium.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Infinium.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        private bool isPlayerSpeaking = false;

        [SerializeField] private string text;

        [SerializeField]
        private List<string> children = new List<string>();

        [SerializeField]
        private Rect rect = new Rect(0, 0, 250, 100);

        [SerializeField]
        private string onBeginAction;

        [SerializeField]
        private string onExitAction;

        [SerializeField]
        private Condition condition;

        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnBeginAction()
        {
            return onBeginAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }

#if UNITY_EDITOR

        public void SetPos(Vector2 newPos)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                if (newText != text)
                {
                    Undo.RecordObject(this, "Update Dialogue Text");
                    text = newText;
                    EditorUtility.SetDirty(this);
                }
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

#endif
    }
}