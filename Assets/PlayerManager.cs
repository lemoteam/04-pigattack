using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private PlayerManager instance;
	public GameObject player;
	public GameObject pursuer;
	public Camera cam;
	public int nbPursuer;
	public int radiusPursuer;
	private List<GameObject> pursuerList;
	
	private void Awake()
	{
		instance = this;
		pursuerList = new List<GameObject>();
		createPursuer();
	}

	
	private void createPursuer()
	{
		for (var i = 0; i < nbPursuer; i++)
		{
			// Prepare position
			var position = calcArroundPosition(i, radiusPursuer) + player.transform.position;
			
			// Instantiate
			var pursuerObj = Instantiate(pursuer);
			
			// Set position
			pursuerObj.transform.parent = transform;
			pursuerObj.transform.position = position;
			
			// Set camera
			var controller = pursuerObj.GetComponent<PlayerController>();
			controller.cam = cam;		
			
			pursuerList.Add(pursuerObj);
		}
	}


	private Vector3 calcArroundPosition(int index, int radius)
	{
		var angle = (360 / nbPursuer) * index;
		var posX = Math.Sin((angle * Math.PI / 180)) * radius;
		var posY = Math.Cos((angle * Math.PI / 180)) * radius;
		
		return new Vector3(ToSingle(posX), 0, ToSingle(posY));
	}
	
	
	private float ToSingle(double value)
	{
		return (float)value;
	}
	
	
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			var ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
	
			if (Physics.Raycast(ray, out hit))
			{
				var playerController = player.GetComponent<PlayerController>();
				playerController.displaceAgent(hit.point);
			}
			
			instance.StopCoroutine(displacePursuer(hit));
			instance.StartCoroutine(displacePursuer(hit));
		}
	}
	
	
	private IEnumerator displacePursuer(RaycastHit hit)
	{
		yield return new WaitForSeconds(1.5f);
		foreach (var pursuer in pursuerList.Select((value, i) => new { i, value }))
		{
			var playerController = pursuer.value.GetComponent<PlayerController>();
			var position = hit.point + calcArroundPosition(pursuer.i, radiusPursuer);
			playerController.displaceAgent(position);
		}
	}
}
