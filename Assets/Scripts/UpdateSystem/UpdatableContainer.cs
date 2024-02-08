
using System.Collections.Generic;

namespace StreetRacing
{
    public class UpdatableContainer
    {
        private List<IUpdatable> objectsToAddToUpdate = new List<IUpdatable>();
        private List<IUpdatable> objectsToUpdate = new List<IUpdatable>();
        private List<IUpdatable> objectsToRemoveFromUpdate = new List<IUpdatable>();

        public void Add(IUpdatable @object)
        {
            objectsToAddToUpdate.Add(@object);
        }

        public void Remove(IUpdatable @object)
        {
            objectsToRemoveFromUpdate.Add(@object);
        }

        public void Update(float deltaTime)
        {
            UpdateMainContainer();

            foreach (var @object in objectsToUpdate)
            {
                @object.UpdateObject(deltaTime);
            }
        }

        private void UpdateMainContainer()
        {
            objectsToUpdate.AddRange(objectsToAddToUpdate);
            objectsToAddToUpdate.Clear();

            foreach (var @object in objectsToRemoveFromUpdate)
            {
                objectsToUpdate.Remove(@object);
            }

            objectsToRemoveFromUpdate.Clear();
        }
    }
}
