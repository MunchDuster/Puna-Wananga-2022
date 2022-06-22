using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimbal : MonoBehaviour
{
	public Vector2 targetAngles = Vector2.zero;
	public float speed = 20;

	private Vector2 currentAngles = Vector2.zero;
	private Vector2 offsetAngles;

	[Space(10)]
	public Transform xRotator;
	public Transform zRotator;

	private Quaternion offsetX;
	private Quaternion offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        offsetAngles = new Vector2(xRotator.localRotation.eulerAngles.y, zRotator.localRotation.eulerAngles.y);
		offsetX = xRotator.localRotation;
		offsetZ = zRotator.localRotation;
	}

    // Update is called once per frame
    void Update()
    {
		Vector2 offset = (targetAngles + offsetAngles) - currentAngles;
		Vector2 maxDelta = speed * Time.deltaTime * offset.normalized;
		Vector2 delta = (maxDelta.magnitude > offset.magnitude) ? offset : maxDelta;
		currentAngles += delta;

        xRotator.localRotation = offsetX * Quaternion.Euler(0, currentAngles.x, 0);
		zRotator.localRotation = offsetZ * Quaternion.Euler(0, currentAngles.y, 0);
	}
}
