using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PopUp: MonoBehaviour {
    public string videoPath;
    private RenderTexture renderTexture;

    private void HidePopUp() {
        Destroy(gameObject);
    }

    public void OnCloseButtonClick() {
        HidePopUp();    
    }

    void Start() {
        int randomVideoIndex = Random.Range(0, PopupManager.Instance.clipArray.Length);
        
        VideoPlayer videoPlayer = GetComponentInChildren<VideoPlayer>();
        RawImage rawImage = GetComponentInChildren<RawImage>();
        RectTransform thisRectTransform = GetComponent<RectTransform>();
        
        VideoClip clip = PopupManager.Instance.clipArray[randomVideoIndex];
        
        renderTexture = new((int)clip.width / 4, (int)clip.height / 4, 1, RenderTextureFormat.ARGBHalf );
        
        videoPlayer.clip = clip;
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
        
        thisRectTransform.rect.Set(thisRectTransform.rect.x, thisRectTransform.rect.y, clip.width, clip.height);
    }
}
