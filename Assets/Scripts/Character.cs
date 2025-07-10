using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Character : MonoBehaviour
{
    float m_EdgePadding = 50f;     
                            
    RectTransform m_Rt;
    private void Awake()
    {
        m_Rt = GetComponent<RectTransform>();
    }
    public void SetLeft()
    {
        m_Rt.anchorMin = m_Rt.anchorMax = new Vector2(0f, 0.5f); 
        m_Rt.pivot = new Vector2(0f, 0.5f);               
        m_Rt.anchoredPosition = new Vector2(m_EdgePadding, 0f);  
    }

    public void SetRight()
    {
        m_Rt.anchorMin = m_Rt.anchorMax = new Vector2(1f, 0.5f); 
        m_Rt.pivot = new Vector2(1f, 0.5f);               
        m_Rt.anchoredPosition = new Vector2(-m_EdgePadding, 0f); 
    }
}
