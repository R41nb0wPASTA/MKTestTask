using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

public class ARWorldController : MonoBehaviour
{
    private GameObject objToSpawn;
    private GameObject rotationTarget;
    
    bool isObjSpawned = false;
    
    private ARRaycastManager arRaycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARModuleWorkStartSignal arModuleWorkStartSignal;
    private SignalBus signalBus;
    
    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        rotationTarget = transform.GetChild(0).gameObject;
    }
    
    public void CustomStart()
    {
        arModuleWorkStartSignal = new ARModuleWorkStartSignal() {goToPlace = objToSpawn};
        signalBus.Fire(arModuleWorkStartSignal);
        objToSpawn = arModuleWorkStartSignal.goToPlace;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;

                    if (!isObjSpawned)
                    {
                        SceneManager.MoveGameObjectToScene(objToSpawn, SceneManager.GetSceneByName("1-2.1"));
                        objToSpawn.transform.position = hitPose.position;
                        objToSpawn.transform.LookAt(new Vector3(rotationTarget.transform.rotation.x, objToSpawn.transform.rotation.y, rotationTarget.transform.rotation.z));
                            
                        isObjSpawned = true;
                    }
                    else
                    {
                        objToSpawn.transform.position = hitPose.position;
                    }
                    
                    signalBus.Fire(new ARObjectPlacedSignal());
                }
            }
        }
        /*else if (Input.GetMouseButtonDown(0))
        {
            if (!isObjSpawned)
            {
                SceneManager.MoveGameObjectToScene(objToSpawn, SceneManager.GetSceneByName("1-2.1"));
                objToSpawn.transform.position = Vector3.zero;
                objToSpawn.transform.LookAt(rotationTarget.transform);
                objToSpawn.transform.rotation = quaternion.Euler(new Vector3(0f, objToSpawn.transform.rotation.y, 0f));
                
                isObjSpawned = true;
            }
                    
            signalBus.Fire(new ARObjectPlacedSignal());
        }*/
    }

    public void SetSignalBus(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
}
