using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Stats
{
	public class GoldPieces : MonoBehaviour
	{
        public int goldPieces;
        public int GetGoldPieces()
        {
            return goldPieces;
        }

        public void GainGoldPieces(int money)
        {
            goldPieces += money;
        }
    }
}
