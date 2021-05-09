using Infinium.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infinium.Core
{
	public class Health : MonoBehaviour, ISaveable
	{
		[SerializeField] float healthPoints = 100f;

		bool isDead = false;

        public bool IsDead()
        {
			return isDead;
        }
        public void TakeDamage(float damage)
        {
			healthPoints = Mathf.Max(healthPoints - damage, 0);
			if(healthPoints == 0)
            {
				Die();
            }
			print(healthPoints);
        }
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            //GetComponent<Animator>().SetTrigger("Die");
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
