using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Utils
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float xSpeed = 0;
        [SerializeField] private float ySpeed = 0;
        [SerializeField] private float zSpeed = 0;
        private Transform transformCashed;

        void Awake()
        {
            transformCashed = transform;
        }

        void Update()
        {
            transformCashed.eulerAngles += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;
        }
    }
}