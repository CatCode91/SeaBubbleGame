using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player instance = null;

        public List<int> Scores { get; private set; }

        public int LastScore => Scores.Last();

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            else
            {
                Destroy(this);
            }

            Initializator();
        }

        private void Initializator()
        {
            Scores = new List<int>();
        }

        public void AddScore(int rate) 
        {
            Scores.Add(rate);
        }
    }
}
