using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public Animation Anim;
    public Material DefaultDrawerMat;
    public Material HighlightDrawerMat;
    public LayerMask DrawerLayerMask;

    private Ray ray;
    private RaycastHit hit;
    private bool pressingButton = false;
    private bool openingDrawer = false;
    private Collider lastDrawerCollider = null;

    private bool openedDrawer1 = false;
    private bool openedDrawer2 = false;

    private void Update()
    {
        if (/*!pressingButton && */!openingDrawer)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, DrawerLayerMask))
            {
                if (lastDrawerCollider != null && lastDrawerCollider != hit.collider)
                {
                    lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                    lastDrawerCollider = null;
                }
                if (lastDrawerCollider == null)
                {
                    lastDrawerCollider = hit.collider;
                    lastDrawerCollider.GetComponent<Renderer>().material = HighlightDrawerMat;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    openingDrawer = true;
                    lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                    if (lastDrawerCollider.gameObject.name == "drawer_1")
                    {
                        if (!openedDrawer1)
                        {
                            Anim.Play("OpenDrawer1");
                            openedDrawer1 = true;
                        }
                        else
                        {
                            Anim.Play("CloseDrawer1");
                            openedDrawer1 = false;
                        }
                    }
                    else
                    {
                        if (!openedDrawer1)
                        {
                            Anim.Play("ShakeDrawer2");
                        }
                        else
                        {
                            openingDrawer = false;
                        }
                    }
                }
            }
            else if (lastDrawerCollider != null)
            {
                lastDrawerCollider.GetComponent<Renderer>().material = DefaultDrawerMat;
                lastDrawerCollider = null;
            }
        }
    }

    public void OnFinishTryingToOpenDrawer()
    {
        openingDrawer = false;
    }
}
