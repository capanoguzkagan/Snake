using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Difficulty : MonoBehaviour
{
	List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	Dropdown dropdown;
	private void Awake()
	{
		dropdown = GetComponent<Dropdown>();
	}
	public void Dropdown() // Dropdown ile statik deðerleri deðiþtirerek level zorluðunu belirliyorum
	{
		if (dropdown.value == 0)
		{
			SnakeController._MoveSpeed =1;
			SnakeController._Gap =100;
			SnakeController.Difficulty=1;
		}
		else if (dropdown.value == 1)
		{
			SnakeController._MoveSpeed =1.5f;		
			SnakeController._Gap = 75;
			SnakeController.Difficulty = 2;
		}
		else if (dropdown.value == 2)
		{
			SnakeController._MoveSpeed = 2;
			SnakeController._Gap = 60;
			SnakeController.Difficulty = 3;
		}
		else if (dropdown.value == 3)
		{
			SnakeController._MoveSpeed = 2.5f;
			SnakeController._Gap = 60;
			SnakeController.Difficulty = 4;
		}
		else if (dropdown.value == 4)
		{
			SnakeController._MoveSpeed = 3;
			SnakeController._Gap = 60;
			SnakeController.Difficulty = 5;
		}
		else if (dropdown.value == 5)
		{
			SnakeController._MoveSpeed = 3.5f;
			SnakeController._Gap = 60;
			SnakeController.Difficulty = 6;
		}
		else if (dropdown.value == 6)
		{
			SnakeController._MoveSpeed = 4;
			SnakeController._Gap = 50;
			SnakeController.Difficulty = 7;
		}
		else if (dropdown.value == 7)
		{
			SnakeController._MoveSpeed = 4.5f;
			SnakeController._Gap = 40;
			SnakeController.Difficulty = 8;
		}
		else if (dropdown.value == 8)
		{
			SnakeController._MoveSpeed = 5;
			SnakeController._Gap = 30;
			SnakeController.Difficulty = 9;
		}
	}

	private void Start()
	{
		PopulateList();
	}

	void PopulateList()
	{
		dropdown.ClearOptions();
		List<string> newOptions = new List<string>();
		for (int i = 0; i < numbers.Count; i++)
		{
			newOptions.Add(numbers[i].ToString());
		}
		dropdown.AddOptions(newOptions);
	}
}

