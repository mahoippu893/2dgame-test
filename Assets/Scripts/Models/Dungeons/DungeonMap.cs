using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEditor.Progress;

#nullable enable

/// <summary>
///     ダンジョンのマップ
/// </summary>
public class DungeonMap : IGeneratableDungeon {

    // ==================================================
    // プロパティ
    // ==================================================

    /// <summary>
    ///     横方向の部屋の数
    /// </summary>
    public int FloorCountX { get; set; }

    /// <summary>
    ///     縦方向の部屋の数
    /// </summary>
    public int FloorCountY { get; set; }

    /// <summary>
    ///     横方向の１部屋あたりのサイズの最小値
    /// </summary>
    public int FloorSizeXMin { get; set; }

    /// <summary>
    ///     横方向の１部屋あたりのサイズの最大値
    /// </summary>
    public int FloorSizeXMax { get; set; }

    /// <summary>
    ///     縦方向の１部屋あたりのサイズの最小値
    /// </summary>
    public int FloorSizeYMin { get; set; }

    /// <summary>
    ///     縦方向の１部屋あたりのサイズの最大値
    /// </summary>
    public int FloorSizeYMax { get; set; }


    // ==================================================
    // コンストラクタ
    // ==================================================

    /// <summary>
    ///     コンストラクタ
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
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     マップ情報を初期化する
    /// </summary>
    public void GenerateDungeon(IViewableDungeon viewer, ISetableCharacterPosition posSetter) {

        DungeonFloorInfo[,] floorList = new DungeonFloorInfo[FloorCountX, FloorCountY];

        // 部屋の数だけループ
        for (int x = 0; x < FloorCountX; x++) {
            for (int y = 0; y < FloorCountY; y++) {

                // 部屋間の距離
                int leftDiff = Random.Range(6, 16);
                int topDiff = Random.Range(6, 16);

                // 部屋情報の生成
                floorList[x, y] = new DungeonFloorInfo(
                    (x == 0) ? leftDiff : floorList[x - 1, y].X + floorList[x - 1, y].W + leftDiff,
                    (y == 0) ? topDiff : floorList[x, y - 1].Y + floorList[x, y - 1].H + topDiff,
                    Random.Range(FloorSizeXMin, FloorSizeXMax + 1),
                    Random.Range(FloorSizeYMin, FloorSizeYMax + 1)
                    );
            }
        }

        List<DungeonFloorInfo> streetList = new List<DungeonFloorInfo>();

        // 通路位置の取得
        for (int x = 0; x < FloorCountX; x++) {
            for (int y = 0; y < FloorCountY; y++) {


                // 通路を生成するかどうかの判定
                bool isGenerateRightStreet = (Random.Range(0, 2) == 0);
                bool isGenerateBottomStreet = (Random.Range(0, 2) == 0);

                // 両方falseの場合到達不可になる可能性があるので、どちらかtrueにする
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


                // 部屋間に、高さ1 or 幅1 の部屋として生成を行う

                // 自分の部屋 <=> 右の部屋
                if (isGenerateRightStreet) {

                    if (x < FloorCountX - 1) {

                        // 通路のX軸を取得
                        // 自分の部屋のX軸 と 右の部屋のX軸 の中間をランダムで取得
                        int streetX = Random.Range(floorList[x, y].X + floorList[x, y].W + 1, floorList[x + 1, y].X + 1);

                        // ----

                        // 自分の部屋のどこかのY軸にランダムにポイントを生成
                        int currentStreetStartY = Random.Range(floorList[x, y].Y, floorList[x, y].Y + floorList[x, y].H + 1);

                        // 自分の部屋から通路まで道を伸ばす
                        streetList.Add(new DungeonFloorInfo(
                            // 座標
                            (floorList[x, y].X + floorList[x, y].W),
                            currentStreetStartY,
                            // サイズ
                            streetX - (floorList[x, y].X + floorList[x, y].W) + 1,
                            1
                            ));

                        // ----

                        // 右側の部屋のどこかのY軸にランダムにポイントを生成
                        int rightStreetStartY = Random.Range(floorList[x + 1, y].Y, floorList[x + 1, y].Y + floorList[x + 1, y].H + 1);

                        // 右側の部屋から通路まで道を伸ばす
                        streetList.Add(new DungeonFloorInfo(
                            // 座標
                            streetX,
                            rightStreetStartY,
                            // サイズ
                            floorList[x + 1, y].X - streetX + 1,
                            1
                            ));

                        // ----

                        // 通路間のY軸座標が2つ以上離れている場合、差分を通路で埋める

                        if (currentStreetStartY - rightStreetStartY >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // 座標
                                streetX,
                                rightStreetStartY,
                                // サイズ
                                1,
                                currentStreetStartY - (rightStreetStartY - 1)
                                ));
                        } else if (rightStreetStartY - currentStreetStartY >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // 座標
                                streetX,
                                currentStreetStartY,
                                // サイズ
                                1,
                                rightStreetStartY - (currentStreetStartY - 1)
                                ));
                        }
                    }
                }

                // 自分の部屋
                //  ↑↓
                // 下の部屋
                if (isGenerateBottomStreet) {

                    if (y < FloorCountY - 1) {

                        // 通路のY軸を取得
                        // 自分の部屋のY軸 と 右の部屋のY軸 の中間をランダムで取得
                        int streetY = Random.Range(floorList[x, y].Y + floorList[x, y].H + 1, floorList[x, y + 1].Y + 1);

                        // ----

                        // 自分の部屋のどこかのX軸にランダムにポイントを生成
                        int currentStreetStartX = Random.Range(floorList[x, y].X, floorList[x, y].X + floorList[x, y].W + 1);

                        // 自分の部屋から通路まで道を伸ばす
                        streetList.Add(new DungeonFloorInfo(
                            // 座標
                            currentStreetStartX,
                            (floorList[x, y].Y + floorList[x, y].H),
                            // サイズ
                            1,
                            streetY - (floorList[x, y].Y + floorList[x, y].H) + 1
                            ));

                        // ----

                        // 右側の部屋のどこかのX軸にランダムにポイントを生成
                        int rightStreetStartX = Random.Range(floorList[x, y + 1].X, floorList[x, y + 1].X + floorList[x, y + 1].W + 1);

                        // 右側の部屋から通路まで道を伸ばす
                        streetList.Add(new DungeonFloorInfo(
                            // 座標
                            rightStreetStartX,
                            streetY,
                            // サイズ
                            1,
                            floorList[x, y + 1].Y - streetY + 1
                            ));

                        // ----

                        // 通路間のX軸座標が2つ以上離れている場合、差分を通路で埋める

                        if (currentStreetStartX - rightStreetStartX >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // 座標
                                rightStreetStartX,
                                streetY,
                                // サイズ
                                currentStreetStartX - (rightStreetStartX - 1),
                                1
                                ));
                        } else if (rightStreetStartX - currentStreetStartX >= 2) {

                            streetList.Add(new DungeonFloorInfo(
                                // 座標
                                currentStreetStartX,
                                streetY,
                                // サイズ
                                rightStreetStartX - (currentStreetStartX - 1),
                                1
                                ));
                        }
                    }
                }
            }
        }


        // マップチップ生成情報の作成
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


        // 右端と下端を設定
        int rightWall = result.Max(item => item.X) + 3;
        int bottomWall = result.Max(item => item.Y) + 3;


        // 壁を埋める
        for (int x = 0; x < rightWall; x++) {
            for (int y = 0; y < bottomWall; y++) {

                if (result.FirstOrDefault(item => item.X == x && item.Y == y) is null) {
                    result.Add(new DungeonMapTipInfo(x, y, DungeonMapTipType.Wall));
                }
            }
        }


        // 床からランダムに一つ取得し、階段を設定する
        while (result.Count(x => x.Type == DungeonMapTipType.Stair) == 0) {

            int randIndex = Random.Range(0, result.Count);

            try {
                if (result[randIndex].Type == DungeonMapTipType.Floor) {
                    result[randIndex].Type = DungeonMapTipType.Stair;
                    break;
                }
            } catch { }
        }

        // 床からランダムに一つ取得し、スタート地点を設定する
        while (result.Count(x => x.Type == DungeonMapTipType.StartPoint) == 0) {

            int randIndex = Random.Range(0, result.Count);

            try {
                if (result[randIndex].Type == DungeonMapTipType.Floor) {
                    result[randIndex].Type = DungeonMapTipType.StartPoint;
                    break;
                }
            } catch { }
        }


        // 画面に表示
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
    ///     ダンジョンのマップ
    ///     １部屋の情報
    /// </summary>
    private class DungeonFloorInfo {

        /// <summary>
        ///     X座標
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        ///     Y座標
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        ///     横幅
        /// </summary>
        public int W { get; private set; }

        /// <summary>
        ///     高さ幅
        /// </summary>
        public int H { get; private set; }

        /// <summary>
        ///     コンストラクタ
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
    ///     マップチップの種類
    /// </summary>
    private enum DungeonMapTipType {
        Floor,
        Street,
        Wall,
        Stair,
        StartPoint
    }

    /// <summary>
    ///     マップチップの情報
    /// </summary>
    private class DungeonMapTipInfo {

        /// <summary>
        ///     X座標
        /// </summary>
        public int X { get; private set; }
 
        /// <summary>
        ///     Y座標
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        ///     マップチップの種類
        /// </summary>
        public DungeonMapTipType Type { get; set; }

        /// <summary>
        ///     コンストラクタ
        /// </summary>
        public DungeonMapTipInfo(int x, int y, DungeonMapTipType type) {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
