using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {
    public RectTransform m_cRectTransform;

    void Start()
    {
        m_cRectTransform = this.gameObject.GetComponent<RectTransform>();
    }
}
