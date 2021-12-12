using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Te
{
    public GameObject koma;
    public int[] moveTo;
    public GameObject killKoma;
    public bool naru;
    public Te(GameObject Koma, int[] MoveTo, GameObject KillKoma, bool Naru){
        koma = Koma;
        moveTo = MoveTo;
        killKoma = KillKoma;
        naru = Naru;
    }
}
public class AICtrl : MonoBehaviour
{
    public int initKomaCount;
    static public int stInitKomaCount;
    public GameObject[] initKomaArray;
    static public GameObject[] stInitKomaArray;
    static public List<GameObject> myKomas = new List<GameObject>(10);
    static public List<GameObject> mochiGoma = new List<GameObject>(10);
    public GameObject mochiGomaPositionMarkerObject;
    static public Vector3 mochiGomaPositionMarker;
    public float waitTime;
    public static float timer = 0;
    void Start()
    {
        stInitKomaArray = initKomaArray;
        stInitKomaCount = initKomaCount;
        for (int i = 0; i < initKomaCount; i++)
        {
            Koma komaScript = initKomaArray[i].GetComponent<Koma>();
            myKomas.Add(initKomaArray[i]);
            komaScript.isPlayersKoma = false;
        }
        mochiGomaPositionMarker = mochiGomaPositionMarkerObject.transform.position;
    }
    static public void Init()
    {
        myKomas.Clear();
        for (int i = 0; i < stInitKomaCount; i++)
        {
            Koma komaScript = stInitKomaArray[i].GetComponent<Koma>();
            myKomas.Add(stInitKomaArray[i]);
            komaScript.isPlayersKoma = false;
            stInitKomaArray[i].transform.eulerAngles = new Vector3(0,0,180); 
        }
        mochiGoma.Clear();
    }
    void Update()
    {
        if(PlayerCtrl.isWaiting)return;
        if(!PlayerCtrl.isMyTurn){
            while(timer<waitTime){
                timer += Time.deltaTime;
                return;
            }
            List<Te> toreruTe = new List<Te>(100);
            for (int i = 0; i < myKomas.Count; i++)
            {
                Koma komaScript = myKomas[i].GetComponent<Koma>();
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if(komaScript.isMochiGoma){
                            Masu masuScript = Board.BoardMap[j,k].GetComponent<Masu>();
                            if(masuScript.notteruKoma==null){
                                Te newTe = new Te();
                                newTe.koma = myKomas[i];
                                newTe.moveTo = new int[2]{j,k};
                                toreruTe.Add(newTe);
                            }
                        }
                        else{
                            if(komaScript.currentMovementPossible[2-j,2-k]){
                                int newX = komaScript.position[0]+Koma.movement[j,k,0];
                                int newY = komaScript.position[1]+Koma.movement[j,k,1];
                                if(newX>=0&&newY>=0&&newX<=2&&newY<=2){
                                    Masu masuScript = Board.BoardMap[newX,newY].GetComponent<Masu>();
                                    if(masuScript.notteruKoma==null){
                                        Te newTe = new Te();
                                        newTe.koma = myKomas[i];
                                        newTe.moveTo = new int[2]{newX,newY};
                                        toreruTe.Add(newTe);
                                    }
                                    else{
                                        Koma notteruKomaScript = masuScript.notteruKoma.GetComponent<Koma>();
                                        if(notteruKomaScript.isPlayersKoma){
                                            Te newTe = new Te();
                                            newTe.koma = myKomas[i];
                                            newTe.moveTo = new int[2]{newX,newY};
                                            newTe.killKoma = masuScript.notteruKoma;
                                            toreruTe.Add(newTe);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if(toreruTe.Count==0){
                timer = 0;
                PlayerCtrl.isMyTurn = true;
            }
            else{
                int rndTeIndex = Random.Range(0,toreruTe.Count);
                TeWoToru(toreruTe[rndTeIndex]);
            }
        }
    }
    void TeWoToru(Te newte){
        bool naru = false;
        Koma newTeKomaScript = newte.koma.GetComponent<Koma>();
        if(newte.killKoma!=null){
            newte.killKoma.transform.Rotate(0,0,180);
            Koma killKomaScript = newte.killKoma.GetComponent<Koma>();
            killKomaScript.Init();
            PlayerCtrl.myKomas.Remove(newte.killKoma);
            killKomaScript.isMochiGoma = true;
            mochiGoma.Add(newte.killKoma);
            myKomas.Add(newte.killKoma);
            UpdateMochiGoma();
        }
        if(newTeKomaScript.isMochiGoma){
            newTeKomaScript.isMochiGoma = false;
            newTeKomaScript.wasJustMochiGoma = true;
            mochiGoma.Remove(newte.koma);
        }
        else if((newTeKomaScript.canNaru&&newte.moveTo[1]==0)||newTeKomaScript.naruOnNextMove){
            naru = true;
        }
        newTeKomaScript.AIMoveTo(newte.moveTo,naru);
        timer = 0;
        PlayerCtrl.isMyTurn = true;
    }
    void UpdateMochiGoma()
    {
        for (int i = 0; i < mochiGoma.Count; i++)
        {
            Koma komaScript = mochiGoma[i].GetComponent<Koma>();
            komaScript.isPlayersKoma = false;
            Vector3 zurasu = new Vector3(-1f*i,0,0);
            mochiGoma[i].transform.position = mochiGomaPositionMarker + zurasu;
        }
    }
}
