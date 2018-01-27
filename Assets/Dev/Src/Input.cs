using UnityEngine;

public class Input : Unit {

    public Unit connectedUnit;

    private Interactable port;

    void Awake() {
        this.port = GetComponentInChildren<Interactable>();
    }

}
