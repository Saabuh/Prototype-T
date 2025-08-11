namespace Prototype_S
{
    /// <summary>
    /// Class representing the dyanmic data of a runtime item in the world.
    /// </summary>
    [System.Serializable]
    public class ItemInstance
    {
        //base, static item data the instance is based off of 
        public ItemData itemData;
        // ... other dynamic data (durability, upgrades)

        public ItemInstance(ItemData itemData)
        {
            this.itemData = itemData;
        }

        /// <summary>
        /// calculate dynamic damage data based on itemData and runtime factors (buffs, upgrades)
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void CalculateDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}

