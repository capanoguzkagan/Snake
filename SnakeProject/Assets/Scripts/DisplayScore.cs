using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    Text _text;	

    private void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = "0";
    }

    void OnEnable() 
    {
		_text.text = "0";
		GameManager.Instance.OnScoreChanged += HandleOnCurrentScoreChanged;
		GameManager.Instance.GameFailed += Instance_GameFailed;
    }


	private void OnDisable()
    {
		GameManager.Instance.OnScoreChanged -= HandleOnCurrentScoreChanged;
		GameManager.Instance.GameFailed -= Instance_GameFailed;
	}

    private void HandleOnCurrentScoreChanged(int score)
    {
        _text.text = score.ToString();
    }
	private void Instance_GameFailed()
	{
		this.gameObject.SetActive(false);
	}

}
