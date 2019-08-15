using TMPro;
using UnityEngine;

public class AdjustChatboxBG : MonoBehaviour {
    
    public float marginTop = 15.0f;
    public float marginBottom = 15.0f;
    public RectTransform rectTransform;
    
    private float verticalMargin;
    private float oldHeight;

    void Start() {
        verticalMargin = (marginTop + marginBottom) / 2;
    }

    public void AdjustChatbox(float chatTextHeight) {
        oldHeight = rectTransform.rect.height;
        AdjustHeight(chatTextHeight);
        AdjustYPosition();
    }
    
    //adjust the height of the background to match the size of the complete chat text
    private void AdjustHeight(float chatTextHeight) {
        var adjustedHeight = chatTextHeight + verticalMargin;
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, adjustedHeight);
    }

    //make sure the bottom border of the background is always on the same position, no matter how many text lines there are.
    private void AdjustYPosition() {
        var height = rectTransform.rect.height;
        var adjustmentYPosition = (height / 2) - (oldHeight / 2);
        var position = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(position.x, position.y + adjustmentYPosition);
    }

}
