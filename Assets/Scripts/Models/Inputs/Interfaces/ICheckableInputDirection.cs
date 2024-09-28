using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     入力に値する移動方向のチェックが可能なクラスに実装するインターフェース
/// </summary>
public interface ICheckableInputDirection {

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     入力に値する移動方向のチェック
    /// </summary>
    public IMoveable.MoveDirection? CheckInputMoveDirection();
}
