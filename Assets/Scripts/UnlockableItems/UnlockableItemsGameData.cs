
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacing.UnlockableItems
{
    [Serializable]
    public class UnlockableItemsGameData
    {
        [SerializeField] private int selectedID;
        [SerializeField] private List<int> receivedIDs = new List<int>() { 1 };

        public int SelectedID => selectedID;

        public void Receive(int id)
        {
            if (receivedIDs.Contains(id))
            {
                return;
            }

            receivedIDs.Add(id);
        }

        public bool TrySelect(int id)
        {
            if (!receivedIDs.Contains(id))
            {
                return false;
            }

            selectedID = id;

            return true;
        }

        public bool IsReceived(int id)
        {
            return receivedIDs.Contains(id);
        }
    }
}
