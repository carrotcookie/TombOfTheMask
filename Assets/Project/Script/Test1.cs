using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {
    [SerializeField] Test0 test0;
    Test info = new Test() { name = "test1", age = 1 };

    void Start() {

    }

    public void Test(Test test) {
        test.name = "¹Ù²ãºÎ·¶¾î";
        test.age = 9999;
    }
}
