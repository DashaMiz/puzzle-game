using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        ManagerScript.Instance.currentLevel = 0;
    }

    void Update()
    {
        
    }

    public void Play(){
        SceneManager.LoadScene("Level_01");
    }

}
