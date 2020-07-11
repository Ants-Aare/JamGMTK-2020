using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InteractableUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI textField = null;
    private Camera cam;
    private Interactable activeInteractable = null;
    private Transform target = null;

    void Start()
    {
        cam = Camera.main;
        SetActiveInteractable(null);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);

            // if(transform.position != screenPos)
            screenPos.z = 0f;
            transform.position = screenPos;
        }
    }

    public void SetActiveInteractable(Interactable newInteractable)
    {
        if (newInteractable == null)
        {
            activeInteractable = null;
            StopAllCoroutines();
            gameObject.SetActive(false);
            return;
        }
        else
        {
            // turn stuff back on, after it was disabled
            if (activeInteractable == null)
            {
                gameObject.SetActive(true);
            }
            textField.text = $"X to {newInteractable.actionName}";
            target = newInteractable.UITarget == null ? newInteractable.transform : newInteractable.UITarget;
            activeInteractable = newInteractable;
        }
    }
}