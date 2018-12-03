using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By Taru Konttinen 2018

public class Card : MonoBehaviour
{
    Sprite cardBack;
    Sprite myPicture;
    public MemoryGameManager manager;
    int id;

	void Start()
    {
        cardBack = GetComponent<SpriteRenderer>().sprite;
	}
	
    // MemoryGameManager calls this every time a shuffle happens
    public void Configure(Sprite newPicture, int newId)
    {
        myPicture = newPicture;
        id = newId;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            // Card will not flip if two cards have already been flipped or if there is a shuffle going on
            if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos) && manager.AskFlippedAmount() < 2 && !manager.AskShuffleStatus())
            {
                Flip();
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = myPicture;
        manager.GetComponent<MemoryGameManager>().CardFlipped(this);
    }

    public void UnFlip()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public int AskID()
    {
        return id;
    }
}
