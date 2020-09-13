﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float thrustPower = 1;
	public float thrustMin = 0.3f; // must > 0.2
	public float thrustMax = 5;
	public float rotateSpeed = 2;
	public float gravityMax = 4;

	public bool isDebug = true;
	public bool isGravity = true;

	private Rigidbody rb;
	private Transform player;
	private Gravity gravityManager;

	private Vector3 velocity = Vector3.zero;
	private Vector3 thrushtVelocity = Vector3.zero;
	private Vector3 gravity = Vector3.zero;
	private float thrust = 0;

	private float thrustIntensity;
	private float rotateIntensity;
	private bool isThrusting = false;
	private bool isRotating = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		player = transform;
		gravityManager = new Gravity();
		gravityManager.Start();

		velocity = new Vector3(0, 0, 0.3f);
	}

	private void Update()
	{
		thrustIntensity = Input.GetAxisRaw("Vertical");
		rotateIntensity = Input.GetAxisRaw("Horizontal");
		isThrusting = thrustIntensity != 0;
		isRotating = rotateIntensity != 0;
	}

	private void FixedUpdate()
	{
		float dt = Time.deltaTime;

		gravity = gravityManager.GetGravity(rb.position, gravityMax);

		if (!isGravity)
			gravity = Vector3.zero;

		rb.rotation *= Quaternion.AngleAxis(rotateIntensity * rotateSpeed * dt, Vector3.up);

		thrust += thrustIntensity * thrustPower * dt;
		thrust = Mathf.Max(thrust, thrustMin);
		thrust = Mathf.Min(thrust, thrustMax);
		thrushtVelocity = player.forward * thrust * thrustPower;

		velocity = velocity == Vector3.zero ? player.forward * thrustMin : velocity;
		velocity += gravity * dt;

		if (isDebug)
		{
			Debug.DrawLine(rb.position, rb.position + thrushtVelocity * 5, Color.red); // thrust
			Debug.DrawLine(rb.position, rb.position + velocity.normalized * 5, Color.green); // path
		}

		rb.position += thrushtVelocity * dt;
		rb.position += velocity * dt;

		DrawPath(dt);
	}

	private void DrawPath(float dt)
	{
		int steps = 10;
		int multiplier = 10;
		Vector3[] points = new Vector3[steps + 1];
		points[0] = rb.position;

		Vector3 previousPoint = points[0];
		Vector3 pos = points[0];
		Vector3 vel = velocity;
		int index = 1;
		for (int i = 0; i < multiplier * steps; i++)
		{
			if (i% multiplier == 0 && isDebug)
				previousPoint = pos;

			vel += gravityManager.GetGravity(pos, gravityMax) * dt;
			pos += (vel + thrushtVelocity) * dt;

			if (i%multiplier == multiplier - 1)
			{
				if (isDebug)
					Debug.DrawLine(previousPoint, pos, Color.yellow);

				points[index] = pos;
				index++;
			}
		}
	}
}