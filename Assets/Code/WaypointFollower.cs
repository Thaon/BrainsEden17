using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointFollower : MonoBehaviour
{
    #region member variables

    public float m_speed;
    public float m_turningSpeed;
    public float m_timeToWaitAtWaypoint;

    private List<Vector3> m_wayPoints;
    private NavMeshAgent m_agent;
    private int m_waypointCounter = 0;
    private bool m_isMoving = false;

    #endregion

    void Start ()
    {
        //initialize stuff
        m_wayPoints = new List<Vector3>();
        m_agent = GetComponent<NavMeshAgent>();

        //make checks
        if (m_agent == null)
            Debug.LogWarning("No NavMeshAgent found on enemy unit");

        //fill in data
        m_agent.updateRotation = false;

        foreach(Transform tran in GetComponentsInChildren<Transform>())
        {
            if (tran.tag == "WayPoint")
            {
                m_wayPoints.Add(tran.transform.position);
                Destroy(tran.gameObject);
            }
        }

        if (m_wayPoints.Count != 0 && m_agent != null)
        {
            m_agent.speed = m_speed;
            m_agent.SetDestination(m_wayPoints[m_waypointCounter]);
            m_agent.Resume();
            m_isMoving = true;

            //initial direction is set here
            Vector3 direction = (m_agent.destination - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
	}
	
	void Update ()
    {
        if (FindObjectOfType<PersistentData>().GetGameState() == GameState.Playing)
        {
            Vector3 pos = transform.position;
            Vector3 des = m_agent.destination;
            pos.y = 0;
            des.y = 0;

            if ((pos - des).magnitude < 0.5f && m_isMoving)
            {
                //increase counter and reset if necessary
                m_waypointCounter++;
                if (m_waypointCounter == m_wayPoints.Count)
                    m_waypointCounter = 0;

                //change destination
                m_agent.SetDestination(m_wayPoints[m_waypointCounter]);

                //pause and then start moving again (could play looking around animation here..)
                m_isMoving = false;
                m_agent.Stop();
                StartCoroutine(GoToNextWaypoint());
            }

            //smoothly change direction if not moving
            if (m_agent.velocity.magnitude == 0)
            {
                Vector3 direction = (m_agent.destination - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), m_turningSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator GoToNextWaypoint()
    {
        yield return new WaitForSeconds(m_timeToWaitAtWaypoint);

        //go to the next point
        m_agent.Resume();
        m_isMoving = true;
    }
}
