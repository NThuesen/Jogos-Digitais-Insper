using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ArenaCamera : MonoBehaviour
{
    private Camera view;
    void Awake() { view = GetComponent<Camera>(); }
    void LateUpdate() { view.orthographicSize = Mathf.Max(5.1f, 8.4f / view.aspect); }
}
