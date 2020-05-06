using System.Collections;
using UnityEngine;

using GameDevJam.Utils;
using GameDevJam.Controls;

namespace GameDevJam.Interactable
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] PlayerSpotter playerSpotter = null;
        [SerializeField] TimePhases timePhases = null;
        [SerializeField] SpriteRenderer spriteRendererCurrent = null;
        [SerializeField] SpriteRenderer spriteRendererNew = null;
        [SerializeField] float phaseTransitTime = 2f;

        [Header("TimeBar")]
        [SerializeField] TimeBar timeBarPref = null;
        [SerializeField] Transform timeBarSpawnPoitn = null;

        TimeBar timeBar = null;

        int currentPhase = 0;
        int maxPhases = 5;

        bool isManupilated = false;

        private void Start()
        {
            maxPhases = timePhases.GetPhases().Length;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && playerSpotter.IsPlayerInRange())
            {
                if (!isManupilated)
                {
                    EnablePlayerMovement(false);
                    isManupilated = true;
                    SpawnTimeBar();
                }
                else
                {
                    EnablePlayerMovement(true);
                    isManupilated = false;
                    DestroyTimeBar();
                }
            }
        }

        private void SpawnTimeBar()
        {
            DestroyTimeBar();
            timeBar = Instantiate(
                timeBarPref, 
                timeBarPref.transform.position, 
                Quaternion.identity, 
                timeBarSpawnPoitn
            );
            timeBar.SetTimePhases(timePhases);

            float startingPos = timePhases.GetPhases()[currentPhase].timeStamp;
            timeBar.SetSliderStartingPos(startingPos);
        }

        private void ChangePhase(int phaseNumber)
        {
            if (phaseNumber > maxPhases - 1)
            {
                Debug.LogError(gameObject.name + " phase limit exeded");
                return;
            }

            TimePhases.Phase phase = timePhases.GetPhases()[phaseNumber];

            spriteRendererNew.color = new Color(
                spriteRendererNew.color.r,
                spriteRendererNew.color.g,
                spriteRendererNew.color.b,
                0f
            );
            spriteRendererNew.sprite = phase.phaseSprite;

            FindObjectOfType<SpriteFader>().SpritesTransition(
                ref spriteRendererCurrent, 
                ref spriteRendererNew, 
                phaseTransitTime
            );
        }

        private void DestroyTimeBar()
        {
            foreach (Transform child in timeBarSpawnPoitn)
            {
                Destroy(child.gameObject);
            }
        }

        private void EnablePlayerMovement(bool state)
        {
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            if (state)
            {
                playerController.EnableControl();
            }
            else
            {
                playerController.DisableControl();
            }
        }
    }
}
