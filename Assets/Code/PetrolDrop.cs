using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PetrolDrop : MonoBehaviour {

    #region member variables

    public GameObject m_lastDroppedPetrol;

    private LineRenderer m_line;

    #endregion

    void Start ()
    {
        
	}
	
	void Update ()
    {
		
	}

    public void SetupTrail(GameObject lastDrop)
    {
        if (m_line == null)
            m_line = GetComponent<LineRenderer>();

        m_lastDroppedPetrol = lastDrop;
        m_line.SetPosition(0, transform.position);
        m_line.SetPosition(1, m_lastDroppedPetrol.transform.position);
    }
}
