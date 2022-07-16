using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Physics
{
    public class BodyMovement
    {
        private readonly IMovableParamenters _paramenters;

        private bool _isBreaked;

        private IReadOnlyList<Tail> _tails;

        public BodyMovement(IMovableParamenters paramenters)
        {
            _paramenters = paramenters;
        }

        public void MoveTo(Vector3 target)
        {
            if (_tails is null)
                return;

            if (_tails.Count == 0)
                return;

            _isBreaked = false;

            float speed = _paramenters.Speed;

            _tails[0].MoveTo(target, speed);
            for (var i = 1; i < _tails.Count; ++i)
            {
                if (_isBreaked)
                {
                    Debug.Log("Break");
                    break;
                }

                _tails[i].MoveTo(_tails[i - 1].transform.position, speed);
            }
        }

        public void SetParts(IEnumerable<Tail> tails)
        {
            _isBreaked = true;
            _tails = (IReadOnlyList<Tail>)tails ?? throw new ArgumentNullException(nameof(tails));
        }
    }
}