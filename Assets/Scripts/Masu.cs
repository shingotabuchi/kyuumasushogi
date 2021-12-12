using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour
{
    public bool isYellow = false;
    public bool isRed = false;
    public int[] position = new int[2];
    public GameObject notteruKoma;
    public void Clicked(){
        if(isYellow){
            Koma.ResetBoard();
            Koma komaScript = PlayerCtrl.selectedKoma.GetComponent<Koma>();
            if(komaScript.isMochiGoma){
                PlayerCtrl.mochiGoma.Remove(PlayerCtrl.selectedKoma);
                komaScript.isMochiGoma = false;
                komaScript.wasJustMochiGoma = true;
            }
            komaScript.MoveTo(position);
            PlayerCtrl.isMyTurn = false;
        }
        else if(isRed){
            Koma.ResetBoard();
            Koma komaScript = PlayerCtrl.selectedKoma.GetComponent<Koma>();
            Koma notteruKomaScript = notteruKoma.GetComponent<Koma>();
            notteruKomaScript.isMochiGoma = true;
            notteruKomaScript.Init();
            notteruKoma.transform.Rotate(0,0,180);
            AICtrl.myKomas.Remove(notteruKoma);
            PlayerCtrl.GetKoma(notteruKoma);
            komaScript.MoveTo(position);
            PlayerCtrl.isMyTurn = false;
        }
    }
    public void ResetMasu(){
        notteruKoma = null;
    }
}
