using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool Extreme;
	int _score = 0;
	[SerializeField] GameObject _player;
	[SerializeField] GameObject _Retry;
	MenuController menuController;
	HighscoreTable HighscoreTable;

	public static GameManager Instance { get; private set; }

	public event System.Action<int> OnScoreChanged;
	public event System.Action GameFailed;
	void Awake()
	{
		SingletonThisObject();
		menuController = FindObjectOfType<MenuController>();
		HighscoreTable = FindObjectOfType<HighscoreTable>();
	}

	private void SingletonThisObject()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	public void IncreaseScore() //Normal Skor Artt�rma
	{
		_score++;
		OnScoreChanged?.Invoke(_score);
	}
	public void BonusFood() //Bonus Skor Artt�rma
	{
		_score = _score + SnakeController.Difficulty;
		OnScoreChanged?.Invoke(_score);
	}	
	public void MinusFood() //BodyPart eksilterek Skor Artt�rma
	{
		_score = _score + ( SnakeController.Difficulty)*2;
		OnScoreChanged?.Invoke(_score);
	}
	public void SpawnPlayer() // Oyun ba�lang�c� Player olu�turma
	{
		_score = 0;
		Instantiate(_player);
	}
	public void PlayerFailed() // Oyun biti�i UI ayarlar�
	{
		menuController.ScoreText(_score);
		HighscoreTable.AddHighscoreEntry(_score);
		HighscoreTable.gameObject.SetActive(true);
		_Retry.SetActive(true);
		GameFailed?.Invoke();
	}
	public void ExtremeGameMod()
	{
		if (Extreme)
		{
			Extreme = false;
		}
		else
		{
			Extreme = true;
		}

	}
}
