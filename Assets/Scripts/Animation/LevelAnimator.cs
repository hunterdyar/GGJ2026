using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public enum GameDirection
{
	VerticalUp,
	HorizontalRight
}
public class LevelAnimator : UnityEngine.MonoBehaviour
{
	public GameDirection direction = GameDirection.HorizontalRight;
	public AnimationCurve Ease = new AnimationCurve();
	public float TransitionSpeed = 7;
	
	public void AnimateLevelTransition(Transform old, Transform level)
	{
		StartCoroutine(AnimateLevel(old, level));
	}

	private IEnumerator AnimateLevel([CanBeNull] Transform old, Transform newLevel)
	{
		Vector3 rightCenterOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width , Screen.height/2f));
		Vector3 topCenterOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width , Screen.height/2f));
		float gapBetweenLevels = topCenterOfScreen.y*2;
		Vector3 dir = Vector3.up;

		if (direction == GameDirection.HorizontalRight)
		{
			gapBetweenLevels = rightCenterOfScreen.x*2; 
			dir = Vector3.right;
		}
		
		Vector3 newStart = newLevel.position+dir*gapBetweenLevels;
		Vector3 newEnd = Vector3.zero;//should be 0,0
		
		//old
		bool hasHold = false;
		Vector3 oldStart = Vector3.zero;
		Vector3 oldEnd = Vector3.zero;
		if (old != null)
		{
			hasHold = true;
			oldStart = old.position; //should be 0,0
			oldEnd = old.position -dir * gapBetweenLevels;
		}

		float timeToMove = gapBetweenLevels/TransitionSpeed;
		float t = 0;
		while (t < 1)
		{
			float f = Ease.Evaluate(t);
			newLevel.position = Vector3.Lerp(newStart, newEnd, f);
			if (hasHold)
			{
				old.position = Vector3.Lerp(oldStart, oldEnd, f);
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