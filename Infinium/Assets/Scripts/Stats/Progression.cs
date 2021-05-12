using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Infinium.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Infinium/Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
	    [SerializeField] ProgessionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level)
                return 0;

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgessionCharacterClass progessionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progessionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progessionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
		class ProgessionCharacterClass
	    {
	        public CharacterClass characterClass;
            public ProgressionStat[] stats;	
	    }
	}
    [System.Serializable]
    public class ProgressionStat
    {
        public Stat stat;
        public float[] levels;
    }
}
