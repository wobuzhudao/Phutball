using System;
using ForTesting;
using NUnit.Framework;
using Phutball.Search.BoardValues;

namespace Phutball.Tests.Search.BoardValues
{
    public class when_getting_distace_value_to_field_on_loosing_border : observations_for_distance_value_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return _fieldsGraph.Borders().Bottom;
        }

        protected override Field WhiteField()
        {
            return _fieldsGraph.GetField(Tuple.Create(1,0));
        }

        [Test]
        public void should_return_loose_vlue()
        {
            _value.ShouldEqual(GetTargetBorder().LooseValue);
        }
        
    }

    public class when_getting_upper_distance_value_to_field_in_middle_board : observations_for_distance_value_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return _fieldsGraph.Borders().Upper;
        }

        protected override Field WhiteField()
        {
            return _fieldsGraph.GetField(Tuple.Create(2,3));
        }

        [Test]
        public void should_return_distance_from_white_field()
        {
            _value.ShouldEqual(3);
        }

    }

    public class when_getting_bottom_distance_value_to_field_in_middle_board : observations_for_distance_value_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return _fieldsGraph.Borders().Bottom;
        }

        protected override Field WhiteField()
        {
            return _fieldsGraph.GetField(Tuple.Create(2,3));
        }

        [Test]
        public void should_return_distance_from_white_field()
        {
            _value.ShouldEqual(1);
        }

    }

    public class when_getting_distace_value_to_field_behind_winning_board : observations_for_distance_value_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return _fieldsGraph.Borders().Upper;
        }

        protected override Field WhiteField()
        {
            return _fieldsGraph.GetField(Tuple.Create(0,0));
        }

        [Test]
        public void should_return_win_value()
        {
            _value.ShouldEqual(GetTargetBorder().WinValue);
        }
    }

    public class when_getting_distance_value_to_field_on_bottom_board : observations_for_distance_value_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return _fieldsGraph.Borders().Bottom;
        }        

        protected  override Field WhiteField()
        {
            return _fieldsGraph.GetField(Tuple.Create(5,0));
        }

        [Test]
        public void should_return_win_value()
        {
            _value.ShouldEqual(GetTargetBorder().WinValue);
        }
    }

    public abstract class observations_for_distance_value_to_border : observations_for_auto_created_sut_of_type<WhiteStoneToBorderDistanceValue>
    {
        public TargetBorder _targetBorder;
        protected IFieldsGraph _fieldsGraph;
        protected int _value;

        protected override WhiteStoneToBorderDistanceValue CreateSut()
        {
            _targetBorder = GetTargetBorder();
            return new WhiteStoneToBorderDistanceValue(_targetBorder);
        }

        protected abstract TargetBorder GetTargetBorder();

        protected override void EstablishContext()
        {
            ProvideImplementationOf<IPhutballOptions>(new PhutballOptions
                                                          {
                                                              RowCount = 7,
                                                              ColumnCount = 5
                                                          });
            _fieldsGraph = new FieldsGraph(Dependency<IPhutballOptions>());
            var whiteField = WhiteField();
            whiteField.PlaceWhiteStone();
            _fieldsGraph.UpdateFields(new[] { whiteField });
        }

        protected override void Because()
        {
            _value = Sut.GetValue(_fieldsGraph);
        }

        protected abstract Field WhiteField();
    }
}