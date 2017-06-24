using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    #region member variables

    public bool m_isMainTarget = false;

    #endregion

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void Explode()
    {
        if (m_isMainTarget)
            FindObjectOfType<PersistentData>().PlayerWon();

        //just destroy for now
        Destroy(this.gameObject);
    }
}
