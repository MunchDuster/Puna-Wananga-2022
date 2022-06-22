using UnityEngine;

public class Servo : MonoBehaviour
{
	public float speed;
	public AnimationCurve acceleration;
	public AnimationCurve deceleration;
	public Vector3 axis;
	public float maxError;

	private float targetAngle;
	private float currentAngle;
	private float angularVelocity;

	private float timeMoving = 0;

	private enum State { Accelerating, Idle, Decelerating };
	private State curState = State.Idle;

	public void SetTargetAngle(float targetAngle)
	{
		this.targetAngle = targetAngle;
	}

	private void Update()
	{
		float errAngle = Mathf.Abs(targetAngle - currentAngle);

		if (errAngle > maxError)
		{
			Debug.Log("Accelerating");

			if (curState != State.Accelerating)
			{
				timeMoving = 0;
				curState = State.Accelerating;
			}

			timeMoving += Time.deltaTime;
			angularVelocity = acceleration.Evaluate(timeMoving) * Mathf.Sign(targetAngle - currentAngle);
		}
		else
		{
			Debug.Log("Decelerating");

			if (curState == State.Accelerating)
			{
				timeMoving = 0;
				curState = State.Decelerating;
			}
			else if (curState == State.Decelerating)
			{
				timeMoving += Time.deltaTime;
				angularVelocity = deceleration.Evaluate(timeMoving);

				if (Mathf.Abs(angularVelocity) < 0.01f)
				{
					curState = State.Idle;
				}
			}
		}

		float deltaAngle = angularVelocity * speed * Time.deltaTime;

		currentAngle += deltaAngle;
		currentAngle %= 360;

		transform.Rotate(axis, deltaAngle);
	}
}