using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIElements
{
    [SerializeField]
    public string TriggerTagName;
    [SerializeField]
    public GameObject UIElementGameObject;
    [SerializeField]
    public bool IsEnabled = false;
}

public class PlayerShowUI : MonoBehaviour
{
    public List<UIElements> TriggerUIElements;

    private void OnTriggerEnter(Collider other)
    {
        foreach(UIElements uiElement in TriggerUIElements)
        {
            if(other.gameObject.tag == uiElement.TriggerTagName)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (uiElement.UIElementGameObject.activeSelf)
                        uiElement.UIElementGameObject.SetActive(false);
                    else
                        uiElement.UIElementGameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (UIElements uiElement in TriggerUIElements)
        {
            if (other.gameObject.tag == uiElement.TriggerTagName)
            {
                if (uiElement.UIElementGameObject.activeSelf)
                    uiElement.UIElementGameObject.SetActive(false);
                else
                    uiElement.UIElementGameObject.SetActive(true);
            }
        }
    }
}
