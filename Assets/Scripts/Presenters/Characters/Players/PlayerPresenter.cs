using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable

/// <summary>
///     �v���C���[��View��Model�̋��n�����s���N���X
/// </summary>
public class PlayerPresenter {

    // �V���O���g���݌v�p�C���X�^���X
    private static PlayerPresenter? _instance;

    // ==================================================
    // �R���X�g���N�^
    // ==================================================

    /// <summary>
    ///     �R���X�g���N�^
    /// </summary>
    private PlayerPresenter() {

        var inputChecker = new KeyboardInput();
        InputDirectionChecker = inputChecker;
    }

    // ==================================================
    // �v���p�e�B
    // ==================================================

    /// <summary>
    ///     �A�����Ĉړ����s���ꍇ�̈ړ����@
    /// </summary>
    public IMoveable.MoveDirection OldDirection { get; set; } = IMoveable.MoveDirection.None;

    /// <summary>
    ///     �ړ������̓��̓`�F�b�N���s���C���^�[�t�F�[�X
    /// </summary>
    public ICheckableInputDirection InputDirectionChecker { get; set; }

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     PlayerPresenter�N���X�̃C���X�^���X���擾����
    /// </summary>
    /// <returns></returns>
    public static PlayerPresenter GetInstance() {

        if (_instance is null) {
            _instance = new PlayerPresenter();
        }

        return _instance;
    }

    /// <summary>
    ///     ���͂𔻒肵�A�ړ��������s��
    /// </summary>
    /// <param name="mover"></param>
    public static void CheckInputMove(IMoveable mover) {

        if (GetInstance().InputDirectionChecker.CheckInputMoveDirection() is IMoveable.MoveDirection direction) {

            // �ړ��ł����ꍇ

            mover.Move(direction);

            // �ړ��l��ێ�
            GetInstance().OldDirection = direction;
        } else {

            // �ړ��ł��Ȃ����͂̏ꍇ

            // �O��ړ����p��
            mover.Move(GetInstance().OldDirection);
        }
    }
}
