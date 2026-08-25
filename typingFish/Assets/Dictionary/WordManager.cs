using System.Collections.Generic;
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
