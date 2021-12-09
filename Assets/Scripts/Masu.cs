using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour
{
    public bool isYellow = false;
    public bool isRed = false;
    public int[] position = new int[2];
    public GameObject notteruKoma;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Clicked(){
        if(isYellow){
            Koma.ResetBoard();
            Koma komaScript = PlayerCtrl.selectedKoma.GetComponent<Koma>();
            komaScript.MoveTo(position);
            PlayerCtrl.isMyTurn = false;
        }
    }
}
