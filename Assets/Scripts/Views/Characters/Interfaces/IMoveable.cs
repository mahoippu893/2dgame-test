using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     移動可能なクラスに実装するインターフェース
/// </summary>
public interface IMoveable {

    /// <summary>
    ///     移動の方向
    /// </summary>
    enum MoveDirection {
        None,
        Top,
        Left,
        Right,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }

    // ==================================================
    // プロパティ
    // ==================================================

    /// <summary>
    ///     移動の速度
    /// </summary>
    public float MoveSpeed { get; set; }

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     移動処理
    /// </summary>
    /// <param name="directoion"></param>
    public void Move(MoveDirection directoion);
}
