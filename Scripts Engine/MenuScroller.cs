using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScroller : MonoBehaviour {
    public float maxXposition = -500f;

    bool holding;
    float yPositionScroll;
    float yScreen;
    float xScreen;
    float yPositionPercentageToScroll = 75; // only the bottom XX% of the screen can be touched to scroll
    float xDragPercentageToStartScroll = 2; // after the 3% of the widht of the screen it begin scrolls
    public float ElasticOffset = 100;
    RectTransform rectTransform;
    Vector3 currentMagnitudeVector;
    float currentMagnitude;
    public static MenuScroller current;
    public bool swiping;
    float minXPosition;
    public bool isElasticScroll = true;
    bool outOfBounds;
    float distanceBetweenElements;
    int elementsNumber;
  public  int nearestElement;
    public bool isOnItemElement;
    public bool stopAtMenuElement = true;
    private void Start()
    {
        if (!isElasticScroll) ElasticOffset = 0;
        rectTransform = GetComponent<RectTransform>();
        yScreen = Screen.height;
        xScreen = Screen.width;
        yPositionScroll = (yScreen / 100);
        yPositionScroll = yPositionScroll * yPositionPercentageToScroll;
        minXPosition = rectTransform.anchoredPosition.x;

        if (stopAtMenuElement && elementsNumber == 0) Debug.Log("Remember to use the void set scroll size if you want to stop at element");

    }
    private void OnEnable()
    {
        current = this;
    }

    void Update()
    {
        if (Mathf.Abs(rectTransform.anchoredPosition.x)==Mathf.Abs(nearestElement*distanceBetweenElements))
        { isOnItemElement = true; }
        else { isOnItemElement = false; }

        if (currentMagnitude != 0)
        {
            currentMagnitude = Mathf.Lerp(currentMagnitude, 0, Time.deltaTime * 10);
        }

        CheckScroll();

        CheckElasticOffset();

        CheckStopAtElement();

    }

    void CheckStopAtElement() {
        if (elementsNumber == 0 || !stopAtMenuElement) return;
        float x = rectTransform.anchoredPosition.x;
        nearestElement = Mathf.Abs(Mathf.RoundToInt(x / distanceBetweenElements));

     
    }
    public void SetScrollSize(float dist,int numb)
    {
        distanceBetweenElements = dist;
        elementsNumber = numb;
        maxXposition = -((dist * numb) - (dist * 1));
    }

    void CheckElasticOffset() {

        Vector3 pos = rectTransform.anchoredPosition;
   
        float elasticReturnPower = 5f;

        if (pos.x > minXPosition) {
            currentMagnitude = 0;
            currentMagnitudeVector = Vector3.zero;
            pos.x = Mathf.Lerp(pos.x, minXPosition-10, elasticReturnPower*Time.deltaTime);
            outOfBounds = true;
            if(!holding)
            rectTransform.anchoredPosition = pos;

            return;
        }

        if (pos.x < maxXposition)
        {
            currentMagnitude = 0;
            currentMagnitudeVector = Vector3.zero;
            pos.x = Mathf.Lerp(pos.x, maxXposition + 10, elasticReturnPower * Time.deltaTime);
            outOfBounds = true;
            if (!holding)
                rectTransform.anchoredPosition = pos;

            return;
        }
        outOfBounds = false;
    }


    void CheckScroll()
    {
      if (Input.GetMouseButtonDown(0))
        {
            swiping = false;
        }

        if (Input.GetMouseButton(0) && !holding && Input.mousePosition.y < yPositionScroll)
        {
            StopAllCoroutines();
            holding = true;
            StartCoroutine(holdingCheckRoutine());
        }
        if (Input.GetMouseButtonUp(0))
        {
            holding = false;
 

            StartCoroutine(continueScrollRelease());
        }

    }

    IEnumerator holdingCheckRoutine()
    {

        float startingPosition = Input.mousePosition.x;

        float percentageTreshold = (xScreen / 100);

        percentageTreshold = percentageTreshold * xDragPercentageToStartScroll;

        Vector3 pos = rectTransform.anchoredPosition;

        while (holding)
        {
            Vector3 beforeFrameMousePos = Input.mousePosition;
            float currentX = beforeFrameMousePos.x;
            float dragValue = Mathf.Abs(startingPosition - currentX);

            yield return new WaitForEndOfFrame();
            if (dragValue >= percentageTreshold)
            {
                swiping = true;

                dragValue = currentX - Input.mousePosition.x;

                pos.x -= dragValue / xScreen * 400;

                pos.x = Mathf.Clamp(pos.x, maxXposition-ElasticOffset, minXPosition + ElasticOffset);
                rectTransform.anchoredPosition = pos;
                currentMagnitudeVector = Input.mousePosition - beforeFrameMousePos;


                currentMagnitude += currentMagnitudeVector.x / xScreen * 50;


                if (currentMagnitude > 0 && currentMagnitudeVector.x / 15 < 0)
                {
                    currentMagnitude = 0;
                }
                if (currentMagnitude < 0 && currentMagnitudeVector.x / 10 > 0)
                {
                    currentMagnitude = 0;
                }


            }
        }



    }

    IEnumerator continueScrollRelease()
    {
        float scrollPower = 150;
        float elasticity = 5f;

        Vector3 currentAnchored = rectTransform.anchoredPosition;
        Vector3 targetAnchored = currentAnchored;


       
            targetAnchored.x += currentMagnitude * scrollPower;
  

        float lerper = 0;
        float elasticTime = elasticity;


        if (stopAtMenuElement)
        {
            elasticity = elasticity / 3 ;
            int mod = -1;
            if (currentMagnitude < 0) mod =+ 1;
            if (Mathf.Abs(currentMagnitude) < 1f) mod = 0;
            if (Mathf.Abs(currentMagnitude) > 5f) mod = mod*2;
            if (Mathf.Abs(currentMagnitude) > 15f) mod = mod * 2;
            targetAnchored.x = -(distanceBetweenElements * (nearestElement + mod));
        }

        currentMagnitude = 0;

        while (lerper <= 1)
        {
            if (outOfBounds) break;           

            yield return new WaitForEndOfFrame();
            lerper += Time.deltaTime / elasticity;
            elasticity += lerper;
            Vector3 anchorPos = rectTransform.anchoredPosition;

           

            anchorPos.x = Mathf.Lerp(currentAnchored.x, targetAnchored.x, (lerper * elasticTime));

            anchorPos.x = Mathf.Clamp(anchorPos.x, maxXposition, minXPosition);

            rectTransform.anchoredPosition = anchorPos;
        }

     
        yield return null;
    }
}
