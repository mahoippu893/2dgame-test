using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     �L�����N�^�[�̉�ʏ�Q�[���I�u�W�F�N�g�̃x�[�X�N���X
/// </summary>
public class BaseCharacterGameObject : MonoBehaviour, IMoveable {

    [SerializeField]
    float _moveSpeed = 0.001f;

    // ==================================================
    // �v���p�e�B
    // ==================================================

    /// <summary>
    ///     �ړ��̑��x
    /// </summary>
    public float MoveSpeed {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �ړ�����
    /// </summary>
    /// <param name="direction"></param>
    public void Move(IMoveable.MoveDirection direction) {

        // TOP(-) or Bottom(+)
        float y = 0.0f;

        // Right(-) or Left(+)
        float x = 0.0f;

        switch (direction) {
            case IMoveable.MoveDirection.Top:

                y = -MoveSpeed;
                break;
            case IMoveable.MoveDirection.Left:

                x = MoveSpeed;
                break;
            case IMoveable.MoveDirection.Right:

                x = -MoveSpeed;
                break;
            case IMoveable.MoveDirection.Bottom:

                y = MoveSpeed;
                break;
            case IMoveable.MoveDirection.TopLeft:

                y = -MoveSpeed;
                x = MoveSpeed;
                break;
            case IMoveable.MoveDirection.TopRight:

                y = -MoveSpeed;
                x = -MoveSpeed;
                break;
            case IMoveable.MoveDirection.BottomLeft:

                y = MoveSpeed;
                x = MoveSpeed;
                break;
            case IMoveable.MoveDirection.BottomRight:

                y = MoveSpeed;
                x = -MoveSpeed;
                break;
        }

        this.transform.position = new Vector3(this.transform.position.x + x, this.transform.position.y + y, this.transform.position.z);
    }
}
