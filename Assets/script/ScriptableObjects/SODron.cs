using UnityEngine;

namespace YouCanBeatAll.ScriptableObjects
{

    [CreateAssetMenu(fileName = "SODron_", menuName = "ScriptableObjects/Dron", order = 1)]
    public class SODron : ScriptableObject
    {
        public int vida = 1;
        public int puntos = 5;
        public int timeBetweenShots = 3;
        public float proyectileSpeed = 5f;
        public TipoDeBala tipoDeBala = TipoDeBala.Indiferente;
    }

    public enum TipoDeBala
    {
        Indiferente = 0,
        Blanca = 1,
        Negra = 2
    }
}
