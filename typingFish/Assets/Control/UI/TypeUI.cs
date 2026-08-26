using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TypeUI : UI
{
    TextMeshProUGUI wordTF;
    TextMeshProUGUI typeTF;
    TMP_Text typeText;
	private void OnEnable()
	{
        Keyboard.OnTypeKeyPress += DisplayKeystroke;
	}

	private void OnDisable()
	{
		Keyboard.OnTypeKeyPress -= DisplayKeystroke;
	}

    void DisplayWord()
    {
        if (!isActive)
            return;

        wordTF.text = WordManager.instance.GetCurrentWord().value;
    }

	void DisplayKeystroke()
    {
        if (!isActive)
            return;

		string typedWord = Keyboard.instance.GetTypedWord();
		char[] typedWordArray = typedWord.ToCharArray();

		for(int i = 0; i < typedWordArray.Length; i++)
        {
            ValidateKeystroke(i);
        }
	}

    bool ValidateKeystroke(int index)
    {
        if (!isActive)
            return false;

        string curWord = WordManager.instance.GetCurrentWord().value;
        char[] curWordArray = curWord.ToCharArray();

        string typedWord = Keyboard.instance.GetTypedWord();
        char[] typedWordArray = typedWord.ToCharArray();

        typeTF.text = typedWord;

        if(curWordArray[index] == typedWord[index])
        {
            ChangeKeystrokeColor(index, Color.green);
            return true;
        }

        else
        {
            ChangeKeystrokeColor(index, Color.red);
			return false;
		}
	}

    void ChangeKeystrokeColor(int index, Color color)
    {
        if (!isActive)
            return;

		typeText.ForceMeshUpdate();
		TMP_TextInfo textInfo = typeText.textInfo;

        if (index > textInfo.characterCount || !textInfo.characterInfo[index].isVisible)
            return;

        textInfo.characterInfo[index].color = color;   


	}
}
