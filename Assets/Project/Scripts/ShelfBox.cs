using UnityEngine;

public class ShelfBox : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material gazedAtMaterial;


    private Renderer _renderer;


    public Transform restTransform;
    public Pickable restPickable;

    public bool HasPickable() => restPickable != null;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetMaterial(false);

        restTransform.localScale = restTransform.lossyScale;
    }

    public void OnPointerEnter()
    {
        if (!CanBeClicked())
        {
            return;
        }

        SetMaterial(true);
        CircleLoader.Instance.Pick(gameObject);
    }


    public void OnPointerExit()
    {
        if (!CanBeClicked())
        {
            return;
        }

        SetMaterial(false);
        CircleLoader.Instance.ExitPick();
    }

    public void OnPointerClick()
    {

        if (Player.Instance.IsPicking())
        {
            Pickable pickedObject = Player.Instance.PickedObject;
            if (!HasPickable())
            {
                restPickable = pickedObject;
                Player.Instance.Detach(restTransform);
                pickedObject.pickTransform.rotation = restTransform.rotation;
                pickedObject.pickTransform.localPosition = restPickable.defaultPos;


                // pickedObject.pickTransform.SetPositionAndRotation(restTransform.position - restPickable.defaultPos, );
                pickedObject.enabled = false;
            }
        }
        else
        {
            if (HasPickable())
            {
                Pickable pickedObject = restPickable;
                Player.Instance.Attach(pickedObject);
                pickedObject.enabled = true;
                restPickable = null;
            }
        }


        CircleLoader.Instance.ExitPick();
    }

    private bool CanBeClicked()
    {
        return (Player.Instance.IsPicking() && !HasPickable()) || (!Player.Instance.IsPicking() && HasPickable());
    }


    private void SetMaterial(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            _renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        }
    }
}