using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     ���͂ɒl����ړ������̃`�F�b�N���\�ȃN���X�Ɏ�������C���^�[�t�F�[�X
/// </summary>
public interface ICheckableInputDirection {

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     ���͂ɒl����ړ������̃`�F�b�N
    /// </summary>
    public IMoveable.MoveDirection? CheckInputMoveDirection();
}
