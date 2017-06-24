using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

    public float m_multiplier;

	void Update ()
    {
        transform.RotateAround(Vector3.up, Time.deltaTime * m_multiplier);
	}
}
