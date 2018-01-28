using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public LayerMask targatables;

    public AkAmbient gunReload;
    public AkAmbient gunShoot2D;
    public AkAmbient gunShoot3D;
    public AkAmbient gunReloadResume;
    public AkAmbient gunReloadInterrupted;

    public ParticleSystem explosion;
    
    private Animator animator;
    private bool readyToFire = false;
    private bool reloadInProgress = false;
    private bool interrupted = false;

    private void Awake() {
        this.animator = this.GetComponent<Animator>();
    }

    public void Fire() {
        if (!this.readyToFire) return;
        this.animator.speed = 1f;
        this.readyToFire = false;
        AkSoundEngine.PostEvent((uint)(int)this.gunShoot2D.eventID, this.gunShoot2D.gameObject);
        AkSoundEngine.PostEvent((uint)(int)this.gunShoot3D.eventID, this.gunShoot3D.gameObject);

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Bootstrap.instance.cg.mech.transform.forward, out hit, this.targatables)) {
            if(hit.collider) {
                Debug.Log(hit.collider.name);
                explosion.transform.position = hit.collider.transform.position;
                explosion.Play();
                //Creature creature = hit.collider.GetComponentInParent<Creature>();
                //
            }
        } else {

        }
    }

    public void Reload() {
        if (this.readyToFire) return;
        if(this.reloadInProgress) {
            if(this.interrupted) {
                this.animator.speed = 1f;
                this.interrupted = false;
                AkSoundEngine.PostEvent((uint)(int)this.gunReloadResume.eventID, this.gunReloadResume.gameObject);
            }
            return;
        }
        this.reloadInProgress = true;
        this.animator.speed = 1f;
        this.animator.Play("Reload");
        AkSoundEngine.PostEvent((uint)(int)this.gunReload.eventID, this.gunReload.gameObject);
    }

    public void Stop() {
        if(this.reloadInProgress) {
            this.interrupted = true;
            AkSoundEngine.PostEvent((uint)(int)this.gunReloadInterrupted.eventID, this.gunReloadInterrupted.gameObject);
        }
        this.animator.speed = 0f;
    }

    public void HasFinishedReload() {
        this.readyToFire = true;
        this.reloadInProgress = false;
        this.interrupted = false;
    }
}
