﻿using Arcanoid.Game.PickUps;
using Arcanoid.Utility;
using UnityEngine;

namespace Arcanoid.Services
{
    public class PickUpService : SingletonMonoBehaviour<PickUpService>
    {
        [Range(0, 100)]
        [SerializeField] private int _pickUpSpawnProbability;
        [SerializeField] private PickUp _pickUpPrefab;

        public void SpawnPickUp(Vector3 position)
        {
            if (_pickUpPrefab == null)
            {
                return;
            }

            int random = Random.Range(0, 101);
            if (random > _pickUpSpawnProbability)
            {
                Instantiate(_pickUpPrefab, position, Quaternion.identity);
            }
        }
    }
}

