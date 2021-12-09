using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    static public bool isMyTurn = true;
    public List<GameObject> mochiGoma = new List<GameObject>(10);
    static public GameObject selectedKoma;
    void Start()
    {
        for (int i = 0; i < mochiGoma.Count; i++)
        {
            Koma komaScript = mochiGoma[i].GetComponent<Koma>();
            komaScript.isPlayersKoma = true;
        }
    }

    void Update()
    {
        if(isMyTurn&&Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hitKoma,hitMasu;
            if( Physics.Raycast( ray, out hitKoma, 100 ,1 << LayerMask.NameToLayer("Koma")) )
            {
                Koma komaScript = hitKoma.transform.gameObject.GetComponent<Koma>();
                if(komaScript.isPlayersKoma)komaScript.Clicked();
            }
            else if( Physics.Raycast( ray, out hitMasu, 100 ,1 << LayerMask.NameToLayer("Masu")) )
            {
                Masu masuScript = hitMasu.transform.gameObject.GetComponent<Masu>();
                masuScript.Clicked();
            }
            else{
                selectedKoma = null;
                Koma.ResetBoard();
            }
        }
    }
}
