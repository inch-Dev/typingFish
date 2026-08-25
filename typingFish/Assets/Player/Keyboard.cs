using System.Linq;
using UnityEngine;

public class Keyboard : MonoBehaviour, IStateable
{
    public static Keyboard instance;
    public void HandleState() { }

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
        Keystroke();
    }

    void Keystroke()
    {
        SetKeys();

        for(int i = 0; i < keyValues.Length; i++)
        {
            if(keys[i])
            {

                //Cast to char

                KeyCode activeKey = (KeyCode)keyValues[i];

                if(activeKey.ToString().Length == 1 && activeKey.ToString().All(char.IsLetter))
                {
					Debug.Log($"Returned {(KeyCode)keyValues[i]}");
				}
                //Send to ui for word
            }
        }
    }
}
