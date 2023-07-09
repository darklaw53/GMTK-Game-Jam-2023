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
        if (Start != null)
        {

        Start.sprite = badStart;
<<<<<<< Updated upstream
        FindObjectOfType<LevelMusic>().playAlertMusic();
=======
        }
>>>>>>> Stashed changes
    }

    private void OnMouseExit()
    {
<<<<<<< Updated upstream
        Start.sprite = goodStart;
        FindObjectOfType<LevelMusic>().playRegularMusic();
=======
        if (Start != null)
        {
            Start.sprite = goodStart;
        }
>>>>>>> Stashed changes
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
