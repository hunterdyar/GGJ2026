using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Peggle
{
	public class Enemy : MonoBehaviour
	{
		private GameManager GameManager;
		private Animator animator;
		private bool isDead = false;
		private void Awake()
		{
			animator = GetComponent<Animator>();
			GameManager = GameObject.FindFirstObjectByType<GameManager>();
		}

		IEnumerator Start()
		{
			GameManager.RegisterEnemy(this);
			yield return new WaitForSeconds(Random.Range(0f, 1f));
			if (!isDead)
			{
				animator.ResetControllerState();
			}
		}
		

		public void Kill()
		{
			isDead = true;
			animator.SetTrigger("Die");
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