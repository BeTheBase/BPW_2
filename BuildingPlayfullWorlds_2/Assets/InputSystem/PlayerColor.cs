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

public class PlayerColor : MonoBehaviour
{
    public List<Material> PlayerMaterials;

    private Renderer playerRenderer;

    public static ColorS playerColors = ColorS.White;

    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerColors = ColorS.Red;
            playerRenderer.material = PlayerMaterials[(int)playerColors];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerColors = ColorS.Blue;
            playerRenderer.material = PlayerMaterials[(int)playerColors];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerColors = ColorS.Black;
            playerRenderer.material = PlayerMaterials[(int)playerColors];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerColors = ColorS.White;
            playerRenderer.material = PlayerMaterials[(int)playerColors];
        }
    }
}
