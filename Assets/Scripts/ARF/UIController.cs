using UnityEngine;

public class UIController : MonoBehaviour
{
    private bool isUIEnabled = true;
    void Update()
    {
        if (isUIEnabled && Input.touchCount > 0)
        {
            isUIEnabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
