using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float radiusGravity = 5;
	public float mass = 10;

	private void Awake()
	{
		ObstacleManager.OBSTACLES.Add(this);
	}

	private void OnDestroy()
	{
		ObstacleManager.OBSTACLES.Remove(this);
	}
}
