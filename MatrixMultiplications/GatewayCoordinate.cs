namespace MatrixMultiplications
{
    class GatewayCoordinate
    {
        public GatewayCoordinate(Coordinate[] gateways)
        {
            this._gateways = gateways;
        }
        //
        private Coordinate[] _gateways;
        //
        public Coordinate[] GetGatewayCoordinates()
        {
            return _gateways;
        }
    }
}