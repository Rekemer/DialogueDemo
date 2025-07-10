using UnityEngine;

public class DialogueBootstrap : MonoBehaviour
{
    [SerializeField] TextAsset StoryJson;

    void Awake()
    {
        var provider = new JsonStoryLoader();
        if (StoryJson == null) Debug.LogError("Story field is not initialized!");
        var story = provider.LoadFromString(StoryJson.text);
        GetComponent<DialogueManager>().BuildStates(story);
    }
}
