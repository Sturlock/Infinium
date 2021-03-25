using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Infinium.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;
        [SerializeField]
        string text;
        [SerializeField]
        List<string> children = new List<string>();
        [SerializeField] 
        Rect rect = new Rect(0,0,250,300);

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

#if UNITY_EDITOR
        public void SetPos(Vector2 newPos)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
           if(newText != text)
            {
                if(newText != text)
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

