using System;
using UnityEngine;

namespace Peggle
{
	public class Enemy : MonoBehaviour
	{
		public GameManager GameManager;

		private void Awake()
		{
			GameManager = GameObject.FindFirstObjectByType<GameManager>();
		}

		void Start()
		{
			GameManager.RegisterEnemy(this);
		}

		public void Kill()
		{
			GameManager.ClearEnemy(this);
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