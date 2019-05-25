using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buttons
{
    Right,
    Left,
    Up,
    Down,
    A,
    B,
    C,
    D,
    E,
    F   
}

public enum Condition
{
    GreaterThan,
    LessThan
}

[System.Serializable]
public class InputAxisState
{
    public string AxisName;
    public float OffValue;
    public Buttons Button;
    public Condition Condition;

    public bool Value
    {
        get
        {
            var val = Input.GetAxis(AxisName);
            switch (Condition)
            {
                case Condition.GreaterThan:
                    return val > OffValue;
                case Condition.LessThan:
                    return val < OffValue;
          
            }
            return false;
        }
    }
}

public class InputManager : MonoBehaviour
{

    public InputAxisState[] Inputs;
    public InputState InputState;

	// Update is called once per frame
	void Update ()
    {
        foreach (var input in Inputs)
        {
            InputState.SetButtonValue(input.Button, input.Value);
        }
	}
}
