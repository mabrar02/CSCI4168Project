using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCore : MonoBehaviour
{
    [SerializeField] private float coreRange;
    private GameObject lastHighlightedObject;



    private Camera cam;
    private RaycastHit hit;
    private LayerMask turretLayer;


    void Start() {
        cam = Camera.main;
        turretLayer = LayerMask.GetMask("Tower");

    }

    private void OnDestroy() {
        ResetHighlight();
    }

    // Update is called once per frame
    void Update() {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, coreRange, turretLayer)) {
            if (hit.collider.gameObject != lastHighlightedObject) {
                ResetHighlight();
                HighlightObject(hit.collider.gameObject);
                lastHighlightedObject = hit.collider.gameObject;
            }
        }
        else {
            ResetHighlight();
            lastHighlightedObject = null;
        }

        if (Input.GetButtonDown("Fire1") && lastHighlightedObject != null) {
            ApplyCore(lastHighlightedObject);
        }

    }

    public virtual void ApplyCore(GameObject target) {

    }

    private void HighlightObject(GameObject obj) {
        if (obj != null) {
            obj.GetComponent<Outline>().enabled = true;
        }
    }

    private void ResetHighlight() {
        if (lastHighlightedObject != null) {
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
        }
    }
}