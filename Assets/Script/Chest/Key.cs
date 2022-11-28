using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : Items
{

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void PickUpItem()
    {
        base.PickUpItem();
        if (PickedUp)
        {
            CharacterController2D.character.m_HasKey = true;
        }
    }
}
