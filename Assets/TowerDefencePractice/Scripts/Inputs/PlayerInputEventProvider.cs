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
        // �}�E�X����̃��C
        Ray mouseRay;

        // ���݂�Grid�ƑO��Grid��Hit���
        RaycastHit gridHit = new RaycastHit();
        RaycastHit gridHitPre = new RaycastHit();

        // ���C���[�}�X�N
        LayerMask mask = new LayerMask();

        // Start is called before the first frame update
        void Start()
        {
            mask = LayerMask.GetMask("Grid", "UI");
        }

        // Update is called once per frame
        void Update()
        {
            // �O���b�h�ւ̃}�E�X�I�[�o�[
            GridMouseOver();
        }

        /// <summary>
        /// �O���b�h�ւ̃}�E�X�I�[�o�[
        /// </summary>
        void GridMouseOver()
        {
            mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // UI�Ɣ���Ă�����
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // �O���b�h�Ƀ}�E�X�I�[�o�[���Ă���ꍇ
            if (Physics.Raycast(mouseRay, out gridHit, Mathf.Infinity, mask))
            {
                // �N���b�N�����m
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (gridHit.collider.GetComponent<GridCellController>().interactable)
                    {
                        Debug.Log("UI��\��");
                    }
                }

                // �n�C���C�g�X�V�֌W--------------------------------------------------------------------------------------------------
                // ���O�Ƀ}�E�X�I�[�o�[���Ă����O���b�h�ƌ��݃}�E�X�I�[�o�[���Ă���O���b�h�������ꍇ
                if (gridHit.collider == gridHitPre.collider)
                {
                    // �������Ȃ�

                }
                // �قȂ�ꍇ
                else
                {
                    //���݂̃O���b�h�̃n�C���C�g
                    gridHit.collider.GetComponent<GridCellController>().GridBrighten();

                    // ���O��Grid�Ƀ}�E�X�I�[�o�[���Ă����ꍇ
                    if (gridHitPre.collider != null)
                    {
                        // �n�C���C�g������
                        gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                    }
                }
            }
            // �O���b�h�Ƀ}�E�X�I�[�o�[���Ă��Ȃ��ꍇ
            else
            {
                // ���O�ɃO���b�h�Ƀ}�E�X�I�[�o�[���Ă����ꍇ
                if (gridHitPre.collider != null)
                {
                    gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                }
            }

            // ���O�̃}�E�X�I�[�o�[���
            gridHitPre = gridHit;
        }
    }
}