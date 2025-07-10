using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections.Generic;
using System;

public enum FrameType
{ 
    // greeting, beginning of the dialogue
    Dialogue = 0, 
    // character's text
    Text = 1,
    // user response
    Option = 2, 
    // last text user reads before dialogue ends
    Final = 3 
}
[Serializable]
public class Option
{
    public string choice;
    public string targetFrameId;
}
[Serializable]
public class Frame
{
    public FrameType type;
    public string nextId;          
    public string id;
    public string characterName;   
    public string spritePosition;
    public string text;            
    public List<Option> options;   


    public override string ToString()
    {
        
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"Frame  id: {id}");
        sb.AppendLine($"       type: {type}");
        if (!string.IsNullOrEmpty(characterName))
            sb.AppendLine($"       character: {characterName}");
        sb.AppendLine($"       text: \"{text}\"");

        if (options != null && options.Count > 0)
        {
            sb.AppendLine("       options:");
            foreach (var o in options)
                sb.AppendLine($"         • {o.choice}  →  {o.targetFrameId}");
        }

        if (!string.IsNullOrEmpty(nextId))
            sb.AppendLine($"       nextId: {nextId}");

        return sb.ToString();

    }

}

[Serializable]
public class StoryData
{
    public List<Frame> frames;
}
