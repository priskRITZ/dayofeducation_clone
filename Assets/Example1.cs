using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Army
{
    virtual public void Fire()
    {

    }
}

public class Firebat : Army
{
    public override void Fire()
    {
        base.Fire();

        // ���̾�� ���̾� ����� �����ϰ�
    }
}

public class Marin : Army
{
    public override void Fire()
    {
        base.Fire();

        // ������ ���̾� ����� �����Ѵ�.
    }
}

// ������
// abstract, interface�� ��üȭ �ɼ� ����.

// ������
// abstract�� interface���̴�
// abstract ��� ������ ������ ������
// interface�� ��� ������ ������ ����.

public abstract class Machine
{
    public int a = 10;

    public void Run()
    {

    }   
}

public interface Car
{
    // ���������ڷν� ����, void �Լ����� 
    // ���ϰ��̶�°� ������ �� �ִµ�
    // void��°��� ������ �ϴ� �Լ��� �ƴ��� �ǹ��ؿ�
    public void Run();
}

struct ExampleStruct
{
    public string name;
    public string description;
}

// �ؾ Ʋ�� ����.
public class BMW : Car
{
    public string Name;
    public int Age;

    public void Run()       
    {
        Debug.Log("BMW");
    }
}



public class Example1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1byte = 8bit
        // 0 ~ 1���� �����Ҽ� �ִµ�
        // bit�� 0 �Ǵ� 1�� ǥ���Ҽ� �ִ� �ּ� ������. 2����
        // 8bit == 00000000 (2����);
        // 0 == 00000000
        // 1 == 00000001 == 1 * 2^0 == 1
        // 2 == 00000010 == 1 * 2^1 == 2
        // 3 == 00000011 == 1 * 2^1 + 1 * 2^0 == 3
        // 4 == 00000100 == 1 * 2^2 + 0 * 2^1 + 0 * 2^0 == 4

        // 10������ 2������ ��ȯ�ϴ� ����� ���μ�����

        // 2^0 == 1;
        // 2^1 == 2;
        // 2^2 == 4;

        // = ���Կ����� == �񱳿�����

        // C#���� �����ϴ� �⺻ �������� ���� �˾ƺ��Կ�
        // int ������ ��Ÿ���� �ڷ����̴�.; 32bit ������ ������ ������. -2,147,483,648  ~ 2,147,483,647
        // int64 ������ ��Ÿ���� �ڷ����̴�.;
        // -9,223,372,036,854,775,808 ~ 9,223,372,036,854,775,807
        // float �ε��Ҽ����� ��Ÿ���� �ڷ����̴�.  32bit ������ ������ ������. 7�ڸ�;
        // double �ε��Ҽ����� ��Ÿ���� �ڷ����̴�. 64bit ������ ������ ������. 15�ڸ�;
        // bool �� �ƴϸ� ������ ���³��� �ڷ����̴�.;
        // string ���ڿ��� ��Ÿ���� �ڷ����̴�.

        // ������ �ȴ�.
        int a = 10;
        int b = -10;

        // ��û ū �������� �ȴ�.
        Int64 c = 100000000000000;

        // f�� ���� �����°��� �� ������ float������ �Ͻ��Ѵ�.
        float d = 1.0000001f;
        float e = 1.00000001f;

        float k = 1.00000000001f;

        // ������ �Ҽ��� 15�ڸ����� ���
        // �� ���� ���ڴ� �ݿø��Ѵ�.
        double f = 1.000000000123454789;
        double g = 1.000000000000000000000001;

        // �ε��Ҽ���, ū �ε��Ҽ���, ����

        // �� �۾����� ���信 �����Ѵ�.
        // ���� / ���� == ����
        // �ε��Ҽ� / ���� == �ε��Ҽ�
        // ���� / �ε��Ҽ� == �ε��Ҽ�

        float z = 10 / 15;
        Debug.Log(z); // ���ġ 0

        float u = 10 / 15.0f;
        Debug.Log(u); // ���ġ 0.6666667

        Debug.Log(f);

        transform.position = Vector3.zero;
        transform.position = new Vector3(0, 0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
