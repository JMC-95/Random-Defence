using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public Text text;

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
        Destroy(GameObject.Find("DataManager"));
    }

    public void LoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
