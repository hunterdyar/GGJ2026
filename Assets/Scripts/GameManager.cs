using System;
using Peggle;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoundData RoundData;

    private void Awake()
    {
        RoundData.StartNewRound(0);
    }
}
