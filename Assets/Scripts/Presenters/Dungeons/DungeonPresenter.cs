using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

/// <summary>
///     ダンジョンのViewとModelの橋渡しを行うクラス
/// </summary>
public class DungeonPresenter {

    // シングルトン設計用インスンス
    private static DungeonPresenter? _instance = null;

    // ==================================================
    // コンストラクタ
    // ==================================================

    /// <summary>
    ///     コンストラクタ
    /// </summary>
    private DungeonPresenter() {

    }

    // ==================================================
    // プロパティ
    // ==================================================

    // ==================================================
    // Publicメソッド
    // ==================================================

    /// <summary>
    ///     DungeonPresenterクラスのインスタンスを取得する
    /// </summary>
    /// <returns></returns>
    public static DungeonPresenter GetInstance() {
   
        if (_instance is null) {
            _instance = new DungeonPresenter();
        }

        return _instance;
    }

    /// <summary>
    ///     ダンジョンの生成を行う
    /// </summary>
    public static void GenerateDungeon(IGeneratableDungeon generator, IViewableDungeon viewer) {

        generator.GenerateDungeon(viewer);
    }
}
