using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

	NavMeshAgent agent;

    private PersistentData m_pData;

	// Use this for initialization
	void Start ()
    {
		agent = GetComponent <NavMeshAgent> ();
        m_pData = FindObjectOfType<PersistentData>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_pData.m_gameState == GameState.Playing)
        {
		    if (Input.GetMouseButtonDown (0))
		    {
			    RaycastHit hit;

			    if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100))
				    agent.destination = hit.point;
		    }
        }
	}

}
