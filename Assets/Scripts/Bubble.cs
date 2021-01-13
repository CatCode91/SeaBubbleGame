using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour, IPointerClickHandler
    {
        private float _minBubbleScale = 1.7f;
        private float _maxBubbleScale = 7f;

        //верхняя точка по Y где шарики уничтожаются
        private float _destroyPoint = 13;

        [SerializeField]
        private Color32[] colors;

        public bool IsMoving { get; set; }

        public int ScoreCoeff = 20;

        public float BaseSpeed = 0.7f;
        public float Size { get; private set; }
        public int Score { get; private set; }
        //событие, что на шарик нажали
        public UnityAction<Bubble> Burst;

        private void Awake()
        {
            IsMoving = false;
            //задаем  размер, цвет, скорость и количество очков при тапе в зависимости от размера шарика
            Size = UnityEngine.Random.Range(_minBubbleScale, _maxBubbleScale);
            GetComponent<Transform>().localScale = new Vector3(Size, Size, Size);

            GetComponent<Renderer>().material.SetColor("Color_011e50e2c03647debbf40a27a5f516c4", colors[Convert.ToInt32(UnityEngine.Random.Range(0, colors.Length))]);
            Score = Convert.ToInt32(ScoreCoeff / Size);
        }

        private void FixedUpdate()
        {
            if (IsMoving) 
            {
                transform.Translate(Vector3.forward * (BaseSpeed / Size));

                if (transform.position.z > _destroyPoint)
                {
                    Destroy(gameObject);
                }
            }       
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Burst?.Invoke(this);
        }

        public void OnMouseDown()
        {
            Burst?.Invoke(this);
        }

        public void MakeFaster(float coef) 
        {
            BaseSpeed += coef;
        }   
    }
}
