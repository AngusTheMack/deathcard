using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class checkTrackableDeath : MonoBehaviour, ITrackableEventHandler
{

  private TrackableBehaviour mTrackableBehaviour;
  public TextMesh text_mesh;
  public GameObject heartTranform;
  public Transform myModelPrefab;
  // Use this for initialization
  void Start ()
  {
    mTrackableBehaviour = GetComponent<TrackableBehaviour>();
    if (mTrackableBehaviour) {
      mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }
  }
  // Update is called once per frame
  void Update ()
  {
  }
  public void OnTrackableStateChanged(
    TrackableBehaviour.Status previousStatus,
    TrackableBehaviour.Status newStatus)
  { 
    if (newStatus == TrackableBehaviour.Status.DETECTED ||
        newStatus == TrackableBehaviour.Status.TRACKED ||
        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
    {
      OnTrackingFound();
    }
  } 
  public void Explode() {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
  }
  private void OnTrackingFound()
  {
    if (myModelPrefab != null)
    {

      Transform myModelTrf = GameObject.Instantiate(myModelPrefab) as Transform;
      myModelTrf.parent = mTrackableBehaviour.transform;
      myModelTrf.localPosition = new Vector3(0f, 0f, 0f);
      myModelTrf.localRotation = Quaternion.identity;
      myModelTrf.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
      myModelTrf.gameObject.active = true;
      heartTranform.GetComponent<Renderer>().enabled = false;
      text_mesh.text="Death";
    }
  }
}