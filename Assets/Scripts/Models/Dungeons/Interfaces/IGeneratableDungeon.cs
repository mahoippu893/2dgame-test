using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �_���W���������\�ȃN���X�Ɏ�������C���^�[�t�F�[�X
/// </summary>
public interface IGeneratableDungeon {

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �_���W�����𐶐�����
    /// </summary>
    public void GenerateDungeon(IViewableDungeon viewer);
}
