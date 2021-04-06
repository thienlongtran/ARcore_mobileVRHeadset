using UnityEngine;
using UnityEngine.XR.ARFoundation;

public sealed class SeeThroughController : MonoBehaviour {
    #region Editor public fields

    [SerializeField]
    Camera seeThroughCamera;

    [SerializeField]
    Camera renderingCamera;

    [SerializeField]
    Material backgroundMaterial;

    [SerializeField]
    ARCameraManager cameraManager;

    [SerializeField]
    ARCameraBackground cameraBackground;

#if UNITY_EDITOR
    [SerializeField]
    Material debugBackgroundMaterial;
#endif

    #endregion

    #region Public properties

    public Material BackgroundMaterial {
        get { return backgroundMaterial; }
        set {
            backgroundMaterial = value;

            seeThroughRenderer.Mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;
            seeThroughRenderer.BackgroundMaterial = backgroundMaterial;

            if (cameraBackground != null) {
                if (!cameraBackground.useCustomMaterial) {
                    cameraBackground.useCustomMaterial = true;
                }
                cameraBackground.customMaterial = backgroundMaterial;
            }
        }
    }

    #endregion

    #region Private fields

    SeeThroughRenderer seeThroughRenderer;

#if UNITY_EDITOR
    WebCamTexture webCamTexture;
#endif

    #endregion

    #region Unity methods

    void Start() {
#if UNITY_EDITOR
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();

        debugBackgroundMaterial.SetTexture("_MainTex", webCamTexture);

        seeThroughRenderer = new SeeThroughRenderer(seeThroughCamera, debugBackgroundMaterial);
#else
        seeThroughRenderer = new SeeThroughRenderer(seeThroughCamera, backgroundMaterial);
#endif

        if (cameraBackground != null) {
            if (!cameraBackground.useCustomMaterial) {
                cameraBackground.useCustomMaterial = true;
            }
            cameraBackground.customMaterial = BackgroundMaterial;
        }

        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    #endregion

    #region Camera handling

    void SetupCameraIfNecessary() {
        seeThroughRenderer.Mode = UnityEngine.XR.ARRenderMode.MaterialAsBackground;

        if (seeThroughRenderer.BackgroundMaterial != BackgroundMaterial) {
            BackgroundMaterial = BackgroundMaterial;
        }
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs) {
        SetupCameraIfNecessary();
    }

    #endregion
}
