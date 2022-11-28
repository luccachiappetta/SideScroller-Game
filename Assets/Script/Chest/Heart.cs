using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Items
{
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void PickUpItem()
    {
        base.PickUpItem();
        if (PickedUp)
        {
            if (CharacterController2D.character.m_Health < 4)
            {
                CharacterController2D.character.m_Health++;
            }
        }
    }
}
