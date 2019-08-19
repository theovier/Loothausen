
public interface IItemContainer {
    bool CanAddItem(Item item);
    bool AddItem(Item item);
    bool RemoveItem(Item item);
    void Clear();
}
