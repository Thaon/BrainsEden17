using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PetrolDrop : MonoBehaviour {

    #region member variables

    public GameObject m_lastDroppedPetrol = null;

    private LineRenderer m_line;
    private bool m_burning = false;
    private GameObject m_player;
    private GameObject m_fizzlePrefab;
    private PetrolManager m_petrolManager;

    #endregion

    void Start ()
    {
        m_line = GetComponent<LineRenderer>();
        m_petrolManager = FindObjectOfType<PetrolManager>();
        m_player = GameObject.FindWithTag("Player");
        m_fizzlePrefab = GetComponentsInChildren<Transform>()[1].gameObject; //first child
        m_fizzlePrefab.SetActive(false);

        if (m_lastDroppedPetrol == null)
        {
            m_line.SetPosition(0, transform.position);
            m_line.SetPosition(1, m_player.transform.position);
        }
	}
	
	void Update ()
    {
    }

    public void SetupTrail(GameObject lastDrop)
    {
        if (m_line == null)
            m_line = GetComponent<LineRenderer>();

        if (!m_burning)
        {
            Vector3 pos0 = transform.position;
            Vector3 pos1 = lastDrop.transform.position;

            pos0.y = 0.1f;
            pos1.y = 0.1f;

            m_lastDroppedPetrol = lastDrop;
            m_line.SetPosition(0, pos0);
            m_line.SetPosition(1, pos1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    public void Burn()
    {
        //instantiate paticle effect
        m_fizzlePrefab.SetActive(true);
        m_fizzlePrefab.transform.parent = null;

        //die and kill next target
        StartCoroutine(Die());

        //explode explosives YAY!
        foreach (Collider coll in Physics.OverlapSphere(transform.position, m_petrolManager.m_petrolDropRadius))
        {
            if (coll.GetComponent<Explosive>())
            {
                coll.GetComponent<Explosive>().Explode();
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(.1f);

        foreach (Collider coll in Physics.OverlapSphere(transform.position, m_petrolManager.m_petrolDropRadius))
            if (coll.GetComponent<PetrolDrop>())
                coll.GetComponent<PetrolDrop>().Burn();

        Destroy(this.gameObject);
    }
}
