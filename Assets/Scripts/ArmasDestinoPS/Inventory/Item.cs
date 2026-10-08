using UnityEngine;

namespace ArmasDestinoPS.Inventory
{
  public class Item
  {
    #region Properties
      public int Id { get; set; }
      public string Name { get; set; }
      public bool IsStackable { get; set; }
      public string Description { get; set; }
      public Sprite Icon { get; set; }
    #endregion

    #region Methods
      public Item(int id, string name, bool isStackable, string description, Sprite icon)
      {
        Id = id;
        Name = name;
        IsStackable = isStackable;
        Description = description;
        Icon = icon;
      }
    #endregion
  }  
}
