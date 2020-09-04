using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float thrustPower;
	public float thrustMin;
	public float thrustMax;
	public float rotateSpeed;
	public float gravityFaceAttract;

	private Rigidbody rb;
	private Transform player;

	private Vector3 velocity = Vector3.forward;
	private float thrust = 0;
	private float rotation = 0;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		player = transform;
	}

	private void Update()
	{
		ChangeThrust(Input.GetAxisRaw("Vertical"));
		ChangeRotation(Input.GetAxisRaw("Horizontal"));
	}

	private void FixedUpdate()
	{
		float dt = Time.deltaTime;

		Vector3 gravity = GetGravity(rb.position);

		FaceGravity(gravityFaceAttract, gravity);

		rb.rotation = Quaternion.AngleAxis(rotation, Vector3.up);

		velocity = player.forward * thrust;

		rb.position += gravity * dt;
		rb.position += velocity * dt;
	}

	public void ChangeThrust(float intensity)
	{
		thrust += intensity * thrustPower;
		thrust = Mathf.Max(thrust, thrustMin);
		thrust = Mathf.Min(thrust, thrustMax);
	}

	public void ChangeRotation(float intensity)
	{
		rotation += intensity * rotateSpeed;
	}

	private void FaceGravity(float intensity, Vector3 gravity)
	{
		rotation += Vector3.SignedAngle(player.forward, gravity, Vector3.up) * intensity;
	}

	private Vector3 GetGravity(Vector3 pos)
	{
		return new Vector3(-2, 0, 0);
	}
}
