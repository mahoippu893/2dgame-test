using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
///     Player�̉�ʏ�Q�[���I�u�W�F�N�g
/// </summary>
public class PlayerGameObject : BaseCharacterGameObject {

    // ==================================================
    // Event�n���h��
    // ==================================================

    /// <summary>
    ///     ���t���[������
    /// </summary>
    void Update() {
        PlayerPresenter.CheckInputMove(this);
    }
}
