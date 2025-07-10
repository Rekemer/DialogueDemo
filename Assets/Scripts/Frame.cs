using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections.Generic;
using System;

public enum FrameType
{ 
    // greeting, beginning of the dialogue
    Dialogue, 
    // character's text
    Text,
    // user response
    Option, 
    // last text user reads before dialogue ends
    Final 
}
[Serializable]
public class Option
{
    public string title;
    public string targetFrameId;
}
[Serializable]
public class Frame
{
    public FrameType type;
    public string nextId;          // for Dialogue, Text (auto-advance)
    public string id;
    public string characterName;   // only for Dialogue
    public string text;            // for Dialogue & Text & Choice title
    public List<Option> options;   // only for Choice


    public override string ToString()
    {
        
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"Frame  id: {id}");
        sb.AppendLine($"       type: {type}");
        if (!string.IsNullOrEmpty(spriteName))
            sb.AppendLine($"       sprite: {spriteName}");
        if (!string.IsNullOrEmpty(characterName))
            sb.AppendLine($"       character: {characterName}");
        sb.AppendLine($"       text: \"{text}\"");

        if (options != null && options.Count > 0)
        {
            sb.AppendLine("       options:");
            foreach (var o in options)
                sb.AppendLine($"         • {o.title}  →  {o.targetFrameId}");
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
