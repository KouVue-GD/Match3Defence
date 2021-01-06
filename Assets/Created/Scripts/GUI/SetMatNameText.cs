using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMatNameText : MonoBehaviour
{
    public ResourceManager rm;
    public int index;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if ( index < rm.GetMaterialNameList().Count ) {
            if ( text.text != ( rm.GetMaterialNameList()[ index ] + ": " ) ) {
                text.text = rm.GetMaterialNameList()[ index ] + ": ";
            }
        }
    }
}
