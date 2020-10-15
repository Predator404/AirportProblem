using System;
using System.Collections.Generic;
using System.Text;

namespace AirportProblem
{
	public class FlightRoute: ICloneable
	{
		public List<Tuple<string, string>> Flights { get; set; }
		public bool Complete { get; set; }


		public FlightRoute(Tuple<string, string> startingRoute)
		{
			Flights.Add(startingRoute);
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
