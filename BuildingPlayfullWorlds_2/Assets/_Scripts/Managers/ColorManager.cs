using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorS
{
    Red = 0,
    Blue = 1,
    Black = 2,
    White = 3
}

public class ColorManager : MonoBehaviour
{
    public List<Material> PlayerMaterials;

    public ColorS playerColors = ColorS.White;

    public static ColorManager Instance;

    public GameObject Player;

    private Renderer playerRenderer;

    private void Awake()
    {
        Instance = this;
        if (Player == null) Player = GameObject.FindGameObjectWithTag("Player");
        playerRenderer = Player.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetColor((int)ColorS.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetColor((int)ColorS.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetColor((int)ColorS.Black);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetColor((int)ColorS.White);
        }
    }

    public void SetColor(int _color)
    {
        playerColors = (ColorS)_color;
        playerRenderer.material = PlayerMaterials[_color];
    }
}
