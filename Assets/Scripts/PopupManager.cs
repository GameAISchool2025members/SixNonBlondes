using UnityEngine;
using UnityEngine.Video;

public class PopupManager: MonoBehaviour {
    public static PopupManager Instance {get; private set;}
    public VideoClip[] clipArray;

    void Awake() {
        Instance = this;
    }
}
