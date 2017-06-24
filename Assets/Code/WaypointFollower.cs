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
    public Color[] m_lightStates;

    private List<Vector3> m_wayPoints;
    private NavMeshAgent m_agent;
    private int m_waypointCounter = 0;
    private bool m_isMoving = false;
    private bool m_chasing = false;
    private GameObject m_player;
    private Light m_light;

    #endregion

    void Start ()
    {
        //initialize stuff
        m_wayPoints = new List<Vector3>();
        m_agent = GetComponent<NavMeshAgent>();
        m_player = GameObject.FindWithTag("Player");
        m_light = GetComponentInChildren<Light>();

        //make checks
        if (m_agent == null)
            Debug.LogWarning("No NavMeshAgent found on enemy unit");

        //fill in data
        m_agent.updateRotation = false;
        m_light.color = m_lightStates[0];

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

            if (!m_chasing)
            {
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

                //check for player
                if (CheckConeOfVIsion(10, 60, 120, 10))
                    m_chasing = true;
            }
            else
            {
                m_light.color = m_lightStates[1];
                Vector3 direction = (m_player.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), m_turningSpeed * Time.deltaTime);
                m_agent.SetDestination(m_player.transform.position);
                m_agent.Resume();
            }
        }
        else
        {
            m_agent.Stop();
            m_agent.velocity = Vector3.zero;
        }
    }

    IEnumerator GoToNextWaypoint()
    {
        yield return new WaitForSeconds(m_timeToWaitAtWaypoint);

        //go to the next point
        m_agent.Resume();
        m_isMoving = true;
    }

    bool CheckConeOfVIsion(float range, float sweepAngle, float distance, float precision)
    {
        Vector3 coneEnd = transform.position + transform.forward * distance;
        float FoV = sweepAngle, radIters = precision, circIters = precision;
        float radius = Mathf.Tan(FoV / 2 * Mathf.Deg2Rad) * distance;

        //get all possible points
        for (float r = 0; r < radius; r += radius / radIters)
        {
            for (int angle = 0; angle < 360; angle += Mathf.RoundToInt(360 / circIters))
            {
                Vector3 dir = Quaternion.AngleAxis(angle, transform.forward) * transform.right;
                Vector3 p = coneEnd + dir.normalized * r;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, p - transform.position, out hit, range))
                {
                    if (hit.collider.tag == "Player")
                    {
                        print("enemy spotted you!");
                        return true;
                    }
                }

                Debug.DrawRay(transform.position, p.normalized * range, Color.white);
            }
        }

        return false;
    }

}
