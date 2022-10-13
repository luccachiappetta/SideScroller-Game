using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Assasin : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        SightRange = 5f;
    }
    
    //run at player
    
    //attack player

    // Update is called once per frame
    public override void attackPlayer()
    {
        Debug.Log("test");
    }
}
