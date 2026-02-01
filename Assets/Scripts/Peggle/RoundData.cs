using System.Collections.Generic;
using UnityEngine;

namespace Peggle
{
	[CreateAssetMenu(fileName = "RoundData", menuName = "Peggle/Round Data", order = 0)]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] public int ShotsPerRound;
		[SerializeField] public GameObject[] Levels;
		
		public float ShootForce = 40;
		
		//called in awake
		
	}
}