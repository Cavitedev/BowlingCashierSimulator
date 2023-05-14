using UnityEngine;

public class Pickable : MonoBehaviour
{

    public Material inactiveMaterial;
    public Material gazedAtMaterial;
    public Transform pickTransform;
    
    
    private Renderer _renderer;
    private BoxCollider _collider;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        SetMaterial(false);
    }

    public void OnPointerEnter()
    {
        if (Player.Instance.IsPicking(this))
        {
            return;
        }
        Debug.Log("Enter");
        SetMaterial(true);
        CircleLoader.Instance.Pick(gameObject);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        if (Player.Instance.IsPicking(this))
        {
            return;
        }
        
        // Debug.Log("Exit");
        SetMaterial(false);
        CircleLoader.Instance.ExitPick();
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        if (Player.Instance.IsPicking(this))
        {
            return;
        }
        CircleLoader.Instance.ExitPick();
        // Debug.Log("Click");
        Player.Instance.Attach(this);
        _collider.enabled = false;
        // transform.SetParent(Player.Instance.transform);
    }

    public void OnAttach()
    {
        _collider.enabled = false;
    }

    public void OnDetach()
    {
        _collider.enabled = true;

    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            _renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        }
    }
}