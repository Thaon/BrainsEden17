using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    #region member variables



    #endregion

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void Explode()
    {
        //just destroy for now
        Destroy(this.gameObject);
    }
}
