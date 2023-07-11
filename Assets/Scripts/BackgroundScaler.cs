using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private readonly float backgroundRatio = 500f / 153f;
    private readonly float heightPercentage = 0.6f;

    void Update()
    {
        float a = Camera.main.orthographicSize * 2f;
        if (Camera.main.aspect / heightPercentage > backgroundRatio) {
            float width = Camera.main.orthographicSize * 2f * Camera.main.aspect;
            this.transform.localScale = new Vector3(width, width);
        } else {
            float height = Camera.main.orthographicSize * 2f * heightPercentage * backgroundRatio;
            this.transform.localScale = new Vector3(height, height);
        }
    }
}
