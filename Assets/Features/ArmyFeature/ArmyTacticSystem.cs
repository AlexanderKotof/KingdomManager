﻿using KM.Features.BattleFeature.BattleSystem3d;
using KM.Features.Raycasterfeature.Raycaster;
using KM.Features.CameraFeature;
using KM.Startup;
using UnityEngine;
using static KM.Features.BattleFeature.BattleSystem3d.Unit;
using System.Collections.Generic;

namespace KM.Features.ArmyFeature
{
    public class ArmyTacticSystem : ISystem
    {
        private ArmySystem _armySystem;

        private RaycasterSystem _raycaster;
        private BattleUnitEntity _selectedUnit;

        public List<Unit> GuardUnits;

        public void Initialize()
        {
            _armySystem = AppStartup.Instance.GetSystem<ArmySystem>();
            _raycaster = AppStartup.Instance.GetSystem<RaycasterSystem>();

            GuardUnits = new List<Unit>();
            _raycaster.OnClick += OnRaycast;
        }

        public void Destroy()
        {
            _raycaster.OnClick -= OnRaycast;
        }

        public void SetActive(bool value)
        {
            _raycaster.active = value;
        }

        private void OnRaycast(RaycastData data)
        {
            if (data.selectUnit != null && data.selectUnit is Citadel)
                return;

            if (data.selectUnit != null)
            {
                _armySystem.MoveToArmy((BattleUnitEntity)data.selectUnit.prototype, 1);
                GuardUnits.Remove(data.selectUnit);
                GameObject.Destroy(data.selectUnit.gameObject);
            }
            else
            {
                if (!_selectedUnit)
                    return;

                PlaceUnit(_selectedUnit, data.raycastHit.point);
            }
        }

        private void PlaceUnit(BattleUnitEntity selectedUnit, Vector3 point)
        {
            if (_armySystem.freeArmy.GetUnitCount(selectedUnit) == 0)
                return;

            SpawnUnit(selectedUnit, point);

            _armySystem.MoveToDefence(selectedUnit, point);
        }

        public void SpawnUnit(IUnitPrototype prototype, Vector3 position)
        {
            var unit = SpawnUnitsUtils.SpawnUnit(prototype, position);
            unit.fraction = Fraction.Fraction1;
            GuardUnits.Add(unit);
        }

        internal void SelectUnit(BattleUnitEntity unit)
        {
            if (_selectedUnit == unit)
            {
                _selectedUnit = null;
                return;
            }

            _selectedUnit = unit;
        }
    }
}