using UnityEngine;

using GameDevJam.Utils;
using GameDevJam.Controls;

namespace GameDevJam.Interactable
{
    public class Interactable : MonoBehaviour
    {
        [Header("Properities")]
        [SerializeField] TimePhases timePhases = null;
        [SerializeField] int currentPhaseIndex = 0;
        [SerializeField] float phaseTransitTime = 2f;

        [Header("TimeBar")]
        [SerializeField] TimeBar timeBarPref = null;
        [SerializeField] Transform timeBarSpawnPoitn = null;

        [Header("PrefabConnections")]
        [SerializeField] public Phase currentPhaseObj = null;
        [SerializeField] Phase newPhaseObj = null;
        [SerializeField] PlayerSpotter playerSpotter = null;

        TimePhases.TimePhase currentTimePhase = null;
        TimeBar timeBar = null;
        public bool isManupilated = false;

        private void Start() 
        {
            currentTimePhase = timePhases.GetPhases().ToArray()[currentPhaseIndex];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && playerSpotter.IsPlayerInRange())
            {
                if (!isManupilated)
                {
                    StartTimeBarMinigame();     
                }
                else
                {
                    EndTimerBarMinigame();
                }
            }
        }

        private void StartTimeBarMinigame()
        {
            EnablePlayerMovement(false);
            isManupilated = true;
            SpawnTimeBar();
            timeBar.StartSliding();
        }

        public void EndTimerBarMinigame()
        {
            EvaluateTimeBar();
            DestroyTimeBar();
            isManupilated = false;
            EnablePlayerMovement(true);
        }

        private void EvaluateTimeBar()
        {
            float sliderValue = timeBar.movingSlider.GetCurrentPos();

            var phases = timePhases.GetPhases();
            foreach (var phase in phases)
            {
                
                float distance = Mathf.Abs(phase.timeStamp - sliderValue);
                if (distance <= timeBar.stopingTolerance)
                {
                    ChangePhase(phase);         
                    break;
                }
            }
        }

        private void SpawnTimeBar()
        {
            DestroyTimeBar();
            timeBar = Instantiate(
                timeBarPref,
                timeBarSpawnPoitn.transform.position, 
                Quaternion.identity, 
                timeBarSpawnPoitn
            );
            timeBar.StopSliding();

            float startingPos = currentTimePhase.timeStamp;
            timeBar.SetSliderStartingPos(startingPos);

            timeBar.SetTimePhases(timePhases);
            timeBar.StartSliding();
        }

        private void ChangePhase(TimePhases.TimePhase newPhase)
        {
            var tempPhase = Instantiate(
                newPhase.phase,
                newPhaseObj.transform.position,
                Quaternion.identity,
                newPhaseObj.transform.parent
            );
            newPhaseObj.phaseCollider.enabled = false;
                
            Destroy(newPhaseObj.gameObject);
            newPhaseObj = tempPhase;

            SpriteRenderer spriteRendererCurrent = currentPhaseObj.phaseSpriteRendered;
            SpriteRenderer spriteRendererNew = newPhaseObj.phaseSpriteRendered;
            spriteRendererNew.color = new Color(
                spriteRendererNew.color.r,
                spriteRendererNew.color.g,
                spriteRendererNew.color.b,
                0f
            );
            FindObjectOfType<SpriteFader>().SpritesTransition(
                ref spriteRendererCurrent, 
                ref spriteRendererNew, 
                phaseTransitTime
            );

            var temp = currentPhaseObj;
            currentPhaseObj = newPhaseObj;
            newPhaseObj = temp;

            currentPhaseObj.phaseCollider.enabled = true;
            currentPhaseObj.LightOn(true);
            newPhaseObj.phaseCollider.enabled = false;
            newPhaseObj.LightOn(false);

            currentTimePhase = newPhase;
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
