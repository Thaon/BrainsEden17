using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimed : MonoBehaviour {

    public float m_time;

	void Start ()
    {
        StartCoroutine(Die());
	}

    IEnumerator Die()
    {
        yield return new WaitForSeconds(m_time);
        Destroy(this.gameObject);
    }
}
