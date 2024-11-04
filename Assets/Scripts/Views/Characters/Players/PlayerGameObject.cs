using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
///     Playerの画面上ゲームオブジェクト
/// </summary>
public class PlayerGameObject : BaseCharacterGameObject {

    // ==================================================
    // プロパティ
    // ==================================================

    /// <summary>
    ///     自身と対応するモデル
    /// </summary>
    public Player Player { get; set; } = new Player();

    // ==================================================
    // Eventハンドラ
    // ==================================================

    /// <summary>
    ///     毎フレーム処理
    /// </summary>
    void Update() {
        PlayerPresenter.CheckInputMove(this);
    }
}
