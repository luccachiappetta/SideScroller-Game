using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpen : MonoBehaviour
{

    private Animator animate;
    [SerializeField] Canvas ItemPrompt;
    [SerializeField] public LayerMask PlayerLayer;
    [SerializeField] private Items DroppedItem;
    private Canvas CanvasInstsnce;
    private BoxCollider2D Collider;
    
    private bool isOpen = false;
    private void Awake()
    {
        animate = GetComponent<Animator>();
        Collider = GetComponent<BoxCollider2D>();
        
        CanvasInstsnce = Instantiate(ItemPrompt, gameObject.transform);

        // ItemPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }

    private void Open()
    {
        if (Physics2D.OverlapCircle(transform.position, 5f, PlayerLayer) && !isOpen)
        {
            Debug.Log("in Range");
            CanvasInstsnce.gameObject.SetActive(true);
            if (Physics2D.OverlapCircle(transform.position, 1f, PlayerLayer))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    SpawnLoot();
                    
                    CanvasInstsnce.gameObject.SetActive(false);
                    isOpen = true;
                    Collider.enabled = false;
                    animate.SetBool("Open", isOpen);
                }
            }
        }
        else
        {
            CanvasInstsnce.gameObject.SetActive(false);
        }
    }

    private void SpawnLoot()
    {
        Instantiate(DroppedItem, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
