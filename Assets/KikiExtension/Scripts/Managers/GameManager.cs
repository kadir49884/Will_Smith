﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using KikiExtension;
using KikiExtension.Managers;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour
{

	private bool IsGameFinish { get; set; }
	private bool IsGameStarted { get; set; }

	public Action GameStart { get; set; }
	public Action GameWin { get; set; }
	public Action GameFail { get; set; }

	public bool RunGame { get => IsGameStarted && !IsGameFinish; }

	
    public static GameManager Instance { get => instance; set => instance = value; }

    private static GameManager instance;

	[SerializeField]
	private bool isReadyForLevel;

	public ScriptableBase[] managers;
	public LevelManager LevelManager { get; private set; }

	private void Awake()
    {
		if (instance == null)
		{
			instance = this;
		}
		if(isReadyForLevel)
        {
			GetLevel();
		}
    }

	private void Start()
    {
		GameStart += Initialize;
		GameWin += Game_Win;
		GameFail += Game_Fail;
	}

	private void GetLevel()
    {
        //getPath = Path.Combine("ScriptableObjects", "LevelManager");
        //LevelManager levelManager = Resources.Load<LevelManager>(getPath);
        LevelManager = (LevelManager)managers.Find(manager => manager.name.Contains("LevelManager"));
        LevelManager.Initialize();
    }


    public void Initialize()
	{
		IsGameStarted = true;
	}

	private void Game_Win()
	{
		IsGameFinish = true;
	}


	private void Game_Fail()
	{
		IsGameFinish = true;
	}

	public void NextLevel()
	{
		PlayerPrefsManager.LevelNext();
        RestartLevel();
    }
	public void PreviousLevel()
	{
		PlayerPrefsManager.PreviousLevel();
        RestartLevel();
    }

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

#if UNITY_EDITOR
    private void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        if (Input.GetKeyDown(KeyCode.N))
			NextLevel();

		if (Input.GetKeyDown(KeyCode.B))
			PreviousLevel();
	}
#endif

}



