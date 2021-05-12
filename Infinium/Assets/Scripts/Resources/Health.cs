using Infinium.Saving;
using Infinium.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infinium.Resources
{
	public class Health : MonoBehaviour, ISaveable
	{
		float healthPoints = -1f;

		bool isDead = false;

        void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
            
        }
        public bool IsDead()
        {
			return isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
			healthPoints = Mathf.Max(healthPoints - damage, 0);
			if(healthPoints == 0)
            {
				Die();
                AwardExperience(instigator);
            }
			print(healthPoints);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            //GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if(healthPoints <= 0)
            {
                Die();
            }
        }
    }
}
