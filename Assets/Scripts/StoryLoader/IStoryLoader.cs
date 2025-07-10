using System.Threading.Tasks;

public interface IStoryLoader 
{
    StoryData Load(string filePath);
}
