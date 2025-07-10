using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterName;
    [SerializeField] TextMeshProUGUI CharacterLine;

    public void SetName(string name)
    {
        CharacterName.text = name;
    }
    public void SetLine(string line)
    {
        CharacterLine.text = line;
    }
}
