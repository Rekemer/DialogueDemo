using System.IO;
public class JsonStoryLoader : IStoryLoader
{
    public  StoryData LoadFromFile(string fileName)
    {
        var json =  File.ReadAllText(fileName);
        return LoadFromString(json);
    }

    public StoryData LoadFromString(string text)
    {
        return UnityEngine.JsonUtility.FromJson<StoryData>(text);
    }
}