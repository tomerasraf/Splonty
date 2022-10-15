using System.Collections;
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

    [Header("Parents Objects")]
    [SerializeField] GameObject barLines;
    [SerializeField] GameObject text3D;

    [Header("Transforms")]
    [SerializeField] Transform level;

    [Header("Buttons")]
    [SerializeField] Button blueButton;
    [SerializeField] Button redButton;
    [SerializeField] Button sprayModeButton;
    [SerializeField] TMP_Dropdown timeSigDropdown;
    [SerializeField] TMP_Dropdown timeDivisionDropdown;

    [Header("Vars")]
    float barLengthByMeter;
    int timeSig;
    int timeDivision;
    float beatCopy;
    [SerializeField] float beatLengthByMeter;
    [SerializeField] int bars;

    [Header("Utils")]
    [SerializeField] GameObject barPrefab;
    [SerializeField] GameObject divisionBarPrefab;
    [SerializeField] GameObject barNumTextPrefab;
    [SerializeField] Transform startingBarPosition;

    [HideInInspector] public bool onSlot;

    // ture = blue false = red
    bool SpawnBlueOrRed;
    bool sprayModeIsOn = true;

    Vector3 lineOriginPosition;
    GameObject transparentClone;

    private void OnEnable()
    {
        timeDivisionDropdown.onValueChanged.AddListener(delegate
        {
            CleanGrid();
            InitializeTimeDivision();
            InitializeBars();
        });

        timeSigDropdown.onValueChanged.AddListener(delegate
        {
            CleanGrid();
            InitializeTimeSig();
            InitializeBars();

        });

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
            sprayModeButton.GetComponentInChildren<TextMeshProUGUI>().text = sprayModeIsOn ? "Spray mode ON" : "Spray Mode OFF";
        });
    }

    private void Start()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, startingBarPosition.position.z + 40);
        beatCopy = beatLengthByMeter;
        InitializeTimeDivision();
        InitializeTimeSig();
        InitializeBars();
    }

    private void Update()
    {
        EditorInputControl();
    }

    private void CleanGrid()
    {
        foreach (Transform child in barLines.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in text3D.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InitializeTimeSig()
    {
        string valueText = timeSigDropdown.options[timeSigDropdown.value].text;
        timeSig = int.Parse(valueText);
        barLengthByMeter = beatLengthByMeter * timeSig;
    }
    void InitializeTimeDivision()
    {
        string valueText = timeDivisionDropdown.options[timeDivisionDropdown.value].text;
        timeDivision = int.Parse(valueText) / 4;
        beatCopy = beatLengthByMeter / timeDivision;
    }

    void InitializeBars()
    {
        lineOriginPosition = startingBarPosition.position;

        for (int i = 0; i < bars; i++)
        {
            float nextLineZPos = lineOriginPosition.z + (barLengthByMeter * i);
            GameObject bar = Instantiate(barPrefab, new Vector3(0, 4.71f, nextLineZPos), Quaternion.Euler(0, 0, 90), barLines.transform);
            GameObject BarIndex = Instantiate(barNumTextPrefab, bar.transform.position - Vector3.right * 15, Quaternion.Euler(90, 0, 0), text3D.transform);
            BarIndex.transform.localScale = new Vector3(2, 2, 2);
            int temp = i + 1;

            BarIndex.GetComponent<TextMeshPro>().text = temp.ToString();

            float currentLine = bar.transform.position.z + beatCopy;

            for (int y = 0; y < (timeSig * timeDivision) - 1; y++)
            {
                GameObject divisionBar = Instantiate(divisionBarPrefab, new Vector3(0, 4.71f, currentLine), Quaternion.Euler(0, 0, 90), barLines.transform);
                GameObject divisionIndex = Instantiate(barNumTextPrefab, divisionBar.transform.position - Vector3.right * 15, Quaternion.Euler(90, 0, 0), barLines.transform);
                divisionIndex.GetComponent<TextMeshPro>().text = $"{temp}.{y + 2}";
                currentLine += beatCopy;
            }
        }
    }

    void EditorInputControl()
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
            onSlot = true;

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
            StartCoroutine(DealyDrag());
        }

        bool leftShiftHeld = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetMouseButton(1) && Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer) && leftShiftHeld)
        {
            Destroy(hitShape.transform.gameObject);
        }
    }

    IEnumerator DealyDrag()
    {
        yield return new WaitForSeconds(0.3f);

        onSlot = false;

        yield return null;
    }
}
