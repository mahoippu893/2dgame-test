using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

/// <summary>
///     �_���W������View��Model�̋��n�����s���N���X
/// </summary>
public class DungeonPresenter {

    // �V���O���g���݌v�p�C���X���X
    private static DungeonPresenter? _instance = null;

    // ==================================================
    // �R���X�g���N�^
    // ==================================================

    /// <summary>
    ///     �R���X�g���N�^
    /// </summary>
    private DungeonPresenter() {

    }

    // ==================================================
    // �v���p�e�B
    // ==================================================

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     DungeonPresenter�N���X�̃C���X�^���X���擾����
    /// </summary>
    /// <returns></returns>
    public static DungeonPresenter GetInstance() {
   
        if (_instance is null) {
            _instance = new DungeonPresenter();
        }

        return _instance;
    }

    /// <summary>
    ///     �_���W�����̐������s��
    /// </summary>
    public static void GenerateDungeon(IGeneratableDungeon generator, IViewableDungeon viewer) {

        generator.GenerateDungeon(viewer);
    }
}
