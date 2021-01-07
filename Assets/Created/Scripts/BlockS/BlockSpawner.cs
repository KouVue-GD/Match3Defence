using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameManager gm;
    public Grid grid;
    public Transform spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    float timer = 0;
    public float spawnTime;
    GameObject currBlock;
    GameObject randomBlock;
    

    //Vector3 randomTransform;
    //Vector3 lowestPoint;

    Vector3 spawnPos;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime) {
            //spawn random blocks at random locations every few seconds
            randomBlock = gm.prefabBlockList[ Random.Range( 0, gm.prefabBlockList.Count ) ];
            currBlock = Instantiate( randomBlock );

            //random pos
            spawnPos = gm.GetListOfSlots()[ Random.Range( 0, grid.gridWidth ) ].transform.position;

            //lowestPoint
            //spawnPos = FindLowestPointOnGrid();
            currBlock.transform.position = new Vector3(spawnPos.x, spawnPoint.position.y, spawnPoint.position.z);

            timer -= spawnTime;
        }
    }

    private Vector3 FindLowestPointOnGrid() {
        Vector3 vectorToReturn = Vector3.positiveInfinity;
        //slots are ordered 0 to width per row
        for ( int i = 0; i < gm.GetListOfSlots().Count; i++ ) {
            //print( gm.GetListOfSlots()[ i ] + " != " + grid.GetListOfSlots()[ i ] );
            //look for next empty
            if ( gm.GetListOfSlots()[ i ] == grid.GetListOfSlots()[ i ] ) {
                vectorToReturn = gm.GetListOfSlots()[ i ].transform.position;
                break;
            }
        }
        

        return vectorToReturn;
    }
}
