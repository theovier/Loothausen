using UnityEngine;

public abstract class CutoutListWrapper<T> : MonoBehaviour, IWrapper<T> {

    protected T content;
    
    public T GetContent() {
        return content;
    }

    public void SetContent(T content) {
        this.content = content;
        Refresh();
    }

    public bool HasContent(T contentCandidate) {
        return contentCandidate.Equals(content);
    }

    public bool IsEmpty() {
        return content == null;
    }

    public void Clear() {
        content = default(T);
        Disable();
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    protected abstract void Refresh();
}
