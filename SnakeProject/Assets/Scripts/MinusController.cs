using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusController : MonoBehaviour
{
	[SerializeField] float timeRemaining = 10;

	void Update()
	{
		if (timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
