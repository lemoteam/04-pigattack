using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public GameObject player;
	public GameObject pursuer;
	public Camera cam;
	public int nbPursuer;
	public int radiusPursuer;
	
	
	private void Awake()
	{
		createPursuer();
	}

	void createPursuer()
	{
		for (var i = 0; i < nbPursuer; i++)
		{
			// Prepare position
			var position = calcArroundPosition(i, radiusPursuer) + player.transform.position;
			
			// Instantiate
			GameObject pursuerObj = Instantiate(pursuer);
			
			// Set position
			pursuerObj.transform.parent = transform;
			pursuerObj.transform.position = position;
			
			// Set camera
			var controller = pursuerObj.GetComponent<PlayerController>();
			controller.cam = cam;
		}
	}


	private Vector3 calcArroundPosition(int index, int radius)
	{
		var angle = (360 / nbPursuer) * index;
		var posX = Math.Sin((angle * Math.PI / 180)) * radius;
		var posY = Math.Cos((angle * Math.PI / 180)) * radius;
		
		return new Vector3(ToSingle(posX), 0, ToSingle(posY));
	}
	
	
	private static float ToSingle(double value)
	{
		return (float)value;
	}
}
