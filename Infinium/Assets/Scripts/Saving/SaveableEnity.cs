using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Infinium.Saving
{
    [ExecuteAlways]
    public class SaveableEnity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";
        static Dictionary<string, SaveableEnity> globalLookup = new Dictionary<string, SaveableEnity>();
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }

        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
#if UNITY_EDITOR
            SerializedObject serializedObject =
                new SerializedObject(this);
            SerializedProperty property =
                serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue) || !IsUniquie(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }

        private bool IsUniquie(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }



            return false;
        }

#endif
    }
}