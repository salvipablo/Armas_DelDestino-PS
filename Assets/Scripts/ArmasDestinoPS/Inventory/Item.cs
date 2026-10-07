namespace ArmasDestinoPS.Inventory
{
  public class Item
  {
    #region Properties
      public int Id { get; set; }
      public string Name { get; set; }
      public bool IsStackable { get; set; }
      public string Description { get; set; }
      public string Icon { get; set; }
    #endregion

    #region Methods
      public Item(string name) { this.Name = name; }

      public Item(int id, string name, bool isStackable, string description, string icon)
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
