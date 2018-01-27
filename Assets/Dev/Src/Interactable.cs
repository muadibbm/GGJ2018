using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour {

    public delegate void Interaction();
    public Interaction OnBegin;
    public Interaction OnEnd;
	
    public void OnInteractionBegin() {
        if (this.OnBegin != null) this.OnBegin();
    }

    public void OnInteractionEnd() {
        if (this.OnEnd != null) this.OnEnd();
    }
}
