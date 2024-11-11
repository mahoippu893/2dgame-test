using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEditor.Progress;

#nullable enable

/// <summary>
///     �_���W�����̃}�b�v
/// </summary>
public class DungeonMap : IGeneratableDungeon {

    // ==================================================
    // �v���p�e�B
    // ==================================================

    /// <summary>
    ///     �������̕����̐�
    /// </summary>
    public int FloorCountX { get; set; }

    /// <summary>
    ///     �c�����̕����̐�
    /// </summary>
    public int FloorCountY { get; set; }

    /// <summary>
    ///     �������̂P����������̃T�C�Y�̍ŏ��l
    /// </summary>
    public int FloorSizeXMin { get; set; }

    /// <summary>
    ///     �������̂P����������̃T�C�Y�̍ő�l
    /// </summary>
    public int FloorSizeXMax { get; set; }

    /// <summary>
    ///     �c�����̂P����������̃T�C�Y�̍ŏ��l
    /// </summary>
    public int FloorSizeYMin { get; set; }

    /// <summary>
    ///     �c�����̂P����������̃T�C�Y�̍ő�l
    /// </summary>
    public int FloorSizeYMax { get; set; }


    // ==================================================
    // �R���X�g���N�^
    // ==================================================

    /// <summary>
    ///     �R���X�g���N�^
    /// </summary>
    public DungeonMap(
        int floorCountX, 
        int floorCountY, 
        int floorSizeXMin, 
        int floorSizeXMax, 
        int floorSizeYMin, 
        int floorSizeYMax) {

        FloorCountX = floorCountX;
        FloorCountY = floorCountY;
        FloorSizeXMin = floorSizeXMin;
        FloorSizeXMax = floorSizeXMax;
        FloorSizeYMin = floorSizeYMin;
        FloorSizeYMax = floorSizeYMax;
    }

    // ==================================================
    // Public���\�b�h
    // ==================================================

    /// <summary>
    ///     �}�b�v��������������
    /// </summary>
    public void GenerateDungeon(IViewableDungeon viewer, ISetableCharacterPosition posSetter) {

        DungeonFloorInfo[,] floorList = new DungeonFloorInfo[FloorCountX, FloorCountY];

        // �����̐��������[�v
        for (int x = 0; x < FloorCountX; x++) {
            for (int y = 0; y < FloorCountY; y++) {

                // �����Ԃ̋���
                int leftDiff = Random.Range(6, 16);
                int topDiff = Random.Range(6, 16);

                // �������̐���
                floorList[x, y] = new DungeonFloorInfo(
                    (x == 0) ? leftDiff : floorList[x - 1, y].X + floorList[x - 1, y].W + leftDiff,
                    (y == 0) ? topDiff : floorList[x, y - 1].Y + floorList[x, y - 1].H + topDiff,
                    Random.Range(FloorSizeXMin, FloorSizeXMax + 1),
                    Random.Range(FloorSizeYMin, FloorSizeYMax + 1)
                    );
            }
        }

        List<DungeonFloorInfo> streetList = new List<DungeonFloorInfo>();

        // �ʘH�ʒu�̎擾
        for (int x = 0; x < FloorCountX; x++) {
            for (int y = 0; y < FloorCountY; y++) {


                // �ʘH�𐶐����邩�ǂ����̔���
                bool isGenerateRightStreet = (Random.Range(0, 2) == 0);
                bool isGenerateBottomStreet = (Random.Range(0, 2) == 0);

                // ����false�̏ꍇ���B�s�ɂȂ�\��������̂ŁA�ǂ��炩true�ɂ���
                if (isGenerateRightStreet == false && isGenerateBottomStreet == false) {
                    
                    switch (Random.Range(0, 2)) {
                        case 0:
                            isGenerateRightStreet = true;
                            break;
                        case 1:
                        default:
                            isGenerateBottomStreet = true;
                            break;
                    }
                }


                // ---------------------


                // �����ԂɁA����1 or ��1 �̕����Ƃ��Đ������s��

                // �����̕��� <=> �E�̕���
                if (isGenerateRightStreet) {

                    if (x < FloorCountX - 1) {

                        // �ʘH��X�����擾
                        // �����̕�����X�� �� �E�̕�����X�� �̒��Ԃ������_���Ŏ擾
                        int streetX = Random.Range(floorList[x, y].X + floorList[x, y].W + 1, floorList[x + 1, y].X + 1);

                        // ----

                        // �����̕����̂ǂ�����Y���Ƀ����_���Ƀ|�C���g�𐶐�
                        int currentStreetStartY = Random.Range(floorList[x, y].Y, floorList[x, y].Y + floorList[x, y].H + 1);

                        // �����̕�������ʘH�܂œ���L�΂�
                        streetList.Add(new DungeonFloorInfo(
                            // ���W
                            (floorList[x, y].X + floorList[x, y].W),
                            currentStreetStartY,
                            // �T�C�Y
                            streetX - (floorList[x, y].X + floorList[x, y].W) + 1,
                            1
                            ));

                        // ----

                        // �E���̕����̂ǂ�����Y���Ƀ����_���Ƀ|�C���g�𐶐�
                        int rightStreetStartY = Random.Range(floorList[x + 1, y].Y, floorList[x + 1, y].Y + floorList[x + 1, y].H + 1);

                        // �E���̕�������ʘH�܂œ���L�΂�
                        streetList.Add(new DungeonFloorInfo(
                            // ���W
                            streetX,
                            rightStreetStartY,
                            // �T�C�Y
                            floorList[x + 1, y].X - streetX + 1,
                            1
                            ));

                        // ----

                        // �ʘH�Ԃ�Y�����W��2�ȏ㗣��Ă���ꍇ�A������ʘH�Ŗ��߂�

                        if (currentStreetStartY - rightStreetStartY >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // ���W
                                streetX,
                                rightStreetStartY,
                                // �T�C�Y
                                1,
                                currentStreetStartY - (rightStreetStartY - 1)
                                ));
                        } else if (rightStreetStartY - currentStreetStartY >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // ���W
                                streetX,
                                currentStreetStartY,
                                // �T�C�Y
                                1,
                                rightStreetStartY - (currentStreetStartY - 1)
                                ));
                        }
                    }
                }

                // �����̕���
                //  ����
                // ���̕���
                if (isGenerateBottomStreet) {

                    if (y < FloorCountY - 1) {

                        // �ʘH��Y�����擾
                        // �����̕�����Y�� �� �E�̕�����Y�� �̒��Ԃ������_���Ŏ擾
                        int streetY = Random.Range(floorList[x, y].Y + floorList[x, y].H + 1, floorList[x, y + 1].Y + 1);

                        // ----

                        // �����̕����̂ǂ�����X���Ƀ����_���Ƀ|�C���g�𐶐�
                        int currentStreetStartX = Random.Range(floorList[x, y].X, floorList[x, y].X + floorList[x, y].W + 1);

                        // �����̕�������ʘH�܂œ���L�΂�
                        streetList.Add(new DungeonFloorInfo(
                            // ���W
                            currentStreetStartX,
                            (floorList[x, y].Y + floorList[x, y].H),
                            // �T�C�Y
                            1,
                            streetY - (floorList[x, y].Y + floorList[x, y].H) + 1
                            ));

                        // ----

                        // �E���̕����̂ǂ�����X���Ƀ����_���Ƀ|�C���g�𐶐�
                        int rightStreetStartX = Random.Range(floorList[x, y + 1].X, floorList[x, y + 1].X + floorList[x, y + 1].W + 1);

                        // �E���̕�������ʘH�܂œ���L�΂�
                        streetList.Add(new DungeonFloorInfo(
                            // ���W
                            rightStreetStartX,
                            streetY,
                            // �T�C�Y
                            1,
                            floorList[x, y + 1].Y - streetY + 1
                            ));

                        // ----

                        // �ʘH�Ԃ�X�����W��2�ȏ㗣��Ă���ꍇ�A������ʘH�Ŗ��߂�

                        if (currentStreetStartX - rightStreetStartX >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // ���W
                                rightStreetStartX,
                                streetY,
                                // �T�C�Y
                                currentStreetStartX - (rightStreetStartX - 1),
                                1
                                ));
                        } else if (rightStreetStartX - currentStreetStartX >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // ���W
                                currentStreetStartX,
                                streetY,
                                // �T�C�Y
                                rightStreetStartX - (currentStreetStartX - 1),
                                1
                                ));
                        }
                    }
                }
            }
        }


        // �}�b�v�`�b�v�������̍쐬
        var result = new List<DungeonMapTipInfo>();
        foreach (var floor in floorList) {

            for (int i = 0; i < floor.W; i++) {
                for (int j = 0; j < floor.H; j++) {
                    result.Add(new DungeonMapTipInfo(floor.X + i, floor.Y + j, DungeonMapTipType.Floor));
                }
            }
        }
        foreach (var street in streetList) {

            for (int i = 0; i < street.W; i++) {
                for (int j = 0; j < street.H; j++) {
                    result.Add(new DungeonMapTipInfo(street.X + i, street.Y + j, DungeonMapTipType.Street));
                }
            }
        }


        // �E�[�Ɖ��[��ݒ�
        int rightWall = result.Max(item => item.X) + 3;
        int bottomWall = result.Max(item => item.Y) + 3;


        // �ǂ𖄂߂�
        for (int x = 0; x < rightWall; x++) {
            for (int y = 0; y < bottomWall; y++) {

                if (result.FirstOrDefault(item => item.X == x && item.Y == y) is null) {
                    result.Add(new DungeonMapTipInfo(x, y, DungeonMapTipType.Wall));
                }
            }
        }


        // �����烉���_���Ɉ�擾���A�K�i��ݒ肷��
        while (result.Count(x => x.Type == DungeonMapTipType.Stair) == 0) {

            int randIndex = Random.Range(0, result.Count);

            try {
                if (result[randIndex].Type == DungeonMapTipType.Floor) {
                    result[randIndex].Type = DungeonMapTipType.Stair;
                    break;
                }
            } catch { }
        }

        // �����烉���_���Ɉ�擾���A�X�^�[�g�n�_��ݒ肷��
        while (result.Count(x => x.Type == DungeonMapTipType.StartPoint) == 0) {

            int randIndex = Random.Range(0, result.Count);

            try {
                if (result[randIndex].Type == DungeonMapTipType.Floor) {
                    result[randIndex].Type = DungeonMapTipType.StartPoint;
                    break;
                }
            } catch { }
        }


        // ��ʂɕ\��
        foreach (var item in result) {

            switch (item.Type) {
                case DungeonMapTipType.Floor:
                case DungeonMapTipType.Street:
                    viewer.ViewFloor(item.X, item.Y);
                    break;
                case DungeonMapTipType.Wall:
                    viewer.ViewWall(item.X, item.Y);
                    break;
                case DungeonMapTipType.Stair:
                    viewer.ViewFloor(item.X, item.Y);
                    viewer.ViewStair(item.X, item.Y);
                    break;
                case DungeonMapTipType.StartPoint:
                    viewer.ViewFloor(item.X, item.Y);
                    posSetter.SetPlayerPosition(item.X, item.Y);
                    break;
            }
        }
    }

    /// <summary>
    ///     �_���W�����̃}�b�v
    ///     �P�����̏��
    /// </summary>
    private class DungeonFloorInfo {

        /// <summary>
        ///     X���W
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        ///     Y���W
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        ///     ����
        /// </summary>
        public int W { get; private set; }

        /// <summary>
        ///     ������
        /// </summary>
        public int H { get; private set; }

        /// <summary>
        ///     �R���X�g���N�^
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public DungeonFloorInfo(int x, int y, int w, int h) {
            X = x; 
            Y = y;
            W = w;
            H = h;
        }
    }


    /// <summary>
    ///     �}�b�v�`�b�v�̎��
    /// </summary>
    private enum DungeonMapTipType {
        Floor,
        Street,
        Wall,
        Stair,
        StartPoint
    }

    /// <summary>
    ///     �}�b�v�`�b�v�̏��
    /// </summary>
    private class DungeonMapTipInfo {

        /// <summary>
        ///     X���W
        /// </summary>
        public int X { get; private set; }
 
        /// <summary>
        ///     Y���W
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        ///     �}�b�v�`�b�v�̎��
        /// </summary>
        public DungeonMapTipType Type { get; set; }

        /// <summary>
        ///     �R���X�g���N�^
        /// </summary>
        public DungeonMapTipInfo(int x, int y, DungeonMapTipType type) {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
