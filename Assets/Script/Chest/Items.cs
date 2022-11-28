using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    
    protected Rigidbody2D rb;
    [SerializeField] protected LayerMask PlayerLayer;
    protected bool PickedUp;

    [SerializeField] private Canvas ItemPrompt;
    private Canvas CamvasInstance;
    

    
    void Start()
    {
        rb.velocity = new Vector2(2f, 3f);
        CamvasInstance = Instantiate(ItemPrompt, gameObject.transform);
        PickedUp = false;
    }

    private void Update()
    {
        if (ShowPrompt())
        {
            PickUpItem();
        }
    }

    protected virtual void PickUpItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickedUp = true;
            Destroy(gameObject);
        }
    }

    private bool ShowPrompt()
    {
        if (Physics2D.OverlapCircle(transform.position, 5f, PlayerLayer))
        {
            CamvasInstance.gameObject.SetActive(true);
            if (Physics2D.OverlapCircle(transform.position, 1f, PlayerLayer))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CamvasInstance.gameObject.SetActive(false);
                    return true;
                }
            }
        }
        else
        {
            CamvasInstance.gameObject.SetActive(false);
            return false;
        }
        return false;
    }

}
