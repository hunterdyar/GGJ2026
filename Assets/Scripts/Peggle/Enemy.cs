using System;
using UnityEngine;

namespace Peggle
{
	public class Enemy : MonoBehaviour
	{
		public RoundData RoundData;

		void Start()
		{
			RoundData.RegisterEnemy(this);
		}

		public void Kill()
		{
			RoundData.ClearEnemy(this);
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<Collider2D>().enabled = false;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Puck"))
			{
				Kill();
			}
		}
	}
}