
public interface IWrapper <T> {
    T GetContent();
    void SetContent(T content);
    bool HasContent(T contentCandidate);
    bool IsEmpty();
    void Clear();
    void Enable();
    void Disable();
}
