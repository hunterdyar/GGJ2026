using UnityEngine;

namespace Peggle
{
	[CreateAssetMenu(fileName = "RoundData", menuName = "Peggle/Round Data", order = 0)]
	public class RoundData : ScriptableObject
	{
		[HideInInspector] public int ShotsLeft;
		[SerializeField] private int ShotsPerRound;
		public float ShootForce = 40;

		public void StartNewRound(int roundNumber)
		{
			ShotsLeft = ShotsPerRound;
		}
	}
}