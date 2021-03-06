﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;

	public Pin[] Pins;	
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		int levelComp = PlayerPrefs.GetInt("levelComp", 0);

		for (int i = 0; i < Pins.Length; i++)
		{
			if (i < levelComp)
			{
				Pins[i].isRed = false;
				Pins[i].isGreen = true;
			}
			else if (i == levelComp)
			{	
				Pins[i].isGreen = false;
				Pins[i].isRed = true;
			}
			else 
			{
				Pins[i].isGreen = false;
				Pins[i].isRed = false;

			}

		}
		if(levelComp > 0)
			Character.Initialise(this, Pins[levelComp-1]);
		else
			Character.Initialise(this, StartPin);
		//DontDestroyOnLoad (transform.gameObject);
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			Character.TrySetDirection(Direction.Up);
		}
		else if(Input.GetKeyUp(KeyCode.DownArrow))
		{
			Character.TrySetDirection(Direction.Down);
		}
		else if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Character.TrySetDirection(Direction.Left);
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			Character.TrySetDirection(Direction.Right);
		}
		// Only load level if it is accessible to the player
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			if(Character.CurrentPin.isGreen)
			{
				PlayerPrefs.SetInt("curLevel", 0);
				SceneManager.LoadScene(Character.CurrentPin.SceneToLoad);
			}
			if(Character.CurrentPin.isRed)
			{
				PlayerPrefs.SetInt("curLevel", 1);
				SceneManager.LoadScene(Character.CurrentPin.SceneToLoad);
			}
			
		}
		else if(Input.GetKeyUp(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}
}
