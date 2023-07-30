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
        var x = FindObjectOfType<LevelMusic>();
        if (x != null) x.playAlertMusic();
        if (Start != null)
        {
            Start.sprite = badStart;
            if (x != null) x.playAlertMusic();
        }
    }

    private void OnMouseExit()
    {
        if (goodStart != null) Start.sprite = goodStart;
        var x = FindObjectOfType<LevelMusic>();
        if (x != null) x.playRegularMusic();
        if (Start != null)
        {
            if (goodStart != null) Start.sprite = goodStart;
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
