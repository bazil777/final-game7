using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //if play button pressed then load into build 1(our game)
    public void onPlayButton(){
        SceneManager.LoadScene(1);

    }

    public void onQuitButton(){
        Application.Quit();
    }

}
