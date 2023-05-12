using UnityEngine;
using UnityEngine.Serialization;

public class Pickable : MonoBehaviour
{

    public Material inactiveMaterial;
    public Material gazedAtMaterial;


    
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
        Debug.Log("Enter");
        SetMaterial(true);
        CircleLoader.Instance.Pick(gameObject);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        Debug.Log("Exit");
        SetMaterial(false);
        CircleLoader.Instance.ExitPick();
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        Debug.Log("Click");

        Vector3 colCenter = _collider.center;
        Vector3 colSize = _collider.size;
        Vector3 scale = transform.lossyScale;

        Vector3 center = new Vector3((colCenter.x * colSize.x) * scale.x, (colCenter.y * colSize.y) * scale.y, (colCenter.z * colSize.z) * scale.z);

        Player.Instance.Attach(transform, center);
        // transform.SetParent(Player.Instance.transform);
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