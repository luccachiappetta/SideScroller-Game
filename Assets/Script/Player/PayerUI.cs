using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PayerUI : MonoBehaviour
{
    [SerializeField] private Slider Stamina;
    [SerializeField] private Image Key;
    private int p_Health = 4;
    [SerializeField] private Image[] health = new Image [4];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
        UpdateKey();
        UpdateHealth();
    }

    private void UpdateKey()
    {
        if (CharacterController2D.character.m_HasKey)
        {
            Key.gameObject.SetActive(true);
        }
        else
        {
            Key.gameObject.SetActive(false);
        }
    }
    
    private void UpdateStamina()
    {
        Stamina.value = CharacterController2D.character.m_Stamina;
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < CharacterController2D.character.m_Health)
            {
                health[i].gameObject.SetActive(true);
            }
            else
            {
                health[i].gameObject.SetActive(false);
            }
        }
    }
}
