using UnityEngine;

public class Output : Unit {

    public Unit connectedUnit;

    private Interactable port;

    void Awake() {
        this.port = GetComponentInChildren<Interactable>();
        this.port.OnBegin += Connect;
        this.port.OnEnd += Disconnect;
    }

    private void Connect() {

    }

    private void Disconnect() {

    }
}
