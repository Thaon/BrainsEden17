using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolManager : MonoBehaviour
{
    #region member variables

    public GameObject m_petrolPrefab;
    public float m_petrolDropRadius;

    private GameObject m_lastDroppedPetrol = null;
    private bool m_burning = false;

    #endregion

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!m_burning)
        {
            if (m_lastDroppedPetrol != null)
            {
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
                    m_lastDroppedPetrol.transform.rotation = Quaternion.LookRotation(-Vector3.up);
                }
            }
            else
            {
                GameObject drop = Instantiate(m_petrolPrefab, transform.position, Quaternion.identity);
                m_lastDroppedPetrol = drop;
                m_lastDroppedPetrol.transform.position = new Vector3(m_lastDroppedPetrol.transform.position.x, 0.1f, m_lastDroppedPetrol.transform.position.z);
            }
        }
	}

    public void BurnTrail()
    {
        FindObjectOfType<PersistentData>().m_gameState = GameState.Paused; //stop game from continuing on affected objects
        m_lastDroppedPetrol.GetComponent<PetrolDrop>().Burn();
    }
}
