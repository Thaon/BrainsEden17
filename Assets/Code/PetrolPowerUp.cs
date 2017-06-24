using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolPowerUp : MonoBehaviour {

    public float m_rechargeAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.GetComponent<PlayerMovement>().m_petrol += m_rechargeAmount;
            if (other.GetComponent<PlayerMovement>().m_petrol > FindObjectOfType<PersistentData>().m_maxPetrol)
                other.GetComponent<PlayerMovement>().m_petrol = FindObjectOfType<PersistentData>().m_maxPetrol;

            Destroy(this.gameObject);
        }
    }

}
