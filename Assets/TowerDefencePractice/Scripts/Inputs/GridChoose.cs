using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using TowerDefencePractice.Grids;
using TowerDefencePractice.UIs;

using Utilities.States;

namespace TowerDefencePractice.Inputs
{
    public class GridChoose : StateMachineBase<GridChoose>
    {
        public enum PlayerInputState
        {
            MouseOnVoid = 0,
            MouseOnUI,
            MouseOnGrid,
        }

        public PlayerInputState currentState = PlayerInputState.MouseOnVoid;

        Ray mouseRay = new Ray();
        RaycastHit gridHit = new RaycastHit();
        RaycastHit gridHitPre = new RaycastHit();
        LayerMask mask = new LayerMask();

        [SerializeField]
        BattleUIController battleUICon;

        private void Start()
        {
            ChangeState(new GridChoose.MouseOnVoid(this));
            mask = LayerMask.GetMask("Grid", "UI");
        }



        // --------------------------------------------------------------------
        // MouseOnVoid
        // --------------------------------------------------------------------
        private class MouseOnVoid : StateBase<GridChoose>
        {
            public MouseOnVoid(GridChoose _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerInputState.MouseOnVoid;
            }

            public override void OnUpdate()
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    machine.ChangeState(new GridChoose.MouseOnUI(machine));
                }
                else
                {
                    // �}�E�X�̈ʒu���X�V
                    machine.mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                    // �O���b�h�Ƀ��C������������
                    if (Physics.Raycast(machine.mouseRay, Mathf.Infinity, machine.mask))
                    {
                        machine.ChangeState(new GridChoose.MouseOnGrid(machine));
                    }
                }
            }
        }



        // --------------------------------------------------------------------
        // MouseOnUI
        // --------------------------------------------------------------------
        public class MouseOnUI : StateBase<GridChoose>
        {
            public MouseOnUI(GridChoose _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerInputState.MouseOnUI;
            }

            public override void OnUpdate()
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (machine.gridHitPre.collider != null)
                    {
                        machine.gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                    }
                    machine.ChangeState(new GridChoose.MouseOnVoid(machine));
                }
            }
        }



        // --------------------------------------------------------------------
        // MouseOnGrid
        // --------------------------------------------------------------------
        private class MouseOnGrid : StateBase<GridChoose>
        {
            public MouseOnGrid(GridChoose _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.currentState = PlayerInputState.MouseOnGrid;

                // ��ԑJ�ڂ����Ƃ��Ƀ}�E�X�I�[�o�[���Ă����O���b�h
                machine.mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(machine.mouseRay, out machine.gridHit, Mathf.Infinity, machine.mask))
                {
                    machine.gridHit.collider.GetComponent<GridCellController>().GridBrighten();
                    machine.gridHitPre = machine.gridHit;
                }
            }

            public override void OnUpdate()
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    machine.ChangeState(new GridChoose.MouseOnUI(machine));
                }
                else
                {
                    // �}�E�X�̈ʒu���X�V
                    machine.mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

                    // �O���b�h�Ƀ��C������������
                    if (Physics.Raycast(machine.mouseRay, out machine.gridHit, Mathf.Infinity, machine.mask))
                    {
                        GridCellController GCCon = machine.gridHit.collider.GetComponent<GridCellController>();

                        // �C���^���N�g�\�Ȃ�UI�\��
                        if (GCCon.interactable)
                        {
                            if (Mouse.current.leftButton.wasReleasedThisFrame)
                            {
                                if (!GCCon.constructableExist)
                                {
                                    machine.battleUICon.PurchaseConstructablePanelActivate();
                                    machine.battleUICon.currentGridCell = machine.gridHit;
                                }
                                else
                                {
                                    machine.battleUICon.UpgradeConstructablePanelActivate();
                                    machine.battleUICon.currentGridCell = machine.gridHit;
                                }
                            }
                        }

                        // ���O�̃O���b�h�ƌ��݂̃O���b�h���قȂ�ꍇ
                        if (machine.gridHit.collider != machine.gridHitPre.collider)
                        {
                            machine.gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                            machine.ChangeState(new GridChoose.MouseOnGrid(machine));
                        }
                    }
                    else
                    {
                        if (machine.gridHitPre.collider != null)
                        {
                            machine.gridHitPre.collider.GetComponent<GridCellController>().GridLightOff();
                        }
                        machine.ChangeState(new GridChoose.MouseOnVoid(machine));
                    }
                }
            }
        }
    }
}