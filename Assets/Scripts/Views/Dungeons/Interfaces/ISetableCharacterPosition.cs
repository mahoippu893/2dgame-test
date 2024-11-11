using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     キャラクターの位置情報を設定可能なクラスに実装するインターフェース
/// </summary>
public interface ISetableCharacterPosition {

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     プレイヤーの位置を設定する
    /// </summary>
    public void SetPlayerPosition(int x, int y);
}
