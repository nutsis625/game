using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvvas : MonoBehaviour
{
    public void LoadSceneByName(string game)
    {
        SceneManager.LoadScene(game);
    }
}
