using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour {

    public Transform cockpit;

    public float accelration = 1f;

    public Weapon weapon;

    public float maxHorizontalVelocity = 10f;
    public float maxVerticalVelocity = 10f;
    public float maxForwardVelocity = 10f;
    
    public AkAmbient mainEngines;
    public AkAmbient yawStart;
    public AkAmbient yawStop;
    public AkAmbient pitchStart;
    public AkAmbient pitchStop;

    private bool yawStartPlayed = false;
    private bool yawStopPlayed = true;
    private bool pitchStartPlayed = false;
    private bool pitchStopPlayed = true;

    private float thv = 0f; // target horizontal velocity
    private float tvv = 0f; // target vertical velocity
    private float tfv = 0f; // target forward velocity

    private float chv = 0f; // current horizontal velocity
    private float cvv = 0f; // current vertical velocity
    private float cfv = 0f; // current forward velocity

    private IEnumerator Start() {
        this.DisableWeapon();
        yield return new WaitForSeconds(1f);
        AkSoundEngine.PostEvent((uint)(int)this.mainEngines.eventID, this.mainEngines.gameObject);
    }

    public void SetHorizontalVelocity(float val) {
        this.thv = val * this.maxHorizontalVelocity;
    }

    public void SetVerticalVelocity(float val) {
        this.tvv = val * this.maxVerticalVelocity;
    }

    public void SetForwardVelocity(float val) {
        this.tfv = val * this.maxForwardVelocity;
    }

    public void DisableWeapon() {
        this.weapon.Stop();
    }

    public void Reload() {
        this.weapon.Reload();
    }

    public void FireGun() {
        this.weapon.Fire();
    }

    private void Update() {
        this.chv = Mathf.Lerp(this.chv, this.thv, Time.deltaTime * this.accelration / Mathf.Abs(this.thv - this.chv));
        this.cvv = Mathf.Lerp(this.cvv, this.tvv, Time.deltaTime * this.accelration / Mathf.Abs(this.tvv - this.cvv));
        this.cfv = Mathf.Lerp(this.cfv, this.tfv, Time.deltaTime * this.accelration / Mathf.Abs(this.tfv - this.cfv));
        this.HorizontalTurn();
        this.VerticalTurn();
        this.Move();
    }

    private void HorizontalTurn() {
        if (this.chv != 0f) {
            if (!this.yawStartPlayed) {
                AkSoundEngine.PostEvent((uint)(int)this.yawStart.eventID, this.yawStart.gameObject);
                this.yawStartPlayed = true;
                this.yawStopPlayed = false;
            }
        } else {
            if (!this.yawStopPlayed) {
                AkSoundEngine.PostEvent((uint)(int)this.yawStop.eventID, this.yawStop.gameObject);
                this.yawStopPlayed = true;
                this.yawStartPlayed = false;
            }
        }
        this.transform.eulerAngles += new Vector3(0f, this.chv * Time.deltaTime, 0f);
    }

    private void VerticalTurn() {
        if (this.cvv != 0f) {
            if (!this.pitchStartPlayed) {
                AkSoundEngine.PostEvent((uint)(int)this.pitchStart.eventID, this.pitchStart.gameObject);
                this.pitchStartPlayed = true;
                this.pitchStopPlayed = false;
            }
        } else {
            if (!this.pitchStopPlayed) {
                AkSoundEngine.PostEvent((uint)(int)this.pitchStop.eventID, this.pitchStop.gameObject);
                this.pitchStopPlayed = true;
                this.pitchStartPlayed = false;
            }
        }
        this.cockpit.localEulerAngles += new Vector3(this.cvv * Time.deltaTime, 0f, 0f);
    }

    private void Move() {
        this.transform.position += this.transform.forward * this.cfv * Time.deltaTime;
    }
}
