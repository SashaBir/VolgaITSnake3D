using System;
using Assets.Project.Physics;
using Snake.Camera;
using Snake.Ui.Game;
using UnityEngine;

namespace Snake.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Player : MonoBehaviour
    {
        [Header("Head")]
        [SerializeField] private HeadMovement _head;

        [Header("Body")] 
        [SerializeField] private TailsCollection _collection;
        [SerializeField] private CameraTransformer _camera;
        [SerializeField] private ObstacleChecker _checker;
        [SerializeField] private FoodEater _eater;

        [Header("Ui")] 
        [SerializeField] private UiScore _uiScore;
        [SerializeField] private UiSnakeLenght _uiSnakeLenght;
        [SerializeField] private UiSnakeParameters _uiSnakeParameters;

        private BodyMovement _body;
        private PlayerMovement _movement;
        
        private void Awake()
        {
            _movement = new PlayerMovement();
            _body = new BodyMovement(_head);

            _uiSnakeParameters.SetSpeed(_head.Speed);
        }

        private void Start()
        {
            _collection.AddInitialBodies();
        }

        private void OnEnable()
        {
            // Game processing
            _head.OnMoved += _body.MoveTo;
            _head.OnTurned += _checker.ChangeColliderPosition;

            _eater.OnEaten += _collection.Add;

            _collection.OnAdded += _body.SetParts;
            _collection.OnChanged += _body.SetParts;

            _movement.OnTurnedLeft += _head.MoveLeft;
            _movement.OnTurnedRight += _head.MoveRight;
            _movement.OnLookedAtUp += _camera.LookAtUp;
            _movement.OnLookedAtDown += _camera.LookAtDown;
            _movement.OnZoomIn += _camera.ZoomIn;
            _movement.OnZoomOut += _camera.ZoomOut;

            // Game over
            _checker.OnObstacleBitten += Die;

            // Ui
            _eater.OnEaten += _uiScore.Add;
            _collection.OnAdded += _uiSnakeLenght.Add;
            _collection.OnChanged += _uiSnakeLenght.Add;
        }

        private void OnDisable()
        {
            // Game processing
            _head.OnMoved -= _body.MoveTo;
            _head.OnTurned -= _checker.ChangeColliderPosition;

            _eater.OnEaten -= _collection.Add;

            _collection.OnAdded -= _body.SetParts;
            _collection.OnChanged -= _body.SetParts;

            _movement.OnTurnedLeft -= _head.MoveLeft;
            _movement.OnTurnedRight -= _head.MoveRight;
            _movement.OnLookedAtUp -= _camera.LookAtUp;
            _movement.OnLookedAtDown -= _camera.LookAtDown;
            _movement.OnZoomIn -= _camera.ZoomIn;
            _movement.OnZoomOut -= _camera.ZoomOut;

            // Game over
            _checker.OnObstacleBitten -= Die;

            // Ui
            _eater.OnEaten -= _uiScore.Add;
            _collection.OnAdded -= _uiSnakeLenght.Add;
            _collection.OnChanged -= _uiSnakeLenght.Add;
        }

        public event Action OnDied = delegate { };

        private void Die()
        {
            _head.Stop();

            OnDied.Invoke();
        }
    }
}