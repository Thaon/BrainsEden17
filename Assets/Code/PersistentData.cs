using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Paused, Playing };

public class PersistentData : MonoBehaviour
{
    #region member variables

    public GameState m_gameState = GameState.Playing;
    public float m_maxPetrol;
    public GameObject m_winningUI;
    public GameObject m_losingUI;

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

    public void PlayerWon()
    {
        StopAllCoroutines();
        m_winningUI.SetActive(true);
        print("Yay, you made it");
        StartCoroutine(ReturnToMenu(2));
    }

    public void PlayerCaught()
    {
        m_losingUI.SetActive(true);
        print("Player has been caught");
        StartCoroutine(ReturnToMenu(3));
    }

    public void PetrolFinished()
    {
        m_losingUI.SetActive(true);
        print("Petrol finished :(");
        StartCoroutine(ReturnToMenu(3));
    }

    public void CheckIfWinning(float time)
    {
        StartCoroutine(Check(time + .1f));
    }

    IEnumerator Check(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerCaught();
    }

    IEnumerator ReturnToMenu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
    }
}
