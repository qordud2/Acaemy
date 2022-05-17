using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEditorTest : MonoBehaviour
{
    //public int Number = 0;
    public List<GameObject> tilelist = new List<GameObject>();
    public Vector2 MapSize = Vector2.zero;
    int count = 0;
    public void CreateMap()
    {
        AllClear();
        for (int i = 0; i < MapSize.y; ++i)
        {
            for(int j = 0; j < MapSize.x; ++j)
            {
                Vector3 pos = new Vector3(i - MapSize.x / 2f, 0, j - MapSize.y / 2f);
                // parent Transform에 넣어주면 SetParent를 안해줘도 된다
                GameObject obj = Instantiate(Resources.Load("Tile"), pos, Quaternion.identity, transform) as GameObject;
                tilelist.Add(obj);
            }
        }
    }

    public void AllClear()
    {
        for(int i = 0; i < tilelist.Count;)
        {
            count++;
            
#if UNITY_EDITOR
            DestroyImmediate(tilelist[i]);
#else
            Destroy(tilelist[i]);
#endif
            tilelist.RemoveAt(i);
        }
    }

}
