using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetSortOrder();
    }

    private void SetSortOrder()
    {
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        spriteRender.sortingOrder = Mathf.FloorToInt(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
