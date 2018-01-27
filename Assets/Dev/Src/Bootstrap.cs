using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour {

    public static Bootstrap instance = null;

    public InteractionManager im;
    public LayerMask interactionLayer;

    void Awake () {
        if (instance != null) Destroy(this);
        instance = this;

        this.im = this.gameObject.AddComponent<InteractionManager>();
        this.im.interactionLayer = this.interactionLayer;

        DontDestroyOnLoad(this.gameObject);
    }
}
