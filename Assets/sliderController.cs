using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderController : MonoBehaviour {
    private GameObject max;
    private GameObject min;
	private GameObject indicator;
	private GameObject[] checkpoint;
	private float checkpointL;
	private float posIndex;
	public PlayerManager playerManager;
	private float previousIndex = -1;
	
	// Use this for initialization
	void Start () {
		indicator = GameObject.FindWithTag("Player");
		max = GameObject.FindWithTag("max");
		min = GameObject.FindWithTag("min");
		checkpoint = GameObject.FindGameObjectsWithTag("Checkpoint");
		checkpointL = checkpoint.Length;
		posIndex = 100 / checkpointL;
		Debug.Log("checkpoint l :" + posIndex);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (indicator && indicator.GetComponent<MeshRenderer>().enabled )
		{
			var mapValue = Map(0, 100, min.transform.position.x, max.transform.position.x, indicator.transform.position.x);
			/* Debug.Log("<color=red> min position : "+ min.transform.position.x+"</color>");
			Debug.Log("<color=yellow> max position : "+ max.transform.position.x+"</color>");
			Debug.Log("<color=white> indicator" + indicator.transform.position.x + "</color>");
			Debug.Log("<color=grey> map " + mapValue + "</color>"); */
			var currentIndex = Mathf.Ceil(mapValue / posIndex);
			;
			if (currentIndex > 0 && previousIndex != currentIndex)
			{
				Debug.Log("<color=grey> currentIndex :  " + currentIndex + "</color>");
				playerManager.goToCheckpoint((int)currentIndex - 1);
				previousIndex = currentIndex;
			}
		}
	}
	
	public float Map(float from, float to, float from2, float to2, float value) {
		if(value <= from2) {
			return from;
		} else if(value >= to2) {
			return to;
		} else {
			return (to - from) * ((value - from2) / (to2 - from2)) + from;
		}
	}
}
