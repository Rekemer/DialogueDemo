using System.Threading.Tasks;
using System.IO;
public class JsonStoryLoader : IStoryLoader
{
    public  StoryData Load(string fileName)
    {
        var json =  File.ReadAllText(fileName);
        return UnityEngine.JsonUtility.FromJson<StoryData>(json);
    }
}