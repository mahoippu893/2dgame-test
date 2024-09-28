using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable

/// <summary>
///     プレイヤーのViewとModelの橋渡しを行うクラス
/// </summary>
public class PlayerPresenter {

    // シングルトン設計用インスタンス
    private static PlayerPresenter? _instance;

    // ==================================================
    // コンストラクタ
    // ==================================================

    /// <summary>
    ///     コンストラクタ
    /// </summary>
    private PlayerPresenter() {

        var inputChecker = new KeyboardInput();
        InputDirectionChecker = inputChecker;
    }

    // ==================================================
    // プロパティ
    // ==================================================

    /// <summary>
    ///     連続して移動を行う場合の移動方法
    /// </summary>
    public IMoveable.MoveDirection OldDirection { get; set; } = IMoveable.MoveDirection.None;

    /// <summary>
    ///     移動方向の入力チェックを行うインターフェース
    /// </summary>
    public ICheckableInputDirection InputDirectionChecker { get; set; }

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     PlayerPresenterクラスのインスタンスを取得する
    /// </summary>
    /// <returns></returns>
    public static PlayerPresenter GetInstance() {

        if (_instance is null) {
            _instance = new PlayerPresenter();
        }

        return _instance;
    }

    /// <summary>
    ///     入力を判定し、移動処理を行う
    /// </summary>
    /// <param name="mover"></param>
    public static void CheckInputMove(IMoveable mover) {

        if (GetInstance().InputDirectionChecker.CheckInputMoveDirection() is IMoveable.MoveDirection direction) {

            // 移動できた場合

            mover.Move(direction);

            // 移動値を保持
            GetInstance().OldDirection = direction;
        } else {

            // 移動できない入力の場合

            // 前回移動を継続
            mover.Move(GetInstance().OldDirection);
        }
    }
}
