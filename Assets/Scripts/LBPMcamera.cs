using UnityEngine;
using System.Collections;

public class LBPMcamera : MonoBehaviour
{
	public Transform player;

	void Update()
	{
		transform.position = new Vector3 (player.position.x, player.position.y, -1.0f);
	}
}
