using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �}�b�v����ʂɕ\���\�ȃN���X�Ɏ�������C���^�[�t�F�[�X
/// </summary>
public interface IViewableDungeon {

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �_���W�����̕��s�\�ȏ���\������
    /// </summary>
    public void ViewFloor(int x, int y);

    /// <summary>
    ///     �_���W�����̕��s�s�ȕǂ�\������
    /// </summary>
    public void ViewWall(int x, int y);

    /// <summary>
    ///     �_���W�����̊K�i��\������
    /// </summary>
    public void ViewStair(int x, int y);
}
