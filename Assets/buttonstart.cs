using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonstart : MonoBehaviour
{
    public Image Start;
    public Sprite goodStart, badStart;
    public bool endscreen;

    private void OnMouseEnter()
    {
        FindObjectOfType<LevelMusic>().playAlertMusic();
        if (Start != null)
        {

        Start.sprite = badStart;
        }
    }

    private void OnMouseExit()
    {
        FindObjectOfType<LevelMusic>().playRegularMusic();
        if (Start != null)
        {
            Start.sprite = goodStart;
        }
    }

    public void StartGame()
    {
        if (!endscreen)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
