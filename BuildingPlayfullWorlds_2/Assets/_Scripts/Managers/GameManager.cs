using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float PlayerLives = 3f;

    public GameObject Player;

    public Image[] Lives;

    public List<AttackStats> Attacks;

    public static Dictionary<string, GameObject> Children = new Dictionary<string, GameObject>();
    public int GameWonBuildIndex;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int index = 0; index <= Lives.Length-1 ; index++)
        {
            if (index < PlayerLives)
            {
                Lives[index].gameObject.SetActive(true);
            }
            else
                Lives[index].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Children.Count >= 4)
            SceneManager.LoadSceneAsync(GameWonBuildIndex);
    }

    public void SetLives(int amount, bool remove)
    {
        if (PlayerLives <= amount && remove)
        {
            KillPlayer();
            return;
        }

        if (remove)
            PlayerLives -= amount;
        else
            PlayerLives += amount;

        for (int index = 0; index <= Lives.Length -1; index++)
        {
            if (index < PlayerLives)
            {
                Lives[index].gameObject.SetActive(true);
            }
            else
                Lives[index].gameObject.SetActive(false);
        }
    }

    public void KillPlayer()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
