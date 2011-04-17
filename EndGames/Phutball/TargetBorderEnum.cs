﻿using System.Linq;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball
{
    public class TargetBorderEnum
    {
        public TargetBorderEnum(IFieldsGraph fieldsGraph)
        {
            Upper = new TargetBorder(() => 1, "Upper")
                .OppositeIs(() => Bottom)
                .CountDistanceUsing(new DistanceToUpperBorderCounter(fieldsGraph))
                .ChoosePlacesForBlackStoneUsing(all => all.Take(5).Concat(new[]
                                                                              {
                                                                                  Direction.N.Multiply(2).Add(
                                                                                      Direction.W),
                                                                                  Direction.N.Multiply(2).Add(
                                                                                      Direction.E),
                                                                              }));

            Bottom = new TargetBorder(() => fieldsGraph.RowCount - 2, "Bottom")
                .OppositeIs(() => Upper)
                .CountDistanceUsing(new DistanceToBottomBorderCounter(fieldsGraph))
                .ChoosePlacesForBlackStoneUsing(all => all.Reverse().Take(5).Concat(new[]
                                                                                        {
                                                                                            Direction.S.Multiply(2).Add(
                                                                                                Direction.E),
                                                                                            Direction.S.Multiply(2).Add(
                                                                                                Direction.W),
                                                                                        }));
        }

        public TargetBorder Upper { get; private set; }
        public TargetBorder Bottom { get; private set; }
    }
}