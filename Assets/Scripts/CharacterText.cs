using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterLine;

    public void SetName(string name)
    {
        characterName.text = name;
    }
    public void SetLine(string line)
    {
        characterLine.text = line;
    }
}
