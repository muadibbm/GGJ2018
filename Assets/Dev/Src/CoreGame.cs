using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour {

    public Mech mech;

	void Start () {
        this.mech = GameObject.FindGameObjectWithTag("Mech").GetComponent<Mech>();
    }
}
