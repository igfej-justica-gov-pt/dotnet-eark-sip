/// <summary>
/// Represents a generic tree structure with a root, parent, and children nodes.
/// </summary>
/// <typeparam name="T">The type of the value stored in the tree nodes.</typeparam>
public class Tree<T>
{
    /// <summary>
    /// Gets the root value of the tree node.
    /// </summary>
    public T Root { get; private set; }

    /// <summary>
    /// Gets the parent node of the current tree node, or null if it is the root.
    /// </summary>
    public Tree<T>? Parent { get; private set; }

    /// <summary>
    /// Gets the list of child nodes of the current tree node.
    /// </summary>
    public List<Tree<T>> Children { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tree{T}"/> class with the specified root value.
    /// </summary>
    /// <param name="root">The value to set as the root of the tree.</param>
    public Tree(T root)
    {
        Root = root;
        Children = new List<Tree<T>>();
    }

    /// <summary>
    /// Determines whether the current tree node is the root node.
    /// </summary>
    /// <returns>True if the current node is the root; otherwise, false.</returns>
    public bool IsRoot()
    {
        return Parent == null;
    }

    /// <summary>
    /// Determines whether the current tree node is a leaf node (has no children).
    /// </summary>
    /// <returns>True if the current node is a leaf; otherwise, false.</returns>
    public bool IsLeaf()
    {
        return Children.Count == 0;
    }

    /// <summary>
    /// Gets the level of the current tree node in the tree hierarchy.
    /// </summary>
    /// <returns>The level of the current node, where the root node is at level 0.</returns>
    public int GetLevel()
    {
        return Parent == null ? 0 : (Parent.GetLevel() + 1);
    }

    /// <summary>
    /// Determines whether a child node with the specified value exists under the given parent node.
    /// </summary>
    /// <param name="wantedChild">The value of the child node to search for.</param>
    /// <param name="parentNode">The value of the parent node under which to search for the child.</param>
    /// <returns>True if the child node exists; otherwise, false.</returns>
    public bool ChildExists(T wantedChild, T parentNode)
    {
        if (Root == null || !Root.Equals(parentNode))
        {
            return false;
        }

        bool exists = false;
        foreach (Tree<T> child in Children)
        {
            if (child.Root != null && child.Root.Equals(wantedChild))
            {
                exists = true;
                break;
            }
        }

        return exists;
    }

    /// <summary>
    /// Retrieves the child node with the specified value under the given parent node.
    /// </summary>
    /// <param name="wantedChild">The value of the child node to retrieve.</param>
    /// <param name="parentNode">The value of the parent node under which to search for the child.</param>
    /// <returns>The child node if found; otherwise, null.</returns>
    public Tree<T>? GetChild(T wantedChild, T parentNode)
    {
        if (Root == null || !Root.Equals(parentNode))
        {
            return null;
        }

        foreach (Tree<T> child in Children)
        {
            if (child.Root != null && child.Root.Equals(wantedChild))
            {
                return child;
            }
        }

        return null;
    }

    /// <summary>
    /// Adds a child node with the specified value under the given parent node.
    /// </summary>
    /// <param name="child">The value of the child node to add.</param>
    /// <param name="parentNode">The value of the parent node under which to add the child.</param>
    /// <returns>The newly added child node, or the existing child node if it already exists.</returns>
    public Tree<T> AddChild(T child, T parentNode)
    {
        Tree<T> childTree;

        if (!ChildExists(child, parentNode))
        {
            childTree = new Tree<T>(child) { Parent = this };
            Children.Add(childTree);
        }
        else
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            childTree = GetChild(child, parentNode);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

#pragma warning disable CS8603 // Possible null reference return.
        return childTree;
#pragma warning restore CS8603 // Possible null reference return.
    }
}