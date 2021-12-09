using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    static public bool isWaiting = false;
    static public bool isMyTurn = true;
    public List<GameObject> myKomas = new List<GameObject>(10);
    static public List<GameObject> mochiGoma = new List<GameObject>(10);
    static public GameObject selectedKoma;
    public GameObject mochiGomaPositionMarkerObject;
    static public Vector3 mochiGomaPositionMarker;
    static public GameObject naruKamoKoma;
    void Start()
    {
        for (int i = 0; i < myKomas.Count; i++)
        {
            Koma komaScript = myKomas[i].GetComponent<Koma>();
            komaScript.isPlayersKoma = true;
        }
        mochiGomaPositionMarker = mochiGomaPositionMarkerObject.transform.position;
    }

    void Update()
    {
        if(isWaiting)return;

        if(isMyTurn&&Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hitKoma,hitMasu;
            bool gothit = false;
            if( Physics.Raycast( ray, out hitKoma, 100 ,1 << LayerMask.NameToLayer("Koma")) )
            {
                Koma komaScript = hitKoma.transform.gameObject.GetComponent<Koma>();
                if(komaScript.isPlayersKoma)komaScript.Clicked();
                gothit = true;
            }
            if( Physics.Raycast( ray, out hitMasu, 100 ,1 << LayerMask.NameToLayer("Masu")) )
            {
                Masu masuScript = hitMasu.transform.gameObject.GetComponent<Masu>();
                masuScript.Clicked();
                gothit = true;
            }
            if(!gothit){
                selectedKoma = null;
                Koma.ResetBoard();
            }
        }
    }

    static public void GetKoma(GameObject aquiredKoma){
        mochiGoma.Add(aquiredKoma);
        UpdateMochiGoma();
    }
    static void UpdateMochiGoma()
    {
        for (int i = 0; i < mochiGoma.Count; i++)
        {
            Koma komaScript = mochiGoma[i].GetComponent<Koma>();
            komaScript.isPlayersKoma = true;
            Vector3 zurasu = new Vector3(1f*i,0,0);
            mochiGoma[i].transform.position = mochiGomaPositionMarker + zurasu;
        }
    }
    public void Naru(){
        Koma naruKamoKomaScript = naruKamoKoma.GetComponent<Koma>();
        naruKamoKomaScript.Naru();
    }
    public void Naranai(){
        Koma naruKamoKomaScript = naruKamoKoma.GetComponent<Koma>();
        naruKamoKomaScript.Naranai();
    }
}
