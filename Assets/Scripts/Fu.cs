using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fu : Koma
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        movementPossible = new bool[,]{
            {false,true,false},
            {false,false,false},
            {false,false,false},
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
