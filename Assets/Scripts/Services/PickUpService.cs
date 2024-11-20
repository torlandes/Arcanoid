using System;
using System.Collections.Generic;
using Arcanoid.Game.PickUps;
using Arcanoid.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Arcanoid.Services
{
    public class PickUpService : SingletonMonoBehaviour<PickUpService>
    {
        #region Variables

        [Header("Overall probability")]
        [Range(0, 100)]
        [SerializeField] private int _pickUpSpawnProbability;

        [Header("Prefabs list with probabilities")]
        [SerializeField] private List<PickUpAndProbability> _pickUpPrefabs;

        #endregion

        #region Unity lifecycle

        private void OnValidate()
        {
            foreach (PickUpAndProbability probability in _pickUpPrefabs)
            {
                probability.Validate();
            }
        }

        #endregion

        #region Public methods

        public void SpawnPickUp(Vector3 position)
        {
            if (_pickUpPrefabs.Count == 0)
            {
                return;
            }

            if (Random.Range(0f, 100f) > _pickUpSpawnProbability)
            {
                return;
            }

            Instantiate(GetRandomFromList(), position, Quaternion.identity);
        }

        #endregion

        #region Private methods

        private PickUp GetRandomFromList()
        {
            float sum = 0f;

            foreach (PickUpAndProbability p in _pickUpPrefabs)
            {
                sum += p.probability;
            }

            float cumulative = 0f;
            float randomValue = Random.Range(0f, sum);

            foreach (PickUpAndProbability pickup in _pickUpPrefabs)
            {
                cumulative += pickup.probability;
                if (randomValue < cumulative)
                {
                    return pickup.pickUpPrefab;
                }
            }

            return _pickUpPrefabs[0].pickUpPrefab;
        }

        #endregion

        #region Local data

        [Serializable]
        private class PickUpAndProbability
        {
            #region Variables

            [HideInInspector]
            public string Name;
            public PickUp pickUpPrefab;
            [FormerlySerializedAs("Probability")]
            [Header("relative probability, not actual percentage")]
            [Range(0f, 100f)]
            public float probability;

            #endregion

            #region Public methods

            public void Validate()
            {
                Name = pickUpPrefab != null ? pickUpPrefab.name : string.Empty;
            }

            #endregion
        }

        #endregion

        // [SerializeField] private PickUp _pickUpPrefab;

        // public void SpawnPickUp(Vector3 position)
        // {
        //     if (_pickUpPrefab == null)
        //     {
        //         return;
        //     }
        //
        //     int random = Random.Range(0, 101);
        //     if (random > _pickUpSpawnProbability)
        //     {
        //         Instantiate(_pickUpPrefab, position, Quaternion.identity);
        //     }
        // }
    }
}