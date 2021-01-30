using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraReferenceRegister : MonoBehaviour
{
    [SerializeField]
    private CameraReference cameraReference;

    private void Awake()
    {
        Camera cam = GetComponent<Camera>();
        cameraReference.Value = cam;
    }
}