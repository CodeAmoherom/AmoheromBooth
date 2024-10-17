using UnityEngine;
using UnityEngine.UI;

public class TestBtnGen : MonoBehaviour
{
    public Sprite legacyButtonSprite; // Assign the sprite via the Unity Inspector

    void Start()
    {
        // Create 10 buttons
        for (int i = 0; i < 10; i++)
        {
            // Create a new button
            GameObject buttonObject = new GameObject("Button" + (i + 1));
            buttonObject.transform.SetParent(this.transform);
            Image image = buttonObject.AddComponent<Image>();

            image.sprite = legacyButtonSprite;

            Button newBtn = buttonObject.AddComponent<Button>();
            newBtn.targetGraphic = image;

            RectTransform transform = newBtn.GetComponent<RectTransform>();
            transform.sizeDelta = new Vector2(50, 75);  // Width = 50, Height = 75
            transform.localScale = new Vector2(1, 1);

        }
    }
}
