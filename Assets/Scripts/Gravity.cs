using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity
{
	private List<Obstacle> obstacles;

	public void Start()
	{
		obstacles = ObstacleManager.OBSTACLES;
	}

	public Vector3 GetGravity(Vector3 position, float maxMagnitude)
	{
		Vector3 gravity = Vector3.zero;

		if (obstacles != null && obstacles.Count > 0)
		{
			foreach (Obstacle body in obstacles)
			{
				Vector3 dir = body.transform.position - position;
				float dist = dir.magnitude;

				if (dist <= body.radiusGravity) // avoid if the player is crashed
				{
					float force = 667.4f * (body.mass) / Mathf.Pow(dist, 2);
					gravity += dir.normalized * force;
				}
			}
		}

		gravity = Vector3.ClampMagnitude(gravity, maxMagnitude);

		return gravity;
	}
}
