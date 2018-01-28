using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public AkAmbient gunReload;
    public AkAmbient gunShoot2D;
    public AkAmbient gunShoot3D;

    private Animator animator;
    private bool readyToFire = false;

    private void Awake() {
        this.animator = this.GetComponent<Animator>();
    }

    public void Fire() {
        if (!this.readyToFire) return;
        this.animator.speed = 1f;
        this.animator.Play("FireGun");
        this.readyToFire = false;
        AkSoundEngine.PostEvent((uint)(int)this.gunShoot2D.eventID, this.gunShoot2D.gameObject);
        AkSoundEngine.PostEvent((uint)(int)this.gunShoot3D.eventID, this.gunShoot3D.gameObject);
        
    }

    public void Reload() {
        if (this.readyToFire) return;
        this.animator.speed = 1f;
        this.animator.Play("Reload");
        AkSoundEngine.PostEvent((uint)(int)this.gunReload.eventID, this.gunReload.gameObject);
    }

    public void Stop() {
        this.animator.speed = 0f;
    }

    public void HasFinishedReload() {
        this.readyToFire = true;
    }
}
