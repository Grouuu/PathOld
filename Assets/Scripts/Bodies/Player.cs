using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject player;
	public float speedMax;
	public float speedMin;
	public float speedFriction;
	public float rotationSpeed;
	public float rotationFriction;

	protected Rigidbody body;

	protected Vector3 velocity = Vector3.zero; // moving vector
	protected Vector3 thrustForce = Vector3.zero; // thrusting vector (forward) TODO

	private void Start() {
		body = player.GetComponent<Rigidbody>();
		body.constraints =
			 RigidbodyConstraints.FreezePositionZ |
			 RigidbodyConstraints.FreezeRotationX |
			 RigidbodyConstraints.FreezeRotationY;
	}

	public void Turn(float intensity) {
		// rotate body
		Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, rotationSpeed * -intensity));
		body.rotation *= rotation;

		// rotate velocity
		float magnitude = velocity.magnitude;
		velocity = rotation * velocity;
		velocity = velocity.normalized * magnitude; // avoid to change the speed
	}

	public void Thrust(float intensity) {
		Vector3 force = body.transform.up * speedMax * intensity;
		Vector3 newVelocity = velocity + force;

		Debug.Log(Vector3.Dot(newVelocity, velocity));

		// prevent thrust backward
		if (Vector3.Dot(newVelocity, velocity) < 0) {
			Debug.LogError("PREVENT");
			velocity = velocity.normalized * speedMin;
		} else {
			velocity = newVelocity;
		}
	}

	public void UpdateVelocity() {
		velocity = VectorUtils.ClampMagnitude(velocity, speedMax, speedMin);
		body.position += velocity;
	}

	/*public void Move(Vector3 force) {
		velocity += force;
	}*/
}
