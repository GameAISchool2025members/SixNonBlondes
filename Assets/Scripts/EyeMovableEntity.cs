using UnityEngine;

public class EyeMovableEntity: MonoBehaviour {
    [SerializeField] private Gaze gaze;
    [SerializeField] private float screenSize;
    [SerializeField] private float extent;

    void Update() {
        Vector3 pos = transform.position;

        pos.x = (2 * (gaze.gazeLocation.x / Screen.currentResolution.width) - 1) * screenSize;

        if (pos.x >= extent) pos.x = extent;
        else if (pos.x <= -extent) pos.x = -extent;
        
        transform.position = new(pos.x, pos.y, pos.z);
    }
}
