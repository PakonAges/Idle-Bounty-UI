using UnityEngine;

public class MapTouchHandler : MonoBehaviour
{
    public GameObject LevelSelectVIewPrefab;
    private GameObject LevelSelectViewInstance;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f);

            if (hit.transform.tag == "MapGrid")
            {
                if (LevelSelectViewInstance)
                {
                    GameObject.Destroy(LevelSelectViewInstance);
                }
                else
                {
                    LevelSelectViewInstance = Instantiate(LevelSelectVIewPrefab, this.transform);
                }
            }
        }
    }
}
