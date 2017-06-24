using UnityEngine;
using UnityEngine.UI;

public class PetrolUI : MonoBehaviour {

    #region member variables

    private PersistentData m_pData;
    private PlayerMovement m_playerMov;
    private Image m_img;

    #endregion

    void Start ()
    {
        m_pData = FindObjectOfType<PersistentData>();
        m_playerMov = FindObjectOfType<PlayerMovement>();
        m_img = GetComponent<Image>();
	}
	
	void Update ()
    {
        m_img.fillAmount = m_playerMov.m_petrol / m_pData.m_maxPetrol;
	}
}
