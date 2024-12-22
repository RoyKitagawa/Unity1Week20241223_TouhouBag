using UnityEngine;

public static class VectorUtil
{
    /// <summary>
    /// Add系
    /// </summary>
    public static Vector3 Add(Vector2 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, b.z);
    }

    public static Vector3 Add(Vector3 a, Vector2 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z);
    }

    public static Vector2 Add(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector2Int Add(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }

    public static Vector3 Add(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3Int Add(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector4 Add(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    /// <summary>
    /// Subtract系
    /// </summary>
    public static Vector3 Sub(Vector2 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, b.z);
    }

    public static Vector3 Sub(Vector3 a, Vector2 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z);
    }

    public static Vector2 Sub(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public static Vector2Int Sub(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }

    public static Vector3 Sub(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3Int Sub(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector4 Sub(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    /// <summary>
    /// Multiply系
    /// </summary>
    public static Vector3 Multiply(Vector2 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, b.z);
    }

    public static Vector3 Multiply(Vector3 a, Vector2 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z);
    }

    public static Vector2 Multiply(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static Vector2Int Multiply(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x * b.x, a.y * b.y);
    }

    public static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector3Int Multiply(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector4 Multiply(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    }
}