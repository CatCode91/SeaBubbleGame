using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour, IPointerClickHandler
    {
        //верхняя точка по Y где шарики уничтожаются
        private float _destroyPoint = 13;

        //событие, что на шарик нажали
        public UnityAction<Bubble> Burst;

        public float Size { get; private set; }
        public float Speed { get; private set; }
        public int Score { get; private set; }

        private Color32[] colors = { Color.red, Color.yellow, Color.blue, Color.cyan, Color.magenta, new Color(255,0,255), new Color(43,123,54), new Color(65,2,211), new Color(211,5,32) };

        private void Awake()
        {
            //задаем  размер, цвет, скорость и количество очков при тапе в зависимости от размера шарика
            Size = UnityEngine.Random.Range(0.7f, 7f);
            GetComponent<Renderer>().material.SetColor("Color_011e50e2c03647debbf40a27a5f516c4", colors[Convert.ToInt32(UnityEngine.Random.Range(0, colors.Length - 1))]);
            GetComponent<Transform>().localScale = new Vector3(Size, Size, Size);
            Speed = (500/Size);
            Score = Convert.ToInt32(20 / Size);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (Speed / 1000 ));
    
            if (transform.position.z > _destroyPoint) 
            {
                Destroy(gameObject);
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
            Speed += (coef * 3);
        }   
    }
}
