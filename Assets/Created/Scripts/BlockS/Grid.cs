using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject slot;
    float width;
    float height;

    [SerializeReference]
    List<GameObject> listOfSlots;

    public int gridWidth;
    public int gridHeight;
    int gridSize;

    //border padding
    public float padding;

    //spacing between panels
    public float spacing;

    public Transform slotParent;

    [HideInInspector]
    public List<float> zeroXPosition;

    [HideInInspector]
    public List<float> zeroYPosition;

    public GameObject botBoundry;
    public GameObject leftBoundry;
    public GameObject rightBoundry;


    // Start is called before the first frame update
    private void Awake()
    {
        //Grid
        width = transform.localScale.x;
        height = transform.localScale.y;

        gridSize = gridWidth * gridHeight;
        int count = 0;

        //creates grid
        listOfSlots = new List<GameObject>();
        while ( listOfSlots.Count < gridSize ) {
            for ( int i = 0; i < gridHeight; i++ ) {
                for ( int j = 0; j < gridWidth; j++ ) {
                    
                    listOfSlots.Add( GameObject.Instantiate( slot ) );
                    listOfSlots[ count ].transform.parent = slotParent;
                    listOfSlots[ count ].transform.localPosition = new Vector3( padding + j + spacing * j, padding + i + spacing * i, 0 );
                    listOfSlots[ count ].transform.rotation = slotParent.rotation;
                    listOfSlots[ count ].transform.localScale = slotParent.localScale;
                    listOfSlots[ count ].transform.name = "Slot " + count;
                    count++;
                    

                }
            }
        }

        //gets the position of bottom row
        zeroXPosition = new List<float>();

        for ( int i = 0; i < gridWidth; i++ ) {
            zeroXPosition.Add(listOfSlots[ i ].transform.position.x);
        }

        //gets the position of the first column
        zeroYPosition = new List<float>();

        for ( int i = 0; i < gridWidth; i++ ) {
            zeroYPosition.Add( listOfSlots[ (i * gridWidth) ].transform.position.y );
        }

        //positions grid boundries
        Vector3 holder;

        botBoundry.transform.localScale = new Vector3(  (padding + gridWidth + ( gridWidth * spacing)), 10, 1 );
        holder = botBoundry.transform.position;
        holder.y = listOfSlots[ 0 ].transform.position.y - 0.5f - padding - .3f;
        botBoundry.transform.position = holder;

        leftBoundry.transform.localScale = new Vector3( 1, 10, ( padding + gridHeight + ( gridHeight * spacing ) ) );
        holder = leftBoundry.transform.position;
        holder.x = listOfSlots[ 0 ].transform.position.x - slot.transform.localScale.x - padding;
        leftBoundry.transform.position = holder;

        rightBoundry.transform.localScale = new Vector3( 1, 10, ( padding + gridHeight + ( gridHeight * spacing ) ) );
        holder = rightBoundry.transform.position;
        holder.x = listOfSlots[ gridWidth - 1 ].transform.position.x + slot.transform.localScale.x + padding;
        rightBoundry.transform.position = holder;
    }

    public List<GameObject> GetListOfSlots() {
        return listOfSlots;
    }


       

}
