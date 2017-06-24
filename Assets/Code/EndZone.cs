using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PersistentData>().m_gameState = GameState.Paused;
            FindObjectOfType<PetrolManager>().BurnTrail();
        }
    }
}
