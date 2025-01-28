// proceedurally generated AI choose your own adventure idea
using UnityEngine;
using System.Collections;

public class StoryGenerator : MonoBehaviour
{
    public string currentStoryText;

    void Start()
    {
        StartCoroutine(GenerateStory("You wake up in a dark forest.", "adventure", "choices"));
    }

    IEnumerator GenerateStory(string prompt, string genre, string style)
    {
        // Example API call (pseudo-code)
        string url = $"https://api.example.com/generate?prompt={prompt}&genre={genre}&style={style}";
        using (WWW request = new WWW(url))
        {
            yield return request;
            if (string.IsNullOrEmpty(request.error))
            {
                currentStoryText = request.text; // Assume the response is plain text
                Debug.Log("Generated Story: " + currentStoryText);
            }
            else
            {
                Debug.LogError("API Error: " + request.error);
            }
        }
    }

    public void ChooseOption(string choice)
    {
        StartCoroutine(GenerateStory($"You chose {choice}.", "adventure", "choices"));
    }
}
