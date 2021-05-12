using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infinium.Saving;

namespace Infinium.Stats
{
	public class Experience : MonoBehaviour, ISaveable
	{
	    [SerializeField] float experiencePoints = 0;

        public float GetExperiencePoints()
        {
            return experiencePoints;
        }
        public float GetPercentage()
        {
            return experiencePoints / GetComponent<BaseStats>().GetStat(Stat.ExperienceToLevelup);
        }
        public void GainExperience(float experience)
	    {
	        experiencePoints += experience;
	    }

        public object CaptureState()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}
