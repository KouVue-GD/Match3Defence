using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     * Contains the slots or a copy of grid to modify
     * 
     * 
     * */
    public GameObject gridGameObject;
    Grid grid;

    public GameObject block;
    public GameObject blockSpawnPoint;

    public float moveSpeed;
    public float alignStrength;
    public float gravity;

    public int blockPreviewAmount;

    bool spawningBlock = false;

    public GameObject blockPreview;

    GameObject currBlock;

    public Vector3 blockPreviewOffset;

    public List<GameObject> prefabBlockList;

    [HideInInspector]
    public List<GameObject> blockPreviewList;

    [SerializeReference]
    List<GameObject> listOfSlots;

    

    private void Start() {
        grid = gridGameObject.GetComponent<Grid>();

        positionFoundList = new List<int>();
        listOfSlots = new List<GameObject>( grid.GetListOfSlots() );

        //populate block preview
        blockPreviewList = new List<GameObject>();
        for ( int i = 0; i < blockPreviewAmount; i++ ) {
            blockPreviewList.Add( GameObject.Instantiate(prefabBlockList[ Random.Range( 0, prefabBlockList.Count ) ] ));
        }

        //orders block preview
        ReorderBlockPreview();
    }

    private void Update() {

        //change boolean, spawn block
        if ( spawningBlock == false ) {
            spawningBlock = true;

            //uses next block
            currBlock = blockPreviewList[ 0 ];
            currBlock.transform.position = blockSpawnPoint.transform.position;
            currBlock.transform.parent = blockSpawnPoint.transform.parent;
            currBlock.GetComponent<Rigidbody>().isKinematic = false;

            //spawns next random block
            blockPreviewList.RemoveAt( 0 );
            blockPreviewList.Add( GameObject.Instantiate( prefabBlockList[ Random.Range( 0, prefabBlockList.Count ) ] ) );
            ReorderBlockPreview();
        }

        //block magic
        MoveBlock();

        //once block lands, click boolean
        if ( currBlockEnded == true ) {
            spawningBlock = false;
            currBlockEnded = false;
        }

        //check for match 3
        CheckMatch();
    }

    /*
     * blockPreviewList 
     * contains next blocks
     * 
     * 
     * */
     
     private void ReorderBlockPreview() {
         for ( int i = 0; i < blockPreviewAmount; i++ ) {
             blockPreviewList[ i ].transform.parent = blockPreview.transform;
             blockPreviewList[ i ].transform.position = blockPreview.transform.position + (i * blockPreviewOffset);
             blockPreviewList[ i ].GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    /*
     * Checking functions
     * 
     * 
     * */
    
    //check each block around it, if there are more than 2 matches, check the rest of the matches and remove
    private void CheckMatch() {
        //check each slot
        for ( int i = 0; i < listOfSlots.Count; i++) {
            //check slot around
            CheckSlots(listOfSlots, i );
        }
    }

    GameObject currDestroy;
    List<int> positionFoundList;
    int targetCount;
    string mat;
    //delete if more than 3 found
    private void CheckSlots( List<GameObject> target, int position ) {
 
        if ( target[ position ].GetComponent<Block>() != null) {
            //get mat
            mat = target[ position ].GetComponent<Block>().material;
            positionFoundList.Clear();
            positionFoundList.Add( position );
            targetCount = 0;

            //check targets around it
            CheckSlotsHelper( target, position, mat, positionFoundList );

            if ( targetCount >= 2 ) {
                print( positionFoundList.Count + " targets found!!" );

                //delete
                for ( int i = 0; i < positionFoundList.Count; i++ ) {
                    currDestroy = target[ positionFoundList[ i ] ];
                    target[ positionFoundList[ i ] ] = grid.GetListOfSlots()[ i ];
                    GameObject.Destroy( currDestroy );
                }
                //score
            }
        }
    }

    public List<GameObject> GetListOfSlots() {
        return listOfSlots;
    }

    //checks around target
    //if found checks around that target
    //and so forth, unless its already found in positionFoundList
    private void CheckSlotsHelper( List<GameObject> target, int pos, string mat, List<int> foundPos ) {

        //check beside it
        for ( int i = pos - 1; i <= pos + 1; i++ ) {
            //checks if original is rightmost of grid and if i is leftmost  ...checks if original is leftmost and if i is rightmost
            if ( !(pos%grid.gridWidth == 0 && i == pos - 1) && !( pos % grid.gridWidth == grid.gridWidth - 1 && i == pos + 1 ) ) {
                if ( i >= 0 && i < target.Count ) {
                    if ( target[ i ].GetComponent<Block>() != null ) {
                        //compare against mat
                        if ( target[ i ].GetComponent<Block>().material == mat ) {
                            //if position hasn't been added yet
                            if ( !foundPos.Contains( i ) ) {
                                //count
                                targetCount += 1;
                                //add to list
                                foundPos.Add( i );
                                //check around new target
                                CheckSlotsHelper( target, i, mat, foundPos );
                            }
                        }
                    }
                }
            }
        }

        //check above it
        for ( int i = pos - 1 + grid.gridWidth; i <= pos + 1 + grid.gridWidth; i++ ) {
            //checks if original is rightmost of grid and if i is leftmost  ...checks if original is leftmost and if i is rightmost
            if ( !( pos % grid.gridWidth == 0 && i == pos - 1 + grid.gridWidth) && !( pos % grid.gridWidth == grid.gridWidth - 1 && i == pos + 1 + grid.gridWidth )){ //|| pos % grid.gridWidth == i % grid.gridWidth ){
                if ( i >= 0 && i < target.Count ) {
                    if ( target[ i ].GetComponent<Block>() != null ) {
                        //compare against mat
                        if ( target[ i ].GetComponent<Block>().material == mat ) {
                            //if position hasn't been added yet
                            if ( !foundPos.Contains( i ) ) {
                                //count
                                targetCount += 1;
                                //add to list
                                foundPos.Add( i );
                                //check around new target
                                CheckSlotsHelper( target, i, mat, foundPos );
                            }
                        }
                    }
                }
            }
        }

        //check under it
        for ( int i = pos - 1 - grid.gridWidth; i <= pos + 1 - grid.gridWidth; i++ ) {
            //checks if original is rightmost of grid and if i is leftmost  ...checks if original is leftmost and if i is rightmost
            if ( !( pos % grid.gridWidth == 0 && i == pos - 1 - grid.gridWidth ) && !( pos % grid.gridWidth == grid.gridWidth - 1 && i == pos + 1 - grid.gridWidth ) ) { //|| pos % grid.gridWidth == i % grid.gridWidth ){
                if ( i >= 0 && i < target.Count ) {
                    if ( target[ i ].GetComponent<Block>() != null ) {
                        //compare against mat
                        if ( target[ i ].GetComponent<Block>().material == mat ) {
                            //if position hasn't been added yet
                            if ( !foundPos.Contains( i ) ) {
                                //count
                                targetCount += 1;
                                //add to list
                                foundPos.Add( i );
                                //check around new target
                                CheckSlotsHelper( target, i, mat, foundPos );
                            }
                        }
                    }
                }
            }
        }
    }


    /*
     * MOVE BLOCKS FUNCTIONS :D
     * 
     * 
     */

    //TODO: change Move to apply physics

    private void MoveBlock() {

        //player moves block
        currBlock.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis( "Vertical" ), 0) * Time.deltaTime * moveSpeed;

        //gravity
        //currBlock.transform.position += new Vector3(0, -gravity, 0 ) * Time.deltaTime;
    }

    bool currBlockEnded;

    public void EndBlock( GameObject target ) {
        if ( target == currBlock ) {
            //stop rb
            target.GetComponent<Rigidbody>().isKinematic = true;
            //set new position
            target.transform.position = GetNearestBlockPosition( target );
            //place in slot
            listOfSlots[ FindSlotNum( target.transform.position ) ] = target;
            //signal end of block
            currBlockEnded = true;


        } else if ( !listOfSlots.Contains( target ) ) {
            //stop rb
            target.GetComponent<Rigidbody>().isKinematic = true;
            //set new position
            target.transform.position = GetNearestBlockPosition( target );
            //place in slot
            listOfSlots[ FindSlotNum( target.transform.position ) ] = target;

        } else {
            //stop rb
            target.GetComponent<Rigidbody>().isKinematic = true;
            //replace
            listOfSlots[ FindSlotNum( target ) ] = grid.GetListOfSlots()[ FindSlotNum( target ) ];
            //set new position
            target.transform.position = GetNearestBlockPosition( target );
            //place in slot
            listOfSlots[ FindSlotNum( target.transform.position ) ] = target;
        }
    }

    private Vector3 GetNearestBlockPosition(GameObject target) {

        return new Vector3( grid.zeroXPosition[ FindXSlot( target ) ], grid.zeroYPosition[ FindYSlot( target ) ], target.transform.position.z );
    }

    private int FindXSlot(GameObject target) {
        //align block to grid
        float smallest = target.transform.position.x - grid.zeroXPosition[ 0 ];
        int smallestIndex = 0;

        //find closest slot
        for ( int i = 1; i < grid.zeroXPosition.Count; i++ ) {
            
            if ( Mathf.Abs(target.transform.position.x - grid.zeroXPosition[ i ]) < smallest ) {
                //print( target.transform.position.x + " - " + grid.zeroXPosition[ i ] + " < " + smallest );
                smallest = target.transform.position.x - grid.zeroXPosition[ i ];
                smallestIndex = i;
            }
        }

        return smallestIndex;
    }

    private int FindYSlot( GameObject target ) {
        //align block to grid
        float smallest = target.transform.position.y - grid.zeroYPosition[ 0 ];
        int smallestIndex = 0;

        //find closest slot
        for ( int i = 1; i < grid.zeroYPosition.Count; i++ ) {
            if ( Mathf.Abs( target.transform.position.y - grid.zeroYPosition[ i ] ) < smallest ) {
                smallest = target.transform.position.y - grid.zeroYPosition[ i ];
                smallestIndex = i;
            }
        }

        return smallestIndex;
    }

    private int FindSlotNum(Vector3 position ) {
        int num = -1;
        Vector3 newPos = position;
        newPos.z = grid.GetListOfSlots()[ 0 ].transform.position.z;

        for ( int i = 0; i < listOfSlots.Count; i++ ) {
            if( newPos == grid.GetListOfSlots()[ i ].transform.position ) {
                num = i;
                break;
            }
        }

        return num;
    }

    private int FindSlotNum( GameObject target ) {
        int num = -1;

        for ( int i = 0; i < listOfSlots.Count; i++ ) {
            if ( target == listOfSlots[i]) {
                num = i;
                break;
            }
        }

        return num;
    }
}
