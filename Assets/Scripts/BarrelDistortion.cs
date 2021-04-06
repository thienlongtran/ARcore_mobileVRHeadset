using UnityEngine;

public class BarrelDistortion : MonoBehaviour {
    #region Editor public fields

    [SerializeField]
    Material distortionMaterial;

    [SerializeField]
    [Range(0.5f, 1.5f)]
    float fov = 1f;

    #endregion

    #region Public properties

    #endregion

    #region Private fields

    #endregion

    #region Unity methods

    void Start() {
        distortionMaterial.SetFloat("_Alpha", 0.5f);
        distortionMaterial.SetFloat("_FOV", fov);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, null, distortionMaterial);
    }

    #endregion
}
