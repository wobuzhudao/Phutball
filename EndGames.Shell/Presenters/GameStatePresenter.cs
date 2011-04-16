using System.Linq;
using System.Windows.Threading;
using Caliburn.PresentationFramework.Filters;
using Caliburn.PresentationFramework.Screens;
using EndGames.Mapping;
using EndGames.Phutball;
using EndGames.Phutball.Events;
using EndGames.Shell.Mapping;
using EndGames.Shell.Presenters.Interfaces;
using System;

namespace EndGames.Shell.Presenters
{
    public class GameStatePresenter : Screen, IGameStatePresenter
    {
        private readonly PhutballGameState _gameState;
        private readonly IEventPublisher _eventPublisher;

        public GameStatePresenter(PhutballGameState gameState,
                                  IEventPublisher eventPublisher)
        {
            _gameState = gameState;
            _eventPublisher = eventPublisher;
            _timer = new DispatcherTimer()
                         {
                             Interval = TimeSpan.FromMilliseconds(200),
                             IsEnabled = false
                         };
            _timer.Tick += UpdatePlayersTimes;
        }

        private void UpdatePlayersTimes(object sender, EventArgs e)
        {
            FirstPlayer.TimeOnMoves = _gameState.Players.First().TimeOnMoves.ToMinutesAndSeconds();
            SecondPlayer.TimeOnMoves = _gameState.Players.Last().TimeOnMoves.ToMinutesAndSeconds();
        }

        private void OnPhutballGameEnded(PhutballGameEnded @event)
        {
            NotifyStateChanged();
        }

        private void OnPlayersStateChanged(PlayersStateChanged playersStateChanged)
        {
            UpdatePlayers();
        }

        private PlayerOnBoardModel _firstPlayer;

        public PlayerOnBoardModel FirstPlayer
        {
            get { return _firstPlayer; }
            set
            {
                _firstPlayer = value;
                NotifyOfPropertyChange(() => FirstPlayer);
            }
        }

        private PlayerOnBoardModel _secondPlayer;
        private DispatcherTimer _timer;

        public PlayerOnBoardModel SecondPlayer
        {
            get { return _secondPlayer; }
            set
            {
                _secondPlayer = value;
                NotifyOfPropertyChange(() => SecondPlayer);
            }
        }


        [Preview("CanStartGame", AffectsTriggers = true)]
        public void StartGame()
        {
            _gameState.StartVsHuman();
            _timer.Start();
            NotifyStateChanged();
        }

        [Preview("CanStartGame", AffectsTriggers = true)]
        public void StartWithComputer()
        {
            _gameState.StartWithComputer();
            _timer.Start();
            NotifyStateChanged();
        }


        private void NotifyStateChanged()
        {
            NotifyOfPropertyChange(() => CanStartGame);
            NotifyOfPropertyChange(() => CanRestartGame);
            NotifyOfPropertyChange(() => FirstPlayer);
            NotifyOfPropertyChange(() => SecondPlayer);
        }

        public bool CanStartGame
        {
            get { return _gameState.CurrentState != PhutballGameStateEnum.Started; }
        }

        [Preview("CanRestartGame", AffectsTriggers = true)]
        public void RestartGame()
        {
            _gameState.Restart();
            _timer.Stop();
            NotifyStateChanged();
        }

        public bool CanRestartGame
        {
            get { return _gameState.CurrentState == PhutballGameStateEnum.Started; }
        }

        protected override void OnInitialize()
        {
            _eventPublisher.Subscribe<PhutballGameEnded>(OnPhutballGameEnded);
            _eventPublisher.Subscribe<PlayersStateChanged>(OnPlayersStateChanged);
            _eventPublisher.Subscribe<CriticalGameOptionsChanged>(OnGameOptionsChanged);
            UpdatePlayers();
        }

        public void OnGameOptionsChanged(CriticalGameOptionsChanged @event)
        {
            RestartGame();
        }

        private void UpdatePlayers()
        {
            FirstPlayer = _gameState.Players.First().Map().To<PlayerOnBoardModel>();
            SecondPlayer = _gameState.Players.Last().Map().To<PlayerOnBoardModel>();
        }
    }
}