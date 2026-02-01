using System;
using Board.Input;
using UnityEngine;

public class MoveByPiece : MonoBehaviour
{
	private BoardGameInput _boardGameInput;
	public int glyphID;
	private Camera _camera;
	public float AngleCalibrate = 0;
	public float zPos;
	public Vector3 PositionCalibrate = new Vector3();
	public BoxCollider2D ValidArea;
	private void Awake()
	{
		_boardGameInput = GameObject.FindFirstObjectByType<BoardGameInput>();
		if (_boardGameInput == null)
		{
			Debug.LogError("Move By Piece requires a BoardGameInput script in the scene.");
		}
	}

	private void Start()
	{
		_boardGameInput.RegisterPieceTransform(this, glyphID);
		_camera = Camera.main;
	}

	public void ProcessInput(BoardContact contact)
	{
		switch (contact.phase)
		{
			case BoardContactPhase.Began:
			case BoardContactPhase.Moved:
			case BoardContactPhase.Ended:
				var pos = GetWorldPosition(contact.screenPosition, contact.orientation);
				if (ValidArea != null)
				{
					if (!ValidArea.OverlapPoint(pos.pos))
					{
						pos.pos = ValidArea.ClosestPoint(pos.pos);
					}
				}
				transform.position = pos.pos;
				transform.rotation = pos.rot;
				break;
		}
	}

	private (Vector3 pos, Quaternion rot) GetWorldPosition(Vector2 contactScreenPosition, float orientation)
	{
		var worldPos = _camera.ScreenToWorldPoint(contactScreenPosition);
		worldPos = new Vector3(worldPos.x, worldPos.y,zPos)+PositionCalibrate;

		// radians clockwise from vertical.
		//assuming that 'up' is not right?
		var rot = Quaternion.Euler(0, 0, orientation * Mathf.Rad2Deg+ AngleCalibrate);

		return (worldPos, rot);
	}
}
