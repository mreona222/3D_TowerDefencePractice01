using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using TowerDefencePractice.Grids;

namespace TowerDefencePractice.Inputs
{
    public class PlayerInputEventProvider : MonoBehaviour
    {
        // マウスからのレイ
        Ray mouseRay;

        // 現在のGridと前のGridのHit情報
        RaycastHit gridHit = new RaycastHit();
        RaycastHit gridHitPre = new RaycastHit();

        // レイヤーマスク
        LayerMask mask = new LayerMask();

        // Start is called before the first frame update
        void Start()
        {
            mask = LayerMask.GetMask("Grid", "UI");
        }

        // Update is called once per frame
        void Update()
        {
            // グリッドへのマウスオーバー
            GridMouseOver();
        }

        /// <summary>
        /// グリッドへのマウスオーバー
        /// </summary>
        void GridMouseOver()
        {
            mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // UIと被っていたら
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // グリッドにマウスオーバーしている場合
            if (Physics.Raycast(mouseRay, out gridHit, Mathf.Infinity, mask))
            {
                // クリックを検知
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (gridHit.collider.GetComponent<GridCellController>().interactable)
                    {
                        Debug.Log("UIを表示");
                    }
                }

                // ハイライト更新関係--------------------------------------------------------------------------------------------------
                // 直前にマウスオーバーしていたグリッドと現在マウスオーバーしているグリッドが同じ場合
                if (gridHit.collider == gridHitPre.collider)
                {
                    // 何もしない

                }
                // 異なる場合
                else
                {
                    //現在のグリッドのハイライト
                    gridHit.collider.GetComponent<GridCellController>().GridBrighten();

                    // 直前にGridにマウスオーバーしていた場合
                    if (gridHitPre.collider != null)
                    {
                        // ハイライトを消す
                        gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                    }
                }
            }
            // グリッドにマウスオーバーしていない場合
            else
            {
                // 直前にグリッドにマウスオーバーしていた場合
                if (gridHitPre.collider != null)
                {
                    gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                }
            }

            // 直前のマウスオーバー情報
            gridHitPre = gridHit;
        }
    }
}