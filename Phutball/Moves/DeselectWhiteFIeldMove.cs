namespace Phutball.Moves
{
    public class DeselectWhiteFieldMove : IPhutballMove
    {
        private readonly Field _field;

        public DeselectWhiteFieldMove(Field field)
        {
            _field = field;
        }

        public void Perform(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.DeSelect();
            board.UpdateFields(_field);
            context.SwitchPlayer.SwapMovingPlayers();
        }

        public void Undo(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.Select();
            board.UpdateFields(_field);            
        }

        public bool CollectToPlayerSwitch(CompositeMove resultMove)
        {
            resultMove.Add(this);
            return true;
        }
    }
}