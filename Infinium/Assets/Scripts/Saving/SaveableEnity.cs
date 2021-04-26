using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Saving
{
    public class SaveableEnity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = System.Guid.NewGuid().ToString();
        public string GetUniqueIdentifier()
        {
            return "";
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
    }
}
