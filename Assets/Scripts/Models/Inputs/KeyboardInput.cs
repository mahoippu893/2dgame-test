using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     キーボードに関する入力を扱うモデル
/// </summary>
public class KeyboardInput : ICheckableInputDirection {

    // 定数定義
    private const KeyCode KEY_TOP = KeyCode.W;
    private const KeyCode KET_LEFT = KeyCode.A;
    private const KeyCode KEY_RIGHT = KeyCode.D;
    private const KeyCode KEY_BOTTOM = KeyCode.S;

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     入力に値する移動方向のチェック
    /// </summary>
    public IMoveable.MoveDirection? CheckInputMoveDirection() {

        bool isTop = Input.GetKey(KEY_TOP);
        bool isLeft = Input.GetKey(KET_LEFT);
        bool isRight = Input.GetKey(KEY_RIGHT);
        bool isBottom = Input.GetKey(KEY_BOTTOM);

        // 移動不可の条件判定
        if (isTop && isBottom) {
            return null;
        }
        if (isLeft && isRight) {
            return null;
        }

        // 移動処理

        if (isTop) {

            if (isRight) {
                return IMoveable.MoveDirection.TopRight;
            }
            if (isLeft) {
                return IMoveable.MoveDirection.TopLeft;
            }

            return IMoveable.MoveDirection.Top;
        }

        if (isBottom) {

            if (isRight) {
                return IMoveable.MoveDirection.BottomRight;
            }
            if (isLeft) {
                return IMoveable.MoveDirection.BottomLeft;
            }

            return IMoveable.MoveDirection.Bottom;
        }

        if (isRight) {
            return IMoveable.MoveDirection.Right;
        }

        if (isLeft) {
            return IMoveable.MoveDirection.Left;
        }

        // 移動先なし
        return IMoveable.MoveDirection.None;
    }
}
