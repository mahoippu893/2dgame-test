using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
///     Playerの画面上ゲームオブジェクト
/// </summary>
public class PlayerGameObject : BaseCharacterGameObject {

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
