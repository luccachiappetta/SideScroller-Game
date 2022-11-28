using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        SightRange = 7f;
        AttackRange = 1.6f;
        e_SPEED = 2.8f;
        e_HEALTH = 100f;
        e_DAMAGE = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
