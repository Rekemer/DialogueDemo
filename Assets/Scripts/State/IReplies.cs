using System;
using UnityEngine;

public interface IRepliesView
{
    void Reset();
    void AddChoice(string text, string nextId, Action<string> cb);
    GameObject gameObject { get; }
}