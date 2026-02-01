using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class LevelAnimator : UnityEngine.MonoBehaviour
{
	public AnimationCurve Ease;
	public float TransitionSpeed;
	
	public void AnimateLevelTransition(Transform old, Transform level)
	{
		StartCoroutine(AnimateLevel(old, level));
	}

	private IEnumerator AnimateLevel([CanBeNull] Transform old, Transform newLevel)
	{
		Vector3 topCenterOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0));
		float gapBetweenLevels = topCenterOfScreen.y*2;
		Vector3 newStart = newLevel.position+Vector3.up*gapBetweenLevels;
		Vector3 newEnd = Vector3.zero;//should be 0,0
		
		//old
		bool hasHold = false;
		Vector3 oldStart = Vector3.zero;
		Vector3 oldEnd = Vector3.zero;
		if (old != null)
		{
			hasHold = true;
			oldStart = old.position; //should be 0,0
			oldEnd = old.position + Vector3.down * gapBetweenLevels;
		}

		float timeToMove = gapBetweenLevels/TransitionSpeed;
		float t = 0;
		while (t < 1)
		{
			newLevel.position = Vector3.Lerp(newStart, newEnd, t);
			if (hasHold)
			{
				old.position = Vector3.Lerp(oldStart, oldEnd, t);
			}

			t += Time.deltaTime / timeToMove;
			yield return null;
		}
		
		newLevel.position = newEnd;
		if (hasHold)
		{
			Destroy(old.gameObject);
		}
	}
}