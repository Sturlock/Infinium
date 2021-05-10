using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Stats
{
	public class BaseStats : MonoBehaviour
	{
	    [Range(1, 99)]
	    [SerializeField] int startingLevel = 1;
	    [SerializeField] CharacterClass characterClass;
		[SerializeField] Progression progression = null;

        void Update()
        {
			print(GetLevel());
        }
        public float GetStat(Stat stat)
        {
			return progression.GetStat(stat, characterClass, GetLevel());
        }

		public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();
			if (experience == null) return startingLevel;

            float currentXP = experience.GetExperiencePoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelup, characterClass);
            for (int level = 1; level < penultimateLevel; level++)
			{
				float EXPToLevelUp = progression.GetStat(Stat.ExperienceToLevelup, characterClass, level);
				if (EXPToLevelUp > currentXP)
				{
					return level;
				}
			}
			return penultimateLevel + 1;
        }
	}
}
