using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MovingSlider : MonoBehaviour
{
    [SerializeField] float period = 2f;

    Slider mySlider;

    float startingPosition;
    float currentPosition;
    float normTime;

    private void Awake() 
    {
        mySlider = GetComponent<Slider>();
    }

    private void Start() 
    {
        startingPosition = mySlider.value;
        currentPosition = startingPosition;
        normTime = NormPos(startingPosition);
    }
    
    void Update()
    {
        float currentNormPos = NormPos(currentPosition);
        float cycles = NormPosCycle() / period;
        float rawSin = Mathf.Sin(cycles * 2f * Mathf.PI - Mathf.PI/2);

        float positiveSin = (rawSin + 1f) / 2f;

        float newValue = mySlider.maxValue * positiveSin;

        mySlider.value = newValue;
        currentPosition = newValue;
    }

    public void SetStartingPos(float startingPos)
    {
        startingPosition = startingPos;
    }

    public float GetCurrentPos()
    {
        return currentPosition;
    }


    private float NormPosCycle()
    {
        float newNormPos = normTime + Time.deltaTime;
        
        normTime = newNormPos;
        return newNormPos;
    }

    private float NormPos(float position)
    {
        return (position / mySlider.maxValue); 
    }


}
