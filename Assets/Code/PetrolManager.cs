using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolManager : MonoBehaviour
{
    #region member variables

    public GameObject m_petrolPrefab;
    public float m_petrolDropRadius;

    private GameObject m_lastDroppedPetrol = null;

    #endregion

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if (m_lastDroppedPetrol != null)
        {
            //we are always the end of the last dropped petrol trail
            m_lastDroppedPetrol.GetComponent<PetrolDrop>().SetupTrail(this.gameObject);

            //only need 2D distance
            Vector3 pos = transform.position;
            Vector3 petrolPos = m_lastDroppedPetrol.transform.position;

            pos.y = 0;
            petrolPos.y = 0;

            if (Vector3.Distance(pos, petrolPos) > m_petrolDropRadius)
            {
                GameObject drop = Instantiate(m_petrolPrefab, transform.position, Quaternion.identity);
                drop.GetComponent<PetrolDrop>().SetupTrail(m_lastDroppedPetrol);
                m_lastDroppedPetrol = drop;
            }
        }
        else
        {
            GameObject drop = Instantiate(m_petrolPrefab, transform.position, Quaternion.identity);
            m_lastDroppedPetrol = drop;
        }
	}
}
