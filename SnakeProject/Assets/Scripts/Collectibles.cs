using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
	[SerializeField] bool isBonus;
	[SerializeField] bool Minus;

	private void OnTriggerEnter(Collider other)
	{
		SnakeController _player = other.GetComponent<SnakeController>();
		if (_player!=null)
		{
			if (!isBonus&&!Minus)
			{
				if (GameManager.Instance.Extreme)
				{
					_player._SteerSpeed = _player._SteerSpeed * -1;
				}
				_player.GrowSnake();
				_player.Spawner();
				GameManager.Instance.IncreaseScore();
				Destroy(this.gameObject);
			}
			else if (Minus&&!isBonus)
			{
				if (GameManager.Instance.Extreme)
				{
					_player._SteerSpeed = _player._SteerSpeed * -1;
				}
				_player.RemoveObject();
				GameManager.Instance.MinusFood();
				Destroy(this.gameObject);
			}
			else
			{
				if (GameManager.Instance.Extreme)
				{
					_player._SteerSpeed = _player._SteerSpeed * -1;
				}
				_player.GrowSnake();
				_player.Spawner();
				GameManager.Instance.BonusFood();
				Destroy(this.gameObject);
			}
		}
	}
}
