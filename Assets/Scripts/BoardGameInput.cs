
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

	private void ProcessGlyphs()
	{
		var contacts = BoardInput.GetActiveContacts(BoardContactType.Glyph);
		foreach (var contact in contacts)
		{
			var id = contact.glyphId;
			if (_transforms.TryGetValue(id, out var piece))
			{
				piece.ProcessInput(contact);
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
