using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonstart : MonoBehaviour
{
    public Image Start;
    public Sprite goodStart, badStart;

    private void OnMouseEnter()
    {
        Start.sprite = badStart;
    }

    private void OnMouseExit()
    {
        Start.sprite = goodStart;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
