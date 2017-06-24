using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region member variables

    NavMeshAgent agent;

    private PersistentData m_pData;
    private float m_petrol;

    #endregion
    // Use this for initialization
    void Start ()
    {
		agent = GetComponent <NavMeshAgent> ();
        m_pData = FindObjectOfType<PersistentData>();

        m_petrol = m_pData.m_maxPetrol;
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
        else
        {
            agent.Stop();
            agent.velocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (agent.velocity.magnitude > 0)
        {

        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            print("Gotcha!");
            FindObjectOfType<PersistentData>().m_gameState = GameState.Paused;
        }
    }

}
