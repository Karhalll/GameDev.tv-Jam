using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevJam.EditorUtils
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public class HierarchySetter : MonoBehaviour
    {
        [SerializeField] ESceneFolders folder = 0;

        private void Start() 
        {
            MakeChildOf(folder.ToString());
        }

        private void MakeChildOf(string parentName)
        {
            if (!GameObject.Find(parentName))
            {
                GameObject newParent = new GameObject(parentName);
            }

            transform.parent = GameObject.Find(parentName).transform;
        }
    }
#endif
}
