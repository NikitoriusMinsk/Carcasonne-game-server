﻿namespace Carcasonne_game_server.Classes.Features
{
    public class CityFeature : Feature
    {
        public CityFeature(Feature[] connectsTo, BonusPoints bonusPoints) : base(connectsTo, bonusPoints) { }

        public override int GetValue()
        {
            throw new NotImplementedException();
        }
    }
}