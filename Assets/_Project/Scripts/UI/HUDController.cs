using UnityEngine;

namespace Tester.UI
{
    /// <summary>
    /// Minimal HUD controller placeholder for prototype UI updates.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Canvas hudCanvas;

        public Canvas HudCanvas => hudCanvas;
    }
}
