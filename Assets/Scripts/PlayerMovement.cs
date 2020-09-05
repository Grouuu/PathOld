using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float thrustPower = 1;
	public float thrustMin = 1;
	public float thrustMax = 5;
	public float rotateSpeed = 1;
	public float gravityFaceAttract = 0.001f;
	public float gravityMax = 1;

	public bool isDebug = true;
	public bool isGravity = true;

	private Rigidbody rb;
	private Transform player;
	private Gravity gravityManager;

	private Vector3 velocity = Vector3.forward;
	private float thrust = 0;
	private float rotation = 0;

	private bool isThrusting = false;
	private bool isRotating = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		player = transform;
		gravityManager = new Gravity();
		gravityManager.Start();
	}

	private void Update()
	{
		float thrustIntensity = Input.GetAxisRaw("Vertical");
		float rotateIntensity = Input.GetAxisRaw("Horizontal");
		isThrusting = thrustIntensity != 0;
		isRotating = rotateIntensity != 0;
		ChangeThrust(thrustIntensity);
		ChangeRotation(rotateIntensity);
	}

	private void FixedUpdate()
	{
		float dt = Time.deltaTime;

		Vector3 gravity = gravityManager.GetGravity(rb.position, gravityMax);

		if (!isGravity)
		{
			gravity = Vector3.zero;
		}

		if (!isThrusting && !isRotating)
		{
			FaceGravity(gravityFaceAttract, gravity);
		}

		rb.rotation = Quaternion.AngleAxis(rotation, Vector3.up);

		velocity = player.forward * thrust;

		if (isDebug)
		{
			Debug.DrawLine(rb.position, rb.position + player.forward * 5, Color.red); // forward
			Debug.DrawLine(rb.position, rb.position + (velocity + gravity).normalized * 5, Color.blue); // path
		}

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
