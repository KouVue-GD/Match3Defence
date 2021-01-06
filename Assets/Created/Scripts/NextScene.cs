using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    public List<string> level;
    public int num;

    public void loadlevel() {
        SceneManager.LoadScene( level[num] );

    }
}
