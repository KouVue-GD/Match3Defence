using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1000f;
    public GameObject left;

    public void Update() {
        var movement = Input.GetAxis( "Horizontal" );
        transform.position += new Vector3( movement, 0, 0 ) * Time.deltaTime * speed;
    }

}
