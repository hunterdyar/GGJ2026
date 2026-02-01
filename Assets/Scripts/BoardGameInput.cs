
using System;
using System.Collections.Generic;
using Board.Input;
using UnityEngine;

public class BoardGameInput : MonoBehaviour
{
    private Dictionary<int, MoveByPiece> _transforms = new Dictionary<int, MoveByPiece>();

	private void Update()
	{
		ProcessGlyphs();
		ProcessFingers();
	}

	private void ProcessFingers()
	{
		var contacts = BoardInput.GetActiveContacts(BoardContactType.Finger);
		foreach (var contact in contacts)
		{
			if (contact.phase == BoardContactPhase.Moved)
			{
				Debug.Log("moved finger: " + contact.glyphId);
			}
		}
		
	}

	private Dictionary<int, bool> _touchState = new Dictionary<int, bool>();
	private void ProcessGlyphs()
	{
		var contacts = BoardInput.GetActiveContacts(BoardContactType.Glyph);
		foreach (var contact in contacts)
		{
			var id = contact.glyphId;
			var phase = UpdateTouchPhase(id, contact.isTouched);
			if (_transforms.TryGetValue(id, out var piece))
			{
				piece.ProcessInput(contact, phase);
			}
		}
	}

	private TouchPhase UpdateTouchPhase(int id, bool touched)
	{
		bool prev = false;
		if (_touchState.ContainsKey(id))
		{
			prev = _touchState[id];
			_touchState[id] = touched;
		}
		else
		{
			_touchState.Add(id,touched);
		}

		if (prev)
		{
			//was touching
			if (touched)
			{
				return TouchPhase.Stay;
			}
			else
			{
				return TouchPhase.Release;
			}
		}
		else
		{
			//wasn't touching
			if (touched)
			{
				return TouchPhase.Press;
			}
			else
			{
				return TouchPhase.None;
			}
		}
	}

	public void RegisterPieceTransform(MoveByPiece moveByPiece, int glyphID)
	{
		if (!_transforms.TryAdd(glyphID, moveByPiece))
		{
			Debug.LogWarning($"Already a piece mapped to glyphID {glyphID}. Can't add another.");
		}
	}
}

public enum TouchPhase
{
	Press,
	Stay,
	Release,
	None
}