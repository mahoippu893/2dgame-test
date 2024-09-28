using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �L�[�{�[�h�Ɋւ�����͂��������f��
/// </summary>
public class KeyboardInput : ICheckableInputDirection {

    // �萔��`
    private const KeyCode KEY_TOP = KeyCode.W;
    private const KeyCode KET_LEFT = KeyCode.A;
    private const KeyCode KEY_RIGHT = KeyCode.D;
    private const KeyCode KEY_BOTTOM = KeyCode.S;

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     ���͂ɒl����ړ������̃`�F�b�N
    /// </summary>
    public IMoveable.MoveDirection? CheckInputMoveDirection() {

        bool isTop = Input.GetKey(KEY_TOP);
        bool isLeft = Input.GetKey(KET_LEFT);
        bool isRight = Input.GetKey(KEY_RIGHT);
        bool isBottom = Input.GetKey(KEY_BOTTOM);

        // �ړ��s�̏�������
        if (isTop && isBottom) {
            return null;
        }
        if (isLeft && isRight) {
            return null;
        }

        // �ړ�����

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

        // �ړ���Ȃ�
        return IMoveable.MoveDirection.None;
    }
}
