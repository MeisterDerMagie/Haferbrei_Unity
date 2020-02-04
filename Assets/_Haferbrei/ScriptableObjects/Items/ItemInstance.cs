using System;

namespace Haferbrei{
public class ItemInstance
{
    
    //basierend auf: http://toqoz.fyi/unity-painless-inventory.html
    
    
    public Guid item_id;
    public byte[] serialized_item_id;
    
    public Item item;
    public int durability;

    //eine neue ItemInstance erstellen
    public ItemInstance(Item _item, int _durability)
    {
        item = _item;
        durability = _durability;
        
        item_id = new Guid();
        serialized_item_id = item_id.ToByteArray();
    }
    
    //Constructor, um eine ItemInstance zu erstellen, die bereits eine ID besitzt, also z.B. wenn man einen Speicherstand lädt
    public ItemInstance(Item _item, int _durability, byte[] _serialized_item_id)
    {
        item = _item;
        durability = _durability;
        
        item_id = new Guid(_serialized_item_id);
        serialized_item_id = _serialized_item_id;
    }
}
}