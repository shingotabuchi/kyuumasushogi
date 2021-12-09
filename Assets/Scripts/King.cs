using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Koma
{
    void Start()
    {
        base.Start();
        movementPossible = new bool[,]{
            {true,true,true},
            {true,false,true},
            {true,true,true},
        };
        isKing = true;
    }
}
