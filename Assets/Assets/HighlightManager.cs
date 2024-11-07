using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableOutlineOnStart : MonoBehaviour

{
    private Transform highlightedObj;
    private Transform selectedObj;
    public LayerMask selectableLayer;

    private Outline highlightOutline;
    private RaycastHit hit;


    // Update is called once per frame
    void Update()
    {
        HoverHighlight();
    }

    public void HoverHighlight()
    {
        if (highlightOutline != null)
        {
            highlightOutline.enabled = false;
            highlightedObj = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, selectableLayer))
        {
            highlightedObj = hit.transform;

            if (highlightedObj.CompareTag("Enemy") && highlightedObj != selectedObj)
            {
                highlightOutline = highlightedObj.GetComponent<Outline>();
                highlightOutline.enabled = true;
            }
            else
            {
                highlightedObj = null;
            }
        }
    }

    public void SelectedHighlight()
    {
        if (highlightedObj.CompareTag("Enemy"))
        {
            if (highlightedObj != null)
            {
                selectedObj.GetComponent<Outline>().enabled = false;
            }

            selectedObj = hit.transform;
            selectedObj.GetComponent<Outline>().enabled = true;

            highlightOutline.enabled = true;
            highlightedObj = null;
        }
    }

    public void DeselecHighlight()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectedObj = null;
    }
}