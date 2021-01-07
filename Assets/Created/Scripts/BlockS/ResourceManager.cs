using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    GameManager gm;
    List<string> materialNameList;

    [SerializeReference]
    List<int> materialValue;

    private void Start() {
        gm = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>();
        materialValue = new List<int>();
        materialNameList = new List<string>();
        for ( int i = 0; i < gm.prefabBlockList.Count; i++ ) {
            materialNameList.Add( gm.prefabBlockList[i].GetComponent<Block>().material );
        }
        
    }

    private void FixedUpdate() {
        if ( materialValue.Count < materialNameList.Count ) {
            materialValue.Add( 0 );
        }
    }

    public void AddResource( string resource, int amount ) {
        materialValue[ materialNameList.IndexOf( resource ) ] += amount;
    }

    public List<int> GetMaterialValue(){
        return materialValue;
    }

    public List<string> GetMaterialNameList() {
        return materialNameList;
    }
}
