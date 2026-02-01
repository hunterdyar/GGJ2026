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
		private CircleCollider2D collider;
		private bool isDead = false;
		private void Awake()
		{
			collider = GetComponent<CircleCollider2D>();
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
			collider.enabled = false;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (isDead)
			{
				return;
			}
			if (other.gameObject.CompareTag("Puck"))
			{
				Kill();
			}
		}
	}
}