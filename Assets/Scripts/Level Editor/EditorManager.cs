using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
    [Header("Layer masks")]
    [SerializeField] LayerMask slotLayer;
    [SerializeField] LayerMask shapeLayer;

    [Header("Editor Shape Prefabs")]
    [SerializeField] GameObject transparentBlueShape;
    [SerializeField] GameObject transparentRedShape;
    [SerializeField] GameObject BlueShape;
    [SerializeField] GameObject RedShape;

    [Header("Transforms")]
    [SerializeField] Transform level;

    [Header("Buttons")]
    [SerializeField] Button blueButton;
    [SerializeField] Button redButton;
    [SerializeField] Button sprayModeButton;
    [SerializeField] Button updateBarsButton;

    [Header("Vars")]
    [SerializeField] float barLengthByMeter;
    [SerializeField] int bars;
    [SerializeField] int noteDiv;

    [Header("Utils")]
    [SerializeField] GameObject barPrefab;
    [SerializeField] GameObject divisionBarPrefab;
    [SerializeField] GameObject barNumTextPrefab;
    [SerializeField] Transform startingBarPosition;

    // ture = blue false = red
    bool SpawnBlueOrRed;
    bool sprayModeIsOn = true;
    float CurrentDivisionLength;

    Vector3 lineOriginPosition;

    GameObject transparentClone;

    private void OnEnable()
    {
        blueButton.onClick.AddListener(() =>
        {
            SpawnBlueOrRed = true;
        });

        redButton.onClick.AddListener(() =>
        {
            SpawnBlueOrRed = false;
        });

        sprayModeButton.onClick.AddListener(() =>
        {
            sprayModeIsOn = !sprayModeIsOn;

            if (sprayModeIsOn)
            {
                sprayModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spray mode ON";
            }
            else
            {
                sprayModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spray mode OFF";
            }
        });

        updateBarsButton.onClick.AddListener(() =>
        {

        });

    }

    private void Start()
    {
        CurrentDivisionLength = barLengthByMeter / 4;
        InitializeBars();
    }

    private void Update()
    {
        EditorLogic();
    }

    void InitializeBars()
    {

        lineOriginPosition = startingBarPosition.position;

        for (int i = 1; i < bars; i++)
        {
            float nextLineZPos = lineOriginPosition.z + (barLengthByMeter * i);
            GameObject bar = Instantiate(barPrefab, new Vector3(0, 4.71f, nextLineZPos), Quaternion.Euler(0, 0, 90));
            GameObject BarIndex = Instantiate(barNumTextPrefab, bar.transform.position - Vector3.right * 15, Quaternion.Euler(90, 0, 0));
            BarIndex.GetComponent<TextMeshPro>().text = i.ToString();

            float currentLine = bar.transform.position.z + CurrentDivisionLength;

            for (int y = 0; y < 3; y++)
            {
                GameObject divisionBar = Instantiate(divisionBarPrefab, new Vector3(0, 4.71f, currentLine), Quaternion.Euler(0, 0, 90));
                GameObject divisionIndex = Instantiate(barNumTextPrefab, divisionBar.transform.position - Vector3.right * 13, Quaternion.Euler(90,0,0));
                divisionIndex.GetComponent<TextMeshPro>().text = $"{i}.{y + 2}";
                currentLine += CurrentDivisionLength;

            }
        }
    }

    void UpdateBars()
    {

    }

    void EditorLogic()
    {
        // Geting the position of the finger on the screen.
        Vector3 mousePos = Input.mousePosition;
        Vector3 touchPosFar = new Vector3(mousePos.x, mousePos.y, Camera.main.farClipPlane);
        Vector3 touchPosNear = new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane);
        Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
        Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

        // Info about the hit object.
        RaycastHit hitSlot;
        RaycastHit hitShape;

        // Shooting a ray from the camera to the desired object.
        if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitSlot, 100, slotLayer))
        {

            if (transparentClone == null)
                transparentClone = Instantiate(SpawnBlueOrRed ? transparentBlueShape : transparentRedShape, hitSlot.transform.position, Quaternion.identity);

            if (sprayModeIsOn)
            {

                if (Input.GetMouseButton(0) && !Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
                {
                    Instantiate(SpawnBlueOrRed ? BlueShape : RedShape, hitSlot.transform.position, Quaternion.identity, level);
                }

                if (Input.GetMouseButton(1) && Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
                {
                    Destroy(hitShape.transform.gameObject);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
                {
                    Instantiate(SpawnBlueOrRed ? BlueShape : RedShape, hitSlot.transform.position, Quaternion.identity, level);
                }

                if (Input.GetMouseButtonDown(1) && Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
                {
                    Destroy(hitShape.transform.gameObject);
                }
            }

        }
        else
        {
            Destroy(transparentClone);
        }
    }
}
