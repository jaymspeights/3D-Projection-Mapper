using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionMesh : MonoBehaviour
{

    public GameObject viewerPerspectiveCameraObject;
    public MeshRenderer screen;

    Camera viewerPerspectiveCamera;
    RenderTexture viewTexture;

    // Start is called before the first frame update
    void Awake()
    {   
        viewerPerspectiveCamera = viewerPerspectiveCameraObject.GetComponent<Camera> ();
    }

    void OnPreCull()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height) {
            if (viewTexture != null) {
                viewTexture.Release ();
            }
            viewTexture = new RenderTexture (Screen.width, Screen.height, 0);
            viewerPerspectiveCamera.targetTexture = viewTexture;
            screen.material.SetTexture ("_MainTex", viewTexture);
        }
        // screen.material.SetInt ("displayMask", 0);

        viewerPerspectiveCamera.Render ();
        
        // screen.material.SetInt ("displayMask", 1);
        screen.material.SetMatrix("_ViewMatrix", viewerPerspectiveCamera.worldToCameraMatrix);
        screen.material.SetMatrix("_ProjectionMatrix", viewerPerspectiveCamera.projectionMatrix);
    }

}
