using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		SnakeController _player = other.GetComponent<SnakeController>();
		if (_player != null)
		{
			GameManager.Instance.PlayerFailed();
			Destroy(_player.transform.parent.gameObject);
		}
	}
}
