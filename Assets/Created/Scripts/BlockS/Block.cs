using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string material;
    public int value;
    ResourceManager rm;
    GameManager gm;

    private void Awake() {
        rm = GameObject.FindGameObjectWithTag( "ResourceManager" ).GetComponent<ResourceManager>();
        gm = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameManager>();
    }

    private void OnDestroy() {
        rm.AddResource( material, value );
        
        //TODO: block destruction animation
    }

    RaycastHit hit;

    public float raycastRange;
    private void FixedUpdate() {
        Physics.Raycast( transform.position, -transform.up, out hit, raycastRange );
        if ( hit.transform != null && !gm.blockPreviewList.Contains( gameObject )) {
            gm.EndBlock( gameObject );
        } else {
            if (!gm.blockPreviewList.Contains(gameObject)) {
                //TODO: delay before dropping
                GetComponent<Rigidbody>().isKinematic = false;

                //TODO: change how gravity works
                //prefabally something like add force 
                transform.position += new Vector3( 0, -gm.gravity, 0 ) * Time.deltaTime;
            }
            
        }
    }
}
