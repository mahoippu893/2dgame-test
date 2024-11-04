using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     ダンジョン生成可能なクラスに実装するインターフェース
/// </summary>
public interface IGeneratableDungeon {

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     ダンジョンを生成する
    /// </summary>
    public void GenerateDungeon(IViewableDungeon viewer);
}
