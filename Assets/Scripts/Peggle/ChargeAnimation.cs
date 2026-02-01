using System;
using UnityEngine;

namespace Peggle
{
	public class ChargeAnimation : MonoBehaviour
	{
		public Transform ChargeShotTransform;
		public AnimationCurve ChargeScaleCurve;
		private float _animShotCharge = 0;
		private SpriteRenderer _spriteRenderer;
		public Color TotallyChargedColor;
		public Color ZeroChargeColor;
		public Color ChargedUpGoalColor;
		private void Awake()
		{
			_spriteRenderer = ChargeShotTransform.GetComponentInChildren<SpriteRenderer>();
		}

		public void SetChargeAmount(float realCharge)
		{
			_animShotCharge = Mathf.Clamp01(Mathf.MoveTowards(_animShotCharge, realCharge, Time.deltaTime * 12));
		}

		private void Update()
		{
			float f = ChargeScaleCurve.Evaluate(_animShotCharge);
			ChargeShotTransform.localScale = Vector3.one * f;
			if (_animShotCharge >= 1)
			{
				_spriteRenderer.color = TotallyChargedColor;
			}
			else
			{
				_spriteRenderer.color = Color.Lerp(ZeroChargeColor, ChargedUpGoalColor,f);
			}
		}
	}
}