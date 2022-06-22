using UnityEngine;

public class TowerShade : MonoBehaviour
{
	public Transform sun;
	public Transform shade;

	public Servo servoX;
	public Servo servoZ;

	private float altitudeAngle;
	private float azimuthAngle;

	// Update is called once per frame
	void Update()
	{
		UpdateAngleOfSun();
		UpdateMotors();
	}

	private void UpdateAngleOfSun()
	{
		altitudeAngle = sun.rotation.eulerAngles.x;
		azimuthAngle = sun.rotation.eulerAngles.z;
	}

	private void UpdateMotors()
	{
		servoX.SetTargetAngle(altitudeAngle);
		servoZ.SetTargetAngle(azimuthAngle);
	}
}
