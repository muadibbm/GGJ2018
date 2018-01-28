using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public AkAmbient idle;
    public AkAmbient angry;
    public AkAmbient hurt;
    public AkAmbient painHit;

    public float hitpoint = 100f;

    public float maxVelocity;

    private enum State { Idle, IdleAngry, Walking, WalkingHurt, Attack, Hit, DIE }
    private State currentState;
    private State previousState;

    private Animator animator;

    private void Awake() {
        this.currentState = State.Idle;
        this.animator = this.GetComponent<Animator>();
    }

    void Start () {
        AkSoundEngine.PostEvent((uint)(int)this.painHit.eventID, this.painHit.gameObject);
    }

    void Update() {
        if(this.previousState != this.currentState) {
            // Deal with state change

        } else {
            // Keep updating current State;

        }
        this.previousState = this.currentState;
    }

    //private void 
	
	public void TakeDamage(float damage) {
        this.hitpoint -= damage;
        AkSoundEngine.PostEvent((uint)(int)this.painHit.eventID, this.painHit.gameObject);
        this.currentState = State.Hit;
        //if (this.hitpoint <= 0f) {
        //    AkSoundEngine.PostEvent((uint)(int)this.hurt.eventID, this.hurt.gameObject);
        //}
    }
}
