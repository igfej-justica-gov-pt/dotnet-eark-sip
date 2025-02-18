public class Tree<T> {
  public T Root { get; private set; }

  public Tree<T>? Parent { get; private set; }

  public List<Tree<T>> Children { get; private set; }

  public Tree(T root) {
    Root = root;
    Children = new List<Tree<T>>();
  }

  public bool IsRoot() {
    return Parent == null;
  }

  public bool IsLeaf() {
    return Children.Count == 0;
  }

  public int GetLevel() {
    return Parent == null ? 0 : (Parent.GetLevel() + 1);
  }

  public bool ChildExists(T wantedChild, T parentNode) {
    if (Root == null || !Root.Equals(parentNode)) {
      return false;
    }

    bool exists = false;
    foreach (Tree<T> child in Children) {
      if (child.Root != null && child.Root.Equals(wantedChild)) {
        exists = true;
        break;
      }
    }

    return exists;
  }

  public Tree<T>? GetChild(T wantedChild, T parentNode) {
    if (Root == null || !Root.Equals(parentNode)) {
      return null;
    }

    foreach (Tree<T> child in Children) {
      if (child.Root != null && child.Root.Equals(wantedChild)) {
        return child;
      }
    }

    return null;
  }

  public Tree<T> AddChild(T child, T parentNode) {
    Tree<T> childTree;

    if (!ChildExists(child, parentNode)) {
      childTree = new Tree<T>(child) { Parent = this };
      Children.Add(childTree);
    } else {
      childTree = GetChild(child, parentNode);
    }

    return childTree;
  }
}