public interface IShiftableCutoutList <in T> {
    void Add(T item);
    bool Remove(T item);
    bool CurrentlyWrapped(T item);
    int GetWrapperIndex(T item);
    void Clear();
    void OnNext();
    void OnPrevious();
}
