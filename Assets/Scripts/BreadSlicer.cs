using UnityEngine;
using UnityEngine.UI;

public class BreadCutter : MonoBehaviour
{
    public GameObject breadTopPrefab;
    public GameObject breadBottomPrefab;
    public GameObject cuttingLine;
    public GameObject cuttingLineInstance;
    public float tolerance = 0.25f;
    public float movingLineSpeed = 2f;
    public bool hasCut = false;
    public Text feedbackText;

    void Start()
    {
        cuttingLineInstance = Instantiate(cuttingLine, new Vector3(0, 0, 0), Quaternion.identity);

        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (cuttingLineInstance != null && !hasCut)
        {
            MoveCuttingLine();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryCutBread();
        }
    }

    void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            StartCoroutine(HideFeedback());
        }
    }

    System.Collections.IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2f);
        feedbackText.gameObject.SetActive(false);
    }

    void MoveCuttingLine()
    {
        float newY = Mathf.Sin(Time.time * movingLineSpeed) * 4f;
        cuttingLineInstance.transform.position = new Vector3(0, newY, 0);
    }

    void TryCutBread()
    {
        Debug.Log($"Position: {cuttingLineInstance.transform.position.y.ToString()}");
        float cutY = cuttingLineInstance.transform.position.y;
        Debug.Log($"Cutting Line Y: {cutY}");
        Vector3 breadPos = transform.position;

        float breadHeight = breadTopPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        float middleY = breadPos.y;

        if (cutY >= middleY - tolerance && cutY <= middleY + tolerance)
        {
            CutBread(cutY, breadPos, breadHeight);
        }
        else
        {
            ShowFeedback("Muito distante do ponto!");
        }
    }

    void CutBread(float cutY, Vector3 breadPos, float breadHeight)
    {
        Debug.Log($"Cutting Bread at Y: {cutY}, Bread position: {breadPos}");

        GameObject topHalf = Instantiate(breadTopPrefab, breadPos, Quaternion.identity);
        GameObject bottomHalf = Instantiate(breadBottomPrefab, breadPos, Quaternion.identity);

        topHalf.transform.position = new Vector3(breadPos.x, cutY + breadHeight / 2 + 0.25f, breadPos.z);

        bottomHalf.transform.position = new Vector3(breadPos.x, cutY - breadHeight / 2 - 0.25f, breadPos.z);

        topHalf.GetComponent<SpriteMask>().transform.position = new Vector3(breadPos.x, cutY + breadHeight / 2 + 0.25f, breadPos.z);
        bottomHalf.GetComponent<SpriteMask>().transform.position = new Vector3(breadPos.x, cutY - breadHeight / 2 - 0.25f, breadPos.z);

        AdjustCollider(topHalf, cutY, true);
        AdjustCollider(bottomHalf, cutY, false);

        Destroy(gameObject);

        hasCut = true;
    }

    void AdjustCollider(GameObject breadHalf, float cutY, bool isTop)
    {
        PolygonCollider2D collider = breadHalf.GetComponent<PolygonCollider2D>();
        Vector2[] points = collider.points;

        Debug.Log($"Collider points before adjustment for {(isTop ? "Top" : "Bottom")} Half: {points.Length} points");

        for (int i = 0; i < points.Length; i++)
        {
            if (isTop && points[i].y < cutY)
            {
                points[i].y = cutY;
            }
            if (!isTop && points[i].y > cutY)
            {
                points[i].y = cutY;
            }
        }

        Debug.Log($"Collider points after adjustment for {(isTop ? "Top" : "Bottom")} Half: {points.Length} points");

        collider.points = points;
    }
}
