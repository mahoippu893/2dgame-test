using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

/// <summary>
///     ダンジョンマップの画面上オブジェクト
/// </summary>
public class DungeonMapGameObject : MonoBehaviour, IViewableDungeon {

    [SerializeField]
    int _floorCountX = 4;

    [SerializeField]
    int _floorCountY = 4;

    [SerializeField]
    int _floorSizeX = 10;

    [SerializeField]
    int _floorSizeXMin = 5;

    [SerializeField]
    int _floorSizeXMax = 15;

    [SerializeField]
    int _floorSizeYMin = 5;

    [SerializeField]
    int _floorSizeYMax = 15;

    [SerializeField]
    GameObject? _wallTile = null;

    [SerializeField]
    GameObject? _floorTile = null;


    // ==================================================
    // プロパティ
    // ==================================================

    /// <summary>
    ///     自身と対応したモデル
    /// </summary>
    public DungeonMap DungeonMap { get; set; }

    /// <summary>
    ///     壁のタイル用
    /// </summary>
    public GameObject? WallTile {
        get { return _wallTile; }
        set { _wallTile = value; }
    }

    /// <summary>
    ///     床のタイル
    /// </summary>
    public GameObject? FloorTile {
        get { return _floorTile; }
        set { _floorTile = value; }
    }

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     ダンジョンの歩行可能な床を表示する
    /// </summary>
    public void ViewFloor(int x, int y) {

        if (FloorTile is null) {
            return;
        }

        Instantiate(FloorTile, new Vector3(x + FloorTile.transform.localScale.x, y + FloorTile.transform.localScale.y), new Quaternion(0, 0, 0, 0));
    }

    /// <summary>
    ///     ダンジョンの歩行不可な壁を表示する
    /// </summary>
    public void ViewWall(int x, int y) {

        if (WallTile is null) {
            return;
        }

        Instantiate(WallTile, new Vector3(x + WallTile.transform.localScale.x, y + WallTile.transform.localScale.y), new Quaternion(0, 0, 0, 0));
    }

    // ==================================================
    // Eventハンドラ
    // ==================================================

    /// <summary>
    ///     開始時処理
    /// </summary>
    void Start() {
        DungeonMap = new DungeonMap(_floorCountX, _floorCountY, _floorSizeXMin, _floorSizeXMax, _floorSizeYMin, _floorSizeYMax);
        DungeonPresenter.GenerateDungeon(DungeonMap, this);
    }
}
