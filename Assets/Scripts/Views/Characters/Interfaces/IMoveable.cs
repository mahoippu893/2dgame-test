using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �ړ��\�ȃN���X�Ɏ�������C���^�[�t�F�[�X
/// </summary>
public interface IMoveable {

    /// <summary>
    ///     �ړ��̕���
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
    // �v���p�e�B
    // ==================================================

    /// <summary>
    ///     �ړ��̑��x
    /// </summary>
    public float MoveSpeed { get; set; }

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �ړ�����
    /// </summary>
    /// <param name="directoion"></param>
    public void Move(MoveDirection directoion);
}
