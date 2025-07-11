using UnityEngine.UI;
using UnityEngine;

public sealed class ButtonClick : MonoBehaviour, IClickable
{
    [SerializeField] Button target;
    public void SetBlocked(bool b) => target.interactable = !b;
}