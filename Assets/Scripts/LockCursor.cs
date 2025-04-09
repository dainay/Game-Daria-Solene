using UnityEngine;

public class LockCursor : MonoBehaviour
{
    //locking the cursor because it stays avaibale after passing from the main menu to the game
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
