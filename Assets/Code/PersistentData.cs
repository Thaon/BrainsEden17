using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Paused, Playing };

public class PersistentData : MonoBehaviour
{
    #region member variables

    public GameState m_gameState = GameState.Playing;
    public float m_maxPetrol;

    #endregion


    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public GameState GetGameState()
    {
        return m_gameState;
    }
}
