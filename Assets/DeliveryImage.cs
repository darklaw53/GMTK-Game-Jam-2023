using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeliveryImage : MonoBehaviour
{
    public Slider slider;
    public AudioClip victoryFanfare;

    public void Progress()
    {
        FindObjectOfType<LevelMusic>().GetComponent<AudioSource>().PlayOneShot(victoryFanfare);
        slider.value += .25f;
        PlayerController.Instance.sendingItem = false;
        if (slider.value == 1)
        {
            SceneManager.LoadScene(2);
        }
        gameObject.SetActive(false);
    }
}
