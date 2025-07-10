using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Character : MonoBehaviour
{
    [SerializeField] float edgePadding = 50f;     // px inset from the edge
                                                  // will scale automatically
                                                  // if your Canvas has a
                                                  // CanvasScaler set to
                                                  // “Scale With Screen Size”

    RectTransform m_Rt;
    private void Awake()
    {
        m_Rt = GetComponent<RectTransform>();
    }
    public void SetLeft()
    {
        m_Rt.anchorMin = m_Rt.anchorMax = new Vector2(0f, 0.5f); // centre-left
        m_Rt.pivot = new Vector2(0f, 0.5f);                // own left edge
        m_Rt.anchoredPosition = new Vector2(edgePadding, 0f);  // +x pushes right
    }

    /// Snap the portrait to the right-centre of its parent.
    public void SetRight()
    {
        m_Rt.anchorMin = m_Rt.anchorMax = new Vector2(1f, 0.5f); // centre-right
        m_Rt.pivot = new Vector2(1f, 0.5f);                // own right edge
        m_Rt.anchoredPosition = new Vector2(-edgePadding, 0f); // –x pushes left
    }
}
