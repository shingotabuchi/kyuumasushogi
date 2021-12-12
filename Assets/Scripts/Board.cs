using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] MasuArray;
    public GameObject KomaParent;
    static public GameObject stKomaParent;
    static public GameObject[,] BoardMap = new GameObject[3,3];
    void Awake()
    {
        stKomaParent = KomaParent;
        for (int i = 0; i < 9; i++)
        {
            BoardMap[i%3,i/3] = MasuArray[i];
        }
    }
    public void ResetGame(){
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Masu masuScript = BoardMap[i,j].GetComponent<Masu>();
                masuScript.ResetMasu();
            }
        }
        PlayerCtrl.Init();
        AICtrl.Init();
        foreach (Transform child in stKomaParent.transform)
        {
            Koma komaScript = child.gameObject.GetComponent<Koma>();
            komaScript.ResetKoma();
        }
        PlayerCtrl.isMyTurn = true;
        AICtrl.timer = 0;
    }
}