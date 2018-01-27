using UnityEngine;

public class Module : Unit {

    public Unit connectedUnitA;
    public Unit connectedUnitB;

    public Interactable connectionA;
    public Interactable connectionB;

    private void Awake() {
        connectionA.OnBegin += test;
        connectionA.OnEnd += test2;
    }

    private void test() {
        Debug.Log("click yay");
    }

    private void test2() {
        Debug.Log("release yay");
    }

}
