using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabInputChanger : MonoBehaviour
{
    EventSystem system;
    public Selectable firstSelect;

    private void Start()
    {
        system = EventSystem.current;
        firstSelect.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable prev = system.currentSelectedGameObject.
                GetComponent<Selectable>().FindSelectableOnUp();

            if (prev != null)
            {
                prev.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.
                GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                next.Select();
            }
        }    
    }
}
