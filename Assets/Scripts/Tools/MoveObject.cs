using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

	public bool Rotate;
	// 0- up , 1- down, 2- left , 3 - right , 4-forward, 5 - back
	public short MoveDirection;
	private Vector3 moveVector;
	public float RotationSpeed;
	public bool Move;
	public float time;
	public float speed;
	float t;
	public bool CycleMove = true;

	private void Awake()
	{
		switch (MoveDirection)
		{
			case 0: moveVector = Vector3.up; break;

			case 1: moveVector = Vector3.down; break;

			case 2: moveVector = Vector3.left; break;

			case 3: moveVector = Vector3.right; break;

			case 4: moveVector = Vector3.forward; break;

			case 5: moveVector = Vector3.back; break;

			case 6: moveVector = transform.forward; break;



			default:
				break;
		}
	}
	// Update is called once per frame
	void Update()
	{

		if (Move)
		{

			t += Time.deltaTime;

			if (CycleMove)
			{

				if (t < time)
				{
					transform.position += moveVector * speed * Time.deltaTime;
				}
				if (t > time)
				{
					transform.position += -moveVector * speed * Time.deltaTime;
				}
				if (t > 2 * time) { t = 0; }
			}
			else
			{
				if (t < time)
				{
					transform.localPosition += moveVector * speed * Time.deltaTime;
				}
			}
		}
		if (Rotate)
		{
			transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
		}
	}
}