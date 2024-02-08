
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacing
{
    public class UpdateSystem : MonoBehaviour
    {
        private const string UpdateSystemName = "UpdateSystem";

        public static UpdateSystem Instance;

        private Dictionary<UpdateLayer, UpdatableContainer> updatableContainers;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            updatableContainers = new Dictionary<UpdateLayer, UpdatableContainer>();

            var layers = Enum.GetValues(typeof(UpdateLayer));
            foreach (var layer in layers)
            {
                updatableContainers.Add((UpdateLayer)layer, new UpdatableContainer());
            }

            StartCoroutine(Coroutine());
        }

        public static void AddToUpdate(IUpdatable @object, UpdateLayer updateLayer)
        {
            if (Instance == null)
            {
                Initialize();
            }

            Instance.updatableContainers[updateLayer].Add(@object);
        }

        public static void RemoveFromUpdate(IUpdatable @object, UpdateLayer updateLayer)
        {
            if (Instance == null)
            {
                Initialize();
            }

            Instance.updatableContainers[updateLayer].Remove(@object);
        }

        private static void Initialize()
        {
            if (Global.IsApplicationQuitting)
            {
                return;
            }

            var gameObject = new GameObject(UpdateSystemName);
            gameObject.AddComponent<UpdateSystem>();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            UpdateContainer(UpdateLayer.Update, deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;

            UpdateContainer(UpdateLayer.FixedUpdate, deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;

            UpdateContainer(UpdateLayer.LateUpdate, deltaTime);
        }

        private IEnumerator Coroutine()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                var deltaTime = Time.deltaTime;
                UpdateContainer(UpdateLayer.EndOfFrame, deltaTime);
            }
        }

        private void UpdateContainer(UpdateLayer updateLayer, float deltaTime)
        {
            Instance.updatableContainers[updateLayer].Update(deltaTime);
        }
    }
}
