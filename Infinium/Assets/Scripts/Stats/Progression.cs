using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Infinium.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Infinium/Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
	    [SerializeField] ProgessionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgessionCharacterClass progessionClass in characterClasses)
            {
                if (progessionClass.characterClass == characterClass)
                {
                    return progessionClass.health[level - 1];
                }
            }
            return 0;
        }


        [System.Serializable]
		class ProgessionCharacterClass
	    {
	        public CharacterClass characterClass;
			public float[] health;
	
	    }
	}
}
