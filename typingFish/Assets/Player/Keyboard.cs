using System.Linq;
using UnityEngine;

public class Keyboard : MonoBehaviour, IStateable
{
    public static Keyboard instance;

    public bool isActive = false;

	public void HandleState()
	{
		if (GameManager.instance.GetState() == GameState.TYPING)
			isActive = true;
		else
			isActive = false;
	}

	string typedWord;
    public string GetTypedWord(){ return typedWord; }

	char curLetter;
    public char GetCurrentLetter(){  return curLetter; }
    public void SetCurrentLetter(char newLetter){  curLetter = newLetter; }


    int[] keyValues;

    bool[] keys;

    void SetKeys()
    {
        for (int i = 0; i < keyValues.Length; i++)
        {
            keys[i] = Input.GetKeyDown((KeyCode)keyValues[i]);
            
        }
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        if (instance == null)
            instance = this;

		keyValues = (int[])System.Enum.GetValues(typeof(KeyCode));

		keys = new bool[keyValues.Length];
	}

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            Keystroke();
    }


    public void Clear()
    {
        typedWord = null;
    }

    public string Keystroke()
    {
        SetKeys();

        for(int i = 0; i < keyValues.Length; i++)
        {
            if(keys[i])
            {
                KeyCode activeKey = (KeyCode)keyValues[i];

                if(activeKey.ToString().Length == 1 && activeKey.ToString().All(char.IsLetter))
                {
                    curLetter = (char)activeKey;
					Debug.Log($"Returned {(KeyCode)keyValues[i]}");
                    //Debug.Log($"Type Manager:{TypeManager.instance.name}");
                    TypeManager.instance.AddTypeInput(curLetter);
					return activeKey.ToString();
				}
            }
        }

        return null;
    }

}
