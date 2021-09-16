using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] Text _scoreText;
	public void PlayButton()
	{
		GameManager.Instance.SpawnPlayer();
	}
	public void ScoreText(int _score)
	{
		_scoreText.text = _score.ToString();
	}
}
