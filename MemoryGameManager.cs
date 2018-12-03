using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// By Taru Konttinen 2018

public class MemoryGameManager : MonoBehaviour
{
    GameObject[] cards;
    Card[] flippedCards;

    int flippedAmount;
    bool shuffling;
    int pairCount;

    public Text pairText;
    public Timer timer;
    public Text shufflingText;

    public Sprite picture1;
    public Sprite picture2;
    public Sprite picture3;
    public Sprite picture4;
    public Sprite picture5;
    public Sprite picture6;
    public Sprite picture7;
    public Sprite picture8;
    public Sprite picture9;
    public Sprite picture10;

    void Start()
    {
        flippedCards = new Card[2];

        cards = GameObject.FindGameObjectsWithTag("Card");
        ShuffleCards();
	}
	
    // Used by cards to report when they've been flipped
	public void CardFlipped(Card card)
    {
        flippedCards[flippedAmount] = card;
        flippedAmount++;

        // Once two cards have been flipped, compare them and either add points or unflip
        if (flippedAmount == 2)
        {
            if (flippedCards[0].AskID() == flippedCards[1].AskID())
            {
                pairCount++;
                pairText.text = LocalizationManager.instance.GetLocalizedValue("score") + pairCount * 10;

                // If all pairs have been found, reshuffle
                if (pairCount % 10 == 0)
                {
                    ShuffleCards();
                    StartCoroutine("ReshufflingAlert");
                    StartCoroutine("UnflipAll");
                }

                flippedAmount = 0;
            }

            else
            {
                StartCoroutine("UnflipCards");
            }
        }
	}

    IEnumerator ReshufflingAlert()
    {
        timer.FreezeTimer();
        shuffling = true;
        shufflingText.text = "Shuffling...";
        shufflingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        shufflingText.text = "Shuffling..";

        yield return new WaitForSeconds(0.5f);

        shufflingText.text = "Shuffling...";

        yield return new WaitForSeconds(0.5f);

        shufflingText.text = "Shuffling..";

        shufflingText.gameObject.SetActive(false);
        shuffling = false;
        timer.UnfreezeTimer();
    }

    IEnumerator UnflipCards()
    {
        yield return new WaitForSeconds(1);

        flippedCards[0].UnFlip();
        flippedCards[1].UnFlip();
        flippedAmount = 0;
    }

    IEnumerator UnflipAll()
    {
        yield return new WaitForSeconds(1);

        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<Card>().UnFlip();
        }
    }

    void ShuffleCards()
    {
        int[] quotas = new int[10];

        for (int i = 0; i < cards.Length; i++)
        {
            bool notOK = true;

            // Give the card a random picture and an ID, but make sure there will be only two duplicates of each picture
            while (notOK)
            {
                int rnd = Random.Range(1, 11);

                if (rnd == 1 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture1, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 2 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture2, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 3 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture3, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 4 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture4, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 5 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture5, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 6 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture6, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 7 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture7, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 8 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture8, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 9 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture9, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }

                else if (rnd == 10 && quotas[rnd - 1] < 2)
                {
                    cards[i].GetComponent<Card>().Configure(picture10, rnd);
                    quotas[rnd - 1]++;
                    notOK = false;
                }
            }
        }
    }

    // Cards use these two methods to make sure they don't flip when two cards have already been flipped or when a shuffle is going on
    public int AskFlippedAmount()
    {
        return flippedAmount;
    }

    public bool AskShuffleStatus()
    {
        return shuffling;
    }
}
