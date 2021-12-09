using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gin : Koma
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        movementPossible = new bool[,]{
            {true,true,true},
            {false,false,false},
            {true,false,true},
        };
        canNaru = true;
        movementPossibleAlt = new bool[,]{
            {true,true,true},
            {true,false,true},
            {false,true,false},
        };
        currentMovementPossible = movementPossible;
    }
}
