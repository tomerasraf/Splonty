using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

    [Header("Buttons")]
    [SerializeField] Button blueButton;
    [SerializeField] Button redButton;

    [Header("Vars")]
    [SerializeField] float barLengthByMeter;
    [SerializeField] int bars;

    [Header("Utils")]
    [SerializeField] GameObject barPrefab;
    [SerializeField] Transform startingBarPosition;

    // ture = blue false = red
    bool SpawnBlueOrRed;
    bool isMouseOnSlot;
    bool isMouseOnShape;

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
    }

    private void Start()
    {
        InitializeBars();
    }

    private void Update()
    {
        EditorLogic();
    }

    void InitializeBars() {

        lineOriginPosition = startingBarPosition.position;
       
        for (int i = 0; i < bars; i++)
        {
            float nextLineZPos = lineOriginPosition.z + (barLengthByMeter * i);
            Instantiate(barPrefab, new Vector3(0,4.71f, nextLineZPos),Quaternion.Euler(0,0,90));
        }
        
    }

    private void EditorLogic()
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

        // Shooting a ray from the camera to the desire player.
        if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitSlot, 100, slotLayer))
        {

            if (transparentClone == null)
                transparentClone = Instantiate(SpawnBlueOrRed ? transparentBlueShape : transparentRedShape, hitSlot.transform.position, Quaternion.identity);

            if (Input.GetMouseButtonDown(0) && !Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
            {
                Instantiate(SpawnBlueOrRed ? BlueShape : RedShape, hitSlot.transform.position, Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(1) && Physics.Raycast(touchPosN, touchPosF - touchPosN, out hitShape, 100, shapeLayer))
            {
                Destroy(hitShape.transform.gameObject);
            }
        }
        else
        {
            Destroy(transparentClone);
        }
    }
}
