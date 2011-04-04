using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{

    public class GameOptionsPresenter : Screen, IGameOptionsPresenter
    {
        private const int BORDER_FIELDS_COUNT = 4;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballOptions _phutballOptions;

        public GameOptionsPresenter(IEventPublisher eventPublisher, IPhutballOptions phutballOptions)
        {
            _eventPublisher = eventPublisher;
            _phutballOptions = phutballOptions;
            InitializeOptionValues();
        }

        private void InitializeOptionValues()
        {
            Height = _phutballOptions.RowCount - BORDER_FIELDS_COUNT;
            Width = _phutballOptions.ColumnCount - BORDER_FIELDS_COUNT;
            _dfsDepth = _phutballOptions.DfsSearchDepth;
            _bfsDepth = _phutballOptions.BfsSearchDepth;
            _alphaBetaDepth = _phutballOptions.AlphaBetaSearchDepth;
        }

        public decimal Width { get; set; }
        public decimal Height { get; set; }

        private decimal _dfsDepth;
        public decimal DfsDepth
        {
            get { return _dfsDepth; }
            set { _dfsDepth = value;
                _phutballOptions.DfsSearchDepth = (int) value;
            }
        }

        private decimal _bfsDepth;
        public decimal BfsDepth
        {
            get { return _bfsDepth; }
            set { _bfsDepth = value;
                _phutballOptions.BfsSearchDepth = (int) value;
            }
        }

        private decimal _alphaBetaDepth;
        public decimal AlphaBetaDepth
        {
            get { return _alphaBetaDepth; }
            set { _alphaBetaDepth = value;
                _phutballOptions.AlphaBetaSearchDepth = (int) value;
            }
        }

        public void UpdateBoardSize()
        {
            _phutballOptions.RowCount = (int)Height + BORDER_FIELDS_COUNT;
            _phutballOptions.ColumnCount = (int)Width + BORDER_FIELDS_COUNT;
            _eventPublisher.Publish(new GameOptionsChanged());
        }

    }

    public class GameOptionsChanged
    {
    }
}