using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    string[] wordGuids;

    [SerializeField] List<Word> allWords;

    [SerializeField] List<Word> learningWords;

    [SerializeField] List<Word> learnedWords;

    Word curWord;

    public Word GetCurrentWord(){  return curWord; }
    public void SetCurrentWord(Word newWord){  curWord = newWord; }


	#region GET WORD
	public Word GetWord(string wordValue)
    {
        return GetWord(wordValue, allWords);
    }

	public Word GetWord(string wordValue, bool wordLearned)
	{
		if (wordLearned)
			return GetWord(wordValue, learnedWords);
		else
			return GetWord(wordValue, learningWords);
	}

	public Word GetWord(string wordValue, List<Word> wordList)
    {
        foreach(Word word in wordList)
        {
            if(word.value == wordValue) 
                return word;
        }

        return null;
    }

	#endregion

	#region GET WORDS
	public List<Word> GetWords(bool wordsLearned)
	{
		if (wordsLearned)
			return learnedWords;
		else
			return learningWords;
	}

    public List<Word> GetWords(bool wordsLearned, WordDifficulty wordDifficulty)
    {
        if (wordsLearned)
            return GetWords(wordDifficulty, learnedWords);
        else
            return GetWords(wordDifficulty, learningWords);
    }

	public List<Word> GetWords(WordDifficulty wordDifficulty)
    {
        return GetWords(wordDifficulty, allWords);
    }

    public List<Word> GetWords(WordDifficulty wordDifficulty, List<Word> wordList)
    {
        List<Word> wordsOfDifficulty = new List<Word>();
        foreach (Word word in wordList)
        {
            if (word.difficulty == wordDifficulty)
                wordsOfDifficulty.Add(word);
        }

        return wordsOfDifficulty;
    }
	#endregion

	#region GET RANDOM WORD

    public Word GetRandomWord()
    {
        return GetRandomWord(allWords);
    }

    public Word GetRandomWord(bool hasLearned)
    {
        if (hasLearned)
            return GetRandomWord(learnedWords);
        else
            return GetRandomWord(learningWords);
    }

    public Word GetRandomWord(WordDifficulty wordDifficulty)
    {
        bool hasDifficulty = false;

        foreach(Word word in allWords)
        {
            if (word.difficulty == wordDifficulty)
                hasDifficulty = true;
        }

        if (!hasDifficulty)
            return null;

        Word randomWord = new Word();
        bool isDifficulty = false;

        while(!isDifficulty)
        {
            randomWord = GetRandomWord(allWords);

            if(randomWord.difficulty == wordDifficulty)
                isDifficulty = true;
        }

        return randomWord;
    }

    public Word GetRandomWord(bool hasLearned, WordDifficulty wordDifficulty)
    {
        if (hasLearned)
            return GetRandomWord(wordDifficulty, learnedWords);
        else
            return GetRandomWord(learningWords);
    }

    public Word GetRandomWord(WordDifficulty wordDifficulty, List<Word> wordList)
    {
		bool hasDifficulty = false;

		foreach (Word word in wordList)
		{
			if (word.difficulty == wordDifficulty)
				hasDifficulty = true;
		}

		if (!hasDifficulty)
			return null;

		Word randomWord = new Word();
		bool isDifficulty = false;

		while (!isDifficulty)
		{
			randomWord = GetRandomWord(wordList);

			if (randomWord.difficulty == wordDifficulty)
                isDifficulty = true;
		}

		return randomWord;
	}

    public Word GetRandomWord(List<Word> wordList)
    {
        int index = Random.Range(0, wordList.Count);
        return wordList[index];
    }
    #endregion

	#region GET RANDOM WORDS
    public List<Word> GetRandomWords(int amount)
    {
        List<Word> randomWords = new List<Word>();

        for(int i = 0; i < amount; i++)
        {
            randomWords.Add(GetRandomWord());
        }

        return randomWords;
    }

    public List<Word> GetRandomWords(int amount, bool hasLearned)
    {
        if (hasLearned)
            return GetRandomWords(amount, learnedWords);
        else
            return GetRandomWords(amount, learningWords);
    }

    public List<Word> GetRandomWords(int amount, WordDifficulty worldDifficulty)
    {
        List<Word> randomWords = new List<Word>();

        for(int i = 0; i < amount; i++)
        {
            randomWords.Add(GetRandomWord(worldDifficulty));
        }

        return randomWords;
    }

    public List<Word> GetRandomWords(int amount, WordDifficulty wordDifficulty, List<Word> wordList)
    {
        List<Word> randomWords = new List<Word>();

        for(int i = 0; i < amount;i++)
        {
            randomWords.Add(GetRandomWord(wordDifficulty, wordList));
        }

        return randomWords;
    }

    public List<Word> GetRandomWords(int amount, bool hasLearned, WordDifficulty wordDifficulty)
    {
        if (hasLearned)
            return GetRandomWords(amount, wordDifficulty, learnedWords);
        else
            return GetRandomWords(amount, wordDifficulty, learningWords);
    }


    public List<Word> GetRandomWords(int amount, List<Word> wordList)
    {
		List<Word> randomWords = new List<Word>();

		for (int i = 0; i < amount; i++)
		{
			randomWords.Add(GetRandomWord());
		}

        return randomWords;
	}

    #endregion

	void ClearWords()
    {
        allWords.Clear();
        learnedWords.Clear();
        learningWords.Clear();
    }


    void InitWords()
    {
        ClearWords();

        wordGuids = AssetDatabase.FindAssets("t:Word");

		foreach (string guid in wordGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Word newWord = AssetDatabase.LoadAssetAtPath(path, typeof(Word)) as Word;

            allWords.Add(newWord);

            if (newWord.hasLearned)
                learnedWords.Add(newWord);
            else
                learningWords.Add(newWord);
        }
    }


    void Start()
    {
        if (instance == null)
            instance = this;

        InitWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
