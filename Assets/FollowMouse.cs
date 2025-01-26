using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private bool shouldFollowMouse = false;
    
    private void Update()
    {
        if (shouldFollowMouse)
        {
            makeFollowMouse();
        }
    }
    
    private void OnMouseDown()
    {
        shouldFollowMouse = true;
    }
    
    private void OnMouseUp()
    {
        shouldFollowMouse = false;
    }
    
    private void makeFollowMouse()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Debug.Log(mousePosition);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
}
