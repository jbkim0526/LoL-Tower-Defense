using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuUI : MonoBehaviour
{


    public SceneFader sceneFader;
    // Start is called before the first frame update



    public void Menu()
    {
        sceneFader.Fadeto("MainMenu");
    }


}
