using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public class Pin : MonoBehaviour
{
	[Header("Options")] //
	public bool IsAutomatic;
	public bool HideIcon;
	public string SceneToLoad;
	public bool isGreen;
	public bool isRed;
	
	[Header("Pins")] //
	public Pin UpPin;
	public Pin DownPin;
	public Pin LeftPin;
	public Pin RightPin;

	private Dictionary<Direction, Pin> _pinDirections; 
	
	SpriteRenderer renderer;

	/// <summary>
	/// Use this for initialisation
	/// </summary>
	private void Start()
	{
		// Load the directions into a dictionary for easy access
		renderer = GetComponent<SpriteRenderer>();


		_pinDirections = new Dictionary<Direction, Pin>
		{
			{ Direction.Up, UpPin },
			{ Direction.Down, DownPin },
			{ Direction.Left, LeftPin },
			{ Direction.Right, RightPin }
		};
		
		// Hide the icon if needed
		if (HideIcon)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	public void Update()
	{
		if (isGreen) 
		{
			renderer.color = new Color(0f, 0.9811321f, 0.05379835f, 1f);
		}
		else if (isRed)
		{
			renderer.color = new Color(0.9803922f, 0.01919986f, 0f, 1f);
		}
	}
	
	/// <summary>
	/// Get the pin in a selected direction
	/// Using a switch statement rather than linq so this can run in the editor
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public Pin GetPinInDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
				return UpPin;
			case Direction.Down:
				return DownPin;
			case Direction.Left:
				return LeftPin;
			case Direction.Right:
				return RightPin;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}

	
	/// <summary>
	/// This gets the first pin thats not the one passed 
	/// </summary>
	/// <param name="pin"></param>
	/// <returns></returns>
	public Pin GetNextPin(Pin pin)
	{
		return _pinDirections.FirstOrDefault(x => x.Value != null && x.Value != pin).Value;
	}
	
	
	/// <summary>
	/// Draw lines between connected pins
	/// </summary>
	private void OnDrawGizmos()
	{
		if(UpPin != null) DrawLine(UpPin);
		if(RightPin != null) DrawLine(RightPin);
		if(DownPin != null) DrawLine(DownPin);
		if(LeftPin != null) DrawLine(LeftPin);
	}


	/// <summary>
	/// Draw one pin line
	/// </summary>
	/// <param name="pin"></param>
	protected void DrawLine(Pin pin)
	{   
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pin.transform.position);
	}
}
