using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     マップを画面に表示可能なクラスに実装するインターフェース
/// </summary>
public interface IViewableDungeon {

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     ダンジョンの歩行可能な床を表示する
    /// </summary>
    public void ViewFloor(int x, int y);

    /// <summary>
    ///     ダンジョンの歩行不可な壁を表示する
    /// </summary>
    public void ViewWall(int x, int y);

    /// <summary>
    ///     ダンジョンの階段を表示する
    /// </summary>
    public void ViewStair(int x, int y);
}
