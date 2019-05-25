using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonState
{
    public bool Value;
    public float HoldTime = 0;
}

public enum Directions
{
    Right = 1,
    Left = -1
}

public class InputState : MonoBehaviour
{
    public Directions Direction = Directions.Right;
    public float AbsVelX = 0f;
    public float AbsVelY = 0f;

    private Rigidbody2D body2d;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

    private void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        AbsVelX = Mathf.Abs(body2d.velocity.x);
        AbsVelY = Mathf.Abs(body2d.velocity.y);
    }

    public void SetButtonValue(Buttons key, bool value)
    {
        if (!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        var state = buttonStates[key];

        if(state.Value && !value)
        {
            state.HoldTime = 0;
        }else if (state.Value && value)
        {
            state.HoldTime += Time.deltaTime;
        }

        state.Value = value;
    }

    public bool GetButtonValue(Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].Value;
        else
            return false;
    }

    public float GetButtonHoldTime(Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].HoldTime;
        else
            return 0;
    }
}
