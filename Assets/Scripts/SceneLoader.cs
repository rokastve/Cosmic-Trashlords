﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
