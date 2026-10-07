using System.Collections.Generic;

namespace ArmasDestinoPS.Inventory
{
  public static class Items
  {
    #region Properties
      public static Dictionary<string, Item> _items = new Dictionary<string, Item>();
    #endregion

    #region Methods
      static Items()
      {
        _items.Add("IT0001", new Item(1, "Stone", true, "La piedra es utilizada para las herramientas basicas.", "stone"));
        _items.Add("IT0002", new Item(2, "Log", true, "Tronco de madera que sirve para crear tablas.", "log"));
        _items.Add("IT0003", new Item(3, "WoodenBoard", true, "Tabla de madera utilizada para diversos crafteos (Herramientas, construcciones, etc).", "woodenboard"));
        _items.Add("IT0004", new Item(4, "Branch", true, "Utilizadas para la confeccion de todas las herramientas basicas.", "branch"));
      }
    #endregion
  }
}
