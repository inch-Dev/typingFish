using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypeUI : UI
{
    public static TypeUI instance;
    [SerializeField] TextMeshProUGUI wordTF;
    [SerializeField] TextMeshProUGUI typeTF;
    [SerializeField] TMP_Text typeText;
    [SerializeField] Slider timeSlider;

	private void Start()
	{
        if (instance == null)
            instance = this;
	}

	public void Clear()
    {
        wordTF.text = string.Empty;
        typeTF.text = string.Empty;
        typeText.text = string.Empty;
    }
    
    public void DisplayTimer()
    {

        Debug.Log("DisplayingTimer...");
        timeSlider.maxValue = TypeManager.instance.GetTypeTimer();
        timeSlider.value = TypeManager.instance.GetTimeToType();

        Debug.Log("Timer Values...Max:{timeSlider.maxValue}, Value:{timeSlider.value}");
    }

    public void DisplayWord()
    {
        if (!isActive)
            return;
        wordTF.text = TypeManager.instance.GetLearningWordValue();
    }

	public void DisplayKeystrokes()
    {
        if (!isActive)
            return;

        string typedInput = TypeManager.instance.GetTypeInput();
		char[] typedInputArray = typedInput.ToCharArray();

        if (typedInput.Length > TypeManager.instance.GetLearningWordValue().Length)
            return;

        //Debug.Log($"Typed input:{typedInput}");

		for(int i = 0; i < typedInputArray.Length; i++)
        {
            ValidateKeystroke(i);
        }
	}

    bool ValidateKeystroke(int index)
    {
        if (!isActive)
            return false;

        //Debug.Log($"Validating keystroke at {index}");
        

        string learningWord = TypeManager.instance.GetLearningWordValue();
        char[] learningWordArray = learningWord.ToCharArray();

        string typedInput = TypeManager.instance.GetTypeInput();
        char[] typedInputArray = typedInput.ToCharArray();

		if (index >= learningWord.Length)
			return false;

		typeTF.text = typedInput;


        if(learningWordArray[index] == typedInput[index])
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


        int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;
        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

        for(int i = 0; i < 4; i++)
        {
			vertexColors[vertexIndex + i] = color;
		}
	

		//textInfo.characterInfo[index].color = color;

        typeText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

	}
}
