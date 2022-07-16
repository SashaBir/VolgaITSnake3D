using System;
using System.Collections.Generic;
using Snake.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Project.Physics
{
    [Serializable]
    public class TailsCollection : ICropper
    {
        [SerializeField] private Tail _prefab;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _container;
        [SerializeField] private int _initialQuantity;

        private readonly IList<Tail> _tails = new List<Tail>();

        private int _indexer;
        private bool _isAddedInitialBodies;

        public void Crop(Tail tail)
        {
            var from = _tails.IndexOf(tail);
            var to = _tails.Count;

            for (var i = from; i < to; i++)
                Object.Destroy(_tails[i].gameObject);

            ((List<Tail>)_tails).RemoveRange(from, to - from);

            _indexer = from - 1;

            OnChanged.Invoke(_tails);
        }

        public event Action<IEnumerable<Tail>> OnAdded = delegate { };
        public event Action<IEnumerable<Tail>> OnChanged = delegate { };

        public void AddInitialBodies()
        {
            if (_isAddedInitialBodies)
                throw new Exception("Initial bodies are spawned.");

            for (var i = 0; i < _initialQuantity; i++)
                Add();

            _isAddedInitialBodies = true;
        }

        public void Add()
        {
            var position = _tails.Count == 0 ? _head.position : _tails[^1].transform.position;
            position += _tails.Count == 0 ? Vector3.back : _tails[^1].DirectionAsOffset;

            var tail = Object.Instantiate
            (
                _prefab,
                position,
                Quaternion.identity,
                _container
            );

            tail.Index = _indexer;
            tail.Cropper = this;
            _tails.Add(tail);

            OnAdded.Invoke(_tails);

            _indexer++;
            _isAddedInitialBodies = true;
            
            PlayerRuntimeData.Lenght = _tails.Count;
        }
    }
}