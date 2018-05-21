using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderController : MonoBehaviour {
	private GameObject indicator;
	private GameObject indicatorS;
	private GameObject[] indexes;
	//private GameObject[] checkpoint;
	//private float checkpointL;
	private float posIndex;
	public PlayerManager playerManager;
	private float previousIndex = -1;
	
	// Use this for initialization
	void Start () {
		indicator = GameObject.FindWithTag("Player");
		indicatorS = GameObject.Find("indicatorS");
		indexes = GameObject.FindGameObjectsWithTag("index");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (indicator && indicatorS.GetComponent<MeshRenderer>().enabled )
		{	
			for (int i = 0; i < indexes.Length; i++)
			{
				var dist = indexes[i].transform.position.x - indicator.transform.position.x;
				if (dist < 2 && dist > 0)
				{
					var currentIndex = i;
					if (previousIndex != currentIndex)
					{
						playerManager.goToCheckpoint(currentIndex);
						previousIndex = currentIndex;
					}
					//DEBUG
					setIndicColor(indexes[i],Color.blue, Color.blue);
				} else {
					setIndicColor(indexes[i],Color.white, Color.white);
				}
			}
			Debug.Log("<color=yellow> distance indic 1 : "+ (indexes[1].transform.position.x - indicator.transform.position.x) +"</color>");
			Debug.Log("<color=white> distance indic 2: " + (indexes[2].transform.position.x - indicator.transform.position.x) + "</color>");
			Debug.Log("<color=red> distance indic 3 : "+(indexes[0].transform.position.x - indicator.transform.position.x) +"</color>");
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
	private void setIndicColor(GameObject indic, Color color, Color specular)
	{
		//Fetch the Renderer from the GameObject
		Renderer rend = indic.GetComponent<Renderer>();

		//Set the main Color of the Material to green
		rend.material.shader = Shader.Find("_Color");
		rend.material.SetColor("_Color", color);

		//Find the Specular shader and change its Color to red
		rend.material.shader = Shader.Find("Specular");
		rend.material.SetColor("_SpecColor", specular);
	}
}
