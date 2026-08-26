using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TypeManager : MonoBehaviour, IStateable
{
	public bool isActive = false;
	public void HandleState()
	{
		if (GameManager.instance.GetState() == GameState.TYPING)
		{
			isActive = true;
			ChooseLearningWord();
		}
		else
			isActive = false;
	}
	public static TypeManager instance;
	[SerializeField] float typeTimer;

	bool canType = false;
	float timeToType = 0f;

	Word learningWord;
	string learningWordValue;

	public string GetLearningWordValue(){ return learningWordValue; }

	public Word GetLearningWord(){ return learningWord; }

	public void SetLearningWord(Word newWord)
	{
		learningWord = newWord;
		learningWordValue = newWord.value;

		TypeUI.instance.DisplayWord();

		learningWord.timesEncountered++;
	}

	string typeInput;

	public string GetTypeInput(){ return typeInput; }
	public void SetTypeInput(string newWord)
	{ 
		typeInput = newWord;
		TypeUI.instance.DisplayKeystrokes();

		if (typeInput == learningWordValue)
			TypedWord();
	}
	public void AddTypeInput(char newLetter)
	{  
		typeInput += newLetter;
		TypeUI.instance.DisplayKeystrokes();

		if (typeInput == learningWordValue || typeInput.Length == learningWordValue.Length)
			TypedWord();
	}

	public void Clear()
	{
		learningWord = null;
		learningWordValue = null;
		typeInput = null;
	}


	private void Start()
	{
		if (instance == null)
			instance = this;
	}

	private void Update()
	{
		if(canType)
		{
			timeToType += Time.deltaTime;
		}
	}

	void ChooseLearningWord()
	{
		SetLearningWord(WordManager.instance.GetRandomWord(false));
		Debug.Log($"Choosing word;{GetLearningWordValue()}");
	}

	void TypedWord()
	{
		TypeUI.instance.Clear();

		canType = false;

		if (typeInput == learningWordValue)
		{
			learningWord.speed += timeToType;
			WordManager.instance.TypedWord(learningWord);
		}

		learningWord.UpdateStats();

		Clear();

		GameManager.instance.SetState(GameState.FISHING);
	}

	IEnumerator TypeTimer()
	{
		yield return new WaitForSeconds(typeTimer);

		TypedWord();

	}
}
