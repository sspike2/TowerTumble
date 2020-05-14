using UnityEditor;
using UnityEngine;
public class MenuTest : MonoBehaviour
{
    public static ColorAssign colorAssign;

    void OnEnable()
    {
        colorAssign = FindObjectOfType<ColorAssign>();
    }

    // Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
    // and give it a shortcut (ctrl-g on Windows, cmd-g on macOS).
    [MenuItem("MyMenu/Do Something with a Shortcut Key %g")]
    static void DoSomethingWithAShortcutKey()
    {
        if (!colorAssign)
        {
            colorAssign = FindObjectOfType<ColorAssign>();
        }

        colorAssign.setColor(colorAssign.id);
        Debug.Log("Doing something with a Shortcut Key...");


    }

    // Add a menu item called "Double Mass" to a Rigidbody's context menu.

}
