using System.Threading.Tasks;

public interface IStoryLoader 
{
    StoryData LoadFromFile(string filePath);
    StoryData LoadFromString(string text);
}
