using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InputManager : ScriptableObject
{
	//GUI
	public static InputSetup inputSetup;

	public static bool GetKey( string name )
	{
		if (Input.GetKey(name))
		{
			foreach (InputSetup.Device d in inputSetup.Devices)
			{
				for (int i = 0; i < inputSetup.Devices.Length; i++)
				{
					if (d.controls[i].name == name)
					{ return true; }
				}
			}
		}
		return false;
	}

	public static bool GetKeyDown( string name )
	{
		if (Input.GetKeyDown(name))
		{
			foreach (InputSetup.Device d in inputSetup.Devices)
			{
				for (int i = 0; i < inputSetup.Devices.Length; i++)
				{
					if (d.controls[i].name == name)
					{ return true; }
				}
			}
		}
		return false;
	}

	public static bool GetKeyUp( string name )
	{
		if (Input.GetKeyUp(name))
		{
			foreach (InputSetup.Device d in inputSetup.Devices)
			{
				for (int i = 0; i < inputSetup.Devices.Length; i++)
				{
					if (d.controls[i].name == name)
					{ return true; }
				}
			}
		}
		return false;
	}

	public static float GetAxis( string name )
	{
		float axis = Input.GetAxis(name);
		if ( axis == 0 )
		{
			foreach (InputSetup.Device d in inputSetup.Devices)
			{
				for (int i = 0; i < inputSetup.Devices.Length; i++)
				{
					if (d.controls[i].name == name)
					{ return axis; }
				}
			}
		}
		return 0;
	}

	public static int GetAxisRaw ( string name )
	{
		if ( Input.GetAxis(name) )
		{
			foreach (InputSetup.Device d in inputSetup.Devices)
			{
				for (int i = 0; i < inputSetup.Devices.Length; i++)
				{
					if (d.controls[i].name == name)
					{ return 1; }
				}
			}
		}
		return 0;
	}
}

/// <summary>
/// Input setup.
/// Stores a setup for the input. Devices and their axes and actions will be listed within.
/// </summary>
[System.Serializable]
public class InputSetup : ScriptableObject
{
	public List<Device> devices = new List<Device>();
	public Device[] Devices { get { return devices.ToArray(); } }

	/// <summary>
	/// The generic device-type. Holding a set of controlls with their actions
	/// </summary>
	[System.Serializable]
	public class Device
	{
		public static int connectedDevices;
		public string name;
		public int ID;
		public Controls[] controls = new Controls[1]; //All the registered axes go here.

		public Device()
		{
			ID = connectedDevices++; 
		}
	}

	[System.Serializable]
	public class Controls
	{
		public string name; //Name of the button
		public ControlAction action;
	}

	[System.Serializable]
	public class ControlAction //Generic 'action' that can be performed in the game. such as jump, shoot, run or flip
	{
		public string 	name;
		public bool		holdAction; //should input register when the action-button is held
		
		//TODO add autodetect and set default
	}

	[System.Serializable]
	public class AxisAction : ControlAction//Generic 'action' that can be performed in the game. such as jump, shoot, run or flip
	{
		public bool 	snap; 		//should input register the value snapped 1/0?
		
		//TODO add autodetect and set default
	}
}


