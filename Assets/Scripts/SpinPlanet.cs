using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPlanet : MonoBehaviour
{
	[SerializeField]
	private bool spin;
	[SerializeField]
	private bool clockwise = true;
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float drift = 0.1f;

	void Update()
	{
		if (!spin) return;

		if (clockwise)
		{
			transform.Rotate(Vector3.up, speed * Time.deltaTime);
			transform.Rotate(Vector3.forward, drift * Time.deltaTime);
		}
		else
		{
			transform.Rotate(-Vector3.up, speed * Time.deltaTime);
			transform.Rotate(-Vector3.forward, drift * Time.deltaTime);
		}
	}
}
