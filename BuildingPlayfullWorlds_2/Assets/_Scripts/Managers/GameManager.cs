using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float PlayerLives = 3f;

    public GameObject Player;

    public GameObject DeathEffect;

    public float TimeToDie = 3f;

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

    public static void CheckChildren()
    {
        if (Children.Count >= 4)
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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
        GameObject deathEffect = Instantiate(DeathEffect, Player.transform.position, DeathEffect.transform.rotation);
        StartCoroutine(Death(TimeToDie));
    }

    private IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }
}
