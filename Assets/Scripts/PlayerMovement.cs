using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region member variables

    private NavMeshAgent agent;
    private PersistentData m_pData;
    private Animator m_anim;

    public float m_petrol;

    #endregion

    // Use this for initialization
    void Start ()
    {
		agent = GetComponent <NavMeshAgent> ();
        m_pData = FindObjectOfType<PersistentData>();
        m_anim = GetComponentInChildren<Animator>();

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
            m_petrol -= Time.deltaTime; //1 unit per second
            if (m_petrol <= 0)
            {
                FindObjectOfType<PersistentData>().PetrolFinished();
                m_pData.m_gameState = GameState.Paused;
            }
        }

        m_anim.SetFloat("Speed", agent.velocity.magnitude);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            FindObjectOfType<PersistentData>().PlayerCaught();
            FindObjectOfType<PersistentData>().m_gameState = GameState.Paused;
        }
    }

}
