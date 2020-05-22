using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

namespace GameDevJam.Interactable
{
    public class Phase : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer phaseSpriteRendered = null;
        [SerializeField] public Collider2D phaseCollider = null;
        [SerializeField] public Light2D[] lights = null; 

        [Tooltip("Multiple sprites to change on manipulating")]
        [SerializeField] public bool multipleSprites = false;
        [SerializeField] public SpriteRenderer[] phaseSprites = null;

        public void LightOn(bool state)
        {
            foreach(var light in lights)
            {
                light.gameObject.SetActive(state);
            }
        }
    }
}
