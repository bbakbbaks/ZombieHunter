using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitbar : MonoBehaviour {
    public RectTransform m_cRectTransform;

    void Start()
    {
        m_cRectTransform = this.gameObject.GetComponent<RectTransform>();
    }
}
