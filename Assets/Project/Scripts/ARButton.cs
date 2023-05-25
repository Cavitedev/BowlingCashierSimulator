using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ARButton : MonoBehaviour
{

    private Button _button;

    private void Start()
    {
        _button = GetComponentInParent<Button>();
        _button.Select();
    }


    public void OnPointerEnter()
    {
        Debug.Log("Enter");
        _button.Select();
        CircleLoader.Instance.Pick(gameObject);
    }


    public void OnPointerExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        CircleLoader.Instance.ExitPick();
    }

    public void OnPointerClick()
    {
        Debug.Log("Click");
        CircleLoader.Instance.ExitPick();
        _button.onClick.Invoke();
    }

    
}
