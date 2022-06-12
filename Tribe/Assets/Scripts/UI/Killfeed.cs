using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Killfeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
	private void Start()
	{
		text.text = "";
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K)) AddText();
	}
	public void AddText()
	{
		text.text += "yes\n";
		Invoke("RemoveLine", 5f);
	}
	private void RemoveLine()
	{
		int i = 0;
		for (i = 0; i < text.text.Length; i++)
		{
			if (text.text[i] == '\n') break;			
		}
		char[] firstWord = new char[i];
		for (int j = 0; j < i; j++)
		{
			firstWord[j] = text.text[j];
		}

		string newKillFeed = text.text.TrimStart(firstWord);
		text.text = newKillFeed.Trim();
	}
}
