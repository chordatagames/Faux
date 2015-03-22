using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InputManager : EditorWindow
{
	//GUI
}

/// <summary>
/// Input setup.
/// Stores a setup for the input. Devices and their axes and actions will be listed within.
/// </summary>
[System.Serializable]
public class InputSetup : ScriptableObject
{
	public List<Device> devices = new List<Device>();
}
/// <summary>
/// Deice.
/// The generic device-type. Holding a set of axes with their actions
/// </summary>
[System.Serializable]
public class Device
{
	public string name;
	public int ID;
	public Axis[] axes = new Axis[8];
//	public List<Axis> axis = new List<Axis>();
}

[System.Serializable]
public class Axis
{
	public string name;
	public Input input;
	public AxisAction action;
}

[System.Serializable]
public class AxisAction //Generic 'action' that can be performed in the game. such as jump, shoot, run or flip
{
	public string 	name;
	public bool 	snap; //should input register the value snapped 1/0?
	public bool		holdAction; //should input register when the action-button is held

	//TODO add autodetect and set default
}

