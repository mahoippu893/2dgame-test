using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �L�����N�^�[�̈ʒu����ݒ�\�ȃN���X�Ɏ�������C���^�[�t�F�[�X
/// </summary>
public interface ISetableCharacterPosition {

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �v���C���[�̈ʒu��ݒ肷��
    /// </summary>
    public void SetPlayerPosition(int x, int y);
}
