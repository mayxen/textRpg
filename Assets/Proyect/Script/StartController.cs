﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

	public void StartGame(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
