using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public AkAmbient idle;
    public AkAmbient angry;
    public AkAmbient hurt;
    public AkAmbient painHit;
    public AkAmbient death;

    public float hitpoint = 100f;

    public float maxRotationVelocity;
    public float maxForwardVelocity;
    public float acceleration = 1f;

    private float currentVelocity;

    private Vector3 nextForward;

    private enum State { Idle, IdleAngry, Walking, WalkingHurt, Attack, Hit, Death }
    private State currentState;
    private State previousState;

    private Animator animator;

    private void Awake() {
        this.animator = this.GetComponent<Animator>();
        this.animator.speed *= 0.4f;
        this.nextForward = this.transform.forward;
    }

    void Start () {
        this.currentState = State.Idle;
        AkSoundEngine.PostEvent((uint)(int)this.idle.eventID, this.idle.gameObject);
    }

    void Update() {
        this.UpdateStates();
        if (this.previousState != this.currentState) {
            this.UpdateAnimations();
            this.UpdateSounds();
        }
        this.previousState = this.currentState;
    }

    private void UpdateStates() {
        switch (this.currentState) {
            case State.Idle:
                this.DoIdle();
                break;
            case State.IdleAngry:
                this.DoIdle();
                break;
            case State.Walking:
                this.DoWalk();
                break;
            case State.WalkingHurt:
                this.DoWalk();
                break;
            case State.Attack:
                this.DoAttack();
                break;
            case State.Hit:
                this.DoHit();
                break;
            case State.Death:
                this.DoDeath();
                // the end has come
                break;
        }
    }

    private void UpdateAnimations() {
        switch(this.currentState) {
            case State.Idle:
                this.animator.Play("Idle");
                break;
            case State.IdleAngry:
                this.animator.Play("Angry");
                break;
            case State.Walking:
                this.animator.Play("Walking");
                break;
            case State.WalkingHurt:
                this.animator.Play("HurtWalk");
                break;
            case State.Attack:
                this.animator.Play("Throw");
                break;
            case State.Hit:
                this.animator.Play("Hit");
                break;
            case State.Death:
                this.animator.Play("Death");
                break;
        }
    }

    private void UpdateSounds() {
        switch (this.currentState) {
            case State.Idle:
                AkSoundEngine.PostEvent((uint)(int)this.idle.eventID, this.idle.gameObject);
                break;
            case State.IdleAngry:
                AkSoundEngine.PostEvent((uint)(int)this.angry.eventID, this.angry.gameObject);
                break;
            case State.Walking:
                AkSoundEngine.PostEvent((uint)(int)this.idle.eventID, this.idle.gameObject);
                break;
            case State.WalkingHurt:
                AkSoundEngine.PostEvent((uint)(int)this.hurt.eventID, this.hurt.gameObject);
                break;
            case State.Attack:
                break;
            case State.Hit:
                AkSoundEngine.PostEvent((uint)(int)this.painHit.eventID, this.painHit.gameObject);
                break;
            case State.Death:
                AkSoundEngine.PostEvent((uint)(int)this.death.eventID, this.death.gameObject);
                break;
        }
    }

    public void TakeDamage(float damage) {
        if (this.currentState == State.Hit) return;
        this.hitpoint -= damage;
        this.currentState = State.Hit;
        if (this.hitpoint <= 0f) this.currentState = State.Death;
    }

    private bool HasAnimationEnded() {
        return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }

    private void DoIdle() {
        this.currentVelocity = 0f;
        if (this.hitpoint > 50) {
            this.currentState = State.Idle;
        } else {
            this.currentState = State.IdleAngry;
        }
    }

    private void DoWalk() {
        if (this.hitpoint > 50) {
            this.currentState = State.Walking;
        } else {
            this.currentState = State.WalkingHurt;
        }
        this.transform.forward = Vector3.Lerp(this.transform.forward, this.nextForward, Time.deltaTime * this.maxRotationVelocity);
        this.currentVelocity = Mathf.Clamp(this.currentVelocity + this.acceleration, 0f, this.maxForwardVelocity);
        this.transform.position += this.transform.forward * this.currentVelocity * Time.deltaTime;
    }

    private void DoAttack() {
        this.currentVelocity = 0f;
    }

    private void DoHit() {
        this.currentVelocity = 0f;
        if (this.HasAnimationEnded()) {
            if (Random.Range(0, 4) == 0) {
                this.DoIdle(); // 25% chance of going idle
            } else {
                this.nextForward = new Vector3(Random.Range(-1f, 1f), this.transform.forward.y, Random.Range(-1f, 1f));
                this.DoWalk();
            }
        }
    }

    private void DoDeath() {
        this.currentVelocity = 0f;
        // do nothing, just die
    }
}
