using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffectScript : MonoBehaviour
{
    // �� ������Ʈ�� ����� �� ��Ÿ�� ȿ���̴�.
    public GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        // �� ������Ʈ�� �ִ� ���� HitEffect ���� ������Ʈ�� �����Ѵ�.
        GameObject.Instantiate(HitEffect, this.transform.position, Quaternion.identity);
        // �� ������Ʈ�� �����Ѵ�.
        GameObject.Destroy(this.gameObject);
    }
}
