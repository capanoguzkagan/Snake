
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour {

	private Transform entryContainer;
	private Transform entryTemplate;
	private List<Transform> highscoreEntryTransformList;
	int index;

	private void Awake() {
		entryContainer = transform.Find("highscoreEntryContainer");
		entryTemplate = entryContainer.Find("highscoreEntryTemplate");
		entryTemplate.gameObject.SetActive(false);
	}
	private void OnEnable() // Her enable olduğunda tekrardan verileri düzenleyip ekrana yazıyor
	{
		index = 0;
		ScoreController();
	}
	private void OnDisable() // Disable olduğunda ürettiği nesneleri sildiriyorum
	{
		for (int i = 1; i < entryContainer.childCount; i++)
		{
			Destroy(entryContainer.gameObject.transform.GetChild(i).gameObject);
		}
	}
	#region Nesneleri üretip düzenlediğim alan
	private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {

		float templateHeight = 31f;
		Transform entryTransform = Instantiate(entryTemplate, container);
		RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
		entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
		if (index < 5)
		{
			entryTransform.gameObject.SetActive(true);
			index++;
		}
		else
		{
			entryTransform.gameObject.SetActive(false);
		}

		int rank = transformList.Count + 1;
		string rankString;
		switch (rank) {
			default:
				rankString = rank + "TH"; break;

			case 1: rankString = "1ST"; break;
			case 2: rankString = "2ND"; break;
			case 3: rankString = "3RD"; break;
		}

		entryTransform.Find("posText").GetComponent<Text>().text = rankString;

		int score = highscoreEntry.score;

		entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

		if (rank == 1) {
			entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
			entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;

		}

		transformList.Add(entryTransform);
	}
	#endregion

	public void AddHighscoreEntry(int score) {
		HighscoreEntry highscoreEntry = new HighscoreEntry { score = score };

		string jsonString = PlayerPrefs.GetString("highscoreTable");
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

		if (highscores == null) {
			// There's no stored table, initialize
			highscores = new Highscores() {
				highscoreEntryList = new List<HighscoreEntry>()
			};
		}

		highscores.highscoreEntryList.Add(highscoreEntry);

		string json = JsonUtility.ToJson(highscores);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
	}
	void ScoreController() // Skorların sıralandığı kısım
	{
		string jsonString = PlayerPrefs.GetString("highscoreTable");
		Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
		for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
			for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
				if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
					HighscoreEntry tmp = highscores.highscoreEntryList[i];
					highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
					highscores.highscoreEntryList[j] = tmp;
				}
			}
		}

		highscoreEntryTransformList = new List<Transform>();
		foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
			CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
		}
	}

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
    }

}
