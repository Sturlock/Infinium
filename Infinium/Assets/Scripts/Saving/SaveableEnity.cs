using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Infinium.Saving
{
    [ExecuteAlways]
    public class SaveableEnity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            print("Capturing State for" + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
        }

        void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
#if UNITY_EDITOR
            SerializedObject serializedObject = 
                new SerializedObject(this);
            SerializedProperty property = 
                serializedObject.FindProperty("uniqueIdentifier");
            
#endif
            print("Editing");
            if (property.stringValue == "")
            {
                uniqueIdentifier = System.Guid.NewGuid().ToString();
            }
        }

    }
}
