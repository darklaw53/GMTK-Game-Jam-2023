using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public bool stealthed;
    public SpriteRenderer sprite;
    public Color stealthShade;
    Color baseColor;

    private void Start()
    {
        baseColor = sprite.color; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cover")
        {
            EnterStealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cover")
        {
            ExitStealth();
        }
    }

    public void EnterStealth()
    {
        stealthed = true;
        sprite.color = stealthShade;
    }

    public void ExitStealth()
    {
        stealthed = false;
        sprite.color = baseColor;
    }
}
