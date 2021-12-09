using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    public bool isKing;
    public bool isPlayersKoma;
    public bool[,] movementPossible;
    public int[] initPosition = new int[2];
    int[] position = new int[2];
    int[,,] movement = new int[,,]{
        {{ -1, 1 },{ 0, 1 },{ 1, 1 }},
        {{ -1, 0 },{ 0, 0 },{ 1, 0 }},
        {{ -1,-1 },{ 0,-1 },{ 1,-1 }},
    };
    public void Start()
    {
        MoveTo(initPosition);
    }

    void Update()
    {
        
    }
    // void OnMouseOver()
    // {
    //     print("koma");
    //     if(Input.GetMouseButton(0)){
    //         if(PlayerCtrl.isMyTurn){
    //             ResetBoard();
    //             ShowPossibleMovement();
    //             PlayerCtrl.selectedKoma = gameObject;
    //         }
    //     }
    // }
    public void Clicked(){
        ResetBoard();
        ShowPossibleMovement();
        PlayerCtrl.selectedKoma = gameObject;
    }
    void ShowPossibleMovement()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                bool isPossible = movementPossible[i,j];
                if(!isPlayersKoma) isPossible = movementPossible[2-i,2-j];
                if(isPossible){
                    int newX = position[0]+movement[i,j,0];
                    int newY = position[1]+movement[i,j,1];
                    if(newX>=0&&newY>=0){
                        Masu masuScript = Board.BoardMap[newX,newY].GetComponent<Masu>();
                        Renderer masuRenderer = Board.BoardMap[newX,newY].GetComponent<Renderer>();
                        Color yellow = new Color(1f,1f,0f,1.0f);
                        Color red = new Color(1f,0f,0f,1.0f);
                        if(masuScript.notteruKoma==null){
                            masuRenderer.material.SetColor("_Color", yellow);
                            masuScript.isYellow = true;
                        }
                        else{
                            Koma komaScript = masuScript.notteruKoma.GetComponent<Koma>();
                            if(!komaScript.isPlayersKoma){
                                masuRenderer.material.SetColor("_Color", red);
                                masuScript.isRed = true;
                            }
                        }
                    }
                }
            }
        }
    }
    static public void ResetBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Renderer masuRenderer = Board.BoardMap[i,j].GetComponent<Renderer>();
                Color white = new Color(1f,1f,1f,1.0f);
                masuRenderer.material.SetColor("_Color", white);
                Masu masuScript = Board.BoardMap[i,j].GetComponent<Masu>();
                masuScript.isYellow = false;
            }
        }
    }
    public void MoveTo(int[] pos)
    {
        Masu masuScript = Board.BoardMap[pos[0],pos[1]].GetComponent<Masu>();
        masuScript.notteruKoma = null;
        Masu newMasuScript = Board.BoardMap[pos[0],pos[1]].GetComponent<Masu>();
        newMasuScript.notteruKoma = gameObject;
        transform.position = Board.BoardMap[pos[0],pos[1]].transform.position;
        position = new int[]{pos[0],pos[1]};
    }
}
