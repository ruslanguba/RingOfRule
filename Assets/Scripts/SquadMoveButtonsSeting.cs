using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMoveButtonsSeting : MonoBehaviour
{
    [SerializeField] Transform forwardArrow;
    [SerializeField] Transform backArrow;
    [SerializeField] Transform rightArrow;
    [SerializeField] Transform leftArrow;
    [SerializeField] Transform turnLeftArrow;
    [SerializeField] Transform turnRightArrow;
    [SerializeField] Transform LeftPivot;
    [SerializeField] Transform RightPivot;

    [SerializeField] GameObject AddLinesButtons;
    [SerializeField] GameObject MoveButtons;

    public void SetArrowsPosition(float rows, float columns, Vector3 center)
    {
        Vector3 centerPosition = center;
        Quaternion rotation = transform.rotation;
        Vector3 forwardOffset = rotation * new Vector3(0f, 0.1f, columns / 2 + 1);
        Vector3 backOffset = rotation * new Vector3(0f, 0.1f, -columns / 2 - 1);
        Vector3 leftOffset = rotation * new Vector3(-rows / 2 - 1, 0.1f, 0);
        Vector3 rightOffset = rotation * new Vector3(rows / 2 + 1, 0.1f, 0);

        Vector3 forwardArrowPosition = centerPosition + forwardOffset;
        Vector3 backArrowPosition = centerPosition + backOffset;
        Vector3 leftArrowPosition = centerPosition + leftOffset;
        Vector3 rightArrowPosition = centerPosition + rightOffset;

        forwardArrow.position = forwardArrowPosition;
        backArrow.position = backArrowPosition;
        leftArrow.position = leftArrowPosition;
        rightArrow.position = rightArrowPosition;

        turnLeftArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(leftArrow.GetComponent<RectTransform>().anchoredPosition.x, forwardArrow.GetComponent<RectTransform>().anchoredPosition.y, forwardArrowPosition.z);
        turnRightArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(rightArrow.GetComponent<RectTransform>().anchoredPosition.x, forwardArrow.GetComponent<RectTransform>().anchoredPosition.y, forwardArrowPosition.z);

        Vector3 leftPivotPosition = centerPosition + rotation * new Vector3(-rows / 2, 0f, columns / 2);
        Vector3 rightPivotPosition = centerPosition + rotation * new Vector3(rows / 2, 0f, columns / 2);
        LeftPivot.position = leftPivotPosition;
        RightPivot.position = rightPivotPosition;
    }
}
