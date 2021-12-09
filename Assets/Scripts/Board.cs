using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] MasuArray;
    static public GameObject[,] BoardMap = new GameObject[3,3];
    void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            BoardMap[i%3,i/3] = MasuArray[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
