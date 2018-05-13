using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	public Camera cam;
	public NavMeshAgent agent;
	public bool isAnimated = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			var ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
	
			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
				isAnimated = true;
				Debug.Log(agent.pathPending);
			}
		}
		
		if (!agent.pathPending && isAnimated)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					Debug.Log("Je suis arrivé");
					isAnimated = false;
				}
			}
		}
	}
}
