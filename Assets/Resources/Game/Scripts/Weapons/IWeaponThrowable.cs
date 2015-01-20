using UnityEngine;
using System.Collections;

public interface IWeaponThrowable
{
	void Spawned ();
	void Wait ();
	void Activate ();
	void Active ();
}
