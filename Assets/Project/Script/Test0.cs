using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test {
    public string name;
    public int age;
}

public class Test0 : MonoBehaviour {
    [SerializeField] Test1 test1;
    Test info = new Test() { name = "test0", age = 0 };

    void Start() {
        test1.Test(info);
        print(info.name);
        print(info.age);
    }

    public void Test(Test test) {
        test.name = "¹Ù²ãºÎ·¶¾î";
        test.age = 9999;
    }
}
