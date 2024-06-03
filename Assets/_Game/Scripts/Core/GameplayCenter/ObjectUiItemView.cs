using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Core.LevelSelection;

namespace GameplayCenter
{
    public class ObjectUiItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txt_count;

        public int counter;

        public void Setup(int count)
        {
            counter = count;
            txt_count.text = "" + count;


        }

        public void ReduceCount()
        {

        }
    }
}