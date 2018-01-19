using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilding : MonoBehaviour
{
    //public vabiables
    public float offSet;
    public float range;
    public GameObject block;
    public Transform camTransform;
    public Transform ghostBlock;

    //private variables
    private bool buildMode;

    void Update()
    {
        CreateBlock();
    }

    private bool CreateGhostBlock()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!buildMode)
                buildMode = true;
            else
                buildMode = false;
        }

        if (buildMode)
        {
            ghostBlock.gameObject.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, range))
            {
                Vector3 position = hit.point + hit.normal * offSet;
                Vector3 CorrectedPosition = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
                ghostBlock.position = CorrectedPosition;
                return true;
            }
            else
            {
                ghostBlock.position = camTransform.position + camTransform.forward * 5;
                return false;
            }
        }
        else
        {
            ghostBlock.gameObject.SetActive(false);
            return false;
        }
    }

    private void CreateBlock()
    {
        if (CreateGhostBlock() && Input.GetMouseButtonDown(0))
        {
            Instantiate(block, ghostBlock.position, ghostBlock.rotation, null);
        }
    }
}
