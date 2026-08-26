using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TypeManager : MonoBehaviour, IStateable
{
	public bool isActive = false;

	public void HandleState()
	{
		if (GameManager.instance.GetState() == GameState.TYPING)
			isActive = true;
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

	public void SetLearningWord(Word newWord)
	{
		learningWord = newWord;
		learningWordValue = newWord.value;

		learningWord.timesEncountered++;
	}

	string typeInputWord;

	public string GetTypeInputWord(){ return typeInputWord; }
	public void SetTypeInputWord(string newWord){ typeInputWord = newWord; }

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
	}

	IEnumerator TypeTimer()
	{
		yield return new WaitForSeconds(typeTimer);

		canType = false;

		learningWord.speed += timeToType;

		//Has typed word successfully
		if(typeInputWord == learningWordValue)
		{
			WordManager.instance.TypedWord(learningWord);
		}

	}
}
