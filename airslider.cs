// using a list to proceedurally generate and manage air bubbles on an air bubble slider
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AirBubbleManager : MonoBehaviour
{
    public GameObject airBubblePrefab; // Prefab for an air bubble icon
    public Transform airBubbleContainer; // UI container for air bubbles
    public int maxAir = 5; // Maximum air bubbles
    private int currentAir;
    private List<GameObject> airBubbles = new List<GameObject>();

    private float timer = 0f; // Timer for air decrease
    public float decreaseInterval = 5f; // Time between air decreases

    void Start()
    {
        // Initialize air bubbles
        currentAir = maxAir;
        for (int i = 0; i < maxAir; i++)
        {
            GameObject bubble = Instantiate(airBubblePrefab, airBubbleContainer);
            bubble.transform.localScale = Vector3.one; // Full size
            airBubbles.Add(bubble);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= decreaseInterval)
        {
            timer = 0f;
            DecreaseAir();
        }
    }

    void DecreaseAir()
    {
        if (currentAir > 0)
        {
            currentAir--;
            UpdateAirBubbles();
        }
    }

    public void CollectAir()
    {
        if (currentAir < maxAir)
        {
            currentAir++;
            UpdateAirBubbles();
        }
    }

    void UpdateAirBubbles()
    {
        for (int i = 0; i < airBubbles.Count; i++)
        {
            if (i < currentAir)
            {
                airBubbles[i].transform.localScale = Vector3.one; // Full size
                airBubbles[i].SetActive(true);
            }
            else if (i == currentAir)
            {
                airBubbles[i].transform.localScale = Vector3.one * 0.5f; // Half size
                airBubbles[i].SetActive(true);
            }
            else
            {
                airBubbles[i].SetActive(false); // Invisible
            }
        }
    }
}
