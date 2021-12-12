using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    public bool wasJustMochiGoma;
    public bool isMochiGoma;
    public GameObject naruCanvas;
    public bool canNaru;
    public bool askedIfNaru = false;
    public bool isKing;
    public bool isPlayersKoma;
    public bool[,] currentMovementPossible;
    public bool[,] movementPossible;
    public bool[,] movementPossibleAlt;
    Sprite normalSprite;
    public Sprite altSprite;
    public int[] initPosition = new int[2];
    public int[] position = new int[2]{-1,-1};
    static public int[,,] movement = new int[,,]{
        {{ -1, 1 },{ 0, 1 },{ 1, 1 }},
        {{ -1, 0 },{ 0, 0 },{ 1, 0 }},
        {{ -1,-1 },{ 0,-1 },{ 1,-1 }},
    };
    public bool naruOnNextMove;
    public void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();  
        normalSprite = spriteRenderer.sprite;
        MoveTo(initPosition);
    }
    public void ResetKoma(){
        currentMovementPossible = movementPossible;
        isMochiGoma = false;
        Init();
        MoveTo(initPosition);
    }
    public void Init(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();  
        spriteRenderer.sprite = normalSprite;
        currentMovementPossible = movementPossible;
        wasJustMochiGoma = false;
        askedIfNaru = false;
        naruOnNextMove = false;
        position = new int[2]{-1,-1};
    }
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
                if(isMochiGoma){
                    Masu masuScript = Board.BoardMap[i,j].GetComponent<Masu>();
                    Renderer masuRenderer = Board.BoardMap[i,j].GetComponent<Renderer>();
                    Color yellow = new Color(1f,1f,0f,1.0f);
                    if(masuScript.notteruKoma==null){
                        masuRenderer.material.SetColor("_Color", yellow);
                        masuScript.isYellow = true;
                    }
                }
                else{
                    bool isPossible = currentMovementPossible[i,j];
                    if(!isPlayersKoma) isPossible = currentMovementPossible[2-i,2-j];
                    if(isPossible){
                        int newX = position[0]+movement[i,j,0];
                        int newY = position[1]+movement[i,j,1];
                        if(newX>=0&&newY>=0&&newX<=2&&newY<=2){
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
                masuScript.isRed = false;
            }
        }
    }
    public void MoveTo(int[] pos)
    {
        if(position[0]!=-1){
            Masu masuScript = Board.BoardMap[position[0],position[1]].GetComponent<Masu>();
            masuScript.notteruKoma = null;  
        }
        Masu newMasuScript = Board.BoardMap[pos[0],pos[1]].GetComponent<Masu>();
        newMasuScript.notteruKoma = gameObject;
        transform.position = Board.BoardMap[pos[0],pos[1]].transform.position;
        position = new int[]{pos[0],pos[1]};
        if(isPlayersKoma&&((isPlayersKoma&&pos[1]==2&&!askedIfNaru)||naruOnNextMove)){
            if(canNaru){
                if(wasJustMochiGoma) naruOnNextMove = true;
                else{
                    PlayerCtrl.isWaiting = true;
                    print(gameObject);
                    print(isPlayersKoma);
                    naruCanvas.SetActive(true);
                    askedIfNaru = true;
                    PlayerCtrl.naruKamoKoma = gameObject;
                    naruOnNextMove = false;
                }
            }
        }
        if(wasJustMochiGoma)wasJustMochiGoma=false;
    }
    public void AIMoveTo(int[] pos, bool naru)
    {
        if(position[0]!=-1){
            Masu masuScript = Board.BoardMap[position[0],position[1]].GetComponent<Masu>();
            masuScript.notteruKoma = null;  
        }
        Masu newMasuScript = Board.BoardMap[pos[0],pos[1]].GetComponent<Masu>();
        newMasuScript.notteruKoma = gameObject;
        transform.position = Board.BoardMap[pos[0],pos[1]].transform.position;
        position = new int[]{pos[0],pos[1]};
        if(naru){
            Naru();
            askedIfNaru = true;
            naruOnNextMove = false;
        }
        if(naruOnNextMove)naruOnNextMove = false;
        if(wasJustMochiGoma){
            if(pos[1]==0&&canNaru)naruOnNextMove = true;
            wasJustMochiGoma=false;
        }
    }


    public void Naru(){
        currentMovementPossible = movementPossibleAlt;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();  
        spriteRenderer.sprite = altSprite;
        PlayerCtrl.isWaiting = false;
        naruCanvas.SetActive(false);
    }
    public void Naranai(){
        PlayerCtrl.isWaiting = false;
        naruCanvas.SetActive(false);
    }
}
