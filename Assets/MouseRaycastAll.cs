using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycastAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30.0F);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (hit.collider.gameObject.tag == "Mouser")
            {
                hit.collider.gameObject.GetComponent<Targets_Container_Attn>().hitMe = true;
            }
        }
    }
}
