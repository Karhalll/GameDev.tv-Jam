using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.Interactable
{
    public class Phase : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer phaseSpriteRendered = null;
        [SerializeField] public Collider2D phaseCollider = null;

        [Tooltip("Multiple sprites to change on manipulating")]
        [SerializeField] public bool multipleSprites = false;
        [SerializeField] public SpriteRenderer[] phaseSprites = null;
    }
}
