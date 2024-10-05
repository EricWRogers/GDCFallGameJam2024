using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardLookAt : MonoBehaviour
{
    public Vector2 f;
    public float d = 0.0f;
    public int index = 0;
    public List<SpriteInfo> spriteInfos;
    private Transform m_player;
    private Renderer m_rend;
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_player);
        transform.forward = transform.forward * -1.0f;

        f = transform.parent.forward;

        d = Vector3.Dot(transform.parent.forward, m_player.forward);

        bool right = Vector3.Dot(transform.parent.right, m_player.forward) >= 0.0f;

        for(int i = 0; i < spriteInfos.Count; i++)
        {
            if (spriteInfos[i].min <= d && spriteInfos[i].max >= d)
            {
                index = i;
                m_rend.material.mainTexture = spriteInfos[i].texture;

                m_rend.material.mainTextureScale = new Vector2 ((right) ? 1.0f : -1.0f, 1.0f);
                return;
            }
        }
    }
}

[System.Serializable]
public class SpriteInfo
{
    public float min;
    public float max;
    public Texture texture;
}