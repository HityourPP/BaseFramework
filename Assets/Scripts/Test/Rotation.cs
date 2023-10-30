using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XLuaProject
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 50f;

        private void Update()
        {
            //自转
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
        }
    }
}
